using Newtonsoft.Json;
using System.Globalization;
using System.Xml.Linq;

namespace TestTaskConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await SerializeToJson(GetItems("data.xml"));
            await SerializeToExcel(GetItems("data.xml"));
        }

        public static IEnumerable<Item>? GetItems(string path)
        {
            XDocument document = XDocument.Load(path);

            var polytic = document.Element("channel")?
                .Elements("item")
                .Where(i => (i.Element("category")?.Value).Contains("Политика"))
                .Select(i => new Item
                {
                    Title = i.Element("title")?.Value,
                    Link = i.Element("link")?.Value,
                    Description = i.Element("description")?.Value,
                    Category = i.Element("category")?.Value,
                    PubDate = DateTime.Parse(i.Element("pubDate")?.Value, CultureInfo.CreateSpecificCulture("en-US"))
                })?
                .OrderBy(i => i.PubDate);

            return polytic;
        }

        public static async Task SerializeToJson(IEnumerable<Item>? items)
        {
            var json = JsonConvert.SerializeObject(items);

            await File.WriteAllTextAsync("data.json", json);
        }

        public static async Task SerializeToExcel(IEnumerable<Item>? items)
        {
            var book = new ExcelConverter().Fill(items);

            await File.WriteAllBytesAsync("data.xlsx", book);
        }
    }
}