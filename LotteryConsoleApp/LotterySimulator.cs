using LotteryConsoleApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryConsoleApp
{
    internal class LotterySimulator
    {
        private readonly AppDbContext _dbContext;

        public LotterySimulator(AppDbContext dbContext)
        {
            _dbContext = dbContext;

            //get history of draws from db
            OldDraws = _dbContext.Draws.ToList();
        }
        public List<Draw> OldDraws { get; set; }


        public Draw AddDraw(DateTime drawDate, List<int> numbers)
        {
            string Nums = string.Join(", ", numbers);
            int id = OldDraws.Count + 1;
            OldDraws.Add(new Draw(id, drawDate, Nums));

            //insert the generated Draw into db
            _dbContext.Draws.Add(new Draw(drawDate, Nums));
            _dbContext.SaveChanges();

            return OldDraws.Last();
        }

        private void PrintOldDraws()
        {
            //retreive all drwas from db
            
            foreach (var draw in OldDraws)
            {
                Console.WriteLine($"\nDraw {draw.Id} - {draw.DrawDateTime}");
                Console.WriteLine(draw.Numbers);
            }
        }

        private void SimulateDraw()
        {
            var random = new Random();
            var numbers = new List<int>();
            for (var i = 0; i < 5; i++)
            {
                int generatedNo = random.Next(1, 50);
                while (numbers.Contains(generatedNo))
                {
                    generatedNo = random.Next(1, 50);
                }
                numbers.Add(generatedNo);
            }
            var lastDraw = AddDraw(DateTime.Now, numbers);
            Console.WriteLine($"\nDraw {lastDraw.Id} - {lastDraw.DrawDateTime} simulated successfuly");
            Console.WriteLine(string.Join(", ", lastDraw.Numbers) + "\n");
        }

        public void menu()
        {
            Console.WriteLine("Lottery Simulator was initiated...\n");

            while (true)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("r ~ (Run) Generate a new draw");
                Console.WriteLine("h ~ (History) Display the history of draws");
                Console.WriteLine("q ~ Quit \n");
                var key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.Q:
                        return;
                    case ConsoleKey.H:
                        // Display history
                        Console.WriteLine("\nDisplaying history...\n");
                        PrintOldDraws();
                        break;
                    case ConsoleKey.R:
                        // Simulate draw
                        Console.WriteLine("\nSimulating draw...\n");
                        SimulateDraw();
                        break;
                    default:
                        // Handle unrecognized key
                        Console.WriteLine("\nUnrecognized key.\n");
                        break;
                }

            }
        }
    }
}
