﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ai_lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            SolveProblem solveProblem = new SolveProblem();
            solveProblem.RunAndReport();
            Console.ReadKey();
        }
    }
}
