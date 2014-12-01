from sys import argv

script, filename = argv

print ("Here's your file %r:" % filename)

# open the file, save the file handle
with open (filename) as fin:
    # echo the file contents
    print (fin.read())
    print()
    print ("To re-read the file, we can use seek(0) to return to the beginning:")
    fin.seek(0)
    print(fin.read())

print("Alternatively, just reopen it in a with open(filename) as smth statement; which resets the cursor:") 
with open (filename) as f:
    print(f.read());
