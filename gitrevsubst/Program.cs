using System;

namespace gitrevsubst
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3) {
                Console.WriteLine(
                    "Usage: GITREVSUBST.EXE <GitDirectory> <InputFile> <OutputFile>");
                Environment.Exit(1);
            }

            string gitDir = args[0];
            string inputFile = args[1];
            string outputFile = args[2];

            var git = new Git(gitDir);
            string rev = git.GetShortRevId();

            if (rev == null) {
                Console.WriteLine("Error retrieving git revision!");
                Environment.Exit(2);
            }

            DateTime? date = git.GetDate(rev);

            if (date == null) {
                Console.WriteLine("Error retrieving git commit date!");
                Environment.Exit(2);
            }

            string contents = System.IO.File.ReadAllText(inputFile);

            contents = contents.Replace("$GITREV$", rev);

            bool? dirtyStatus = git.GetDirtyStatus();
            if (dirtyStatus == null) {
                Console.WriteLine("Error retrieving git dirty status!");
                Environment.Exit(2);
            }

            string dirtyString = (bool)dirtyStatus ? "-dirty" : "";
            contents = contents.Replace("$GITDIRTY$", dirtyString);

            contents = contents.Replace("$GITDATE$",
                ((DateTime) date).ToString("yyyyMMdd"));

            System.IO.File.WriteAllText(outputFile, contents);
        }
    }
}