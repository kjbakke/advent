def get_wins(time, distance):
    wins = 0
    for hold in range(time):
        speed = hold
        if speed * (time - hold) > distance:
            wins += 1
    return wins


# races = [(7, 9), (15, 40), (30, 200)]
# races = [(56, 334), (71, 1135), (79, 1350), (99, 2430)]
# races = [(71530, 940200)]
races = [(56717999, 334113513502430)]
tot_wins = [get_wins(time, distance) for time, distance in races]
result = 1
for win in tot_wins:
    result *= win

print(result)
