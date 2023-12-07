import collections

with open('input') as f:
    lines = [line.strip() for line in f if line.strip()]


def hand(h, p):
    if p == 1:
        h = h.replace('J', 'X')
    card_indices = ['J23456789TXQKA'.index(card) for card in h]
    max_hand_type = max(
        [(1, 1, 1, 1, 1), (1, 1, 1, 2), (1, 2, 2), (1, 1, 3), (2, 3), (1, 4), (5,)].index(
            tuple(sorted(collections.Counter(h.replace('J', r)).values()))
        ) for r in '23456789TQKA'
    )

    return max_hand_type, *card_indices


for part in (1, 2):
    hands = sorted((hand(h, part), int(bid)) for h, bid in (line.split() for line in lines))
    total = sum(i * bid + bid for i, (_, bid) in enumerate(hands))
    print(f'Part {part}: {total}')
