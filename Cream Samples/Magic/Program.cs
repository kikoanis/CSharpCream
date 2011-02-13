/*
* @(#)Magic.CS
* Magic squares
* There is only one magic square of 3*3.
* There are 880 magic squares of 4*4.
*/
using System;
using Cream;

public class Magic
{
    public static void magic(int n)
    {
        Network net = new Network();
        IntVariable[][] square = new IntVariable[n][];
        for (int i = 0; i < n; i++)
        {
            square[i] = new IntVariable[n];
        }

        // All squares have different numbers 1 .. n*n
        IntVariable[] v = new IntVariable[n * n];
        int k = 0;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                square[i][j] = new IntVariable(net, 1, n * n);
                v[k++] = square[i][j];
            }
        }
        new NotEquals(net, v);

        // Sum of each row is n*(n*n+1)/2
        IntVariable s;
        int sum = n * (n * n + 1) / 2;
        for (int i = 0; i < n; i++)
        {
            s = square[i][0];
            for (int j = 1; j < n; j++)
                s = s.Add(square[i][j]);
            s.Equals(sum);
        }

        // Sum of each column is n*(n*n+1)/2
        for (int j = 0; j < n; j++)
        {
            s = square[0][j];
            for (int i = 1; i < n; i++)
                s = s.Add(square[i][j]);
            s.Equals(sum);
        }

        // Sum of down-diagonal is n*(n*n+1)/2
        s = square[0][0];
        for (int i = 1; i < n; i++)
            s = s.Add(square[i][i]);
        s.Equals(sum);

        // Sum of up-diagonal is n*(n*n+1)/2
        s = square[0][n - 1];
        for (int i = 1; i < n; i++)
            s = s.Add(square[i][n - i - 1]);
        s.Equals(sum);

        // Left-upper corner is minimum
        square[0][0].Lt(square[0][n - 1]);
        square[0][0].Lt(square[n - 1][0]);
        square[0][0].Lt(square[n - 1][n - 1]);

        // Upper-right is less than lower-left
        square[0][n - 1].Lt(square[n - 1][0]);

        Console.Out.WriteLine("Start");
        long time0 = (DateTime.Now.Ticks - 621355968000000000) / 10000;
        int count = 0;
        bool output = true;
        Solver solver = new DefaultSolver(net);
        for (solver.Start(); solver.WaitNext(); solver.Resume())
        {
            if (output)
            {
                Solution solution = solver.Solution;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        Console.Out.Write(solution.GetIntValue(square[i][j]) + " ");
                    }
                    Console.Out.WriteLine();
                }
                Console.Out.WriteLine();
            }
            count++;
        }
        solver.Stop();
        int time = (int)(((DateTime.Now.Ticks - 621355968000000000) / 10000 - time0) / 1000);
        Console.Out.WriteLine(count + " solutions found in " + time + " seconds");
    }

    [STAThread]
    public static void Main(String[] args)
    {
        if (args.Length == 1)
        {
            magic(Int32.Parse(args[0]));
        }
        else
        {
            magic(4);
        }
        Console.ReadLine();
    }
}