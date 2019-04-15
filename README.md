# KsHomeWork
WCF service + Rest service + tests

# Задание:
Тестовое задание состоит из 3 частей: 
Сервисы должны работать по протоколу http. 
Оба сервиса должны логировать запросы/ответы/ошибки через .Net библиотеку Serilog.
Тестовое задание выполнить и прислать в виде одного солюшина (Visual Studio), который должен включать в себя оба сервиса и проект с тестами.


# Описание данного проекта:
1. Проект представляет из себя  решение (Solution) из трех проектов:
   1) WritterService - SOAP сервис/ Запускается как консольное приложение. 
   2) ReaderApiService - REST API .NET Core сервис. Запускается как сайт IIS
   3) NUnitAutoTests - библиотека классов на базе .Net Core Console Apllication и библиотеки NUnit. 
                       Запускается посредством TestExplorer , или через отдельное приложение от NUnit



# Запуск сервисов:
  1. сервис WritterService
    -- так как данный сервис является self-hosted приложением, 
       то для возможности прослушивать какой-либо порт ему требуются права администратора.
    1 вариант запуска: Запустить проект (Debug -> Start (F5)) из VisualStudio открытой с правами администратора
    2 вариант запуска: Запустить exe-файл "WritterService.exe" из папки "...\KsHomeWork\WritterService\bin\Debug" от имени администратора

    Для проверки работоспособности сервиса перейдите по ссылке: http://localhost:59888/WritterService
    Или подключите http://localhost:59888/WritterService?wsdl файл к вашему проекту и вызовите метод Add()

  2. ReaderApiService 
     При наличии библиотек .Net Core никаких особых действий для запуска сервиса не требуется. Просто запустите проект (Debug -> Start (F5))
Проверить можно через Swagger. Откроется автоматически


# Настройки сервисов:
    1. сервис WritterService содержит настройки (Program.cs):
       		ServiceUrl = @"http://localhost:59888/WritterService";
                string _workPath = @"C:\tmp\ks\";

    2. сервис ReaderApiService содержит настройки:
                "WorkPath":  "C:/tmp/ks/"  - из файла конфигурации (appsetting.json)
		"App Url":   http://localhost:49905/  - ReaderApiService--> Propeties --> Debug

    3. проект автотестов:
       		RestApiUrl = @"http://localhost:49905"; // Ссылка к RestApi сервису чтения данных (Tests --> BaseTests.cs)
        	WcfSoapUrl = @"http://localhost:59888/WritterService"; // Ссылка к SOAP WCF сервису (Tests --> BaseTests.cs)
		"Uri":   "http://localhost:59888/WritterService?wsdl", // ссылка на контракт сервиса (Connected Services -> WritterWcfService -> ConnectedServices.json)

Соответственно ссылки на сервисы и БД в разных проектах должны совпадать.

# Запуск тестов
Для запуска рекомендуется запустить оба сервиса:
 - WritterService открыть согласно второго варианта запуска
 - RestApiService запустить из студии и остановить Debug (IIS останется запущен)
После чего запускать тесты (Test - Windows - Test Explorer - Run All)

	
   
