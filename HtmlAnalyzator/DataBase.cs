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
        //Путь к html-файлу
        private string pathFile = "";
        //Путь к базе данных
        private string pathDataBase = "";

        private SqliteConnection connection;
        private SqliteCommand command;

        public DataBase(string pathFile, string pathDataBase)
        {
            this.pathFile = pathFile;
            this.pathDataBase = pathDataBase;

            //Подключение к БД
            connection = new SqliteConnection(@"Data Source=" + pathDataBase);
            connection.Open();

            //Создание таблицы в БД, если она еще не создана
            command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = "CREATE TABLE IF NOT EXISTS Files (File TEXT NOT NULL, Word TEXT NOT NULL, Count INTEGER NOT NULL)";
            command.ExecuteNonQuery();

        }

        //Метод для записи поля в таблицу
        public void Insert(string word, int count)
        {
            connection = new SqliteConnection(@"Data Source=" + pathDataBase);
            connection.Open();

            command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = string.Format("INSERT INTO Files (File, Word, Count) Values ('{0}', '{1}', {2})", pathFile, word, count);
            command.ExecuteNonQuery();
        }
    }
}