# Midas


## Overview
Demo system which allows to manage multi-currency wallets.

## System requirements

<ul type="none">
  <li>1. System downloads currency exchange rates periodically from table B provided by the NBP and saves them.</li>
  <li>2. Wallets can be listed along with their contents.</li>
  <li>3. System allows to perform the following transactions:
    <ul type="none">
      <li>3.1. Desposit any amount of the money in any currency available in table B in a wallet.</li>
      <li>3.2. Withdraw any amount of money from a wallet.</li>
      <li>3.3 Convert a part of a wallet  into another currency, using the latest available exchange rates (conversion must be done through PLN).</li>
    </ul>
  </li>
  <li>4. System does not need to offer GUI in its MVP scope (API is enough).</li>
  <li>5. System does not need to offer authorization, nor authentication.</li>
</ul>
   
## Future considerations

<ul type="none">
  <li>1. How the application could be deployed on the Azure platform, including which services to use.</li>
  <li>2. How to ensure good performance of the system in case of large number of transactions.</li>
  <li>3. How to report transactions to external systems.</li>
  <li>4. How to ensure that a transaction in "Midas" system is not "approved" until the external system accepts it.</li>
</ul>

## System dependencies

<ul type="none">
  <li>1. NBP Web API: https://api.nbp.pl/en.html</li>
</ul>

## Technology stack

- **ASP.NET Core 6.0 Web API** - to list wallets, deposit/withdraw/convert money in a wallet
- **Azure Function .NET 6** - to periodically "download" exchange rates using NBP Web API
- **Entity Framework Core 6.0 + SQL** - to store exchange rates and multi-currency wallets
- **Swagger** - to design, test and document Wallets Web API

## Architecture

Architecture of the solution was very much inspired by Clean Architecture Pluralsight course by Matthew Renze:

- [Clean Architecture: Patterns, Practices, and Principles](https://app.pluralsight.com/library/courses/clean-architecture-patterns-practices-principles/table-of-contents)

Overview:

- Architecture is domain-centric (focused on use cases)
- Database access is done through EF Core 6.0 using CQRS (commands and queries)
- Web API methods are very "thin" and just call commands and queries (the whole logic is there)

## Core classes (entry points)

- Downloading exchange rates from NBP Web API: [UpdateExchangeRates.cs](https://github.com/michalantolik/midas/blob/main/Midas/MidasRatesUpdater/UpdateExchangeRates.cs)
- Deposit/Withdraw/Convert money in/from wallet: [WalletsController.cs](https://github.com/michalantolik/midas/blob/main/Midas/MidasWalletManagerAPI/Controllers/WalletsController.cs)

## How to run and test this repository locally

### Prerequisites

- Visual Studio 2022 (with Azure workload installed) + .NET 6 SDK
- Visual Studio 2022 (or Microsoft SQL Server Management Studio) for browsing MS SQL Local DB

### Running from Visual Studio

1. Open `Midas.sln` in VS 2022.
2. Right-click solution 👉 Properties 👉 configure "Multiple startup projects" like this 👉 press "OK"
<img src="https://michalantolik.blob.core.windows.net/midas/how_to_run_2.png" width=600/>
4. Press F5 in VS 2022 to start solution.<br/>
5. Solution will be built and two windows will appear.<br/>
6. Azure Function which updates Exchange Rates DB every 30 second by downloading them from NBP Web API:<br/><br/>
<img src="https://michalantolik.blob.core.windows.net/midas/azure_function_running.png" width=600/>
7. Swagger page for testing WalletAPI - make your calls here and observe changes in your SQL Local DB:<br/><br/>
<img src="https://michalantolik.blob.core.windows.net/midas/web_api_running.png" width=600/>

## Browsing Midas Database

- You need to connect to `(localdb)\MSMSQLLocalDB` using any tool.<br/>
- E.g. you can use Microsoft SQL Server Management Studio 👉 [Download SSMS](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)<br/>
- Other example tools: VS 2022 SQL Server Object Explorer, LINQPad

<img src="https://michalantolik.blob.core.windows.net/midas/SSMS_connect_to_sql_server.png" width=350/><br/>
<img src="https://michalantolik.blob.core.windows.net/midas/midas_database.png" width=350/>

## Initial content of the Midas Database

- Once, run the solution, Midas database will be created (if not exist).
- Database will be seeded with example wallet (with example transactions)
- ExchangeRates table will be periodcally updated by downloading exchange rates using NBP Web API

### Wallets
<img src="https://michalantolik.blob.core.windows.net/midas/wallets.png" width=700/>

### Balances
<img src="https://michalantolik.blob.core.windows.net/midas/balances.png" width=700/>

### Transactions
<img src="https://michalantolik.blob.core.windows.net/midas/transactions.png" width=700/>

### Exchange Rates
<img src="https://michalantolik.blob.core.windows.net/midas/exchange_rates.png" width=700/>
