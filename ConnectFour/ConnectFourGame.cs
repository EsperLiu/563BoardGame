using System;
using System.Collections.Generic;
using BoardGameFramework;

namespace ConnectFour
{
    public class Connect4Game : Game
    {
        private static readonly Connect4Game instance = new Connect4Game();
        static Connect4Game()
        {
        }
        private Connect4Game()
        {
        }
        public static Connect4Game Instance
        {
            get => instance;
        }


        protected override void InitializeRound()
        {
            GameBoard = new ConnectFourBoard(7, 6);
            GameMoveHistory = new ConnectFourMoveHistory();
            Console.WriteLine($"========={Players[0]} vs. {Players[1]}:  Round #{RoundCount}=========");
            Console.WriteLine();
            MoveCount = 0;
        }

        public override void Configure()
        {
            // Create the first player
            string playerName = null; 
            while (playerName == null)
            {
                Console.WriteLine("Please enter a name for player 1: ");
                playerName = Utils.TakeStringInput(true);
            }
            Players.Add(new HumanC4Player(playerName));

            // Select game mode (PvC or PvP)
            string mode;
            do
            {
                Console.WriteLine("Please select a game mode: ");
                Console.WriteLine("[1] Player vs. Computer");
                Console.WriteLine("[2] Player vs. Player");
                mode = Console.ReadLine();
            } while (mode != "1" && mode != "2");

            if (mode == "1") // PvC mode selected
            {
                string aiSelect;
                do
                {
                    Console.WriteLine("Please select an AI to play against: ");
                    Console.WriteLine("[1] Simple random algorithm");
                    Console.WriteLine("[2] Simple greedy algorithm");
                    aiSelect = Console.ReadLine();
                } while (mode != "1" && mode != "2");

                ComputerC4Player opponent = new ComputerC4Player("Computer");
                if (aiSelect == "1")
                {
                    opponent.Strategy = new C4RandomStrategy();
                } else if (aiSelect == "2")
                {
                    opponent.Strategy = new C4GreedyStrategy();
                }
                Players.Add(opponent);
            }

            else if (mode == "2") // PvP mode selected
            {
                Console.WriteLine("Please enter a name for player 2: ");
                playerName = Console.ReadLine();
                Players.Add(new HumanC4Player(playerName));
            }

            string firstPlayerChoice;
            do
            {
                Console.WriteLine("Who should take yellow pieces and play first?");
                Console.WriteLine($"[1] {Players[0].Name}");
                Console.WriteLine($"[2] {Players[1].Name}");
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
            (ActivePlayer as ConnectFourPlayer).Color = "Yellow" ;
            (NextPlayer() as ConnectFourPlayer).Color = "Red";

            GameScoreboard = new Scoreboard(Players[0], Players[1]);
            GameScoreboard.DisplayScores();
            Console.WriteLine();
        }

        protected override bool CheckVictory()
        {
            List<(int,int)> directions = new List<(int, int)>
            {
                (1,0), (0,1), (1,1), (-1,1)
            };

            foreach ((int,int) direction in directions)
            {
                if (Utils.FindConnected(
                    (GameBoard as ConnectFourBoard).MatrixFormat(), 
                    ActivePlayer.Id, direction.Item1, direction.Item2) == 4)
                {
                    return true;
                }
            }
            return false;
        }

        protected override void HandleVictory()
        {
            Console.WriteLine($"{ActivePlayer} Won! ");
            GameScoreboard.ModifyScore(ActivePlayer, 3);
            ActivePlayer = NextPlayer();
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
            Console.WriteLine("Press any key to continue, or [Esc] to quit.");
            var input = Console.ReadKey().Key;
            Console.WriteLine();
            if (input == ConsoleKey.Escape)
            {
                Console.WriteLine("Thanks for playing! ");
                Environment.Exit(0);
            }

            RoundCount++;
        }

        public override Board GoToMove(int moveNumber)
        {
            ConnectFourBoard newBoard = (GameMoveHistory.ReconstructBoard(moveNumber)) as ConnectFourBoard;
            return newBoard;

            //Console.WriteLine("[Y] Continue from this position");
            //Console.WriteLine("[Enter] Cancel and return to the latest position");
            //string choice = Console.ReadLine();
            //if (choice.ToUpper() == "Y")
            //{
            //    ContinueFromMove(moveNumber, newBoard);
            //}
            //GameBoard.Render();
        }

        public override void ContinueFromMove(int moveNumber, Board board)
        {
            GameBoard = board;
            // Set ActivePlayer correctly in regard to move order.
            ActivePlayer = Utils.GetPlayerById(Players, moveNumber % 2 == 0 ? 1 : 2);
            // Remove all subsequent moves in the list.
            GameMoveHistory.MoveList = GameMoveHistory.MoveList.GetRange(0, moveNumber);
            Console.WriteLine($"Reloaded from move #{moveNumber}. ");
        }

    }
}