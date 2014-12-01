name = "Michael Muraya"
age = 30
height = 68 # inches
weight = 172 #lbs
eyes = 'Brown'
teeth = 'White'
hair = 'Black'

print ("Let's talk about %s" % name)
print ("He's %d inches tall" % height)
print ("He weighs %d pounds" % weight)
print ("He's got %s eyes and %s hair" % (eyes, hair))
print ("His teeth are usually %s, depending on how many cups of coffee he's had" % teeth)
# this line is tricky
print ("If I had %d, %d and %d I'd get %d" % (age, height, weight, age + height + weight))


print ("")
print ("Here are other common python format characters:")
print ("%d, %i - signed integer decimal")
print ("%o - unsigned octal")
print ("%u - unsigned decimal")
print ("%e - floating point exponential")
print ("%f - floating point decimal")
print ("%c - single character")
print ("%r - string (converts any python object using repr())")
print ("%s - string (converts any python object using str())")
print ("repr() returns a printable representation of an object, which can then")
print ("be passed into eval() to recreate the original object. Meanwhile, ")
print ("str() returns a string that is printable; no attempt is made to make it")
print ("a representation that can be acceptable to eval().")
