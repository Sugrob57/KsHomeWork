﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;
using Serilog;

namespace WritterService
{
    class Program
    {
        public static ServiceHost HttpHost { get; set; } // хост, где будет расположен сервис
        public static string DbPath { get; set; } // место хранения файла БД
        public static string ServiceUrl { get; set; } // Ссылка для сервиса

        static void Main(string[] args)
        {
            try
            {
                // Service configure 
                ServiceUrl = @"http://localhost:59888/WritterService";
                string _workPath = @"C:\tmp\ks\"; // путь к служебным файлам (БД, логи)
                System.IO.Directory.CreateDirectory(_workPath);
                DbPath = _workPath + @"clientDB.db";
                string log_path = _workPath + @"Writter_.log";

                // Инициализация сервиса
                InitDB(DbPath);
                InitLogger(log_path);
                InitSoapService();
                
                // Ожидание команды на закрытие сервиса
                Console.ReadLine();
                // Закрываем службу

                HttpHost.Close();
                CloseDB(DbPath);
                Log.Information("Service stopped");
            }
            catch (Exception e)
            {
                Log.Fatal(e.Message);
                Console.WriteLine(e);
                Console.ReadLine();
            }    
        }

        private static void InitLogger(string log_path) // Активация логгера Serilog
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(log_path, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Debug("add log file to " + log_path);
        }

        private static void InitDB(string dbPath)
        {
            DBProvider _provider = new DBProvider(dbPath);
            _provider.InitializeDB();
        }

        private static void CloseDB(string dbPath)
        {
            DBProvider _provider = new DBProvider(dbPath);
            _provider.CloseDB();
        }

        private static void InitSoapService() // Запуск WCF SOAP сервиса
        {
            Log.Debug("Starting service...");
            // Инициализируем службу, указываем адрес, по которому она будет доступна
            ServiceHost host = new ServiceHost(typeof(WritterService),
                new Uri(ServiceUrl));
            // Добавляем конечную точку службы с заданным интерфейсом, привязкой (создаём новую) и адресом конечной точки
            host.AddServiceEndpoint(typeof(IWritterService), new BasicHttpBinding(), "");

            // Собираем wsdl-описание контракта сервиса
            ServiceMetadataBehavior metad
              = host.Description.Behaviors.Find<ServiceMetadataBehavior>();
            if (metad == null)
                metad = new ServiceMetadataBehavior();
            metad.HttpGetEnabled = true;
            host.Description.Behaviors.Add(metad);
            host.AddServiceEndpoint(
              ServiceMetadataBehavior.MexContractName,
              MetadataExchangeBindings.CreateMexHttpBinding(),
              "mex"
            );

            // Запускаем службу
            host.Open();
            HttpHost = host;

            Console.WriteLine("Service started");
            Log.Information("Service started");
        }

    }
}
