namespace FileCorrupter
{
    internal class Program
    {
        public static Random ran = new();
        public static bool running = true;
        public const int optionAmount = 5;

        public static string? filePath;
        public static string? fileName;

        public static int chosenOption = 0;

        public static int sizeMb = 0;

        static void Main(string[] args)
        {
            bool fileExists = false;
            filePath = "none";

            int fileNameStartPos = filePath.LastIndexOf('\\') + 1;
            int fileNameLength = filePath.Length - fileNameStartPos;

            while (running)
            {
                while (!fileExists)
                {
                    Console.Write("Enter the file path (can drag and drop): ");
                    string? inp = Console.ReadLine();
                    filePath = inp?.Length > 0 ? inp : filePath;
                    filePath = filePath.Replace("\"", "");

                    fileNameStartPos = filePath.LastIndexOf('\\') + 1;
                    fileNameLength = filePath.Length - fileNameStartPos;

                    if (File.Exists(filePath)) fileExists = true;
                }

                fileName = filePath.Substring(fileNameStartPos, fileNameLength);

                Console.WriteLine("\n" + "Selected file: " + fileName + "\n");

                Messages.DisplayAllOptions();
                Console.Write("\n" + "Choose an option: ");

                bool isChoiceValid = false;

                ValidateChoice(Console.ReadLine(), ref isChoiceValid);

                while (!isChoiceValid)
                {
                    Console.Write("Invalid choice! Choose an option: ");
                    ValidateChoice(Console.ReadLine(), ref isChoiceValid);
                }

                switch (chosenOption)
                {
                    case 1:
                        {
                            Corrupt();
                            break;
                        }

                    case 2:
                        {
                            CorruptWithNulls();
                            break;
                        }

                    case 4:
                        { 
                            DeleteTriplePass();
                            break;
                        }

                    case 5:
                        {
                            DeleteExtreme();
                            break;
                        }

                    default:
                        {
                            Console.WriteLine("This feature is a work in progress, or doesn't exist \n");
                            break;
                        }
                }

                Console.WriteLine("Press any key to corrupt a new file, q to quit");
                char i = Console.ReadKey(true).KeyChar;
                Console.Clear();

                if (i == 'q' || i == 'Q') running = false;

                fileExists = false;
            }
        }

        public static void Corrupt()
        {
            byte[]? bytes;

            try { bytes = File.ReadAllBytes(filePath); }
            catch (IOException) { Console.WriteLine("An error occured, please try again"); return; }

            if (bytes != null)
            {
                long start = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                ran.NextBytes(bytes);
                File.WriteAllBytes(filePath, bytes);

                long end = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                Console.WriteLine($"\nDone corrupting {fileName} in {FormatHelper.FormatTime((int)(end - start))} \n");
            }
        }

        public static void CorruptWithNulls()
        {
            byte[]? bytes;

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

        public static void DeleteTriplePass()
        {
            byte[]? bytes;

            try { bytes = File.ReadAllBytes(filePath); }
            catch (IOException) { Console.WriteLine("An error occured, please try again"); return; }

            if (bytes != null)
            {
                long start = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                for (int i = 0; i < 3; i++)
                {
                    Array.Fill<byte>(bytes, (byte)ran.Next(0, 254));

                    File.WriteAllBytes(filePath, bytes);
                }

                Array.Fill<byte>(bytes, 0);
                File.WriteAllBytes(filePath, bytes);

                File.Delete(filePath);

                long end = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                Console.WriteLine($"\nDone destroying {fileName} in {FormatHelper.FormatTime((int)(end - start))} \n");
            }
        }

        public static void DeleteExtreme()
        {
            try
            {
                byte[] bytes = File.ReadAllBytes(filePath);

                long start = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                // perform 16 random passes
                for (int i = 0; i < 16; i++)
                {
                    ran.NextBytes(bytes);
                    File.WriteAllBytes(filePath, bytes);
                }

                // final null pass
                Array.Fill(bytes, (byte)0);
                File.WriteAllBytes(filePath, bytes);

                File.Delete(filePath);

                long end = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                Console.WriteLine($"\nDone overwriting {fileName} in {FormatHelper.FormatTime((int)(end - start))} \n");
            }
            catch (IOException)
            {
                Console.WriteLine("An error occurred, please try again");
            }
        }

        public static void ValidateChoice(string? choice, ref bool validBool)
        {
            int cache = 0;

            bool succeeded = int.TryParse(choice, out cache) && cache > 0 && cache <= optionAmount;

            validBool = succeeded;
            chosenOption = succeeded ? cache : 0;
        }
    }
}
