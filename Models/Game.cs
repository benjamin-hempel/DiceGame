using System;
using System.Collections.Generic;
using System.Text;

using DiceGame.Models;

namespace DiceGame
{
    class Game
    {
        public List<Player> Players { get; private set; }
        public int CurrentPlayer { get; private set; }
        
        private Random Random { get; set; }

        public Game()
        {
            Players = new List<Player>();
            Player player1 = new Player("Jim");
            Player player2 = new Player("Jane");
            Player player3 = new Player("Joyce");
            Players.Add(player1);
            Players.Add(player2);
            Players.Add(player3);

            CurrentPlayer = 0;

            Random = new Random();
        }

        public void Toss(int selected)
        {
            int result = Random.Next(1, 6 + 1);
            Players[CurrentPlayer].UpdateResult(selected, result);
        }

        public bool NextPlayer()
        {
            CurrentPlayer++;
            if (CurrentPlayer > Players.Count - 1)
                return false;

            return true;
        }

        public Player GetCurrentPlayer()
        {
            return Players[CurrentPlayer];
        }

        public Player GetWinner()
        {
            int maxResult = 0;
            Player winner = null;

            foreach(Player player in Players)
            {
                if (player.Result > maxResult)
                {
                    maxResult = player.Result;
                    winner = player;
                }
            }

            return winner;
        }

        public void ResetGame()
        {
            CurrentPlayer = 0;

            foreach(Player player in Players) 
                player.Reset();
        }

    }
}
