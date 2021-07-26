using System.IO;

namespace Pixicity.Domain.Helpers
{
    public static class IOHelper
    {
        public static void CreateDirectory(string path)
        {
            bool folderExists = Directory.Exists(path);

            if (!folderExists)
                Directory.CreateDirectory(path);
        }
    }
}
