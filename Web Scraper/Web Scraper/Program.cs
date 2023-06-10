using HtmlAgilityPack;
using System;
using System.Net.Http;

namespace webScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            //send get request to weather.com
            string url = "https://weather.com/en-GB/weather/today/l/4b16a412b83c27a7c115990ab3c07594d961e429fc71f9f45e975906113575903f095c11a638bf2a78dedafb1a284fa9";
            var httpClient = new HttpClient();
            var html = httpClient.GetStringAsync(url).Result;
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            //get the temperature
            var temperatureElement = htmlDocument.DocumentNode.SelectSingleNode("//span[@class='CurrentConditions--tempValue--MHmYY']");
            var temperature = temperatureElement.InnerText.Trim();
            Console.WriteLine("Temperature: " + temperature);
            //get the conditions
            var conditionElement = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='CurrentConditions--phraseValue--mZC_p']");
            var conditions = conditionElement.InnerText.Trim();
            Console.WriteLine("Conditions: " + conditions);

            //get the location
            var cityElement = htmlDocument.DocumentNode.SelectSingleNode("//h1[@class='CurrentConditions--location--1YWj_']");
            var city = cityElement.InnerText.Trim();
            Console.WriteLine("Location: " + city);

        }
    }
}