using Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace site.Data
{
    public class RentItemsService
    {
        public static async Task<RentItem[]> GetRentItemsAsync()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables() 
                .Build();

            var EndpointUri = config["APPSETTING_CosmosEndpointUri"];
            var PrimaryKey = config["APPSETTING_CosmosPrimaryKey"];
            
            var databaseId = "rentstorage";
            var containerId = "rentstorage";

            var cosmosClient = new CosmosClient(EndpointUri, PrimaryKey,
                new CosmosClientOptions() {ApplicationName = "CosmosDBDotnetQuickstart"});
            var container = cosmosClient.GetContainer(databaseId, containerId);
            Console.WriteLine($"Got Container: {container.Id}\n");

            var queryDefinition = new QueryDefinition($"SELECT * FROM c");
            var rentItemList = new List<RentItem>();
            await foreach (JsonElement item in container.GetItemQueryIterator<JsonElement>(queryDefinition))
            {
                rentItemList.Add(JsonConvert.DeserializeObject<RentItem>(item.GetRawText()));
            }

            return rentItemList.ToArray();
        }
    }
}
