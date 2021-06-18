using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfishIsland
{
    public class Bunny : IHaveLive
    {
        public Position position;
        public static int dublicateChance = 20;
        private bool isAlive = true;
                
        public Bunny(Position position)
        {
            this.position = position;
        }

        public Bunny(int x, int y)
        {
            position = new Position(x, y);
        }

        public void Duplicate()
        {
            if (Form1.random.Next(1, 101) < dublicateChance)
                Form1.bunnysList.Add(new Bunny(position));
        }

        public bool IsAlive()
        {
            return isAlive;
        }

        public void Die()
        {
            isAlive = false;
        }
    }
}
