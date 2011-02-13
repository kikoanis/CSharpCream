/*
* @(#)FirstStep.cs
*/
using System;
using Cream;

public class FirstStep
{
    [STAThread]
    public static void Main(String[] args)
    {
        // Create a constraint network
        Network net = new Network();
        // Declare variables
        IntVariable x = new IntVariable(net);
        IntVariable y = new IntVariable(net);
        // x >= 0
        x.Ge(0);
        // y >= 0
        y.Ge(0);
        // x + y == 7
        x.Add(y).Equals(7);
        // 2x + 4y == 20
        x.Multiply(2).Add(y.Multiply(4)).Equals(20);
        // Solve the problem
        Solver solver = new DefaultSolver(net);
        /*Solution solution = solver.findAll(Solution);
        int xv = solution.getIntValue(x);
        int yv = solution.getIntValue(y);
        Console.Out.WriteLine("x = " + xv + ", y = " + yv);
        */
        
        for (solver.Start(); solver.WaitNext(); solver.Resume()) {
        Solution solution = solver.Solution;
        int xv = solution.GetIntValue(x);
        int yv = solution.GetIntValue(y);
        Console.Out.WriteLine("x8 = " + xv + ", y = " + yv);
        }
        solver.Stop();
        
        
        //solver.findAll(new FirstStepHandler(x, y));
        Console.In.ReadLine();
        
    }
}

class FirstStepHandler : ISolutionHandler
{
    internal IntVariable x;
    internal IntVariable y;

    public FirstStepHandler(IntVariable x, IntVariable y)
    {
        this.x = x;
        this.y = y;
    }

    //UPGRADE_NOTE: Synchronized keyword was removed from method 'solved'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
    public virtual void Solved(Solver solver, Solution solution)
    {
        lock (this)
        {
            if (solution != null)
            {
                int xv = solution.GetIntValue(x);
                int yv = solution.GetIntValue(y);
                Console.Out.WriteLine("x = " + xv + ", y = " + yv);
            }
        }
    }

}