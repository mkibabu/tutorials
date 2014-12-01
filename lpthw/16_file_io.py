from sys import argv

script, filename = argv

print ("We're going to erase %s" % filename)
print ("Hit CTRL + C to cancel, ENTER to continue")
input("?")

print ("Opening the file...")
# specify that the file is to be opened for writing.
# For safety, open requires 'w' to write
print ("Here are the modes to open a file:")
print ("\tr = read a file")
print ("\tw = overwrite a file")
print ("\ta = append to the file")
print ("\tr+ = read and write to file")
print ("\tw+ = overwrite and read a file")
print ("\ta+ = append to and read the file")

target = open(filename, 'w')

# truncate empties the file. Not required if opening the file with "w" flag, as
# 'w' overwrites the current file's contents.

print ("Truncating the file...")
# empty the file
target.truncate()

print ("Enter three lines:")
line = ""
for x in range(1,4):
    line += input("Enter a line: ")
    line += "\n"


print ("Writing to the file...",)
# write to the file
target.write(line)
print ("done!")

# If we were to read the file as done above (i.e. target = open(filename)), we'd
# have to explicitly close the file. However, if a read fails, or an error occurs
# just after target = open(),closing doesn't succeed.
print ("Now we close the file...")
target.close()


print ("Read the contents of the file...")
# Opening a file within a with statement is safest; the file is automatically
# closed whether read succeeds or not.
with open(filename, 'r') as f:
    txt = f.read()
    print (txt)


# Opening a file within a with statement is safest; the file is automatically
# closed whether read succeeds or not.
print ("Appending to the file")
with open(filename, 'a+') as f:
    line = ""
    for x in range(1,4):
        line += input("Enter a number: ")
        line += "\n"
    f.write(line)
    print ("File's new contents: ")
    f.seek(0);
    txt = f.read()
    print (txt)
