﻿var pattern = File.ReadAllLines("pattern").ToList();

char[,] map = new char[0, 0];
int[,] distance = new int[0, 0], prev = new int[0, 0];
List<(int, int)> lows = new List<(int, int)>();
int w, h, sx = 0, sy = 0, ex = 0, ey = 0, gen = 0;

parse(pattern);

void parse(List<string> input) {
    w = input[0].Length;
    h = input.Count;
    map = new char[w, h];
    distance = new int[w, h];
    prev = new int[w, h];
    for (int y = 0; y < h; y++) {
        for (int x = 0; x < w; x++) {
            map[x, y] = input[y][x];
            distance[x, y] = 0;
            if (map[x, y] == 'S') { sx = x; sy = y; map[x, y] = 'a'; }
            if (map[x, y] == 'E') { ex = x; ey = y; map[x, y] = 'z'; }
            if (map[x, y] == 'a') { lows.Add((x, y)); }
        }
    }
}

int BFS(List<(int, int)> start) {
    List<(int, int)> stack = start;
    List<(int, int)> dirs = new List<(int, int)> { (-1, 0), (1, 0), (0, -1), (0, 1) };
    gen += 10000;
    int dst = ++gen;
    foreach ((int sx, int sy) in start) distance[sx, sy] = dst;
    while (stack.Count > 0) {
        List<(int, int)> next = new List<(int, int)>();
        dst++;
        foreach ((int cx, int cy) in stack) {
            foreach ((int dx, int dy) in dirs) {
                int nx = cx + dx, ny = cy + dy;
                if (nx >= 0 && nx < w && ny >= 0 && ny < h) {
                    if (distance[nx, ny] < gen && map[nx, ny] <= map[cx, cy] + 1) {
                        prev[nx,ny] = cy * 1000 + cx;
                        if ((nx, ny) == (ex, ey)) return dst - gen;
                        distance[nx, ny] = dst;
                        next.Add((nx, ny));
                    }
                }
            }
        }
        stack = next;
    }
    return -1;
}

Console.WriteLine(BFS(new List<(int, int)> { (sx, sy) }).ToString());
Console.WriteLine(BFS(lows).ToString());