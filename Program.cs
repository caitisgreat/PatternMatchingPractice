using System;
using System.Text;


namespace PatternMatchingPractice
{
  public class Program
  {
    public static void Main(string[] args)
    {
      string patternLength = "3";
      string input = "zf3kabxcde224lkzf3mabxc51+crsdtzf3nab="; // "zf3kabxcde224lkzf3mabxc51+crsdtzf3nab=";
      int pl;

      if (string.IsNullOrEmpty(input)) {
        // collect the necessary program data
        System.Console.Write("Please provide the pattern length: ");
        patternLength = System.Console.ReadLine();

        System.Console.Write("Please provide the input string: ");
        input = System.Console.ReadLine();
      }

      // perform some validation
      try {
        if (!Int32.TryParse(patternLength, out pl)) {
          throw new ArgumentException("The provided 'patternLength' is not an integer.");
        }
        if(input.Length < pl) {
          throw new ArgumentException("The provided 'input' must be greater in length than the value of 'patternLength'.");
        }
      }
      catch (Exception exc) {
        System.Console.WriteLine(exc.Message);
        System.Console.WriteLine("Exiting...");
        return;
      }

      System.Console.WriteLine();
      System.Console.WriteLine("Your responses");
      System.Console.WriteLine("--------------");
      System.Console.WriteLine("patternLength: " + patternLength);
      System.Console.WriteLine("input: " + input);
      System.Console.WriteLine();

      // create words from each character in the provided string
      System.Console.WriteLine("Constructing the ternary search tree...");
      TernarySearchTree tst = new TernarySearchTree(input, pl);
      for (int i = 0; i <= input.Length - pl; i++) {
        tst.Insert(null, new char[pl], i);
      }

      System.Console.WriteLine();
      System.Console.WriteLine("Printing patterns of the specified length with more than one occurence...");
      tst.PrintTree();

      System.Console.WriteLine();
      System.Console.WriteLine("Process complete, exiting...");
    }
  }
}
