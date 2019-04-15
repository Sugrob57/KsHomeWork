using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using Serilog;

namespace WritterService
{
    public class DBProvider
    { // Работа с БД SQLLite
        SQLiteConnection Connect { get; set; }
        string DbPath { get; set; }

        public DBProvider(string dbPath)
        {
            DbPath = dbPath;
            Connect = new SQLiteConnection(@"Data Source=" + DbPath + @"; Version=3;");
        }

        public void InitializeDB()
        {
            if (!File.Exists(DbPath)) // если базы данных нету, то создадим ее
            {
                SQLiteConnection.CreateFile(DbPath);
                Log.Information("Create DB in path:" + DbPath);
            }
            CreateTables();
        }

        private void CreateTables() // создание таблиц в новой БД
        {
            using (Connect)
            {
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

        public int AddUser(string firstName, string secondName, int gender, string dateOfBirth, string middleName = null)
        {
            using (Connect)
            {
                string commandText = String.Format("INSERT INTO [systemUser] ('firstName', 'secondName' ,'gender', 'dateOfBirth', 'middleName')" +
                    "values ('{0}','{1}','{2}','{3}','{4}')", firstName, secondName, gender, dateOfBirth, middleName);
                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                Connect.Open(); // открыть соединение
                Command.ExecuteNonQuery(); // выполнить запрос вставки записи

                // возврат ИД только что созданной записи
                commandText = "SELECT last_insert_rowid() as 'id'";
                Command = new SQLiteCommand(commandText, Connect);
                SQLiteDataReader reader = Command.ExecuteReader();
                int _userId = 0;
                if (reader.Read())
                {
                    _userId = System.Convert.ToInt32(reader["id"]);
                    Log.Debug("Inserted record in DB. Id new record = {0}", _userId);
                    //Connect.Close();
                    return _userId;
                }
                else
                {
                    //Connect.Close();
                    throw new NullReferenceException();
                }
            }
        }

        public void CloseDB()
        {
            try
            {
                Connect.Close();
            }
            catch { }
            
        }
    }
}
