using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using ReaderRestApi.Models;
//using Serilog;

namespace ReaderRestApi.Providers
{
    public class DBProvider
    {
        SQLiteConnection Connect { get; set; }
        string DbPath { get; set; }

        public DBProvider(string dbPath)
        {
            DbPath = dbPath;
            Connect = new SQLiteConnection(@"Data Source=" + DbPath + @"; Version=3;");
        }

        public void InitializeDB()
        {
            if (!File.Exists(DbPath)) // если базы данных нету, то...
            {
                SQLiteConnection.CreateFile(DbPath);
                //Log.Information("Create DB in path:" + DbPath);
            }
            CreateTables();
        }

        private void CreateTables()
        {
            using (Connect)
            {
                // строка запроса, который надо будет выполнить
                string commandText = "CREATE TABLE IF NOT EXISTS [systemUser] ( " +
                    "[id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
                    "[firstName] NVARCHAR(50), " +
                    "[secondName] NVARCHAR(50), " +
                    "[middleName] NVARCHAR(50), " +
                    "[dateOfBirth] DATE," +
                    "[gender] INTEGER)"; // создать таблицу, если её нет
                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                Connect.Open(); // открыть соединение
                Command.ExecuteNonQuery(); // выполнить запрос
                Connect.Close(); // закрыть соединение
            }
        }

        public List<User> GetUsers(int userId)
        {
            using (Connect)
            {
                List<User> _users = new List<User>();

                string commandText = "SELECT 'id','firstName', 'secondName', 'middleName', 'dateOfBirth' ,'gender'" +
                    "FROM [systemUser]";
                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                Connect.Open(); // открыть соединение      
                SQLiteDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    _users.Add(new User(
                        reader['firstname'],
                        reader['secondName'],
                        reader['dateOfBirth'],
                        reader['gender'],
                        reader['id'],
                        re
                        ));
                }
                //Log.Debug("Inserted record in DB. Id new record = {0}", _userId);

                Connect.Close(); // закрыть соединение

                return _users;
            }
        }
    }
}
