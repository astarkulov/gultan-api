using Newtonsoft.Json;

namespace Gultan.Infrastructure.Services.StockDataService;

public class StockDataOptions
{
    [JsonProperty("apikey")] 
    public string ApiKey { get; set; }
}