using System;
using System.Collections.Generic;
using System.Text;

namespace DiceGame.Models
{
    class Player
    {
        public string Name { get; private set; }
        public int Result { get; private set; }
        public Dictionary<int, bool> Selected { get; private set; }

        public Player(string name)
        {
            Name = name;
            Result = 0;

            Selected = new Dictionary<int, bool>();
            Selected.Add(1, false);
            Selected.Add(10, false);
            Selected.Add(100, false);
        }

        public void UpdateResult(int selected, int result)
        {
            Result = Result + selected * result;
            Selected[selected] = true;
        }

        public void Reset()
        {
            Result = 0;

            Selected[1] = false;
            Selected[10] = false;
            Selected[100] = false;
        }

        public bool HasSelectedEverything()
        {
            return Selected[1] && Selected[10] && Selected[100];
        }
    }
}
