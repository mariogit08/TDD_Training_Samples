using System;
using System.Collections.Generic;
using System.Text;

namespace Sticks.Core
{
    public class Player
    {
        public Player(string name)
        {
            Name = name;
        }
        public string Name { get; }
        public int Score { get; internal set; } = 0;
    }
}
