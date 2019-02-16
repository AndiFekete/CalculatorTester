# Calculator tester solution
Calculator tester application using Selenium and NUnit
## How to use
* Clone the repository on a machine that has Visual Studio and a browser (Chrome or Firefox) installed
* Make sure that Visual Studio has the NUnit 3 test adapter extension installed
* Open the solution in Visual Studio and build
* The tests will appear in the Test explorer view
* Copy the browser driver executables into the Debug directory from CalculatorTester/CalculatorTester
* Run or debug tests from the Test explorer
## Configuration
The configuration file is constructed as follows:
```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <section name="remoteTestingOptions" type="System.Configuration.NameValueFileSectionHandler, 
      System, Version=1.0.5000.0, Culture=neutral, 
      PublicKeyToken=b77a5c561934e089"/>
  </configSections>
  
  <appSettings>
    <add key="PreferedBrowser" value="[BROWSER TYPE]"/> <!-- Firefox or Chrome -->
    
    <add key="IsRemote" value="[TESTING ON A REMOTE BROWSER]"/>  <!--true or false-->
    
    <add key="Server" value="[SERVER URI]"/>  <!--Uri if remote testing is selected-->
    
  </appSettings>
  
  <remoteTestingOptions>
    <!-- add name="" value="" -->
    <!-- Remote testing options go here -->
    <!-- these will be added to the appropriate DriverOptions object when initializing the test -->
  </remoteTestingOptions>
</configuration>
```
