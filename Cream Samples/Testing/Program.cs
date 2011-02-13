using System;
using System.Collections.Generic;
using System.IO;
using Cream;
using Cream.AllenTemporal;
using System.Collections;
using System.Runtime.InteropServices;

namespace Testing
{
    class Program
    {
        //While fibonacci is probably not the most useful algorithm to implement, 
        //the fact that its natural implementation is a recursive one, and thus 
        //performs rather horribly for larger input, makes it an ideal candidate 
        //for optimization methods that can be easily extended to other algorithms as well.
        //One optimization that can often be used, when you can't change the recursive 
        //nature of the algorithm, and the algorithm will repeatedly calculate the same 
        //data over and over again, the Memoize pattern is very useful.
        //Basically you wrap the function in a form of cache that tracks the input 
        //parameters and their corresponding outputs, only calling the actual 
        //function when input parameters are unknown.
        //A trivial C# implementation is as follows:

        static public Func<ulong, ulong> Memorize(Func<ulong, ulong> func)
        {
            //OrderedDictionary<ulong, ulong> cache = new OrderedDictionary<ulong, ulong>(); 
            Dictionary<ulong, ulong> cache = new Dictionary<ulong, ulong>();
            return delegate(ulong key)
            {
                ulong result;
                if (cache.TryGetValue(key, out result))
                    return result;
                cache[key] = result = func(key);
                return result;
            };
        }

        static ulong  anotherway(ulong m)
        {
            Func<ulong, ulong> fib = null;
            fib = n =>n > 1 ? fib(n - 1) + fib(n - 2) : n;
            fib = Memorize(fib);
            return fib(m);
            
        }

        static void st()
        {
            Func<int, int> fib = null;
            fib = n => n > 1 ? fib(n - 1) + fib(n - 2) : n;

            Console.WriteLine("10th Fibonacci #: " + fib(10));

           // Func<int, int> fib2 = fib;

            //fib = n => n + 1;

            //Console.WriteLine("Increment the number 10: " + fib(10));
            Console.WriteLine("1000th Fibonacci #: " + fib(1000) + "???");

        }
        public static ulong Fibonacci(ulong n)
        {
            ulong t = 0, i = 0, v = 0, w;
            do { w = v; v = t; t = i < 2 ? i : t + w; } while (i++ < n);
            return t;
        }

        //[DllImport("GTCSP.dll")]
        //public static extern int generateFile(int argc, string[] argv);
        //public static extern int getp(int argc);
        //[DllImport("msvcrt.dll")]
        //internal static extern int _flushall();

