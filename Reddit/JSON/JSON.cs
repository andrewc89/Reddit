
namespace Reddit.JSON
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class JSON
    {
        private const char TOKEN_QUOTE = '\"';
        private const char TOKEN_OPEN_CURLY = '{';
        private const char TOKEN_CLOSE_CURLY = '}';
        private const char TOKEN_OPEN_BRACKET = '[';
        private const char TOKEN_CLOSE_BRACKET = ']';
        private const char TOKEN_COMMA = ',';

        public List<char> Json { get; set; }
        public Hashtable HashTable { get; set; }

        public void Convert (string Input)
        {
            this.Json = Input.ToList();
            HashTable = new Hashtable();
            Recursive(HashTable, this.Json);           
        }

        public object Recursive (Hashtable HashTable, List<char> Input)
        {
            object Output = null;
            int Index = 0;

            while (Index < Input.Count - 1)
            {
                char Next = NextToken(Index);
                switch (Next)
                {
                    case TOKEN_OPEN_CURLY:
                        Recursive(new Hashtable(), Input.GetRange(Index, Input.Count - Index));
                        
                        break;

                    case TOKEN_CLOSE_CURLY:

                        break;  
                  
                    case TOKEN_OPEN_BRACKET:

                        break;

                    case TOKEN_CLOSE_BRACKET:

                        break;

                    case TOKEN_QUOTE:
                        int ClosingQuoteIndex = Input.IndexOf('\"', Index + 1);
                        string Key = string.Join("", Input.GetRange(Index + 1, ClosingQuoteIndex - Index));
                        Next = NextToken(Index);
                        if (!Next.Equals(':'))
                        {
                            Console.WriteLine("Something's Gone Wrong @ " + Index + " - '" + Input.ElementAt(Index) + "'");
                        }
                        HashTable.Add(Key, Recursive(HashTable, Input.GetRange(Index, Input.Count - Index)));
                        break;

                    default:

                        break;
                }
            }

            return Output;
        }

        public char NextToken (int Index)
        {
            ClearWhiteSpace(Index);
            return Json.ElementAt(Index);
        }

        public void ClearWhiteSpace (int Index)
        {
            var EmptyChars = new List<char> { ' ', '\n', '\t', '\r' };
            while (Index < Json.Count - 1)
            {
                if (EmptyChars.Contains(Json.ElementAt(Index)))
                {
                    Json.RemoveAt(Index);
                }
                else
                {
                    break;
                }
            }    
        }
    }
}
