using System;
using System.Text;

namespace PatternMatchingPractice
{

  class TernarySearchTree {
    public TernarySearchTree (string input, int patternLength) {
      this.input = input;
      this.patternLength = patternLength;

    }
    protected bool debug = false;
    protected string input { get; set; }
    protected int patternLength { get; set; }
    protected Node root { get; set; }

    public void Insert(Node node, int index) {
      char key = input[index];

      // beginning a new word
      if (node == null) {
        // no root entry, create a new node
        if (root == null) {
          root = new Node(key);
        }

        node = root;
      }

      if(debug) {
        System.Console.WriteLine("{0}, {1}, {2}", node.data, index, node.depth);
      }

      // character is less than node
      // advance left
      if(node.data > key) {
        if (node.left == null) { node.left = new Node(key); }
        Insert(node.left, index);
      }

      // character is greater than node
      // advance right
      else if(node.data < key) {
        if (node.right == null) { node.right = new Node(key); }
        Insert(node.right, index);
      }

      // we don't need to branch here, because we've found a matching node
      else {
        // if we're at the end of the word, mark this node as such and return
        // the length of the pattern determines the depth of recursion
        if (node.depth == (patternLength -1)) {
          node.isEndOfWord = true;
          node.occurrences++;
          if (debug) {
            System.Console.WriteLine("occur: {0}", node.occurrences);
          }
        }
        // otherwise begin on the next character in the word
        else {
          if (input.Length > (index + 1)) {
            // retrieve the next character in the word
            // if center isn't set, this character becomes the new center
            key = input[index + 1];

            if (node.center == null) {
              node.center = new Node (key, (node.depth + 1));
              Insert(node.center, (index + 1));
            }
            else {
              if(node.center.data > key) {
                if (node.left == null) { node.left = new Node(key, (node.depth + 1)); }
                Insert(node.left, (index + 1));
              }
              else if (node.center.data < key) {
                if (node.right == null) { node.right = new Node(key, (node.depth + 1)); }
                Insert(node.right, (index + 1));
              }
              else {
                Insert(node.center, (index + 1));
              }
            }
          }
        }
      }
    }

    public void PrintTree () {
      char [] sb = new char[patternLength];
      Traverse(sb, root);
    }

    public void Traverse (char[] sb, Node node = null) {

      if(node.left != null) {
        Traverse(sb, node.left);
      }

      sb[node.depth] = node.data;

      if (node.center != null) {
        Traverse(sb, node.center);
      }

      if (node.right != null) {
        Traverse(sb, node.right);
      }

      if (node.isEndOfWord) {
        string output = new string(sb);
        if (node.occurrences > 1) {
          System.Console.WriteLine("{0}, occurs {1} time(s)", output, node.occurrences);
        }
      }
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      int patternLength = 3;
      string input = "swingingdancing"; // "zf3kabxcde224lkzf3mabxc51+crsdtzf3nab=";

      if (string.IsNullOrEmpty(input)) {
        // collect the necessary program data
        System.Console.Write("Please provide the pattern length: ");
        string plString = System.Console.ReadLine();

        if (!Int32.TryParse(plString, out patternLength)) {
          System.Console.WriteLine("Provided value is not an integer.");
          return;
        }

        System.Console.Write("Please provide the input string: ");
        input = System.Console.ReadLine();

        System.Console.WriteLine();
        System.Console.WriteLine("Your responses");
        System.Console.WriteLine("--------------");
        System.Console.WriteLine();
        System.Console.WriteLine("patternLength: " + plString);
        System.Console.WriteLine("input: " + input);
      }

      // create words from each character in the provided string
      TernarySearchTree tst = new TernarySearchTree(input, patternLength);
      for (int i = 0; i <= input.Length - patternLength; i++) {
        tst.Insert(null, i);
      }

      tst.PrintTree();
    }
  }
}
