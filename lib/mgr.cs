using System.Reflection;
using System.Security.Cryptography;
using System.Text;

using ICSharpCode.SharpZipLib.Zip;

namespace lib
{
    public static class mgr
    {
        public static async Task<string?> store(string pwd, string filename, Stream file)
        {
            string key = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
            string zip_file_path = getFilePath(key, pwd);

            bool success = false;
            try
            {
                using (var zip_file = File.Create(zip_file_path))
                {
                    using (var zip_file_stream = new ZipOutputStream(zip_file))
                    {
                        zip_file_stream.Password = pwd;
                        await file.CopyToAsync(zip_file_stream);
                    }
                }
                success = true;
            }
            catch (Exception exp)
            {
                Console.WriteLine("Store EXCEPTION:\n{0}", exp);
            }

            if (success)
                return key;

            try
            {
                File.Delete(zip_file_path);
            }
            catch { }

            return null;
        }

        private static string getFilePath(string key, string pwd)
        {
            string filename =
                Convert.ToHexString(SHA512.HashData(Encoding.Unicode.GetBytes(key + pwd))) + ".dat";

            string file_path = Path.Combine(sm_dirPath, filename);
            return file_path;
        }

        private static string sm_dirPath =
                Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName ?? ""), "files");
    }
}
