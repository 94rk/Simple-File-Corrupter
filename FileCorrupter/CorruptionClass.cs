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

                Console.WriteLine("\nStarting...");
                Shared.random.NextBytes(bytes);

                Console.WriteLine("Writing...");
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

                Console.WriteLine("\nStarting...");
                Array.Fill<byte>(bytes, 0);

                Console.WriteLine("Writing...");
                File.WriteAllBytes(filePath, bytes);

                long end = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                Console.WriteLine($"\nDone corrupting {fileName} in {FormatHelper.FormatTime((int)(end - start))} \n");
            }
        }

        public static void CorruptResize(in string filePath, string? fileName)
        {
            long newSizeMB = 0;
            int remainingMB = 0;

            byte[]? blockArray;
            byte[]? remainingArray;
            fileName ??= "file";

            Console.Write("\nEnter the new file size in MB: ");
            ValidationHelper.ValidateNumber(Console.ReadLine() ?? "0", out newSizeMB);

            newSizeMB = Math.Max(newSizeMB, 0);
            remainingMB = (int)(newSizeMB % 10);

            long start = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            blockArray = new byte[1024L*1024 * 10];

            File.WriteAllText(filePath, "");

            Console.WriteLine("\nStarting...\n");

            for (long i = 0; i < newSizeMB / 10; i++)
            {
                Array.Fill<byte>(blockArray, (byte)Shared.random.Next(1, 256));
                File.AppendAllBytes(filePath, blockArray);

                Console.Write($"\rProgress: {((i+1)/(float)newSizeMB * 1000):0.0}%   ");
            }

            remainingArray = new byte[1024 * 1024 * remainingMB];
            Shared.random.NextBytes(remainingArray);

            File.AppendAllBytes(filePath, remainingArray);
            Console.Write("\rProgress: 100,0%   ");

            Console.WriteLine("\n\n" + "Cleaning up...");

            blockArray = null; remainingArray = null; GC.Collect();

            long end = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            Console.WriteLine($"\nDone corrupting {fileName} in {FormatHelper.FormatTime((int)(end - start))} \n");
        }
    }
}
