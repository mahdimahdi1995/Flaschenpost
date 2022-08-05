using Flaschenpost.Models;
using Flaschenpost.Services.Helpers;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Flaschenpost.Services
{
    public static class FlaschenpostApiService
    {
        private static HttpClient client = new HttpClient();

        public static async Task<List<Article>> GetArticles()
        {
            List<Brand> brands = GetBrands();
            var articlesList = new List<Article>();

            foreach (var brand in brands)
            {
                foreach (var article in brand.Articles)
                {
                    article.Name = brand.Name;
                    articlesList.Add(article);
                }
            }

            return articlesList;
        }

        private static List<Brand> GetBrands()
        {
            var brandsList = new List<Brand>();
            string url = "https://flapotest.blob.core.windows.net/test/ProductData.json";

            try
            {
                var jsonStream = GetStream(url).Result;
                brandsList = SerializingHelper.GetObjects<Brand>(jsonStream);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            return brandsList;
        }

        private static async Task<Stream> GetStream(string url)
        {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsStream();
        }

        public static async Task<List<Article>> SortAscending(IEnumerable<Article> articles)
        {
            ; return articles.OrderBy(a => a.PricePerUnitText).ToList();
        }

        public static async Task<List<Article>> SortDescending(IEnumerable<Article> articles)
        {
            return articles.OrderByDescending(a => a.PricePerUnitText).ToList();
        }

        public static async Task<List<Article>> GetFilteredList(IEnumerable<Article> articles)
        {
            List<Article> filteredList = new List<Article>();

            foreach (var article in articles)
            {
                var pricePerUnit = GetPrice(article.PricePerUnitText);
                if (pricePerUnit <= 2)
                {
                    filteredList.Add(article);
                }

            }

            return filteredList;
        }

        private static float GetPrice(string pricePerUnitText)
        {
            try
            {
                var resultString = Regex.Match(pricePerUnitText, @"\d+.+\d").Value;
                NumberFormatInfo provider = new NumberFormatInfo();
                provider.NumberDecimalSeparator = ",";
                return float.Parse(resultString, provider);
            }
            catch (Exception e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return 0;
            }
        }
    }
}