# KsHomeWork
WCF service + Rest service + tests

# Задание
> Тестовое задание состоит из 3 частей: 
> Сервисы должны работать по протоколу http. 
> Оба сервиса должны логировать запросы/ответы/ошибки через .Net библиотеку Serilog.
> Тестовое задание выполнить и прислать в виде одного солюшина (Visual Studio), который должен включать в себя оба сервиса и проект с тестами.


# Описание данного проекта
Проект представляет из себя  решение (Solution) из трех проектов:
   1. **WritterService** - SOAP сервис. Запускается как консольное приложение; 
   2. **ReaderApiService** - REST API .NET Core сервис. Запускается как сайт IIS;
   3. **NUnitAutoTests** - библиотека классов на базе .Net Core Console Apllication и библиотеки NUnit; 
      Запускается посредством TestExplorer , или через отдельное приложение от NUnit

# Запуск сервисов
## сервис WritterService
***Для возможности прослушивать какой-либо порт приложению требуются права администратора***

* **1 вариант запуска**: Запустить проект (Debug -> Start (F5)) из VisualStudio открытой с правами администратора;
* **2 вариант запуска**: Запустить exe-файл "WritterService.exe" из папки "...\KsHomeWork\WritterService\bin\Debug" от имени администратора;

   Для проверки работоспособности сервиса перейдите по ссылке: <http://localhost:59888/WritterService>
   Или подключите <http://localhost:59888/WritterService?wsdl> файл к вашему проекту и вызовите метод Add()

## ReaderApiService 
При наличии библиотек .Net Core никаких особых действий для запуска сервиса не требуется. Просто запустите проект (Debug -> Start (F5));
Проверить можно через Swagger - откроется автоматически.


# Настройки сервисов
## сервис WritterService
      ```
      ServiceUrl = @"http://localhost:59888/WritterService"; \\(Program.cs)
      string _workPath = @"C:\tmp\ks\"; \\(Program.cs)
      ```

## сервис ReaderApiService
      ```
      "WorkPath":  "C:/tmp/ks/"; \\  - из файла конфигурации (appsetting.json)
      "App Url":   http://localhost:49905/  \\ - (ReaderApiService--> Properties --> Debug)
      ```

## автотесты NUnitAutoTests
      ```
      RestApiUrl = @"http://localhost:49905"; // Ссылка к RestApi сервису чтения данных (Tests --> BaseTests.cs)
      WcfSoapUrl = @"http://localhost:59888/WritterService"; // Ссылка к SOAP WCF сервису (Tests --> BaseTests.cs)
      "Uri":   "http://localhost:59888/WritterService?wsdl", // ссылка на контракт сервиса (Connected Services -> WritterWcfService -> ConnectedServices.json)
      ```

***Соответственно ссылки на сервисы и БД в разных проектах должны совпадать.***

# Запуск тестов
Для запуска рекомендуется:
 1. Запустить оба сервиса:
  * WritterService открыть согласно второго варианта запуска
  * RestApiService запустить из студии и остановить Debug (IIS останется запущен)
 2. После чего запускать тесты (Test - Windows - Test Explorer - Run All)

	
   
