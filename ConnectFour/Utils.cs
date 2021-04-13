using BoardGameFramework;
using System;
using System.Collections.Generic;

namespace ConnectFour
{
    public static class Utils
    {
        public static ConnectFourPlayer GetPlayerById(List<Player> players, int id)
        {
            foreach (ConnectFourPlayer player in players)
            {
                if (player.Id == id)
                {
                    return player;
                }
            }
            return null;
        }
        public static int FindConnected(int[,] matrix, int id, int dx, int dy)
        {
            int longest = 0;

            for (int x = 0; x < matrix.GetLength(0); x++)
            {
                for (int y = 0; y < matrix.GetLength(1); y++)
                {
                    if (matrix[x, y] == id)
                    {
                        int counter = 1;
                        bool hasNext = true;
                        while (hasNext)
                        {
                            try
                            {
                                int xNew = x + dx * counter;
                                int yNew = y + dy * counter;
                                if (matrix[xNew, yNew] == id)
                                {
                                    counter++;
                                }
                                else
                                {
                                    hasNext = false;
                                }
                            }
                            catch (IndexOutOfRangeException e)
                            {
                                hasNext = false;
                            }
                        }
                        if (counter > longest)
                        {
                            longest = counter;
                        }
                    }
                }
            }
            return longest;
        }

        public static int[,] CopyBoardMatrix(int[,] matrix)
        {
            int[,] copy = new int[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    copy[i, j] = matrix[i, j];
                }
            }

            return copy;
        }

        public static string TakeStringInput(bool allowCommand)
        {
            string str = Console.ReadLine();
            string s = str.ToUpper();
            if (s.StartsWith("/"))
            {
                if (s == "/" || s == "/HELP")
                {
                    Help();
                    return null;
                }
                else if (s.StartsWith("/SAVE"))
                {
                    C4TextSaveRepository sr = new C4TextSaveRepository();
                    string fileName = "";
                    try
                    {
                        fileName = s.Split(" ")[1];
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        fileName = DateTime.Now.ToString("MMdd-HHmm");
                    }
                    sr.Save(fileName);
                }
                else if (s.StartsWith("/LOAD"))
                {
                    C4TextSaveRepository sr = new C4TextSaveRepository();
                    try
                    {
                        string fileName = s.Split(" ")[1];
                        MoveHistory loadedHistory = sr.Load(fileName);
                        if (loadedHistory != null)
                        {
                            ConnectFourBoard newBoard = loadedHistory.ReconstructBoard(loadedHistory.MoveList.Count) as ConnectFourBoard;
                            newBoard.Render();
                            Console.WriteLine(">> Warning: Loading a position will terminate the current round. ");
                            Console.WriteLine(">> You may want to save your position with /save <filename> before continuing. ");
                            Console.WriteLine(">> Press [Enter] to continue, or [Esc] to cancel. ");
                            if (Console.ReadKey().Key == ConsoleKey.Enter)
                            {
                                Connect4Game.Instance.GameBoard = newBoard;
                                Connect4Game.Instance.GameMoveHistory = loadedHistory;
                                Connect4Game.Instance.ContinueFromMove(loadedHistory.MoveList.Count,
                                    Connect4Game.Instance.GameBoard); // set the new board to live, and continue with the correct ActivePlayer.
                            }
                        }
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Console.WriteLine(">> Found the following save files: ");
                        foreach (var fileName in sr.FetchSaves())
                        {
                            Console.WriteLine(fileName);
                        }
                        Console.WriteLine(">> (End of list) ");
                    }
                }

                // Traverse the move list...
                else if (s.StartsWith("/TRAVERSE"))
                {
                    if (Connect4Game.Instance.GameMoveHistory == null || Connect4Game.Instance.GameMoveHistory.MoveList.Count == 0)
                    {
                        Console.WriteLine(">> No move history to traverse. Use this feature once you've made some moves!");
                        return null;
                    }
                    Console.WriteLine("********************************************************************************");
                    Console.WriteLine(">> You are now in traverse mode. ");
                    Console.WriteLine(">> Use /moves to see a list of past moves. ");
                    Console.WriteLine(">> Use /tb and /rp to traverse the move list; ");
                    Console.WriteLine(">> Use /goto <#> (i.e. \"/goto 5\") to go to a position directly;");
                    Console.WriteLine(">> use /select to continue from the chosen position.");
                    Console.WriteLine(">> Press [Enter] with no input to exit traverse mode. ");
                    Console.WriteLine("********************************************************************************");
                    int current = Connect4Game.Instance.GameMoveHistory.MoveList.Count;
                    bool quit = false;
                    while (!quit)
                    {
                        string input = Console.ReadLine().ToUpper();
                        if (input.StartsWith("/TAKEBACK") || input.StartsWith("/TB"))
                        {
                            if (current > 0)
                            {
                                current--;
                            }
                            else
                            {
                                Console.WriteLine("No more move to take back.");
                            }
                            Connect4Game.Instance.GoToMove(current).Render();
                        }
                        else if (input.StartsWith("/REPLAY") || input.StartsWith("/RP"))
                        {
                            if (current < Connect4Game.Instance.GameMoveHistory.MoveList.Count)
                            {
                                current++;
                            }
                            else
                            {
                                Console.WriteLine("No more move to replay.");
                            }
                            Connect4Game.Instance.GoToMove(current).Render();
                        }
                        else if (input.StartsWith("/SELECT"))
                        {
                            quit = true;
                            Connect4Game.Instance.ContinueFromMove(current, Connect4Game.Instance.GoToMove(current));
                            Connect4Game.Instance.GameBoard.Render();
                        }
                        else if (input.StartsWith("/MOVES"))
                        {
                            Console.WriteLine(Connect4Game.Instance.GameMoveHistory);
                        }
                        else if (input.StartsWith("/GOTO "))
                        {
                            if (int.TryParse(input.Substring(6), out current))
                            {
                                Connect4Game.Instance.GoToMove(current).Render();
                            };
                        }
                        else if (input == "")
                        {
                            // Re-renders the board and quit.
                            Connect4Game.Instance.GameBoard.Render();
                            quit = true;
                        }
                    }
                }
                return null;
            }
            return str;
        }

        public static void Help()
        {
            Console.WriteLine("******************************User's Guide******************************");
            Console.WriteLine(">>   Connect-Four rules: https://gamezrules.com/connect-4-rules/");
            Console.WriteLine();
            Console.WriteLine(">>   Available Commands: ");
            Console.WriteLine(">>   #: Puts a piece on column #. Move is illegal if all squares in that column is occupied. ");
            Console.WriteLine(">>   /help: Displays this document.");
            Console.WriteLine(">>   /save: Saves the current game position. Current datetime will be used as file name unless you provide one, i.e. /save my_save. ");
            Console.WriteLine(">>   /load: Displays a list of save files available to load.");
            Console.WriteLine(">>   /load <filename.txt>: Loads the save file specified. The current game state will be lost. ");
            Console.WriteLine();
            Console.WriteLine(">>   /traverse: Starts traversing the move history of the current game. Commands available in this mode are: ");
            Console.WriteLine(">>           /moves: display move history. ");
            Console.WriteLine(">>           /tb: Takes back one move. ");
            Console.WriteLine(">>           /rp: Replays one move." );
            Console.WriteLine(">>           /goto <#> Go directly to move #.");
            Console.WriteLine(">>           /select: Selects the current position and continue from there. Once you continue from a previous position, move history make beyond that point will be lost. ");
            Console.WriteLine(">>***********************************************************************");
        }

    }
}