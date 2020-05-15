using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace site.Data
{
    public class CharDataService
    {
        public static async Task<string[]> GetCharsForUserAsync(ApplicationUser user)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            using SqlConnection connection = new SqlConnection(config.GetConnectionString("DefaultConnection"));
            connection.Open();

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DISTINCT(character) ");
            sb.Append("FROM [dbo].[CHARINFO] ");
            sb.Append($"WHERE email = @email and personalkey = @key");
            var sql = sb.ToString();

            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add("@email", SqlDbType.VarChar);
            command.Parameters["@email"].Value = user.Email;
            command.Parameters.Add("@key", SqlDbType.VarChar);
            command.Parameters["@key"].Value = user.PersonalKey;

            using SqlDataReader reader = command.ExecuteReader();
            var characters = new List<string>();
            while (await reader.ReadAsync())
            {
                var character = reader.GetString(0);
                Console.WriteLine(character);
                characters.Add(character);
            }

            return characters.ToArray();
        }

        public static async Task<CharData[]> GetCharDataAsync(ApplicationUser user)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            using SqlConnection connection = new SqlConnection(config.GetConnectionString("DefaultConnection"));
            connection.Open();

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * ");
            sb.Append("FROM [dbo].[CHARINFO] ");
            sb.Append($"WHERE email = @email and personalkey = @key");
            var sql = sb.ToString();

            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add("@email", SqlDbType.VarChar);
            command.Parameters["@email"].Value = user.Email;
            command.Parameters.Add("@key", SqlDbType.VarChar);
            command.Parameters["@key"].Value = user.PersonalKey;

            var returnChars = new List<CharData>();

            using SqlDataReader reader = command.ExecuteReader();
            while (await reader.ReadAsync())
            {
                var stats = new Stats()
                {
                    STR = reader.GetFieldValue<Int32>("stats_str"),
                    INT = reader.GetFieldValue<Int32>("stats_int"),
                    WIL = reader.GetFieldValue<Int32>("stats_wil"),
                    DEX = reader.GetFieldValue<Int32>("stats_dex"),
                    CON = reader.GetFieldValue<Int32>("stats_con"),
                };
                var charToAdd = new CharData()
                {
                    Character = reader.GetString("character"),
                    Age = reader.GetFieldValue<Int32>("age"),
                    Race = reader.GetString("Race"),
                    Class = reader.GetString("class"),
                    Stats = stats
                };
                returnChars.Add(charToAdd);
            }

            return returnChars.ToArray();
        }
    }
}
