# practice exercise
print ('Here\'s your \t allotement \n of various \'escape \\ characters\'')


poem = '''
\tCome with me to a place of fantasy
I'll take you on a seesaw
Come with me to a place that's by the sea
I'll take you on a boardwalk
\n\t\t I'll be sure to write you from the war
Put your guns away it's tea time
'''

print ("--------------")
print (poem)
print ("--------------")


print ("This should be five: %d" % (10 - 2 - 6 + 3))

def canning_formula(started):
    jelly_beans = started * 500
    jars = jelly_beans / 1000
    crates = jars / 100
    return jelly_beans, jars, crates

starting_jb = 100000
beans, jars, crates = canning_formula(starting_jb)
print ("Starting with %d jelly_beans, we'd have" % beans)
print ("%d jars, which would be packed into %d crates" % (jars, crates))

starting_jb = 20000
print ("However, given %d beans, we'd have %d jars and %d crates"
    % canning_formula(starting_jb))