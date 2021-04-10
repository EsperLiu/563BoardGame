using System;
using System.Collections.Generic;

namespace BoardGameFramework
{
    public abstract class Game
    {
        protected Game(string gameName)
        {
            GameName = gameName;
            RoundCount = 1;
            Players = new List<Player>(
            );
        }

        public string GameName { get; set; }
        public bool Finished { get; set; }
        public Board GameBoard { get; set; }
        public List<Player> Players { get; set; }
        public Player ActivePlayer { get; set; }
        public Scoreboard GameScoreboard { get; set; }


        protected int RoundCount { get; set; }
        protected int MoveCount { get; set; }

        public void Play()
        {
            // Template
            InitializeRound();
            GameBoard.Render();
            while (!Finished)
            {
                Move move = ActivePlayer.MakeMove(GameBoard);
                ExecuteMove(move);
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
        protected abstract bool ExecuteMove(Move move);
        protected abstract void HandleVictory();
        protected abstract void HandleDraw();
        protected abstract void HandleContinue();
    }
}