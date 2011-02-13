using System;
using Cream;

public class JSSP
{
    [STAThread]
    public static void Main(String[] args)
    {
        String problem = "ft10";
        long timeout = 1000 * 60 * 10;
        String[] solverNames = new String[] { "sa", "ibb", "taboo" };
        int i = 0;
        if (i < args.Length)
            problem = args[i++];
        if (i < args.Length)
            timeout = Int32.Parse(args[i++]) * 1000;
        if (i < args.Length)
        {
            solverNames = new String[args.Length - i];
            int j = 0;
            for (; i < args.Length; i++)
            {
                solverNames[j++] = args[i];
            }
        }
        Network network = (new JSSPProblem(problem)).network();
        if (network == null)
            return;
        //int opt = Solver.MINIMIZE | Solver.BETTER;
        int opt = Solver.Default;
        Solver[] solvers = new Solver[solverNames.Length];
        for (i = 0; i < solvers.Length; i++)
        {
            String name = solverNames[i];
            if (name.Equals("sa"))
            {
                solvers[i] = new SimulatedAnneallingSearch((Network)network.Clone(), opt, name);
            }
            else if (name.Equals("ibb"))
            {
                solvers[i] = new IterativeBranchAndBoundSearch((Network)network.Clone(), opt, name);
            }
            else if (name.Equals("taboo") || name.Equals("tabu"))
            {
                solvers[i] = new TabooSearch((Network)network.Clone(), opt, name);
            }
            else if (name.Equals("rw"))
            {
                solvers[i] = new LocalSearch((Network)network.Clone(), opt, name);
            }
            else
            {
                Console.Out.WriteLine("Unknown solver name " + name);
                solvers[i] = null;
            }
        }
        Solver all = new ParallelSolver(solvers);
        
        //Monitor monitor = new Monitor();
         //monitor.setX(0, (int)(timeout/1000));
        //all.setMonitor(monitor);
        //SolutionHandler sh=null;
        Solution solution = all.FindBest(timeout);
        Console.Out.WriteLine(solution);
        Console.In.ReadLine();
    }
}


class JSSPProblem
{
    public String problem = null;

    public int numberOfJobs;
    public int numberOfMachines;
    public int[][] job_machine;
    public int[][] job_pt;

    public JSSPProblem(String problem)
    {
        this.problem = problem;
    }

    private void ft06()
    {
        // 55
        numberOfJobs = 6;
        numberOfMachines = 6;
        int[][] _job_machine = new int[][] { new int[] { 2, 0, 1, 3, 5, 4 }, 
                                             new int[] { 1, 2, 4, 5, 0, 3 }, 
                                             new int[] { 2, 3, 5, 0, 1, 4 }, 
                                             new int[] { 1, 0, 2, 3, 4, 5 }, 
                                             new int[] { 2, 1, 4, 5, 0, 3 }, 
                                             new int[] { 1, 3, 5, 0, 4, 2 } };
        job_machine = _job_machine;
        int[][] _job_pt = new int[][] { new int[] { 1, 3, 6, 7, 3, 6 }, 
                                        new int[] { 8, 5, 10, 10, 10, 4 }, 
                                        new int[] { 5, 4, 8, 9, 1, 7 }, 
                                        new int[] { 5, 5, 5, 3, 8, 9 }, 
                                        new int[] { 9, 3, 5, 4, 3, 1 }, 
                                        new int[] { 3, 3, 9, 10, 4, 1 } };
        job_pt = _job_pt;
    }

