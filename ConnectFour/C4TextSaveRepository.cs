using BoardGameFramework;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConnectFour
{
    public class C4TextSaveRepository : ISaveRepository

    {
        public string FilePath { get; set; } = Directory.GetCurrentDirectory();

        public MoveHistory Load(string fileName)
        {
            if (Connect4Game.Instance.GameMoveHistory == null)
            {
                Console.WriteLine(">> Please configure the game before loading a position!");
                return null;
            }

            var d = new DirectoryInfo(FilePath);
            var Files = d.GetFiles("*.c4save");

            ConnectFourMoveHistory moveHistory = new ConnectFourMoveHistory();

            bool found = false;
            foreach (var file in Files)
            {
                if (file.Name.ToUpper() == fileName.ToUpper() || (file.Name).ToUpper() == (fileName+".c4save").ToUpper())
                {
                    found = true;
                    using StreamReader sr = new StreamReader(file.Name);
                    string m;
                    while ((m = sr.ReadLine()) != null)
                    {
                        moveHistory.AppendMove(m);
                    }
                }
            }

            if (!found)
            {
                Console.WriteLine($">> File {fileName} not found.");
                return null;
            }
            return moveHistory;
        }

        public bool Save(string fileName)
        {
            if (Connect4Game.Instance.GameMoveHistory == null)
            {
                Console.WriteLine(">> Please configure the game before saving a position!");
                return false;
            }

            string[] lines = Connect4Game.Instance.GameMoveHistory.ToArray();
            string fullPath = Path.Combine(FilePath, $"{fileName}.c4save");
            using (var outputFile = new StreamWriter(fullPath))
            {
                foreach (var line in lines)
                {
                    outputFile.WriteLine(line);
                }
            }

            Console.WriteLine($"Position saved to {fullPath}");
            return true;
        }

        public string[] FetchSaves()
        {
            var d = new DirectoryInfo(FilePath);
            var Files = d.GetFiles("*.c4save");
            var saves = new List<string>();
            foreach (var file in Files)
            {
                saves.Add(file.Name);
            }
            return saves.ToArray();
        }
    }
}