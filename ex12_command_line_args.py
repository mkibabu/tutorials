# import the arguments collection variable from the sys module
from sys import argv

# unpack argv's values into individual variables
script, first, second, third = argv

print "The script is called ", script
print "The first variable is", first
print "The second variable is", second
print "The third variable is", third