    private void ft10()
    {
        // 930
        numberOfJobs = 10;
        numberOfMachines = 10;
        int[][] _job_machine = new int[][] { new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 
                                             new int[] { 0, 2, 4, 9, 3, 1, 6, 5, 7, 8 }, 
                                             new int[] { 1, 0, 3, 2, 8, 5, 7, 6, 9, 4 }, 
                                             new int[] { 1, 2, 0, 4, 6, 8, 7, 3, 9, 5 }, 
                                             new int[] { 2, 0, 1, 5, 3, 4, 8, 7, 9, 6 }, 
                                             new int[] { 2, 1, 5, 3, 8, 9, 0, 6, 4, 7 }, 
                                             new int[] { 1, 0, 3, 2, 6, 5, 9, 8, 7, 4 }, 
                                             new int[] { 2, 0, 1, 5, 4, 6, 8, 9, 7, 3 }, 
                                             new int[] { 0, 1, 3, 5, 2, 9, 6, 7, 4, 8 }, 
                                             new int[] { 1, 0, 2, 6, 8, 9, 5, 3, 4, 7 } };
        job_machine = _job_machine;
        int[][] _job_pt = new int[][] { new int[] { 29, 78, 9, 36, 49, 11, 62, 56, 44, 21 }, 
                                        new int[] { 43, 90, 75, 11, 69, 28, 46, 46, 72, 30 }, 
                                        new int[] { 91, 85, 39, 74, 90, 10, 12, 89, 45, 33 }, 
                                        new int[] { 81, 95, 71, 99, 9, 52, 85, 98, 22, 43 }, 
                                        new int[] { 14, 6, 22, 61, 26, 69, 21, 49, 72, 53 }, 
                                        new int[] { 84, 2, 52, 95, 48, 72, 47, 65, 6, 25 }, 
                                        new int[] { 46, 37, 61, 13, 32, 21, 32, 89, 30, 55 }, 
                                        new int[] { 31, 86, 46, 74, 32, 88, 19, 48, 36, 79 }, 
                                        new int[] { 76, 69, 76, 51, 85, 11, 40, 89, 26, 74 }, 
                                        new int[] { 85, 13, 61, 7, 64, 76, 47, 52, 90, 45 } };
        job_pt = _job_pt;
    }

    private void la40()
    {
        // 1222
        numberOfJobs = 15;
        numberOfMachines = 15;
        int[][] _job_machine = new int[][] { new int[] { 9, 10, 4, 12, 2, 14, 5, 8, 6, 3, 1, 7, 13, 11, 0 }, 
                                             new int[] { 0, 1, 11, 2, 4, 9, 14, 8, 13, 12, 6, 3, 10, 5, 7 }, 
                                             new int[] { 14, 3, 1, 12, 6, 5, 8, 11, 7, 10, 2, 13, 0, 9, 4 }, 
                                             new int[] { 1, 6, 7, 4, 14, 10, 9, 5, 11, 2, 13, 8, 3, 12, 0 }, 
                                             new int[] { 12, 5, 9, 4, 14, 13, 0, 8, 11, 1, 2, 7, 6, 10, 3 }, 
                                             new int[] { 1, 5, 2, 13, 4, 14, 6, 7, 9, 10, 11, 0, 3, 12, 8 }, 
                                             new int[] { 9, 7, 6, 14, 3, 13, 2, 4, 12, 8, 1, 10, 0, 5, 11 }, 
                                             new int[] { 3, 7, 4, 8, 5, 2, 14, 12, 11, 0, 13, 10, 1, 6, 9 }, 
                                             new int[] { 3, 8, 6, 9, 14, 1, 5, 4, 13, 7, 11, 12, 10, 2, 0 }, 
                                             new int[] { 0, 5, 7, 4, 10, 12, 1, 13, 6, 8, 11, 9, 2, 14, 3 }, 
                                             new int[] { 2, 0, 6, 4, 3, 5, 12, 9, 14, 13, 8, 7, 11, 10, 1 }, 
                                             new int[] { 10, 14, 4, 9, 3, 1, 12, 13, 6, 8, 11, 7, 5, 2, 0 }, 
                                             new int[] { 0, 13, 3, 6, 1, 14, 11, 4, 10, 9, 5, 8, 7, 2, 12 }, 
                                             new int[] { 10, 12, 13, 4, 1, 3, 8, 5, 9, 0, 6, 7, 2, 14, 11 }, 
                                             new int[] { 1, 10, 6, 12, 4, 8, 3, 7, 13, 11, 5, 9, 2, 14, 0 } };
        job_machine = _job_machine;
        int[][] _job_pt = new int[][] { new int[] { 65, 28, 74, 33, 51, 75, 73, 32, 13, 81, 35, 59, 38, 55, 27 }, 
                                        new int[] { 64, 53, 83, 33, 6, 52, 72, 7, 90, 21, 23, 10, 39, 49, 72 }, 
                                        new int[] { 73, 82, 23, 62, 88, 21, 65, 70, 53, 81, 93, 77, 61, 28, 78 }, 
                                        new int[] { 12, 51, 33, 15, 72, 98, 94, 12, 42, 24, 15, 28, 6, 99, 41 }, 
                                        new int[] { 97, 7, 96, 15, 73, 43, 32, 22, 42, 94, 23, 86, 78, 24, 31 }, 
                                        new int[] { 72, 88, 93, 13, 44, 66, 63, 14, 67, 17, 85, 35, 68, 5, 49 }, 
                                        new int[] { 15, 82, 21, 53, 72, 49, 99, 26, 56, 45, 68, 51, 8, 27, 96 }, 
                                        new int[] { 54, 24, 14, 38, 36, 52, 55, 37, 48, 93, 60, 70, 23, 23, 83 }, 
                                        new int[] { 12, 69, 26, 23, 28, 82, 33, 45, 64, 15, 9, 73, 59, 37, 62 }, 
                                        new int[] { 87, 12, 80, 50, 48, 90, 72, 24, 14, 71, 44, 46, 15, 61, 92 }, 
                                        new int[] { 54, 22, 61, 46, 73, 16, 6, 94, 93, 67, 54, 75, 32, 40, 97 }, 
                                        new int[] { 92, 36, 22, 9, 47, 77, 79, 36, 30, 98, 79, 7, 55, 6, 30 }, 
                                        new int[] { 49, 83, 73, 82, 82, 92, 73, 31, 35, 54, 7, 37, 72, 52, 76 }, 
                                        new int[] { 98, 34, 52, 26, 28, 39, 80, 29, 70, 43, 48, 58, 45, 94, 96 }, 
                                        new int[] { 70, 17, 90, 67, 14, 23, 21, 18, 43, 84, 26, 36, 93, 84, 42 } };
        job_pt = _job_pt;
    }

