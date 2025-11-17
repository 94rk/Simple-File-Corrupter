using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCorrupter
{
    internal class CorruptionClass
    {
        public static void CorruptRandom(in string filePath, string? fileName)
        {
            byte[]? bytes;
            fileName ??= "file";

            try { bytes = File.ReadAllBytes(filePath); }
            catch (IOException) { Console.WriteLine("An error occured, please try again"); return; }

            if (bytes != null)  
            {
                long start = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                Shared.random.NextBytes(bytes);
                File.WriteAllBytes(filePath, bytes);

                long end = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                Console.WriteLine($"\nDone corrupting {fileName} in {FormatHelper.FormatTime((int)(end - start))} \n");
            }
        }

        public static void CorruptNull(in string filePath, string? fileName)
        {
            byte[]? bytes;
            fileName ??= "file";

            try { bytes = File.ReadAllBytes(filePath); }
            catch (IOException) { Console.WriteLine("An error occured, please try again"); return; }

            if (bytes != null)
            {
                long start = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                Array.Fill<byte>(bytes, 0);

                File.WriteAllBytes(filePath, bytes);

                long end = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                Console.WriteLine($"\nDone corrupting {fileName} in {FormatHelper.FormatTime((int)(end - start))} \n");
            }
        }
    }
}
