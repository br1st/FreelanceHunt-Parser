using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FreelanceHunt_Parser
{
    class Program
    {
        static int page = 1;
        static int projects = 0;
        static string rateLimitLeft = string.Empty;
        static int[] catalogIds = { Data.Skills["C#"], Data.Skills["Data parsing"] }; // Watch Data Class to see all skills !!!
        static string apiLink = "https://api.freelancehunt.com/v2/projects?filter[skill_id]={0}&&page[number]={1}";

        static void Main(string[] args)
        {
            Console.Title = "Freelancehunt Projects Parser by br1st";

            string catalogIdsString = string.Empty;

            foreach (var item in catalogIds)
            {
                if (!Data.Skills.ContainsValue(item))
                    return;
            }

            foreach (var id in catalogIds)
            {
                if (id != catalogIds[catalogIds.Length - 1])
                    catalogIdsString += id + ",";
                else
                    catalogIdsString += id;
            }

            IRestResponse response = null;

            while (true)
            {
                string api = String.Format(apiLink, catalogIdsString, page);
                var client = new RestClient(api);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", "Bearer " + Data.token + "");
                response = client.Execute(request);
                var _object = JsonConvert.DeserializeObject<Object>(response.Content);

                if (_object.Data == null)
                    break;

                foreach (var item in _object.Data)
                {
                    if(item.Params.Budget == null)
                    {
                        Console.Write(item.Params.Name + " - ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("цена неуказана");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(item.Params.Name + " - ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(item.Params.Budget.Amount + " " + item.Params.Budget.Currency);
                        Console.ResetColor();
                    }
                }

                projects += _object.Data.Length;
                page++;
            }

            if(response != null)
            {
                foreach (var header in response.Headers)
                {
                    if (header.Name == "X-RateLimit-Remaining")
                        rateLimitLeft = header.Value.ToString();
                }
            }

            Console.Write($"\nВсего ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{projects}");
            Console.ResetColor();
            Console.Write(" проектов по данному фильтру ( ");

            for (int i = 0; i < catalogIds.Length; i++)
            {
                if(i != (catalogIds.Length - 1))
                    Console.Write(Data.Skills.FirstOrDefault(item => item.Value == catalogIds[i]).Key + ", ");
                else
                    Console.Write(Data.Skills.FirstOrDefault(item => item.Value == catalogIds[i]).Key + " ");
            }

            Console.WriteLine(")");


            Console.Write($"\nОсталось ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{rateLimitLeft}");
            Console.ResetColor();
            Console.WriteLine(" запросов в час.");

            Console.ReadKey();
        }
    }
}
