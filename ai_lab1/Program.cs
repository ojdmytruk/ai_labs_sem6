using System;
using System.Collections.Generic;

namespace ai_lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            KnightSpearmanState knightsSpearman = new KnightSpearmanState();
            LinkedList<KnightSpearmanState> moves = null;
            DFS solver = new DFS();
            moves = solver.Solve(knightsSpearman);

            int n = 1;

            foreach (KnightSpearmanState state in moves)
            {
                KnightSpearmanState nextState = (KnightSpearmanState)state;

                Console.WriteLine(n.ToString() + " ");

                if (nextState.IsSolution())
                    Console.WriteLine("Solution");

                Console.WriteLine(nextState.ToString());
                n++;
            }

            Console.ReadKey();
        }
    }
}
