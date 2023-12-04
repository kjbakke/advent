with open('input', 'r') as file:
    lines = [line.strip() for line in file.readlines()]

lines = [line.split(':').pop(1).split('|') for line in lines]
cards = [[line[0].split(), line[1].split(), 1] for line in lines]

points = 0
for i, card in enumerate(cards):
    times = 0
    for number in card[0]:
        if number in card[1]:
            times += 1
    for time in range(times):
        if i+time+1 < len(cards):
            cards[i+time+1][2] += cards[i][2]

    points += 2 ** (times - 1) if times > 0 else 0

card_sum = sum(card[2]for card in cards)

print(points, card_sum)