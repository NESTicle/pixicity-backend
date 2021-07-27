using System.Text.Json;

namespace Pixicity.Domain.Helpers
{
    public static class JSONHelper
    {
        public static JsonSerializerOptions JsonSerializerStandardOptions()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };

            return options;
        }
    }
}
