from sys import argv

script, filename = argv

# open the file, save the file handle
txt = open(filename)

print "Here's your file %r:" % filename
# echo the file contents
print txt.read()

print "Type the filename again:"
file_again = raw_input("> ")

txt_again = open(file_again)

print txt_again.read()
