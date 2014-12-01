
# array of arguments for functions: *args. Somewhat same purpose as argv is
# for scripts
def print_two(*args):
    arg1, arg2 = args
    print ("arg1: %r, arg2: %r" % (arg1, arg2))

def print_two_again(arg1, arg2):
    print ("arg1: %r, arg2: %r" % (arg1, arg2))

def print_none():
    print("I gots nuthin")

print_two("musha", "boom");
print_two_again("man", "moon")
print_none()