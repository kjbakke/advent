from math import lcm

directions, rest = open("input").read().strip().split('\n\n')
nodes = {}
for line in rest.split('\n'):
    node, connections = line.split(' = ')
    nodes[node] = connections[1:-1].split(', ')


def move(directions, nodes, start, end):
    steps, i = 0, 0
    while not end(start):
        start = nodes[start][0] if directions[i % len(directions)] == 'L' else nodes[start][1]
        steps += 1
        i += 1
    return steps


part1 = move(directions, nodes, 'AAA', lambda x: x == 'ZZZ')
print(part1)

starts = [start for start in nodes if start.endswith('A')]
part2 = lcm(*[move(directions, nodes, start, lambda x: x[-1] == 'Z') for start in starts])
print(part2)
