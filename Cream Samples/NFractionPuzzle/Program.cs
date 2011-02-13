using System;
using Cream;

namespace NFractionPuzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            Network net = new Network();

            IntVariable[] A = new IntVariable[6];
            for (int j=0; j< 6; j++)
            {
                IntDomain d = new IntDomain(1, 9);
                A[j] = new IntVariable(net);
                A[j].Domain = d;
                Trail trail = new Trail();
            }
            ((A[4].Multiply(10).Add(A[5])).Multiply(A[0])).Add(
            ((A[1].Multiply(10).Add(A[2])).Multiply(A[3]))).Equals(
                (A[1].Multiply(10).Add(A[2])).Multiply(A[4].Multiply(10).Add(A[5])));
            new NotEquals(net, A);
            Solver solver = new DefaultSolver(net);
            int i = 0;
            for (solver.Start(); solver.WaitNext(); solver.Resume())
            {
                Solution solution = solver.Solution;
                Console.Out.WriteLine();
                Console.Out.WriteLine(solution.GetIntValue(A[0]) + "    " + solution.GetIntValue(A[3]));
                Console.Out.WriteLine("-- + -- = 1");
                Console.Out.WriteLine(solution.GetIntValue(A[1])+""+solution.GetIntValue(A[2])+"   "+
                                      solution.GetIntValue(A[4]) + solution.GetIntValue(A[5]));
                Console.Out.WriteLine("=========");
                i++;
            }
            Console.Out.WriteLine("There are {0} solutions",i);

            solver.Stop();
            Console.In.ReadLine();
            //------------------------------------------------------------------------------------------

        }
    }
}
