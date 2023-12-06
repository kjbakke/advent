seeds = []
maps = [[], [], [], [], [], [], []]
phase = 0

for line in open('input', 'r'):
    line = line.strip()
    if len(line) == 0:
        continue
    if line[0].isalpha():
        phase += 1
        if phase == 1:
            seeds = [int(x) for x in line[line.find(':')+1:].split()]
    else:
        dr, sr, n = [int(x) for x in line.split()]
        maps[phase - 2].append((sr, sr + n, dr))


def trace(x):
    for p in range(len(maps)):
        for start, end, destination in maps[p]:
            if start <= x < end:
                x = destination + (x-start)
                break
    return x


print(min(trace(x) for x in seeds))

r = 999_999_999
for i in range(0, len(seeds), 2):
    for x in range(seeds[i], seeds[i] + seeds[i+1]):
        r = min(r, trace(x))
print(r)
