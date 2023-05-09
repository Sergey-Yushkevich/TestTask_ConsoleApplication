using Newtonsoft.Json;

namespace TestTaskConsoleApp
{
    public static class Serializer
    {
        public static async Task SerializeToJson(IEnumerable<Item>? items)
        {
            var json = JsonConvert.SerializeObject(items);

            await File.WriteAllTextAsync("data.json", json);
        }

        public static async Task SerializeToExcel(IEnumerable<Item>? items)
        {
            var book = ExcelConverter.Fill(items);

            await File.WriteAllBytesAsync("data.xlsx", book);
        }
    }
}
