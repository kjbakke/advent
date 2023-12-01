with open('input', 'r') as file:
    lines = [line.strip() for line in file.readlines()]


def calibration_values(lines):
    total_sum = 0
    for line in lines:
        first_digit = None
        last_digit = None

        for char in line:
            if char.isdigit():
                first_digit = char
                break

        for char in reversed(line):
            if char.isdigit():
                last_digit = char
                break

        if first_digit is not None and last_digit is not None:
            total_sum += int(first_digit + last_digit)

    return total_sum


def replace_number_words(line):
    for word, number in {
        'eightwo': '82',
        'eighthree': '83',
        'twone': '21',
        'nineight': '92',
        'threeight': '38',
        'fiveight': '58',
        'oneight': '18',
        'sevenine': '79',
        'nine': '9',
        'eight': '8',
        'seven': '7',
        'six': '6',
        'five': '5',
        'four': '4',
        'three': '3',
        'two': '2',
        'one': '1'
    }.items():
        line = line.replace(word, number)

    return line


converted_lines = [replace_number_words(line) for line in lines]
print(calibration_values(converted_lines))