formatter = "%r %r %r %r"

print formatter % (1, 2, 3, 4)
print formatter % ("one", "two", "three", "four")
print formatter % (True, False, True, False)
print formatter % (formatter, formatter, formatter, formatter)

# recall that a comma at the end of a print keeps it from adding a newline
print formatter % ("I had this thing",
                   "That you could type up right",
                   "But it did not sing",
                   "So I said goodnight")

days = "Mon Tue Wed Thur Fri Sat Sun"
months = "Jan\nFeb\nMar\nApr\nMay"

print "Here are the days.", days
print "Here are the months.", months

print """
These lines are enclosed
in three double quotes
which allows us to print multiple lines
without using escape characters.
Three single quotes can be used instead.
"""
