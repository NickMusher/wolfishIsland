using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfishIsland
{
    public class Wolf : IHaveLive
    {
        public Position position;

        public Gender gender;
        public enum Gender
        {
            Male = 0,
            Female = 1
        }

        public double lifePoints = 1;

        public Wolf(Position position, Gender gender)
        {
            this.position = position;
            this.gender = gender;
        }

        public Wolf(int x, int y, Gender gender)
        {
            position.X = x;
            position.Y = y;
            this.gender = gender;
        }

        public static Gender GetRandomGender()
        {
            int gender = Form1.random.Next(0, 2);
            return (Gender)gender;
        }

        public Wolf Duplicate()
        {
            int gender = Form1.random.Next(0, 2);
            return new Wolf(position, (Gender)gender);
        }

        public bool IsAlive()
        {
            return lifePoints > 0;
        }
    }
}
