using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Schema;

namespace BoardGameDesign
{
    public abstract class Game
    {
        public string GameName { get; set; }
        public bool Finished { get; set; }
        public Board GameBoard { get; set; }
        public List<Player> Players { get; set; }
        public Player ActivePlayer { get; set; }
        public Scoreboard GameScoreboard { get; set; }
        protected int GameCount { get; set; }
        protected int MoveCount { get; set; }


        protected Game(string gameName)
        {
            GameName = gameName;
            GameCount = 1;
            Players = new List<Player>(
            );
                
        }

        public void Play()
        {
            // Template
            InitializeRound();
            GameBoard.Render();
            while (!Finished)
            {
                string move; 
                do
                {
                    move = ActivePlayer.MakeMove();
                } while (ExecuteMove(move) == false);
                GameBoard.Render();
                if (CheckVictory())
                {
                    HandleVictory();
                    break;
                }
                MoveCount++;
                if (CheckDraw())
                {
                    HandleDraw();
                    break;
                }
                ActivePlayer = NextPlayer();
            }
            HandleContinue();
        }

        // method to switch between 2 players should be universal, since we are making a 2-player framework
        protected Player NextPlayer()
        {
            foreach (Player p in Players)
            {
                if (p != ActivePlayer)
                {
                    return p;
                }
            }
            throw new IndexOutOfRangeException("Cannot find the next player - " +
                                               "Are you sure you initiated the game with 2 players?");
        }

        public abstract void Configure();
        protected abstract void InitializeRound();
        protected abstract bool CheckVictory();
        protected abstract bool CheckDraw();
        protected abstract bool ExecuteMove(string move);
        protected abstract void HandleVictory();
        protected abstract void HandleDraw();
        protected abstract void HandleContinue();
    }

    public class Connect4Game : Game
    {

        public Connect4Game(string gameName): base(gameName)
        {
            
        }

        protected override void InitializeRound()
        {
            GameBoard = new Board(7, 6);
            Console.WriteLine($"========={ActivePlayer} vs. {NextPlayer()}:  Round #{GameCount}=========");
            Console.WriteLine();
            MoveCount = 0;
        }


        public override void Configure()
        {
            // Create the first player
            Console.WriteLine("Please enter a name for player 1: ");
            string playerName = Console.ReadLine();
            Players.Add(new HumanC4Player(playerName, '○'));

            // Select game mode (PvC or PvP)
            string mode;
            do
            {
                Console.WriteLine("Please select a game mode: ");
                Console.WriteLine("[1] Player vs. Computer(Easy)");
                Console.WriteLine("[2] Player vs. Player");
                mode = Console.ReadLine();
            } while (mode != "1" && mode != "2");
            
            if (mode == "1") // PvC mode selected
            {
                // Add an easy AI that makes random legal moves to Players list.
                Players.Add(new ComputerC4Player("Computer", '●', new C4ComputerStrategy("RANDOM")));
            }
            else if (mode == "2") // PvP mode selected
            {
                Console.WriteLine("Please enter a name for player 2: ");
                playerName = Console.ReadLine();
                Players.Add(new HumanC4Player(playerName, '●'));
            }

            string firstPlayerChoice;
            do
            {
                Console.WriteLine("Who should play first?");
                Console.WriteLine($"[1] {Players[0]}");
                Console.WriteLine($"[2] {Players[1]}");
                firstPlayerChoice = Console.ReadLine();
            } while (firstPlayerChoice != "1" && firstPlayerChoice != "2");

            if (firstPlayerChoice == "1")
            {
                ActivePlayer = Players[0];
            }
            else if (firstPlayerChoice == "2")
            {
                ActivePlayer = Players[1];
            }
            
            GameScoreboard = new Scoreboard(Players[0], Players[1]);
            GameScoreboard.DisplayScores();
            Console.WriteLine();
        }

        protected override bool ExecuteMove(string move)
        {
            int targetColumn = int.Parse(move);
            for (int y = GameBoard.Length - 1; y >=0 ; y--)
            {
                if (GameBoard.Squares[targetColumn, y].Occupant == null)
                {
                    GameBoard.Squares[targetColumn, y].Occupant = 
                        new ConnectFourPiece(ActivePlayer, ActivePlayer.Stone, GameBoard.Squares[targetColumn, y]);
                    return true;
                };
            }
            return false;
        }
        
        protected override bool CheckVictory()
        {
            foreach (Square s in GameBoard.Squares)
            {
                if (s.Occupant != null && s.Occupant.Owner == ActivePlayer)
                {
                    if (
                        FindConnected(4, s.Occupant as ConnectFourPiece, 1, 0) || // check horizontal 
                        FindConnected(4, s.Occupant as ConnectFourPiece, 0, 1) || // check vertical
                        FindConnected(4, s.Occupant as ConnectFourPiece, 1, 1) || // check diagonal 1
                        FindConnected(4, s.Occupant as ConnectFourPiece, -1, 1)) // check diagonal 2
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        protected override void HandleVictory()
        {
            Console.WriteLine($"{ActivePlayer} Won! ");
            GameScoreboard.ModifyScore(ActivePlayer, 3);
        }

        protected override bool CheckDraw()
        {
            return MoveCount == 42;
        }

        protected override void HandleDraw()
        {
            Console.WriteLine("The round ends in a draw!");
            GameScoreboard.ModifyScore(ActivePlayer, 1);
            GameScoreboard.ModifyScore(NextPlayer(), 1);
        }

        protected override void HandleContinue()
        {
            GameScoreboard.DisplayScores();
            Console.WriteLine("Press any key to continue, or [Q] to quit.");
            var input = Console.ReadKey().Key;
            Console.WriteLine();
            if (input == ConsoleKey.Q)
            {
                Console.WriteLine("Thanks for playing! ");
                Environment.Exit(0);
            }

            GameCount++;
        }

        protected bool FindConnected(int n, ConnectFourPiece piece, int dx, int dy)
        {
            // Check for connected pieces in a horizontal, vertical or diagonal line.
            // n is the number of connected pieces needed to return a true.
            // piece: the initial piece to exam
            // (dx, dy) determines which direction to look for connected pieces. 
            int counter = 0;

            for (int i = 1; i <= n; i++)
            {
                try
                {
                    Piece checkPiece = GameBoard.Squares[piece.Location.X + (dx * i), piece.Location.Y + (dy * i)].Occupant;
                    if (checkPiece != null && checkPiece.Owner == piece.Owner)
                    {
                        counter++;
                        if (counter == n - 1)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                // Possible to transverse outside the board matrix during calculation,
                // in which case we expect an IndexOutofRangeException
                catch (IndexOutOfRangeException e)
                {
                    break;
                }
                // If there is no piece in the adjacent square we should expect a NullReferenceException
                catch (NullReferenceException e)
                {
                    break;
                }
            }
            return false;
        }
    }
}