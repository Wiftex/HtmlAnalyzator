using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlAnalyzator
{
    class DataBase
    {
        public DataBase(string pathFile)
        {
            using (var connection = new SqliteConnection(@"Data Source=data\db.db"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = "CREATE TABLE " + pathFile +"(_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Name TEXT NOT NULL, Age INTEGER NOT NULL)";
                command.ExecuteNonQuery();

                Console.WriteLine("Таблица " + pathFile + " создана");
            }
        }
    }
}
