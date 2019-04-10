using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace WritterService
{
    class Program
    {
        public static ILogger Logger { get; set; }

        static void Main(string[] args)
        {
            try
            {
                DBProvider _provider = new DBProvider();
                _provider.InitializeDB();
                Logger = new Logger();
                Logger.Debug("1234567890");
                
                // Инициализируем службу, указываем адрес, по которому она будет доступна
                ServiceHost host = new ServiceHost(typeof(WritterService),
                    new Uri("http://localhost:59888/WritterService"));
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
                Console.WriteLine("Сервер запущен");
                Console.ReadLine();
                // Закрываем службу
                host.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
      
        }
    }
}
