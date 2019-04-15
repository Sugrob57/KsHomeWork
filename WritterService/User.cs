using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;


namespace WritterService
{
    public class User
    {
        // Класс, описывающий модель данных - Пользователь
        public string FirstName { get; set; } // Имя
        public string SecondName { get; set; } // Фамилия
        public string MiddleName { get; set; } // Отчетство
        public DateTime DateOfBirth { get; set; } // дата рождения
        public int Gender { get; set; } // пол (0 - ж, 1 - м)
        public int UserId { get; set; } // идентификатор пользователя в БД

        public bool Save() // метод сохранения объекта в БД
        {
            try
            {
                DBProvider _provider = new DBProvider(Program.DbPath);
                UserId = _provider.AddUser(FirstName, SecondName, Gender, DateOfBirth.ToString("yyyy-MM-dd"), MiddleName);
                return true;
            }
            catch (Exception e)
            {
                Log.Error("Error in process saving user in DB: {0}", e.Message);
                return false;
            }
        }
    }
}
