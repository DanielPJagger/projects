using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using HtmlAgilityPack;
using System;
using System.Net.Http;

namespace XamarinWebScraper
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
        }
    }
}

namespace WebScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            //send get request to weather.com
            string url = "https://weather.com/en-GB/weather/today/l/347e3f93acdde1193f806d49dcc71d0d1aa6cef1156906e2a3932703187e18c1";
            var httpClient = new HttpClient();
            var html = httpClient.GetStringAsync(url).Result;
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            //get temperature
            var temperatureElement = htmlDocument.DocumentNode.SelectSingleNode("//span[@class='class=\"CurrentConditions--tempValue--MHmYY\']");
            var temperature = temperatureElement.InnerText.Trim();

            //get location
            var LocationElement = htmlDocument.DocumentNode.SelectSingleNode("//h1[@class='class=\"CurrentConditions--location--1YWj_\']");
            var Location = LocationElement.InnerText.Trim();
        }
    }
}