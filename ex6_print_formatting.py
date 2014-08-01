x = "There are %d types of people." % 10
binary = "binary"
do_not = "don't"
y = "Those who know %s and those who %s." % (binary, do_not)

print x
print y

print "I said: %r." % x
print "I also said: '%s'." % y

# strings may contain the format character
hilarious = False
joke_evaluation = "Isn't that joke funny?! %r"
print joke_evaluation % hilarious
print ""

mi = "milk"
br = "bread"
bu = "butter"
to_do = "These are the things I need from the store: %s, %s and %s"
print to_do % (mi, br, bu)
print ""

w = "This is the left side of..."
e = "a string with a right side"
print w + e
