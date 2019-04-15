# KsHomeWork
WCF service + Rest service + tests

# �������:
�������� ������� ������� �� 3 ������: 
������� ������ �������� �� ��������� http. 
��� ������� ������ ���������� �������/������/������ ����� .Net����������� Serilog.
�������� ������� ��������� � �������� � ���� ������ �������� (Visual Studio), ������� ������ �������� � ���� ��� ������� � ������ � �������.


# �������� ������� �������:
1. ������ ������������ �� ����  ������� (Solution) �� ���� ��������:
   1) WritterService - SOAP ������/ ����������� ��� ���������� ����������. 
   2) ReaderApiService - REST API .NET Core ������. ����������� ��� ���� IIS
   3) NUnitAutoTests - ���������� ������� �� ���� .Net Core Console Apllication � ���������� NUnit. 
                       ����������� ����������� TestExplorer , ��� ����� ��������� ���������� �� NUnit



# ������ ��������:
  1. ������ WritterService
    -- ��� ��� ������ ������ �������� self-hosted �����������, 
       �� ��� ����������� ������������ �����-���� ���� ��� ��������� ����� ��������������.
    1 ������� �������: ��������� ������ (Debug -> Start (F5)) �� VisualStudio �������� � ������� ��������������
    2 ������� �������: ��������� exe-���� "WritterService.exe" �� ����� "...\KsHomeWork\WritterService\bin\Debug" �� ����� ��������������

    ��� �������� ����������������� ������� ��������� �� ������: http://localhost:59888/WritterService
    ��� ���������� http://localhost:59888/WritterService?wsdl ���� � ������ ������� � �������� ����� Add()

  2. ReaderApiService 
     ��� ������� ��������� .Net Core ������� ������ �������� ��� ������� ������� �� ���������. ������ ��������� ������ (Debug -> Start (F5))
��������� ����� ����� Swagger. ��������� �������������


# ��������� ��������:
    1. ������ WritterService �������� ��������� (Program.cs):
       		ServiceUrl = @"http://localhost:59888/WritterService";
                string _workPath = @"C:\tmp\ks\";

    2. ������ ReaderApiService �������� ���������:
                "WorkPath":  "C:/tmp/ks/"  - �� ����� ������������ (appsetting.json)
		"App Url":   http://localhost:49905/  - ReaderApiService--> Propeties --> Debug

    3. ������ ����������:
       		RestApiUrl = @"http://localhost:49905"; // ������ � RestApi ������� ������ ������ (Tests --> BaseTests.cs)
        	WcfSoapUrl = @"http://localhost:59888/WritterService"; // ������ � SOAP WCF ������� (Tests --> BaseTests.cs)
		"Uri":   "http://localhost:59888/WritterService?wsdl", // ������ �� �������� ������� (Connected Services -> WritterWcfService -> ConnectedServices.json)

�������������� ������ �� ������� � �� � ������ �������� ������ ���������.

# ������ ������
��� ������� ������������� ��������� ��� �������:
 - WritterService ������� �������� ������� �������� �������
 - RestApiService ��������� �� ������ � ���������� Debug (IIS ��������� �������)
����� ���� ��������� ����� (Test - Windows - Test Explorer - Run All)

	
   
