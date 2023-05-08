using System.Globalization;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace TestTaskConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

        }

        public static IEnumerable<Item> GetItems(string path)
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