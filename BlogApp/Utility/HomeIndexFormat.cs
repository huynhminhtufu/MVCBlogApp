using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogApp.Utility
{
    public class HomeIndexFormat
    {
        public static string Format(string sample, int limit)
        {
            string s = sample;
            if (sample.Length < limit)
            {
                return s;
            }
            Stack<string> stackOneTag = new Stack<string>();
            Stack<string> stackPairTag = new Stack<string>();
            int t = limit;
            for (int i = 0; i < s.Length; i++)
            {

                if (s[i].Equals('<'))
                {
                    if (s[i + 1].Equals('/'))
                    {
                        stackOneTag.Push("</");
                        stackPairTag.Pop();
                    }
                    else
                    {
                        stackOneTag.Push("<");
                        stackPairTag.Push("<>");
                    }
                }
                if (s[i].Equals('>'))
                {
                    stackOneTag.Pop();
                }
                if (i > limit && stackOneTag.Count == 0 && stackPairTag.Count == 0)
                {
                    t = i;
                    break;
                }
            }
            if (stackOneTag.Count != 0 || stackPairTag.Count != 0)
            {
                return "Error!!!";
            }
            return s.Substring(0, t + 1);
        }
    }
}