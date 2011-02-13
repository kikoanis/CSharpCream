/*
* @(#)Magic3.cs
* Magic squares of 3+17
*/
using System;
using Cream;

public class Magic3
{
    [STAThread]
    public static void Main(String[] args)
    {
        Network net = new Network();
        int n = 3;
        int sum = n * (n * n + 1) / 2;
        IntVariable[][] v = new IntVariable[n][];
        for (int i = 0; i < n; i++)
        {
            v[i] = new IntVariable[n];
        }
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                v[i][j] = new IntVariable(net, 1, n * n);
        IntVariable[] u = new IntVariable[] { v[0][0], v[0][1], v[0][2], v[1][0], v[1][1], v[1][2], v[2][0], v[2][1], v[2][2] };
        new NotEquals(net, u);
        for (int i = 0; i < n; i++)
            v[i][0].Add(v[i][1]).Add(v[i][2]).Equals(sum);
        for (int j = 0; j < n; j++)
            v[0][j].Add(v[1][j]).Add(v[2][j]).Equals(sum);
        v[0][0].Add(v[1][1]).Add(v[2][2]).Equals(sum);
        v[0][2].Add(v[1][1]).Add(v[2][0]).Equals(sum);
        Solver solver = new DefaultSolver(net);
        for (solver.Start(); solver.WaitNext(); solver.Resume())
        {
            Solution solution = solver.Solution;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    Console.Out.Write(solution.GetIntValue(v[i][j]) + " ");
                Console.Out.WriteLine();
            }
            Console.Out.WriteLine();
        }
        solver.Stop();
        Console.ReadLine();
    }
}