    private void swv20()
    {
        // Unknown
        numberOfJobs = 50;
        numberOfMachines = 10;
        int[][] _job_machine = new int[][]{new int[]{8, 7, 4, 9, 2, 1, 5, 0, 3, 6}, 
                                           new int[]{4, 6, 3, 7, 8, 0, 9, 2, 1, 5}, 
                                           new int[]{5, 0, 1, 7, 2, 8, 3, 4, 9, 6}, 
                                           new int[]{4, 6, 0, 2, 7, 1, 9, 3, 8, 5}, 
                                           new int[]{0, 1, 4, 8, 3, 5, 9, 7, 2, 6}, 
                                           new int[]{6, 7, 1, 0, 2, 9, 5, 4, 8, 3}, 
                                           new int[]{1, 4, 5, 9, 7, 2, 6, 8, 0, 3}, 
                                           new int[]{2, 3, 1, 7, 6, 4, 8, 5, 0, 9}, 
                                           new int[]{1, 6, 0, 3, 2, 9, 8, 4, 7, 5}, 
                                           new int[]{6, 3, 8, 0, 4, 1, 5, 7, 9, 2}, 
                                           new int[]{9, 5, 6, 3, 0, 7, 4, 1, 2, 8}, 
                                           new int[]{7, 0, 9, 2, 5, 6, 1, 3, 8, 4}, 
                                           new int[]{5, 8, 9, 4, 7, 0, 6, 2, 1, 3}, 
                                           new int[]{1, 5, 0, 7, 6, 3, 4, 9, 8, 2}, 
                                           new int[]{0, 8, 3, 6, 9, 4, 2, 1, 7, 5}, 
                                           new int[]{8, 1, 9, 7, 4, 5, 6, 3, 2, 0}, 
                                           new int[]{8, 6, 4, 7, 1, 3, 0, 9, 5, 2}, 
                                           new int[]{4, 2, 6, 1, 3, 0, 8, 9, 5, 7}, 
                                           new int[]{8, 5, 1, 3, 0, 4, 2, 7, 6, 9}, 
                                           new int[]{2, 9, 0, 1, 5, 8, 7, 3, 4, 6}, 
                                           new int[]{7, 4, 2, 3, 0, 9, 5, 8, 6, 1}, 
                                           new int[]{6, 8, 4, 7, 1, 2, 0, 3, 5, 9}, 
                                           new int[]{5, 0, 2, 6, 4, 1, 8, 7, 3, 9}, 
                                           new int[]{5, 6, 3, 8, 0, 2, 9, 1, 4, 7}, 
                                           new int[]{3, 2, 5, 8, 7, 6, 1, 0, 4, 9}, 
                                           new int[]{0, 9, 4, 6, 2, 7, 1, 3, 8, 5}, 
                                           new int[]{4, 1, 0, 7, 9, 2, 5, 6, 3, 8}, 
                                           new int[]{3, 5, 2, 7, 8, 0, 4, 6, 1, 9}, 
                                           new int[]{0, 7, 6, 2, 8, 4, 5, 3, 9, 1}, 
                                           new int[]{0, 3, 2, 4, 6, 1, 9, 7, 5, 8}, 
                                           new int[]{1, 0, 4, 8, 6, 5, 7, 2, 3, 9}, 
                                           new int[]{1, 9, 3, 0, 2, 5, 6, 4, 8, 7}, 
                                           new int[]{3, 4, 1, 7, 6, 5, 9, 0, 2, 8}, 
                                           new int[]{4, 3, 7, 0, 6, 9, 5, 8, 1, 2}, 
                                           new int[]{3, 8, 9, 2, 5, 4, 7, 0, 6, 1}, 
                                           new int[]{2, 4, 5, 0, 8, 9, 1, 7, 3, 6}, 
                                           new int[]{9, 6, 5, 3, 2, 0, 1, 7, 4, 8}, 
                                           new int[]{1, 3, 2, 5, 4, 7, 0, 9, 6, 8}, 
                                           new int[]{3, 8, 6, 9, 5, 7, 4, 2, 0, 1}, 
                                           new int[]{0, 8, 9, 5, 3, 7, 2, 1, 4, 6}, 
                                           new int[]{8, 5, 0, 1, 9, 7, 3, 4, 6, 2}, 
                                           new int[]{2, 1, 6, 0, 9, 8, 5, 7, 3, 4}, 
                                           new int[]{1, 7, 3, 6, 8, 9, 0, 2, 5, 4}, 
                                           new int[]{2, 1, 4, 9, 5, 0, 3, 6, 8, 7}, 
                                           new int[]{3, 7, 2, 9, 1, 5, 6, 4, 0, 8}, 
                                           new int[]{2, 7, 6, 1, 0, 8, 5, 4, 9, 3}, 
                                           new int[]{1, 4, 8, 9, 5, 0, 3, 6, 7, 2}, 
                                           new int[]{5, 4, 7, 8, 9, 2, 6, 0, 1, 3}, 
                                           new int[]{1, 7, 4, 8, 0, 2, 3, 6, 9, 5}, 
                                           new int[]{4, 6, 7, 5, 2, 8, 0, 3, 9, 1}};
        job_machine = _job_machine;
        int[][] _job_pt = new int[][]{new int[]{100, 30, 42, 11, 31, 71, 41, 1, 55, 94}, 
                                      new int[]{81, 20, 96, 39, 29, 90, 61, 64, 86, 47}, 
                                      new int[]{80, 56, 88, 19, 68, 95, 44, 22, 60, 80}, 
                                      new int[]{86, 70, 88, 15, 50, 54, 88, 25, 89, 33}, 
                                      new int[]{48, 57, 86, 60, 78, 4, 60, 40, 11, 25}, 
                                      new int[]{23, 9, 90, 51, 52, 14, 30, 1, 25, 83}, 
                                      new int[]{30, 75, 76, 100, 54, 41, 50, 75, 1, 28}, 
                                      new int[]{46, 78, 37, 12, 56, 50, 66, 39, 8, 72}, 
                                      new int[]{24, 90, 32, 6, 99, 22, 12, 63, 81, 52}, 
                                      new int[]{62, 9, 59, 66, 41, 32, 29, 79, 84, 4}, 
                                      new int[]{57, 99, 2, 17, 51, 10, 14, 64, 99, 27}, 
                                      new int[]{81, 67, 83, 30, 25, 87, 29, 7, 93, 1}, 
                                      new int[]{65, 53, 48, 28, 74, 60, 77, 22, 5, 98}, 
                                      new int[]{97, 37, 71, 49, 51, 17, 38, 67, 28, 31}, 
                                      new int[]{20, 94, 39, 73, 63, 8, 57, 27, 26, 42}, 
                                      new int[]{77, 68, 20, 100, 1, 77, 17, 35, 65, 86}, 
                                      new int[]{68, 62, 79, 84, 60, 56, 10, 86, 60, 30}, 
                                      new int[]{71, 74, 6, 56, 69, 8, 50, 78, 4, 89}, 
                                      new int[]{29, 5, 59, 96, 46, 91, 48, 53, 21, 82}, 
                                      new int[]{19, 96, 73, 39, 54, 50, 60, 50, 65, 78}, 
                                      new int[]{68, 15, 26, 26, 13, 13, 96, 70, 27, 93}, 
                                      new int[]{41, 18, 66, 9, 31, 92, 3, 78, 41, 53}, 
                                      new int[]{9, 64, 15, 73, 12, 43, 89, 69, 32, 22}, 
                                      new int[]{93, 19, 74, 81, 72, 94, 19, 26, 53, 7}, 
                                      new int[]{48, 29, 51, 72, 35, 32, 38, 98, 58, 54}, 
                                      new int[]{94, 23, 41, 53, 53, 27, 62, 68, 84, 49}, 
                                      new int[]{4, 4, 66, 90, 78, 29, 2, 86, 23, 46}, 
                                      new int[]{78, 61, 97, 68, 92, 15, 12, 77, 12, 22}, 
                                      new int[]{100, 89, 71, 70, 89, 72, 78, 23, 37, 2}, 
                                      new int[]{91, 74, 36, 72, 62, 80, 20, 77, 47, 80}, 
                                      new int[]{44, 67, 66, 99, 59, 5, 15, 38, 40, 19}, 
                                      new int[]{69, 35, 86, 7, 35, 32, 66, 89, 63, 52}, 
                                      new int[]{3, 68, 66, 27, 41, 2, 77, 45, 40, 39}, 
                                      new int[]{66, 42, 79, 55, 98, 44, 6, 73, 55, 1}, 
                                      new int[]{80, 18, 94, 27, 42, 17, 74, 65, 6, 27}, 
                                      new int[]{73, 70, 51, 84, 29, 95, 97, 28, 68, 89}, 
                                      new int[]{85, 56, 54, 76, 50, 43, 8, 93, 17, 65}, 
                                      new int[]{1, 17, 61, 38, 71, 18, 40, 94, 41, 74}, 
                                      new int[]{30, 22, 39, 56, 3, 64, 74, 21, 93, 1}, 
                                      new int[]{17, 8, 20, 38, 85, 5, 63, 18, 89, 88}, 
			                          new int[]{87, 44, 42, 34, 11, 13, 71, 88, 32, 12}, 
                                      new int[]{39, 73, 43, 48, 77, 48, 23, 66, 94, 68}, 
                                      new int[]{98, 19, 69, 5, 85, 19, 30, 43, 87, 70}, 
                                      new int[]{45, 60, 30, 71, 35, 75, 75, 41, 67, 37}, 
                                      new int[]{63, 39, 16, 69, 46, 20, 57, 51, 66, 40}, 
                                      new int[]{7, 73, 17, 21, 24, 2, 68, 22, 36, 60}, 
                                      new int[]{20, 17, 12, 29, 28, 7, 38, 57, 22, 75}, 
                                      new int[]{53, 7, 5, 27, 38, 100, 48, 53, 11, 18}, 
                                      new int[]{49, 47, 81, 9, 20, 63, 15, 1, 10, 5}, 
                                      new int[]{49, 27, 17, 64, 30, 56, 42, 97, 82, 34}};
        job_pt = _job_pt;
    }

