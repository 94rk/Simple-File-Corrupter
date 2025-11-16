namespace FileCorrupter
{
    internal class Program
    {
        public static Random ran = new();
        public static string? filePath;
        public static string? fileName;
        public static int chosenOption = 0;
        public static int sizeMb = 0;
        public static bool running = true;

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
                            Console.WriteLine("This feature is a work in progress, expect it soon \n");
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

                Console.WriteLine($"Done corrupting {fileName} in {end - start} ms");
            }
        }

        public static void ValidateChoice(string? choice, ref bool validBool)
        {
            int cache = 0;

            bool succeeded = int.TryParse(choice, out cache) && cache > 0 && cache <= 2;

            validBool = succeeded;
            chosenOption = succeeded ? cache : 0;
        }
    }
}