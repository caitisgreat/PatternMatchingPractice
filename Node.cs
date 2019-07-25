namespace PatternMatchingPractice {

  /// <summary>
  /// a node within the ternary tree structure
  /// </summary>
  public class Node {
    public Node (char data) {
      this.data = data;
      this.isEndOfWord = false;
      this.occurrences = 0;
    }
    /// <summary>
    ///  a character within the input string
    /// </summary>
    /// <value></value>
    public char data { get; set; }
    /// <summary>
    ///  represents whether this node marks the end of a word (as determined by the pattern length)
    /// </summary>
    /// <value></value>
    public bool isEndOfWord { get; set; }
    /// <summary>
    ///  the word, set when isEndOfWord = true
    /// </summary>
    /// <value></value>
    public char[] word { get; set; }
    /// <summary>
    ///  current number of occurrences of this word
    /// </summary>
    /// <value></value>
    public int occurrences { get; set; }
    /// <summary>
    ///  "less than" neighbors within the ternary search tree
    /// </summary>
    /// <value></value>
    public Node left { get; set; }
    /// <summary>
    ///  "greater than" neighbors within the ternary search tree
    /// </summary>
    /// <value></value>
    public Node right { get; set; }
    /// <summary>
    ///  "equal to" neighbors within the ternary search tree
    /// </summary>
    /// <value></value>
    public Node center { get; set; }
  }
}