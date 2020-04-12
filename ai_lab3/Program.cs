using System;

namespace ai_lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            State problem;

            problem = new State();
            problem.DrawRoom();
            Console.WriteLine();

            SearchAnneal controller = new SearchAnneal(problem);
            controller.SimulatedAnnealing();

            Console.ReadKey(true);
        }
    }
}
