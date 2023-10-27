<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Text.Encodings.Web</Namespace>
</Query>

/*********************************************************************************************************************************************************************
************************************                 Demo of getting Currency Exchange Rates using NBP Web API                   *************************************
/*********************************************************************************************************************************************************************

-----------------
- Resources:
-----------------

1) NBP Web API:                                       https://api.nbp.pl/en.html

2) General info about NBP rates (e.g. A,B,C tables):  https://nbp.pl/en/statistic-and-financial-reporting/rates/

2) Dates of NBP currency exchange rates publication:  https://nbp.pl/en/statistic-and-financial-reporting/rates/dates-of-nbp-currency-exchange-rates-publication/


*********************************************************************************************************************************************************************/

async Task Main()
{
	//*************************************************************************************************************************
	//*** Exchange rate query parameters
	//*************************************************************************************************************************	

	string table = "A";              // table type (A, B, or C)
	string code = "EUR";             // three- letter currency code (ISO 4217 standard)
	string topCount = "3";           // number determining the maximum size of the returned data series
	string date = "2023-10-23";      // date in the YYYY-MM-DD format (ISO 8601 standard)
	string startDate = "2023-10-22"; // date in the YYYY-MM-DD format (ISO 8601 standard)
	string endDate = "2023-10-24";   // date in the YYYY-MM-DD format (ISO 8601 standard)

	//*************************************************************************************************************************
	//*** Currency exchange rates --> Queries for complete tables
	//*************************************************************************************************************************

	// Current table of exchange rates of type {table}"
	await DumpRequestAsync($"http://api.nbp.pl/api/exchangerates/tables/{table}/");

	// Series of latest {topCount} tables of exchange rates of type {table}
	await DumpRequestAsync($"http://api.nbp.pl/api/exchangerates/tables/{table}/last/{topCount}/");

	// Exchange rate table of type {table} published today (or lack of data)
	await DumpRequestAsync($"http://api.nbp.pl/api/exchangerates/tables/{table}/today");

	// Exchange rate table of type {table} published on {date} (or lack of data)
	await DumpRequestAsync($"http://api.nbp.pl/api/exchangerates/tables/{table}/{date}");

	// Series of exchange rate tables of type {table} published from {startDate} to {endDate} (or lack of data)
	await DumpRequestAsync($"http://api.nbp.pl/api/exchangerates/tables/{table}/{startDate}/{endDate}");

	//*************************************************************************************************************************
	//*** Currency exchange rates --> Queries for particular currency
	//*************************************************************************************************************************

	// Current exchange rate {code} from the exchange rate table of type {table}
	await DumpRequestAsync($"http://api.nbp.pl/api/exchangerates/rates/{table}/{code}/");

	// Series of latest {topCount} exchange rates of currency {code} from the exchange rate table of type {table}
	await DumpRequestAsync($"http://api.nbp.pl/api/exchangerates/rates/{table}/{code}/last/{topCount}/");

	// Exchange rate of currency {code} from the exchange rate table of type {table} published today (or lack of data)
	await DumpRequestAsync($"http://api.nbp.pl/api/exchangerates/rates/{table}/{code}/today/");

	// Exchange rate of currency {code} from the exchange rate table of type {table} published on {date} (or lack of data)
	await DumpRequestAsync($"http://api.nbp.pl/api/exchangerates/rates/{table}/{code}/{date}/");

	// Exchange rate of currency {code} from the exchange rate table of type {table} published from {startDate} to {endDate} (or lack of data)
	await DumpRequestAsync($"http://api.nbp.pl/api/exchangerates/rates/{table}/{code}/{startDate}/{endDate}/");
}

//**************************************************************************************************************************************************************************
//**************************************************************************************************************************************************************************
//**************************************************************************************************************************************************************************

private async Task DumpRequestAsync(string requestUri)
{
	"**************************************************************************************************************************************".Dump();
	Console.WriteLine(requestUri);
	"**************************************************************************************************************************************".Dump();

	using(var client = new HttpClient())
	{
		HttpResponseMessage response = await client.GetAsync(requestUri);

		if (response.IsSuccessStatusCode)
		{
			// Read HTTP response content as Stream
			Stream jsonStream = await response.Content.ReadAsStreamAsync();
			
			// Deserialize Stream (with HTTP response content) into JsonElement
			JsonElement jsonElement = await JsonSerializer.DeserializeAsync<JsonElement>(jsonStream);
			
			// Serialize JsonElement (with HTTP response content) into indented JSON string
			string jsonStringIndented = JsonSerializer.Serialize(jsonElement, new JsonSerializerOptions{ WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping });

			Console.WriteLine($"\n{jsonStringIndented}\n");
		}
		else
		{
			Console.WriteLine($"StatusCode: {response.StatusCode}\nReasonPhrase: {response.ReasonPhrase}\n");
		}
	}
}
