using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Flaschenpost.Services.Helpers
{
    public static class SerializingHelper
    {
        public static List<T> GetObjects<T>(Stream jsonStream)
        {
            var objectsList = new List<T>();

            try
            {
                var options = new JsonSerializerOptions();
                options.PropertyNameCaseInsensitive = true;
                options.Converters.Add(new JsonStringEnumConverter());
                objectsList = JsonSerializer.DeserializeAsync<List<T>>(jsonStream, options).Result;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            return objectsList;
        }

        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
