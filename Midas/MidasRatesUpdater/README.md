# ExchangeRatesUpdater

#### Microsoft documentation

1. Azure Functions in VS - https://learn.microsoft.com/en-us/azure/azure-functions/
1. Enable CORS - https://learn.microsoft.com/en-us/azure/app-service/app-service-web-tutorial-rest-api/

#### How to enable CORS from Azure Portal to Azure Function App (in Azure Portal)

1. Login to Azure Portal.
2. Open Function App.
3. Go to "CORS" page.
4. Type "https://portal.azure.com" into text box under "Allowed Origins"
5. Press "Save".

#### How to enable CORS from Azure Portal to Azure Function App (in Cloud Shell)

1. Login to Azure Portal.
2. Open Cloud Shell.
3. Run the following command (replace `<rg-name>` and `<app-name>` placeholders):
   
```PowehShell
az webapp cors add --resource-group <rg-name> --name <app-name> --allowed-origins 'https://portal.azure.com'
```