    private void abz08()
    {
        // 656
        numberOfJobs = 20;
        numberOfMachines = 15;
        int[][] _job_machine = new int[][] { new int[] { 6, 5, 8, 4, 1, 14, 13, 11, 10, 12, 2, 3, 0, 7, 9 }, 
                                             new int[] { 1, 5, 0, 3, 6, 9, 7, 12, 10, 13, 8, 4, 11, 14, 2 }, 
                                             new int[] { 0, 4, 2, 10, 6, 14, 8, 13, 7, 3, 9, 12, 1, 11, 5 }, 
                                             new int[] { 7, 5, 4, 8, 0, 9, 13, 12, 10, 3, 6, 14, 1, 11, 2 }, 
                                             new int[] { 2, 3, 12, 11, 6, 4, 10, 7, 0, 13, 1, 14, 5, 9, 8 }, 
                                             new int[] { 5, 3, 6, 12, 10, 0, 13, 2, 11, 7, 4, 1, 14, 9, 8 }, 
                                             new int[] { 13, 0, 11, 12, 4, 6, 5, 3, 9, 2, 7, 10, 1, 14, 8 }, 
                                             new int[] { 2, 12, 9, 11, 13, 8, 14, 5, 6, 3, 1, 4, 0, 7, 10 }, 
                                             new int[] { 2, 10, 14, 6, 8, 3, 12, 0, 13, 9, 7, 1, 11, 4, 5 }, 
                                             new int[] { 4, 9, 3, 11, 13, 7, 0, 2, 5, 12, 1, 10, 14, 8, 6 }, 
                                             new int[] { 13, 0, 3, 8, 5, 6, 14, 7, 1, 2, 4, 9, 12, 11, 10 }, 
                                             new int[] { 14, 10, 0, 3, 13, 6, 7, 2, 12, 5, 4, 11, 1, 8, 9 }, 
                                             new int[] { 6, 12, 4, 2, 8, 5, 14, 3, 9, 1, 11, 13, 7, 10, 0 }, 
                                             new int[] { 5, 14, 0, 8, 7, 4, 9, 13, 1, 12, 6, 11, 3, 10, 2 }, 
                                             new int[] { 5, 3, 10, 6, 4, 12, 11, 13, 7, 9, 14, 1, 2, 0, 8 }, 
                                             new int[] { 8, 5, 9, 6, 1, 7, 11, 2, 4, 0, 10, 3, 12, 14, 13 }, 
                                             new int[] { 1, 4, 8, 3, 10, 5, 12, 7, 9, 14, 11, 13, 0, 2, 6 }, 
                                             new int[] { 7, 5, 13, 9, 10, 4, 14, 0, 3, 11, 6, 8, 1, 2, 12 }, 
                                             new int[] { 14, 11, 5, 2, 13, 10, 4, 8, 3, 9, 6, 7, 0, 1, 12 }, 
                                             new int[] { 1, 7, 11, 8, 14, 6, 5, 3, 13, 2, 0, 4, 9, 12, 10 } };
        job_machine = _job_machine;
        int[][] _job_pt = new int[][] { new int[] { 14, 21, 13, 11, 11, 35, 20, 17, 18, 11, 23, 13, 15, 11, 35 }, 
                                        new int[] { 35, 31, 13, 26, 14, 17, 38, 20, 19, 12, 16, 34, 15, 12, 14 }, 
                                        new int[] { 30, 35, 40, 35, 30, 23, 29, 37, 38, 40, 26, 11, 40, 36, 17 }, 
                                        new int[] { 40, 18, 12, 23, 23, 14, 16, 14, 23, 12, 16, 32, 40, 25, 29 }, 
                                        new int[] { 35, 15, 31, 28, 32, 30, 27, 29, 38, 11, 23, 17, 27, 37, 29 }, 
                                        new int[] { 33, 33, 19, 40, 19, 33, 26, 31, 28, 36, 38, 21, 25, 40, 35 }, 
                                        new int[] { 25, 32, 33, 18, 32, 28, 15, 35, 14, 34, 23, 32, 17, 26, 19 }, 
                                        new int[] { 16, 33, 34, 30, 40, 12, 26, 26, 15, 21, 40, 32, 14, 30, 35 }, 
                                        new int[] { 17, 16, 20, 24, 26, 36, 22, 14, 11, 20, 23, 29, 23, 15, 40 }, 
                                        new int[] { 27, 37, 40, 14, 25, 30, 34, 11, 15, 32, 36, 12, 28, 31, 23 }, 
                                        new int[] { 25, 22, 27, 14, 25, 20, 18, 14, 19, 17, 27, 22, 22, 27, 21 }, 
                                        new int[] { 34, 15, 22, 29, 34, 40, 17, 32, 20, 39, 31, 16, 37, 33, 13 }, 
                                        new int[] { 12, 27, 17, 24, 11, 19, 11, 17, 25, 11, 31, 33, 31, 12, 22 }, 
                                        new int[] { 22, 15, 16, 32, 20, 22, 11, 19, 30, 33, 29, 18, 34, 32, 18 }, 
                                        new int[] { 27, 26, 28, 37, 18, 12, 11, 26, 27, 40, 19, 24, 18, 12, 34 }, 
                                        new int[] { 15, 28, 25, 32, 13, 38, 11, 34, 25, 20, 32, 23, 14, 16, 20 }, 
                                        new int[] { 15, 13, 37, 14, 22, 24, 26, 22, 34, 22, 19, 32, 29, 13, 35 }, 
                                        new int[] { 36, 33, 28, 20, 30, 33, 29, 34, 22, 12, 30, 12, 35, 13, 35 }, 
                                        new int[] { 26, 31, 35, 38, 19, 35, 27, 29, 39, 13, 14, 26, 17, 22, 15 }, 
                                        new int[] { 36, 34, 33, 17, 38, 39, 16, 27, 29, 16, 16, 19, 40, 35, 39 } };
        job_pt = _job_pt;
    }

