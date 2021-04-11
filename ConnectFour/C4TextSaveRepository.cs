using System;
using System.Collections.Generic;
using System.IO;
using BoardGameFramework;

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
            var Files = d.GetFiles("*.txt"); 

            ConnectFourMoveHistory moveHistory = new ConnectFourMoveHistory();

            bool found = false;
            foreach (var file in Files)
            {
                if (file.Name.ToUpper() == fileName.ToUpper())
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
            string fullPath = Path.Combine(FilePath, $"C4SAVE_{fileName}.txt");
            using (var outputFile = new StreamWriter(fullPath))
            {
                foreach (var line in lines){
                    outputFile.WriteLine(line);}
            }

            Console.WriteLine($"Position saved to {fullPath}");
            return true;
        }

        public void ListTextSaves()
        {
            Console.WriteLine(">> Found the following save files: ");
            Console.WriteLine(">> Use \"/load filename\" to load a saved position. ");
            var d = new DirectoryInfo(FilePath);
            var Files = d.GetFiles("*.txt");
            var str = "";
            foreach (var file in Files)
            {
                if (file.Name.StartsWith("C4SAVE_"))
                {
                    Console.WriteLine(file.Name);
                }
            }
        }
    }
}