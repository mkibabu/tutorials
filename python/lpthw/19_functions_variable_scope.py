# functions and variable scope:
# Java-like

def cheese_and_crackers(cheese_count, cracker_count):
    print("You have %d cheeses!" % cheese_count)
    print("And %d boxes of crackers!" % cracker_count)

print ("We can just call the functions with numbers directly:")
cheese_and_crackers(30, 50)

print ("Or any other expression that gives a number:")
cheese_count = 34
cracker_count = 22
cheese_and_crackers(cheese_count + 10, cracker_count + 10)