# functions and files

from sys import argv

script, input = argv

def print_all(f):
    print f.read()

def rewind(f):
    f.seek(0)

def print_a_line(line_count, f):
    print ("%d %s" % (line_count, f.readline()))

inf = open(input)

print ("First, print all the file contents:\n")
print_all(inf)

print ("\nNow rewind:\n")
rewind(inf)

print ("Let's print three lines:")
count = 0
while count < 3:
    print_a_line(count, inf)
    count += 1
