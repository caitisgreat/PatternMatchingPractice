# Pattern Matching Practice

## Problem Statement:

Write a program and take these values to find all of the different patterns in "input" of length "patternLength" and output all of the patterns that occur more than once and the number of times they occur.
For example, when searching the string "zf3kabxcde224lkzf3mabxc51+crsdtzf3nab=", with a specified pattern length of 3, the method should return the pattern "abx" with an occurrence value of two, and "zf3" with an occurrence value of three.
The answer should be written in C#, and a link to the runnable result shared via https://dotnetfiddle.net

## Assumptions:

While a fixed length string "input" was provided, the instructions did not clarify whether a fixed length string would always be provided.
Therefore, it can be assumed that the "input" can be variable length (0 - inf) and a space-efficient solution should be considered in addition to a time-efficient one.
A fixed pattern length limits the scope of the problem such that only so many unique combinations of "patternLength" can occur in a given string.
Any pattern not matching that length can be ignored.

Ex.
```
input: "banana"
patternLength (l): 3
worst-case unique combinations = length of input (n) - pattern length (l) + 1
combinations: [ban, ana, nan], in this example, "ana" repeats twice.
```

Since we're given a fixed pattern length, we should assume that the "input" string is at least that long.
One advantage about having a fixed pattern length is that we can very easily organize the original input string into a trie with a fixed depth equal to that of the pattern length.
However, that's still not very space efficient, since alphanumeric and special characters are used in the input, we could potentially have at worst case (depending on the charset used) a very large number of unique tree nodes (>= 128 characters for ASCII).
Regardless which character set the string is provided in, a space-efficient solution would account for a potential expansion of the character set.
A better way might be to evaluate the input string into something like a binary search tree where each character's value is considered greater than, less, than or equal to the root node's character.

I was told that the character set provided in this example would be ASCII.

## Solution:

I chose to implement a ternary search tree.
This maintains the basic structure of a prefix trie, but allows for some flexibility on space by acting somewhat like a binary search tree.  Additionally, traversing tries is pretty fast.
I chose not to implement a search function because it wasn't required within the scope of the problem statement, but it wouldn't be hard to do at this point.
An alternative solution might be to use regular expressions to capture groups of pattern length for any type of characters.
