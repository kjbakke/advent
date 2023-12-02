games = []

with open('input', 'r') as file:
    for index, line in enumerate(file.readlines()):
        rounds = line.replace(f'Game {index + 1}: ', '').strip().split('; ')
        round_colors = {}

        for round in rounds:
            colors = round.split(', ')
            for color_count in colors:
                if len(color_count.split()) == 2:
                    count, color = color_count.split()
                    count = int(count)
                else:
                    print(f"Unexpected format: '{color_count}'")

                if color not in round_colors or round_colors[color] < count:
                    round_colors[color] = count

        games.append(round_colors)


def check_possible_game(bag, game):
    for color, count in game.items():
        if color not in bag or bag[color] < count:
            return False

    return True


def power_per_game(game):
    power = 1
    for color, count in game.items():
        power *= count

    return power


bag = {"red": 12, "green": 13, "blue": 14}
print(sum([index + 1 for index, game in enumerate(games) if check_possible_game(bag, game)]))
print(sum([power_per_game(game) for game in games]))