        static void Main(string[] args)
        {
            //string[] sss = {"20", "0.5", "0.6", "0.7", "3", "3", "0.7", "0.02", "80", "67", "1", ">", "file.txt"};
            //generateFile(13, sss);
            //return;
            //Console.Out.WriteLine(getp(9));
            //Console.In.ReadLine();
            //_flushall();
            Network net = new Network();
            // reading events and SOPO
            StreamReader re = File.OpenText("numfile.txt");//"Anum1.txt");//"fn1.txt"); //"numfile.txt");
            string input = re.ReadLine();
            string[] s = input.Split(' ');
            int n;
            try
            {
                n = Convert.ToInt16(s[1]);   // no of events
            }
            catch
            {
                Console.WriteLine("Failed to read no of events!!");
                return;
            }
            if (n==0)   // no events
            {
                Console.WriteLine("No of events must be greater than 0 !!");
                return; 
            }
            var A = new AllenVariable[n];
            for (var i=0; i<n;i++)
            {
                try
                {
                    //input = "";
                    //while (input.Equals(""))
                    input = re.ReadLine();
                    s = input.Split(' ');
                    A[i] = new AllenVariable(net, Convert.ToInt16(s[1]), Convert.ToInt16(s[2]),
                                                  Convert.ToInt16(s[3]), Convert.ToInt16(s[4]), "A" + i);
                }
                catch
                {
                    Console.WriteLine("Error occured while reading file numfile.txt !!");
                    return; 
                }
                
            }
            re.Close();
            // READING REALTIONS
            re = File.OpenText("symbfile.txt");//"Asym1.txt");//"fs1.txt");//"symbfile.txt");
            try
            {
                re.ReadLine(); // we don't need the first line
                input = re.ReadLine(); // this is the number of constraints
                int m = Convert.ToInt16(input); // m represents the number of constraints
                for (int i = 0; i < m; i++)
                {
                    input = re.ReadLine();
                    s = input.Split(' ');
                    int firstVar = Convert.ToInt16(s[0]);
                    int secondVar = Convert.ToInt16(s[1]);
                    string rel = s[2];
                    
                    for (int j = 0; j < rel.Length; j++)
                    {
                        char r = rel[j];
                        switch (r)
                        {
                            case 'P':
                                A[firstVar].Precedes(A[secondVar]);
                                break;
                            case 'p':
                                A[firstVar].Precededby(A[secondVar]);
                                break;
                            case 'F':
                                A[firstVar].Finishes(A[secondVar]);
                                break;
                            case 'f':
                                A[firstVar].FinishedBy(A[secondVar]);
                                break;
                            case 'M':
                                A[firstVar].Meets(A[secondVar]);
                                break;
                            case 'm':
                                A[firstVar].MetBy(A[secondVar]);
                                break;
                            case 'S':
                                A[firstVar].Starts(A[secondVar]);
                                break;
                            case 's':
                                A[firstVar].StartedBy(A[secondVar]);
                                break;
                            case 'D':
                                A[firstVar].During(A[secondVar]);
                                break;
                            case 'd':
                                A[firstVar].Contains(A[secondVar]);
                                break;
                            case 'O':
                                A[firstVar].Overlaps(A[secondVar]);
                                break;
                            case 'o':
                                A[firstVar].OverlappedBy(A[secondVar]);
                                break;
                            case 'E':
                                A[firstVar].Equals(A[secondVar]);
                                break;
                            default:
                                Console.WriteLine("Error occured while reading relations");
                                return;
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("Error occured while reading file symbfile.txt !!");
                return;
            }
            /*AllenVariable[] A = new AllenVariable[3];
            A[0] = new AllenVariable(net, 30, 60, 25, "M");
            A[1] = new AllenVariable(net, 20, 60, 30, "L");
            A[2] = new AllenVariable(net, 20, 46, 20, "J");
            A[0].meets(A[1]);
            A[0].during(A[1]);
            A[0].overlaps(A[1]);
            A[0].starts(A[1]);
            A[0].precedes(A[1]);
            A[1].equals(A[2]);
            A[1].starts(A[2]);
            A[1].startedBy(A[2]);
            A[0].equals(A[2]);
            A[0].overlaps(A[2]);
            A[0].overlappedBy(A[2]);
            A[0].starts(A[2]);
            A[0].startedBy(A[2]);
            A[0].finishes(A[2]);
            A[0].finishedBy(A[2]);
            A[0].during(A[2]);
            A[0].contains(A[2]);*/
            /*AllenVariable John = new AllenVariable(net, 0, 40, 30,1, "John");
            AllenVariable Mary = new AllenVariable(net, 35, 60, 20, "Mary");
            AllenVariable Wendy = new AllenVariable(net, 0, 60, 50, "Wendy");
            AllenVariable Soccer = new AllenVariable(net, 30, 135, 105, "Soccer");
            John.equals(Mary);
            John.starts(Mary);
            John.startedBy(Mary);
            John.meets(Mary);
            John.equals(Wendy);
            John.starts(Wendy);
            John.startedBy(Wendy);
            John.meets(Wendy);
            John.overlaps(Soccer);
            Mary.finishes(Wendy);
            //Mary.during(Wendy);
            Mary.finishedBy(Wendy);
            Mary.during(Soccer);
            Mary.contains(Soccer);*/
            Solver solver = new AllenSolver(net);
            //solver.findFirst();

            //solver.waitNext();
            int c = 0;
            long timer = DateTime.Now.Ticks;
            StreamWriter wr = File.CreateText("Output.txt");
            wr.WriteLine("Started at "+DateTime.Now);
            Console.WriteLine("Started at "+DateTime.Now);
            wr.WriteLine("================================");
            Console.WriteLine("================================");
            //st();
            //Console.Out.WriteLine(Fibonacci(9));
            //Console.Out.WriteLine(anotherway(4400));
            //Console.In.ReadLine();
            //return;
            for (solver.Start(); solver.WaitNext(); solver.Resume())
            {
                Solution solution = solver.Solution;
                
                AllenDomain[] ad = new AllenDomain[n];
                for (int k=0; k<n; k++)
                {
                    ad[k] = (AllenDomain) (A[k].Domain);
                }
                int[] s0 = new int[n];
                for (int k=0; k<n; k++)
                {
                    s0[k] = solution.GetIntValue(A[k]) + ad[k].Duration;
                Console.Out.WriteLine(solution.GetIntValue(A[k]) + "-" + s0[k]);
                wr.WriteLine(solution.GetIntValue(A[k]) + "-" + s0[k]);

                }
                /*AllenDomain ad0 = (AllenDomain)(John.Domain);
                AllenDomain ad1 = (AllenDomain)(Mary.Domain);
                AllenDomain ad2 = (AllenDomain)(Wendy.Domain);
                AllenDomain ad3 = (AllenDomain)(Soccer.Domain);
                int s0 = solution.getIntValue(John) + ad0.Duration;
                int s1 = solution.getIntValue(Mary) + ad1.Duration;
                int s2 = solution.getIntValue(Wendy) + ad2.Duration;
                int s3 = solution.getIntValue(Soccer) + ad3.Duration;
                Console.Out.WriteLine(solution.getIntValue(John) + "-" + s0 + "  John   ");
                Console.Out.WriteLine(solution.getIntValue(Mary) + "-" + s1+"   Mary");
                Console.Out.WriteLine(solution.getIntValue(Wendy) + "-" + s2+"   Wendy");
                Console.Out.WriteLine(solution.getIntValue(Soccer) + "-" + s3+"   Soccer");*/
                c++;
                wr.WriteLine("C==" + c);
                wr.WriteLine("================================");
                wr.WriteLine(DateTime.Now);
                Console.WriteLine(DateTime.Now);
                Console.WriteLine("C==" + c);
                Console.WriteLine("================================");
               // if (c==1)
                    break;
            }
            wr.WriteLine("Finished at " + DateTime.Now);
            Console.WriteLine("Finished at " + DateTime.Now);
            wr.WriteLine("================================");
            Console.WriteLine("================================");
            timer = DateTime.Now.Ticks - timer;
            Console.Out.WriteLine("timer: "+timer/10000/1000/60.0);
            wr.WriteLine("timer: " + timer / 10000);
            wr.Close();
            //foreach (Constraint c1 in net.Constraints)
            //{
            //    Console.WriteLine(c1.ToString());
            //}
            Console.WriteLine(c);
            solver.Stop();
            Console.In.ReadLine();
            
            /*CVarToVar[] df = new CVarToVar[net.Variables.Count];
            IEnumerator va = net.Variables.GetEnumerator();
            AllenVariable aa;
            while (va.MoveNext())
            {
                aa = (AllenVariable) va.Current;
                df = aa.getConstraintsVars();

                for (int l = 0; l < net.Variables.Count; l++)
                {
                    if (df[l].ToString() != null)
                    Console.WriteLine(df[l]);
                }
                Console.WriteLine();
            }

            int [][][] h = NetWorkConstraints.getConstraintNetwork(net);
            int[][][] BB = AllenEvents.AllenComposition;*/
        }
        
    }

}
