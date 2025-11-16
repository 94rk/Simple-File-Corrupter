namespace FileCorrupter
{
    internal class Program
    {
        public static string? filePath;
        public static int chosenOption = 0;
        public static int sizeMb = 0;

        static void Main(string[] args)
        {
            bool fileExists = false;
            filePath = Environment.ProcessPath ?? "none";

            int fileNameStartPos = filePath.LastIndexOf('\\') + 1;
            int fileNameLength = filePath.Length - fileNameStartPos;

            while (true)
            {
                while (!fileExists)
                {
                    Console.Write("Enter the file path (can drag and drop): ");
                    string? inp = Console.ReadLine();
                    filePath = inp.Length > 0 ? inp : filePath;
                    filePath = filePath.Replace("\"", "");

                    fileNameStartPos = filePath.LastIndexOf('\\') + 1;
                    fileNameLength = filePath.Length - fileNameStartPos;

                    if (File.Exists(filePath)) fileExists = true;
                }

                Console.WriteLine("\n" + "Selected file: " + filePath.Substring(fileNameStartPos, fileNameLength) + "\n");

                Messages.DisplayAllOptions();
                Console.Write("\n" + "Choose an option: ");

                bool isChoiceValid = false;

                ValidateChoice(Console.ReadLine(), ref isChoiceValid);

                while (!isChoiceValid)
                {
                    Console.Write("Invalid choice! Choose an option: ");
                    ValidateChoice(Console.ReadLine(), ref isChoiceValid);
                }

                string opt = Messages.options[chosenOption - 1];
                Console.WriteLine("\n" + "You chose: " + opt.Substring(3, opt.Length - 3) + "\n");

                Corrupt();
                fileExists = false;
            }
        }

        public static void Corrupt()
        {
            byte[]? bytes;

            try { bytes = File.ReadAllBytes(filePath); }
            catch (IOException) { Console.WriteLine($"File must be smaller than 2.14 GB!"); return; }

            if (bytes != null)
            {
                Console.Write("File size is " + (float)bytes.Length / 1_000 + " KB, ");
                Console.WriteLine("wow my shit WORKS AYYYY \n");
            }
        }

        public static void ValidateChoice(string? choice, ref bool validBool)
        {
            int cache = 0;

            bool succeeded = int.TryParse(choice, out cache) && cache > 0 && cache <= 2;

            validBool = succeeded;
            chosenOption = succeeded ? cache : 0;

            return;
        }
    }
}