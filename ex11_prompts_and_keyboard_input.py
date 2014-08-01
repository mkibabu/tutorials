print "How old are you?",
age = raw_input()
print "How tall are you?",
height = raw_input()
# alternate, better prompt
weight = raw_input("How much do you weigh?")

print "You're %r years old, %r tall and weigh %r" % (age, height, weight)