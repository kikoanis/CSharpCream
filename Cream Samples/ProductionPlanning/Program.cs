/*
* @(#)PP.java
*/
using System;
using Cream;

namespace ProductionPlanning
{
    /// <summary> Production planning.
    /// 
    /// </summary>
    /// <author>  Naoyuki Tamura (tamura@kobe-u.ac.jp)
    /// </author>
    public class PP
    {
        internal static void pp()
        {
            Network net = new Network();
            // number of materials
            int m = 3;
            // limit of each material
            int[] limit = new int[] { 1650, 1400, 1800 };
            // number of products
            int n = 2;
            // profit of each product
            int[] p = new int[] { 5, 4 };
            // amount of materials required to make each product 
            int[][] a = new int[][] { new int[] { 15, 10, 9 }, new int[] { 11, 14, 20 } };

            // initialize variables for products
            IntVariable[] x = new IntVariable[n];
            for (int j = 0; j < n; j++)
            {
                x[j] = new IntVariable(net);
                x[j].Ge(0);
            }
            // generate constraits of limiting materials
            for (int i = 0; i < m; i++)
            {
                IntVariable sum = new IntVariable(net, 0);
                for (int j = 0; j < n; j++)
                {
                    sum = sum.Add(x[j].Multiply(a[j][i]));
                }
                sum.Le(limit[i]);
            }
            // total profit
            IntVariable profit = new IntVariable(net, 0);
            for (int j = 0; j < n; j++)
            {
                profit = profit.Add(x[j].Multiply(p[j]));
            }
            // maximize the total profit
            net.Objective = profit;
            // iteratively find a better solution until the optimal solution is found 
            Solver solver = new DefaultSolver(net, Solver.Maximize | Solver.Better);
            for (solver.Start(); solver.WaitNext(); solver.Resume())
            {
                Solution solution = solver.Solution;
                Console.WriteLine(solver.GetCount());
                Console.Out.WriteLine("Profit = " + solution.GetIntValue(profit));
                for (int j = 0; j < n; j++)
                {
                    Console.Out.WriteLine("x[" + j + "]=" + solution.GetIntValue(x[j]));
                }
                Console.Out.WriteLine();
            }
           
            solver.Stop();
            Console.ReadLine();
        }

        [STAThread]
        public static void Main(String[] args)
        {
            pp();
        }
    }
}