using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCorrupter
{
    internal class DeletionClass
    {
        public static void DeleteTriplePass(in string filePath, string? fileName)
        {
            byte[]? bytes;
            fileName ??= "file";

            try { bytes = File.ReadAllBytes(filePath); }
            catch (IOException) { Console.WriteLine("An error occured, please try again"); return; }

            if (bytes != null)
            {
                long start = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                for (int i = 0; i < 3; i++)
                {
                    Array.Fill<byte>(bytes, (byte)Shared.random.Next(0, 254));

                    File.WriteAllBytes(filePath, bytes);
                }

                Console.WriteLine("\nStarting...");
                Array.Fill<byte>(bytes, 0);

                Console.WriteLine("Writing...");
                File.WriteAllBytes(filePath, bytes);

                Console.WriteLine("Deleting...");
                File.Delete(filePath);

                long end = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                Console.WriteLine($"\nDone deleting {fileName} in {FormatHelper.FormatTime((int)(end - start))} \n");
            }
        }

        public static void DeleteExtreme(in string filePath, string? fileName)
        {
            try
            {
                byte[] bytes = File.ReadAllBytes(filePath);
                fileName ??= "file";

                long start = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                Console.WriteLine("\nStarting... \n");

                // perform random passes
                for (int i = 0; i < 6; i++)
                {
                    Shared.random.NextBytes(bytes);
                    Console.WriteLine($"Writing iteration {i + 1}...");
                    File.WriteAllBytes(filePath, bytes);
                }

                // final null pass
                Console.WriteLine("\nDeleting...");
                Array.Fill(bytes, (byte)0);
                File.WriteAllBytes(filePath, bytes);

                File.Delete(filePath);

                long end = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                Console.WriteLine($"\nDone deleting {fileName} in {FormatHelper.FormatTime((int)(end - start))} \n");
            }
            catch (IOException)
            {
                Console.WriteLine("An error occurred, please try again");
            }
        }
    }
}
