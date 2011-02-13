/*
* @(#)Golomb.cs
* See http://4c.ucc.ie/~tw/csplib/prob/prob006/index.html
* in http://csplib.org/
*/
using System;
using Cream;

public class Golomb
{
    internal static void golomb(int m)
    {
        int n = (1 << (m - 1)) - 1;
        Network net = new Network();
        IntVariable[] a = new IntVariable[m];
        a[0] = new IntVariable(net, 0);
        for (int i = 1; i < m; i++)
        {
            a[i] = new IntVariable(net, 1, n);
            a[i - 1].Lt(a[i]);
        }
        IntVariable[] d = new IntVariable[m * (m - 1) / 2];
        int k = 0;
        for (int i = 0; i < m; i++)
        {
            for (int j = i + 1; j < m; j++)
            {
                d[k++] = a[j].Subtract(a[i]);
            }
        }
        //d[0].Lt((d[m - 1]));
        new NotEquals(net, d);
        net.Objective=a[m - 1];

        Solver solver = new DefaultSolver(net, Solver.Minimize);
        Solution solution;
        for (solver.Start(); solver.WaitNext(); solver.Resume())
        {
            solution = solver.Solution;
            //estSolution = solver.BestSolution;

            Console.Out.Write("0");
                   for (int i = 1; i < m; i++)
                   {
                       Console.Out.Write("," + solution.GetIntValue(a[i]));
                   }
                   Console.Out.WriteLine();
        }
        solver.Stop();
        solution = solver.FindBest();
        Console.Out.WriteLine("===========================");
        Console.Out.Write("0");
        for (int i = 1; i < m; i++)
        {
            Console.Out.Write("," + solution.GetIntValue(a[i]));
        }
        Console.Out.WriteLine();
    }

    [STAThread]
    public static void Main(String[] args)
    {
        int m = 8;
        if (args.Length == 1)
        {
            m = Int32.Parse(args[0]);
        }
        golomb(m);
        Console.ReadLine();
    }
}