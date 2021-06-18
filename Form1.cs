using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace WolfishIsland
{
    public partial class Form1 : Form
    {
        public static Random random = new Random();
        public static List<Bunny> bunnysList = new List<Bunny>();
        public static List<Wolf> wolfsList = new List<Wolf>();
        public static List<Wolf> newbornsWolfsList = new List<Wolf>();
        public bool trigger = true;

        public Form1()
        {
            var number = random.Next(5, 10);
            for (int i = 0; i < number; i++)
                bunnysList.Add(new Bunny(Position.GetRandom()));

            number = random.Next(3, 5);
            for (int i = 0; i < number; i++)
                wolfsList.Add(new Wolf(Position.GetRandom(), Wolf.GetRandomGender()));

            InitializeComponent();
            DrawMap();
            GoMove();
        }

        public void DrawMap()
        {            
            for (int i = 0; i < 20; i++) 
                for(int j = 0; j < 20; j++)
                {
                    var cell = new Button
                    {
                        Size = new Size(50, 50),
                        Location = new Point(i * 50, j * 50),
                        Text = Animals(i, j)
                    };
                    Controls.Add(cell);
                }
            Thread.Sleep(1000);
        }

        public string Animals(int i, int j)
        {
            i++;
            j++;
            var cell = new Position(i, j);
            var box = new StringBuilder();

            foreach (var bunny in bunnysList)
                if (bunny.position == cell)
                    box.Append("🐰 ");

            foreach (var wolf in wolfsList)
                if (wolf.position == cell)
                    if (wolf.gender == 0)
                        box.Append("🐺 ");
                    else if ((int)wolf.gender == 1)
                        box.Append("🦊 ");

            return box.ToString();
        }

        public void GoMove()
        {
            foreach (var bunny in bunnysList)
                BunnyMove(bunny);
            DrawMap();

            foreach (var wolf in wolfsList)
                WolfMove(wolf);
            DrawMap();

            foreach (var wolf in wolfsList)
                DoAction(wolf);

            Clear();
            DrawMap();

            if (trigger) GoMove();
        }

        public void BunnyMove(Bunny bunny)
        {
            bunny.position.RandomMove();
        }

        public void WolfMove(Wolf wolf)
        {
            foreach (var bunny in bunnysList)
                if (bunny.position.IsAround(wolf.position))
                {
                    wolf.position.MoveTo(bunny.position);
                    return;
                }
            if (wolf.gender == Wolf.Gender.Male)
                foreach (var anotherWolf in wolfsList)
                    if (anotherWolf.position.IsAround(wolf.position) &&
                        anotherWolf.gender == Wolf.Gender.Female)
                    {
                        wolf.position.MoveTo(anotherWolf.position);
                        return;
                    }
            wolf.position.RandomMove();
        }

        public void DoAction(Wolf wolf)
        {
            foreach (var bunny in bunnysList)
                if (bunny.position.IsTogetherWith(wolf.position) && bunny.IsAlive())
                {
                    wolf.lifePoints++;
                    bunny.Die();
                    return;
                }
            if (wolf.gender == Wolf.Gender.Male)
                foreach (var anotherWolf in wolfsList)
                    if (anotherWolf.position.IsTogetherWith(wolf.position) &&
                        anotherWolf.gender == Wolf.Gender.Female)
                        newbornsWolfsList.Add(anotherWolf.Duplicate());
            wolf.lifePoints -= 0.1;
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            trigger = false;
        }

        public void Clear()
        {
            Clear(ref bunnysList);
            Clear(ref wolfsList);
            foreach (var wolf in newbornsWolfsList)
                wolfsList.Add(wolf);
            newbornsWolfsList.Clear();
        }

        public void Clear<T>(ref List<T> animals)
            where T : IHaveLive
        {
            var deadAnimals = new List<T>();
            foreach (var animal in animals)
                if (!animal.IsAlive())
                    deadAnimals.Add(animal);
            foreach (var animal in deadAnimals)
                animals.Remove(animal);
        }
    }
}
