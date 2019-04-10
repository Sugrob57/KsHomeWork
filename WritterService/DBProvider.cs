using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace WritterService
{
    public class DBProvider
    {
        SQLiteConnection Connect { get; set; }

        public DBProvider()
        {
            Connect = new SQLiteConnection(@"Data Source=C:\clientDB.db; Version=3;");
        }


        public void InitializeDB()
        {
            if (!File.Exists(@"C:\clientDB.db")) // если базы данных нету, то...
            {
                SQLiteConnection.CreateFile(@"C:\clientDB.db");
            }

            Connect = new SQLiteConnection(@"Data Source=C:\clientDB.db; Version=3;");
            CreateDB();
        }

        private void CreateDB()
        {
            using (Connect)
            {
                // строка запроса, который надо будет выполнить
                string commandText = "CREATE TABLE IF NOT EXISTS [Client] ( " +
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

        public int AddClient(string firstName, string secondName, int gender, string dateOfBirth, string middleName = null)
        {
            using (Connect)
            {
                string commandText = String.Format("INSERT INTO [Client] ('firstName', 'secondName', 'middleName', 'dateOfBirth' ,'gender')" +
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
                }

                Connect.Close(); // закрыть соединение

                return _userId;
            }
        }
    }
}
