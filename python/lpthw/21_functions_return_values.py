# functions with return values

def add(a, b):
    print ("ADDING %d + %d" % (a, b))
    return a + b

def subtract(a, b):
    print ("SUBTRACTING %d - %d" % (a, b))
    return a - b

def multiply(a, b):
    print ("MULTIPLYING %d * %d" % (a, b))
    return a * b

def divide(a, b):
    print ("DIVIDING %d / %d" % (a, b))
    return a / b

age = add(23, 7)
height = subtract(73, 6)
weight = multiply(35, 5)
iq = divide(230, 2)

print ("Age: %d, Height: %d, Weight: %d, IQ: %d" % (age, height, weight, iq))

# functions can also return multiple values:
def all_math(a, b):
    return a + b, a - b, a * b, a / b

add, sub, mul, div = all_math(10, 5)

print ("The sum, difference, product and quotient of 10 and 5 are %d, %d, %d and %d"
    % (add, sub, mul, div))