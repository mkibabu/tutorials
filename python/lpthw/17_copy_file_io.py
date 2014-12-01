from sys import argv
from os.path import exists

# Copy one file to another

script, from_file, to_file = argv

print ("Copying from %s to %s" % (from_file, to_file))

in_data = ""

print ("Checking if input file exists...")

if exists(from_file) == False:
    print("File %s does not exist!" % from_file)
    exit(0)
else:
    print ("File %s found! Now reading..." % from_file)
    with open(from_file) as f:
        in_data = f.read()
        
    print ("The input is %d bytes long" % len(in_data))

print ("File %s exists: %r" % (to_file, exists(to_file)))
input ("Hit CTRL-C to quit, ENTER to continue:")

with open(to_file, 'w') as f:
    f.write(in_data)

print("Done!")