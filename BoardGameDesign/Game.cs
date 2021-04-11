using System;
using System.Collections.Generic;

namespace BoardGameFramework
{
    public abstract class Game
    {
        protected Game()
        {
            RoundCount = 1;
            Players = new List<Player>(
            );
        }

        public bool Finished { get; set; }
        public Board GameBoard { get; set; }
        public List<Player> Players { get; set; }
        public Player ActivePlayer { get; set; }
        public Scoreboard GameScoreboard { get; set; }
        public MoveHistory GameMoveHistory { get; set; }


        protected int RoundCount { get; set; }
        protected int MoveCount { get; set; }

        public void Play()
        {
            // Template
            InitializeRound();
            GameBoard.Render();
            while (!Finished)
            {
                Move move;
                do
                {
                    move = ActivePlayer.MakeMove(GameBoard);
                } while (move == null);
                
                if (GameBoard.ExecuteMove(move, ActivePlayer))
                {
                    GameMoveHistory.AppendMove(move);
                }
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
            // End of a round
            HandleContinue();
        }

        // method to switch between 2 players should be universal, since we are making a 2-player framework
        protected Player NextPlayer()
        {
            foreach (var p in Players)
                if (p != ActivePlayer)
                    return p;
            throw new IndexOutOfRangeException("Cannot find the next player - " +
                                               "Are you sure you initiated the game with 2 players?");
        }

        public abstract void Configure();
        protected abstract void InitializeRound();
        protected abstract bool CheckVictory();
        protected abstract bool CheckDraw();
        protected abstract void HandleVictory();
        protected abstract void HandleDraw();
        protected abstract void HandleContinue();
        public abstract Board GoToMove(int moveNumber);
        public abstract void ContinueFromMove(int moveNumber, Board board);
    }
}