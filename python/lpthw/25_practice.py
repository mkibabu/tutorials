# practice exercise

def break_words(stuff):
    """This function breaks up words for us"""
    words = stuff.split()
    return words

def sort_words(words):
    """Sorts the words"""
    return sorted(words)

def print_first_word(words):
    """ Prints the first word after popping it off"""
    print(words.pop(0))

def print_last_word(words):
    """Print the last word after popping it off"""
    print(words.pop(-1))

def sort_sentence(line):
    """ Takds in a full sentence and returns the sorted words"""
    return sort_words(break_words(line))

def first_and_last(line):
    """Takes a full sentence, prints out the first and last words"""
    words = break_words(line)
    print_first_word(words)
    print_last_word(words)

def first_and_last_sorted(line):
    """ Takes a full sentence, sorts it, and prints out the first and last words"""
    swords = sort_words(break_words(line))
    print_first_word(swords)
    print_last_word(swords)