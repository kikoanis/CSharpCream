using System;
using Cream;
namespace SevenEleven
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var net = new Network();

            var x = new IntVariable(net, 0, 708);
            var y = new IntVariable(net, 0, 708);
            var z = new IntVariable(net, 0, 708);
            var t = new IntVariable(net, 0, 708);
            x.Add(y).Add(z).Add(t).Equals(711);
            x.Ge(y);
            y.Ge(z);
            z.Ge(t);
            x.Multiply(y).Multiply(z).Multiply(t).Equals(711000000);
            Solver solver = new DefaultSolver(net);
            for (solver.Start(); solver.WaitNext(); solver.Resume())
            {
                var solution = solver.Solution;
                Console.Out.WriteLine();
                Console.Out.WriteLine(" {0:F} + {1:F} + {2:F} + {3:F} = {4:F} ", 
                                      solution.GetIntValue(x)/100.0, solution.GetIntValue(y)/100.0, 
                                      solution.GetIntValue(z)/100.0, solution.GetIntValue(t)/100.0,7.11 );
            }
            solver.Stop();
            Console.ReadLine();
        }
    }
}
