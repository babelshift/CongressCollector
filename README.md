## What is CongressCollector?
CongressCollector is a console application that can be used to collect data related to U.S. Congressional Legislative Bills and Resolutions in bulk.

## What are the prerequisites?
Currently, the application is written to be compiled against the .NET Core SDK / Microsoft.NETCore.App 1.1.0. You can download the current version of the .NET Core SDK [here](https://www.microsoft.com/net/download/core#/current). You can check the *project.json* file to see all the supported platforms, runtimes, and required dependencies.

## How does it work?
The U.S. Government Publishing Office (GPO) maintains a database of congressional items such as legislative bills/resolution texts, statuses, and summaries. The easiest way to obtain this data is via the GPO FDsys bulk data site. You can inspect these files manually [here](https://www.gpo.gov/fdsys/bulkdata). You can read more about the details of the GPO FDsys bulk data at the [GitHub repo](https://github.com/usgpo/bulk-data).

This application will assemble HTTP requests to retrieve the ZIP files in each container/congress/measure combination from the [GPO FDsys](https://www.gpo.gov/fdsys/) system. Each ZIP file contains a collection of XML files representing each legislative item. Additionally, this application will convert the XML files to JSON files to make data consumption less cumbersome.

**GPO FDsys Folder Structure**

In a nutshell, the GPO FDsys system stores XML files for each legislative item in a specific folder structure.

    COLLECTION --> CONGRESS --> MEASURE --> ZIP/XML

COLLECTION is a data collection related to Bill Summaries, Bill Text, or Bill Statuses.

CONGRESS is the number of the Congress in session at the time of the legislation.

MEASURE is the legislative measure types such as House Resolutions, Senate Joint Resolutions, and more.

**Some Folder Structure Examples**

    BILLSTATUS --> 115TH --> HR --> ZIP/XML
    BILLSTATUS --> 113TH --> SCONRES --> ZIP/XML
    BILLSUM --> 114th --> S --> ZIP/XML

## How do I use it?

First, you'll need to install the latest .NET Core SDK for Windows or Linux [here](https://www.microsoft.com/net/download/core#/current), depending on your OS.

Then, you can open a terminal to clone, build, and run the application:

### Clone and Build

    git clone https://github.com/babelshift/CongressCollector.git
    cd ./CongressCollector/src
    dotnet build
    
### Common Uses

**Help Command**

    dotnet CongressCollector.dll -?

    CongressCollector - For all your congressional data needs.

    Usage: CongressCollector [options] [command]

    Options:
      -? | -h | --help  Show help information

    Commands:
      collect  Initiate the process of collecting bulk data from the GPO FDsys database.
      list     List some valid inputs for commands and options.

    Use "CongressCollector [command] --help" for more information about a command.

**List Command Help**

    dotnet CongressCollector.dll list -?

    Usage: CongressCollector list [options] [command]

    Options:
      -? | -h | --help  Show help information

    Commands:
      collections  List all collections that can be used as an argument of the 'collect' command.
      congresses   List all congresses that can be used in the '-c' option of the 'collect' command.
      measures     List all measures that can be used in the '-m' option of the 'collect' command.

    Use "list [command] --help" for more information about a command.

**List Collections**

    dotnet CongressCollector.dll list collections
    
    'billstatus' - Detailed bill or resolution metadata
    'billsum' - Text summaries of bills, resolutions, or other documents associated with measures such as amendments, reports, or public laws

**List Congresses**

    dotnet CongressCollector.dll list congresses
    
    '113' - 113th Congress (2013 - 2014)
    '114' - 114th Congress (2015 - 2016)
    '115' - 115th Congress (2017 - 2018)

**List Measures**

    dotnet CongressCollector.dll list measures
    
    'hconres' - House Concurrent Resolution
    'hjres' - House Joint Resolution
    'hres' - House Simple Resolution
    'hr' - House Bill
    'sconres' - Senate Concurrent Resolution
    'sjres' - Senate Joint Resolution
    'sres' - Senate Simple Resolution
    's' - Senate Bill

**Collect Command Help**

    dotnet CongressCollector.dll collect -?

    Usage: CongressCollector collect [arguments] [options]

    Arguments:
      collection  Bulk data collection to retrieve. See 'list collections' for valid inputs.

    Options:
      -? | -h | --help            Show help information
      -c | --congress <congress>  Congress for which to receive data. See 'list congresses' for valid inputs.
      -m | --measure <measure>    Legislative measures to retrieve. See 'list measures' for valid inputs.
      
**Collect Specific Legislative Items for a Specific Congress**

*House Concurrent Resolutions* for the *115th Congress* in the *BILLSTATUS* collection.

    dotnet CongressCollector.dll collect billstatus -c 115 -m hconres
    
**Collect All Legislative Items for a Specific Congress**

All legislative measures for the *115th Congress* in the *BILLSTATUS* collection.

    dotnet CongressCollector.dll collect billstatus -c 115

**Collect a Specific Legislative Item for all Congresses**

*House Concurrent Resolutions* for all congresses in the *BILLSTATUS* collection.

    dotnet CongressCollector.dll collect billstatus -m hconres

**Collect All Legislative Items**

All legislative measures for all congresses.

    dotnet CongressCollector.dll collect billstatus
    
### Output

The application will output XML and JSON files for each retrieved legislative measure in the following folder structure:

    ./COLLECTION/CONGRESS/MEASURE
    
Such as:

    ./BILLSTATUS/113/HJRES/BILLSTATUS-113hjres1.json
    ./BILLSTATUS/113/HJRES/BILLSTATUS-113hjres1.xml
