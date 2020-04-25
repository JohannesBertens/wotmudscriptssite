using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Text;

namespace site.Data
{
    public class RentItemsService
    {
        public static async Task<RentItem[]> GetRentItemsAsync(ApplicationUser user)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            using SqlConnection connection = new SqlConnection(config.GetConnectionString("DefaultConnection"));
            connection.Open();

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT [city], [name], [slot], [item], [character] ");
            sb.Append("FROM [dbo].[RENTSTORAGE] ");
            sb.Append($"WHERE email = @email and personalkey = @key");
            var sql = sb.ToString();

            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add("@email", SqlDbType.VarChar);
            command.Parameters["@email"].Value = user.Email;
            command.Parameters.Add("@key", SqlDbType.VarChar);
            command.Parameters["@key"].Value = user.PersonalKey;


            using SqlDataReader reader = command.ExecuteReader();
            var rentItemList = new List<RentItem>();
            while (await reader.ReadAsync())
            {
                var rentItem = new RentItem()
                {
                    city = reader.GetString(0),
                    name = reader.GetString(1),
                    slot = reader.GetInt32(2),
                    item = reader.GetString(3),
                    character = reader.GetString(4)
                };
                Console.WriteLine(rentItem);
                rentItemList.Add(rentItem);
            }

            return rentItemList.ToArray();
        }
    }
}
