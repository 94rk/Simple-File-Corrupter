namespace FileCorrupter
{
    internal class Program
    {
        public static bool running = true;

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
                    Messages.DisplayHeader();
                    Console.Write("Enter the file path (can drag and drop): ");
                    string? inp = Console.ReadLine();
                    filePath = !string.IsNullOrWhiteSpace(inp) ? inp.Replace("\"", "") : filePath;

                    fileNameStartPos = filePath.LastIndexOf('\\') + 1;
                    fileNameLength = filePath.Length - fileNameStartPos;

                    if (File.Exists(filePath))
                    {
                        fileExists = true;
                        fileName = filePath.Substring(fileNameStartPos, fileNameLength);

                        Console.WriteLine("\nSelected file: " + fileName + "\n");
                    }
                }

                Messages.DisplayAllOptions();
                Console.WriteLine("\n" + "Choose an option: ");

                bool isChoiceValid = false;

                ValidationHelper.ValidateChoice(Console.ReadLine(), ref isChoiceValid, out chosenOption);

                while (!isChoiceValid)
                {
                    Console.Write("Invalid choice! Choose an option: ");
                    ValidationHelper.ValidateChoice(Console.ReadLine(), ref isChoiceValid, out chosenOption);
                }

                switch (chosenOption)
                {
                    case 1:
                        {
                            CorruptionClass.CorruptRandom(filePath, fileName);
                            break;
                        }

                    case 2:
                        {
                            CorruptionClass.CorruptNull(filePath, fileName);
                            break;
                        }

                    case 3:
                        {
                            CorruptionClass.CorruptResize(filePath, fileName);
                            break;
                        }

                    case 4:
                        {
                            DeletionClass.DeleteTriplePass(filePath, fileName);
                            break;
                        }

                    case 5:
                        {
                            DeletionClass.DeleteExtreme(filePath, fileName);
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
    }
}
