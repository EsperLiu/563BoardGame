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
            int[,] copy = new int[matrix.GetLength(0),matrix.GetLength(1)];

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
                    Console.WriteLine("Help Doc.");
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
                        fileName = DateTime.Now.ToString("MMddyyyy-HHmmss");
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
                            ConnectFourBoard newBoard =
                                loadedHistory.ReconstructBoard(loadedHistory.MoveList.Count) as ConnectFourBoard;
                            newBoard.Render();
                            Console.WriteLine(
                                ">> Warning: Loading a position will terminate the current round, unless you have saved it. ");
                            Console.WriteLine(">> Press [Enter] to continue, or [Esc] to cancel. ");
                            if (Console.ReadKey().Key == ConsoleKey.Enter)
                            {
                                Connect4Game.Instance.GameBoard = newBoard;
                                Connect4Game.Instance.GameMoveHistory = loadedHistory;
                                Connect4Game.Instance.ContinueFromMove(loadedHistory.MoveList.Count,
                                    Connect4Game.Instance
                                        .GameBoard); // set the new board to live, and continue from the last move
                                // this will also determine which player should move next
                            }
                        }
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        sr.ListTextSaves();
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
                    Console.WriteLine("     You are now in traverse mode. ");
                    Console.WriteLine("     Use /moves to see a list of past moves. ");
                    Console.WriteLine("     Use /tb and /rp to traverse the move list; ");
                    Console.WriteLine("     Use /goto + [move #] (i.e. \"/goto 5\") to retrieve a position directly;");
                    Console.WriteLine("     then use /select to continue from the chosen position.");
                    Console.WriteLine("     Press [Enter] with no input to exit traverse mode. ");
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
                            }else {
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
    }
}