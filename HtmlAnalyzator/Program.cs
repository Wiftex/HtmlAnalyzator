using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace HtmlAnalyzator
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\wifte\source\repos\HtmlAnalyzator\HtmlAnalyzator\bin\Debug\net5.0\Тестовые Файлы\file1.html";

            string[] words = Parsing(path);

            Dictionary<string, int> dict = new Dictionary<string, int>();

            for (int i = 0; i<words.Length; i++)
            {
                int count = 0;

                for (int j = i; j < words.Length; j++)
                {
                    if (words[i] == words[j])
                    {
                        count++;
                    }
                }

                bool keyExists = false;
                foreach(string s in dict.Keys)
                {
                    if(words[i] == s)
                    {
                        keyExists = true;
                    }
                }

                if (!keyExists)
                    dict.Add(words[i], count);

            }

            foreach(KeyValuePair<string, int> keyValue in dict.OrderByDescending(keyValue => keyValue.Value))
            {
                Console.WriteLine(keyValue.Key + " - " + keyValue.Value);
            }
        }

        static string[] Parsing(string path)
        {
            string text;

            StreamReader reader = new StreamReader(path);
            text = reader.ReadToEnd();

            Regex regex = new Regex(@"(>([^<]*)<)");
            MatchCollection matches = regex.Matches(text);
            text = "";
            if (matches.Count > 0)
            {

                foreach (Match match in matches)
                    text = String.Concat(text, match.Value);
            }

            text = Regex.Replace(text, @"&([\s\S]+?);", "");

            string[] words = text.Split(new char[] { ' ', '<', '>', ',', '.', '&', ';', ':', '\n', '\u000D' }, StringSplitOptions.RemoveEmptyEntries);

            return words;
        }
    }
}
