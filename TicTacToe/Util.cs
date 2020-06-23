using System;
using System.Collections.Generic;
using System.Text;
using static TicTacToe.Util;

namespace TicTacToe
{
    public class Util
    {
        public static readonly List<List<int>> WinningCriterions = new List<List<int>>
        {
            new List<int>{ 11, 12, 13},
            new List<int>{ 21, 22, 23},
            new List<int>{ 31, 32, 33},

            new List<int>{ 11, 21, 31},
            new List<int>{ 12, 22, 32},
            new List<int>{ 13, 23, 33},

            new List<int>{ 11, 22, 33},
            new List<int>{ 13, 22, 31}
        };

        public enum CurrentPlayer
        {
            Player1 = 0,
            Player2 = 1
        }

        public enum WinStatus
        {
            Playing,
            Win,
            Loose,
            Draw
        }
    }
    public class Player
    {
        public bool HasWon { get; set; }
        public List<int> Marks { get; set; }
    }
    /*
    * ----------------------
    * |  11  |  12  |  13  |
    * ----------------------
    * |  21  |  22  |  23  |
    * ----------------------
    * |  31  |  32  |  33  |
    * ----------------------
    * 
    */
    
}
