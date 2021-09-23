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
            //Ввод пользователем пути к файлу
            Console.Write("Введите путь к файлу: ");
            string path = Console.ReadLine();

            //Обработка HTML документа и запись результата в массив слов
            string[] words = Parsing(path);

            DataBase dataBase = new DataBase(path);

            //Ключ словаря - слово, значение - количество в файле
            Dictionary<string, int> dict = new Dictionary<string, int>();

            //Если массив слов не пуст
            if (words != null)
            {
                //Подсчет упоминаний слова в файле
                for (int i = 0; i < words.Length; i++)
                {
                    //счетчик
                    int count = 0;

                    //Если слово повторяется, то увеличиваем счетчик
                    for (int j = i; j < words.Length; j++)
                    {
                        if (words[i] == words[j])
                        {
                            count++;
                        }
                    }

                    //Проверка на то записано ли слово в словарь
                    bool keyExists = false;
                    foreach (string s in dict.Keys)
                    {
                        if (words[i] == s)
                        {
                            keyExists = true;
                        }
                    }

                    //Если нет, то записываем
                    if (!keyExists)
                        dict.Add(words[i], count);

                }

                //Сортировка словаря и вывод результата
                foreach (KeyValuePair<string, int> keyValue in dict.OrderByDescending(keyValue => keyValue.Value))
                {
                    Console.WriteLine(keyValue.Key + " - " + keyValue.Value);
                }
            }

            Console.ReadLine();
        }

        static string[] Parsing(string path)
        {
            string text;

            try
            {
                //Запись символов из файла в переменную
                StreamReader reader = new StreamReader(path);
                text = reader.ReadToEnd();

                //Регулярное выражение фильтрующее теги
                Regex regex = new Regex(@"(>([^<]*)<)");
                MatchCollection matches = regex.Matches(text);
                text = "";
                if (matches.Count > 0)
                {
                    foreach (Match match in matches)
                        text = String.Concat(text, match.Value);
                }

                //Регулярное выражение фильтрующее неподходящие символы
                text = Regex.Replace(text, @"&([\s\S]+?);", "");

                //Разделение текста на массив строк
                string[] words = text.Split(new char[] { ' ', '<', '>', ',', '.', '&', ';', ':', '\n', '\u000D' }, StringSplitOptions.RemoveEmptyEntries);

                return words;
            }
            catch(FileNotFoundException ex)
            {
                //Если файл не найден, то выводится сообщение и записывается лог-файл
                Console.WriteLine("Файл не найден");

                StreamWriter writer = new StreamWriter("log/log_" + DateTime.Now.ToShortDateString() + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Second + ".log", true);
                writer.WriteLine(ex.Message);
                writer.Close();

                return null;
            }
        }
    }
}