    public virtual Network network()
    {
        if (problem.Equals("ft06"))
        {
            ft06();
        }
        else if (problem.Equals("ft10"))
        {
            ft10();
        }
        else if (problem.Equals("la40"))
        {
            la40();
        }
        else if (problem.Equals("swv20"))
        {
            swv20();
        }
        else if (problem.Equals("abz08"))
        {
            abz08();
        }
        else
        {
            Console.Out.WriteLine("Unknown problem");
            return null;
        }
        int[][] machine_op = new int[numberOfMachines][];
        for (int i = 0; i < numberOfMachines; i++)
        {
            machine_op[i] = new int[numberOfJobs];
        }
        int[][] machine_pt = new int[numberOfMachines][];
        for (int i2 = 0; i2 < numberOfMachines; i2++)
        {
            machine_pt[i2] = new int[numberOfJobs];
        }
        for (int j = 0; j < job_machine.Length; j++)
        {
            for (int k = 0; k < job_machine[j].Length; k++)
            {
                int m = job_machine[j][k];
                machine_op[m][j] = k;
                machine_pt[m][j] = job_pt[j][k];
            }
        }

        Network net = new Network();
        Variable endVar = new Variable(net, new IntDomain(0, IntDomain.MaxValue));
        net.Objective =endVar;
        Variable[][] job = new Variable[numberOfJobs][];
        for (int i3 = 0; i3 < numberOfJobs; i3++)
        {
            job[i3] = new Variable[numberOfMachines + 1];
        }
        for (int j = 0; j < numberOfJobs; j++)
        {
            for (int k = 0; k < numberOfMachines; k++)
            {
                job[j][k] = new Variable(net, new IntDomain(0, IntDomain.MaxValue));
            }
            job[j][numberOfMachines] = endVar;
            Sequential seq = new Sequential(net, job[j], job_pt[j]);
        }
        Variable[][] machine = new Variable[numberOfMachines][];
        for (int i4 = 0; i4 < numberOfMachines; i4++)
        {
            machine[i4] = new Variable[numberOfJobs];
        }
        for (int m = 0; m < numberOfMachines; m++)
        {
            for (int j = 0; j < numberOfJobs; j++)
            {
                int k = machine_op[m][j];
                machine[m][j] = job[j][k];
            }
            Serialized ser = new Serialized(net, machine[m], machine_pt[m]);
        }
        return net;
    }
}