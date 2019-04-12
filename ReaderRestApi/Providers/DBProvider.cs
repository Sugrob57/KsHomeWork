using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using ReaderRestApi.Models;
using CoreTestApi;
using Serilog;

namespace ReaderRestApi.Providers
{
    public class DBProvider
    {
        SQLiteConnection Connect { get; set; }
        string DbPath { get; set; }

        public DBProvider()
        {
            DbPath = Startup.WorkPath + @"clientDB.db";
            Connect = new SQLiteConnection(@"Data Source=" + DbPath + @"; Version=3;");
        }

        public void InitializeDB()
        {
            Log.Debug("Search DB file in path {0}", DbPath);
            if (!File.Exists(DbPath)) // если базы данных нет, то...
            {
                SQLiteConnection.CreateFile(DbPath);
                Log.Debug("Create DB in path:" + DbPath);
                CreateTables();
            }
            else
            {
                Log.Debug("DB file is exist");
            }
            
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
                    "[dateOfBirth] NVARCHAR(10)," +
                    "[gender] INTEGER)"; // создать таблицу, если её нет
                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                Connect.Open(); // открыть соединение
                Command.ExecuteNonQuery(); // выполнить запрос
                Connect.Close(); // закрыть соединение
            }
        }

        public List<List<object>> GetUsers()
        {
            CheckConnect();
            using (Connect)
            {
                List<List<object>> _users = new List<List<object>>();

                string commandText = "SELECT id, firstName, secondName, middleName, dateOfBirth ,gender " +
                    "FROM [systemUser]";
                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                Connect.Open(); // открыть соединение      
                SQLiteDataReader reader = Command.ExecuteReader();

                int n = 0;
                while (reader.Read())
                {
                    _users.Add(
                        new List<object>
                            {
                                reader["firstname"],
                                reader["secondName"],
                                reader["dateOfBirth"],
                                reader["gender"],
                                reader["id"],
                                reader["middleName"]
                            }
                        );
                    n++;
                }
                Log.Debug("readed {0} records from DB", n);

                Connect.Close(); // закрыть соединение

                return _users;
            }
        }

        public User GetUser(int userId)
        {
            CheckConnect();
            using (Connect)
            {
                User _user = new User();

                string commandText = "SELECT id, firstName, secondName, middleName, dateOfBirth ,gender " +
                    "FROM [systemUser] WHERE id = " + userId.ToString();
                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                Connect.Open(); // открыть соединение      
                SQLiteDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    _user = new User(
                        reader["firstname"].ToString(),
                        reader["secondName"].ToString(),
                        System.Convert.ToDateTime(reader["dateOfBirth"]),
                        System.Convert.ToInt16(reader["gender"]),
                        System.Convert.ToInt32(reader["id"]),
                        reader["middleName"].ToString()
                        );
                }
                //Log.Debug("Inserted record in DB. Id new record = {0}", _userId);

                Connect.Close(); // закрыть соединение

                return _user;
            }
        }

        private void CheckConnect()
        {
            try
            {
                var st = Connect.State;
            }
            catch (ObjectDisposedException)
            {
                Connect = new SQLiteConnection(@"Data Source=" + DbPath + @"; Version=3;");
            }
        }
    }
}
