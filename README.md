# Midas


## Overview
Demo system which allows to manage multi-currency wallets.

## System requirements

<ul type="none">
  <li>1. System downloads currency exchange rates periodically from table B provided by the NBP and saves them.</li>
  <li>2. Wallets can be listed along with their contents.</li>
  <li>3. Wallets can be listed along with their contents.</li>
  <li>4. System allows to perform the following transactions:
    <ul type="none">
      <li>4.1. Desposit any amount of the money in any currency available in table B in a wallet.</li>
      <li>4.2. Withdraw any amount of money from a wallet.</li>
      <li>4.3 Convert a part of a wallet  into another currency, using the latest available exchange rates (conversion must be done through PLN).</li>
    </ul>
  </li>
  <li>5. System does not need to offer GUI in its MVP scope (API is enough).</li>
  <li>6. System does not need to offer authorization, nor authentication.</li>
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
  <li>1. NBP Web API:
  <li>https://api.nbp.pl/en.html</li>
</ul>
