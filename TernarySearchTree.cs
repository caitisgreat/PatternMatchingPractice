using System;
using System.Text;

namespace PatternMatchingPractice
{
  public class TernarySearchTree {
    protected bool debug = false;
    protected string input { get; set; }
    protected int patternLength { get; set; }
    protected Node root { get; set; }

    public TernarySearchTree (string input, int patternLength) {
      this.input = input;
      this.patternLength = patternLength;
    }

    /// <summary>
    ///  The insertion method for the ternary search tree.
    ///  This builds the trie while also constructing words from the provided characters in the input
    ///  We don't know what the words should look like until we know the pattern length, and
    ///  splitting the input into words of appropriate size into an array would consume more space but still be linear with respect to the length of the input.
    ///  Instead this creates words while iterating and assigns the word value of the node, when we've reached the end of a word.
    /// </summary>
    /// <param name="node">Initially null, the root is created below.  Following that it's a child of the root.</param>
    /// <param name="word">Represents the word as it's currently formed.  Capacity determined by pattern length.</param>
    /// <param name="index">This is the index of the input character, it's incremented for lookahead but never modified outside of Insert</param>
    /// <param name="charCount">This tracks the index of the word we're forming, increments when we proceed center/down the tree</param>
    public void Insert(Node node, char[] word, int index, int charCount = 0) {
      char key = input[index];

      // beginning a new word
      if (node == null) {
        // no root entry, create a new node
        if (root == null) {
          root = new Node(key);
        }

        node = root;
      }

      // debug statements can be used to see the full constuction of the tree
      // debug flag is hard-coded above in class def.  I left this in for demonstration purposes only
      if (debug) {
        System.Console.WriteLine("{0}, {1}, {2}", node.data, index, charCount);
      }

      // NOTE - the following comparisons are made with respect to ASCII,
      // but they might work okay for Unicode assuming decimal character values could be retrieved

      // character is less than node
      // step left
      if(node.data > key) {
        if (node.left == null) { node.left = new Node(key); }
        if (debug) { System.Console.WriteLine("step left"); }
        Insert(node.left, word, index, charCount);
      }

      // character is greater than node
      // step right
      else if(node.data < key) {
        if (node.right == null) { node.right = new Node(key); }
        if (debug) { System.Console.WriteLine("step right"); }
        Insert(node.right, word, index, charCount);
      }

      // we don't need to branch here, because we've found a matching node
      else {
        // if we're at the end of the word, mark this node as such and return
        // the length of the pattern determines the depth of recursion
        if (charCount == (patternLength -1)) {
          // finalize the current word
          word[charCount] = key;
          node.isEndOfWord = true;
          node.occurrences++;
          node.word = word;

          if (debug) {
            System.Console.WriteLine("word: {0}, occur: {1}", new string(word), node.occurrences);
          }
        }
        // otherwise begin on the next character in the word (lookahead)
        else {
          if (input.Length > (index + 1)) {
            // retrieve the next character in the word
            // if center isn't set, this character becomes the new center
            // assign the character to the word array
            word[charCount] = key;
            key = input[index + 1];
            charCount++;

            if (node.center == null) {
              node.center = new Node (key);
              Insert(node.center, word, (index + 1), charCount);
            }

            // otherwise the lookahead already exists somewhere
            // find it, then continue inserting from there
            else {
              if(node.center.data > key) {
                if (node.left == null) { node.left = new Node(key); }
                Insert(node.left, word, (index + 1), charCount);
              }
              else if (node.center.data < key) {
                if (node.right == null) { node.right = new Node(key); }
                Insert(node.right, word, (index + 1), charCount);
              }
              else {
                Insert(node.center, word, (index + 1), charCount);
              }
            }
          }
        }
      }
    }

    /// <summary>
    ///  traverses the full-breadth of the trie from the root
    /// </summary>
    public void PrintTree () {
      Traverse(root);
    }

    /// <summary>
    ///  in-order trie traversal allows for us to see words in order by character code
    /// </summary>
    /// <param name="node">The current node being traversed</param>
    public void Traverse (Node node = null, bool showAll = false) {

      if(node.left != null) {
        Traverse(node.left);
      }

      if (node.center != null) {
        Traverse(node.center);
      }

      if (node.right != null) {
        Traverse(node.right);
      }

      if (node.isEndOfWord) {
        string output = new string(node.word);
        if (debug) {
          System.Console.WriteLine("{0}, occurs {1} time(s)", output, node.occurrences);
        }
        else {
          if (node.occurrences > 1) {
            System.Console.WriteLine("{0}, occurs {1} time(s)", output, node.occurrences);
          }
        }
      }
    }
  }
}