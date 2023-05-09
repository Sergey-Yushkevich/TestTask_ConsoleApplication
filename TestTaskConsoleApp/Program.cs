using System.Globalization;
using System.Xml.Linq;

namespace TestTaskConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var items = GetItems("data.xml");

            Console.WriteLine("Enter type of format: 1 - json, 2 - excel");
            var result = Console.Read();

            switch (result)
            {
                case 1:
                    await Serializer.SerializeToJson(items);
                    break;
                case 2:
                    await Serializer.SerializeToExcel(items);
                    break;
                default:
                    break;
            }
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

        
    }
}