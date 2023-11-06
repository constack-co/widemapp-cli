using System.Reflection;

namespace Constack.Widemapp.Common.Helpers
{
    public static class EmbededHelper
    {
        public static async Task<string> ReadEmbededFileAsync(Assembly assembly, string filename)
        {
            var resources = assembly.GetManifestResourceNames();

            using Stream stream = assembly.GetManifestResourceStream(resources.First(x => x.Contains(filename)));

            using StreamReader reader = new(stream);

            return await reader.ReadToEndAsync();
        }
    }
}
