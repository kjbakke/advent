with open('input', 'r') as file:
    schematic = [line.strip() for line in file.readlines()]

adjacent_offsets = [(-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1)]
grid = [list(row) for row in schematic]
gears_with_adjacent_numbers = {}


def has_adjacent_gear(row, num_start_index, num_length, num):
    for y in range(num_length):
        for dx, dy in adjacent_offsets:
            nx, ny = row + dx, num_start_index + y + dy
            if 0 <= nx < len(grid) and 0 <= ny < len(grid[nx]) and grid[nx][ny] == '*':
                if (nx, ny) not in gears_with_adjacent_numbers:
                    gears_with_adjacent_numbers[(nx, ny)] = [int(num)]
                else:
                    gears_with_adjacent_numbers[(nx, ny)].append(int(num))
                return


for i, row in enumerate(grid):
    num = ''
    for j, cell in enumerate(row):
        if cell.isdigit():
            num += cell
            if j == len(row) - 1 or not row[j + 1].isdigit():
                has_adjacent_gear(i, j - len(num) + 1, len(num), num)
                num = ''

gear_ratio_sum = 0
for key, value in gears_with_adjacent_numbers.items():
    if len(value) == 2:
        gear_ratio_sum += value[0] * value[1]
print(gear_ratio_sum)
