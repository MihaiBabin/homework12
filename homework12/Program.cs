namespace homework12
{

    class Program
    {
        static async Task Main(string[] args)
        {
            string directoryPath = "D:\\exercitii_c#\\test";

            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine("Directorul nu există.");
                return;
            }
            string[] files = Directory.GetFiles(directoryPath, "*.txt");
            var readTasks = files.Select(async file =>
            {
                int lineCount = await ReadLinesAsync(file);
                return (FileName: Path.GetFileName(file), LineCount: lineCount);
            });
            var results = await Task.WhenAll(readTasks);
            foreach (var result in results)
            {
                Console.WriteLine($"{result.FileName}: {result.LineCount} linii");
            }
        }
        static async Task<int> ReadLinesAsync(string filePath)
        {
            int count = 0;

            using var reader = new StreamReader(filePath);
            while (await reader.ReadLineAsync() is not null)
            {
                count++;
            }
            return count;
        }
    }

}
