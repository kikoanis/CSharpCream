/*
* Family.java
*/
using System;
using Cream;

namespace Family
{
    /// <summary> Example obtained from "Finite Domain Constraint Programming in Oz"
    /// 
    /// Maria and Clara are both heads of households, and both families have three
    /// boys and three girls. Neither family includes any children closer in age than
    /// one year, and all children are under age 10. The youngest child in Maria's
    /// family is a girl, and Clara has just given birth to a little girl.
    /// 
    /// In each family, the sum of the ages of the boys equals the sum of the ages of
    /// the girls, and the sum of the squares of the ages of the boys equals the sum
    /// of the the squares of ages of the girls. The sum of the ages of all children
    /// is 60.
    /// 
    /// What are the ages of the children in each family?
    /// 
    /// </summary>
    /// <author>  Naoyuki Tamura (tamura@kobe-u.ac.jp)
    /// </author>
    public class Family
    {

        private static IntVariable sum(IntVariable[][] v)
        {
            IntVariable sum = null;
            for (int i = 0; i < v.Length; i++)
            {
                for (int j = 0; j < v[i].Length; j++)
                {
                    sum = (sum == null) ? v[i][j] : sum.Add(v[i][j]);
                }
            }
            return sum;
        }

        private static IntVariable sum(IntVariable[] v)
        {
            IntVariable sum = null;
            for (int i = 0; i < v.Length; i++)
            {
                sum = (sum == null) ? v[i] : sum.Add(v[i]);
            }
            return sum;
        }

        private static IntVariable sum2(IntVariable[] v)
        {
            IntVariable sum = null;
            for (int i = 0; i < v.Length; i++)
            {
                IntVariable x = v[i].Multiply(v[i]);
                sum = (sum == null) ? x : sum.Add(x);
            }
            return sum;
        }

        public static void solve()
        {
            int FAMILIES = 2;
            int CHILDREN = 6;
            int maxAge = 9;

            Network net = new Network();

            IntVariable[][] isBoy = new IntVariable[FAMILIES][];
            for (int i = 0; i < FAMILIES; i++)
            {
                isBoy[i] = new IntVariable[CHILDREN];
            }
            IntVariable[][] age = new IntVariable[FAMILIES][];
            for (int i2 = 0; i2 < FAMILIES; i2++)
            {
                age[i2] = new IntVariable[CHILDREN];
            }
            IntVariable[][] boyAge = new IntVariable[FAMILIES][];
            for (int i3 = 0; i3 < FAMILIES; i3++)
            {
                boyAge[i3] = new IntVariable[CHILDREN];
            }
            IntVariable[][] girlAge = new IntVariable[FAMILIES][];
            for (int i4 = 0; i4 < FAMILIES; i4++)
            {
                girlAge[i4] = new IntVariable[CHILDREN];
            }

            for (int family = 0; family < FAMILIES; family++)
            {
                for (int child = 0; child < CHILDREN; child++)
                {
                    isBoy[family][child] = new IntVariable(net, 0, 1);
                    age[family][child] = new IntVariable(net, 0, maxAge);
                    if (child > 0)
                        age[family][child].Gt(age[family][child - 1]);
                    boyAge[family][child] = age[family][child].Multiply(isBoy[family][child]);
                    girlAge[family][child] = age[family][child].Subtract(boyAge[family][child]);
                }
            }

            isBoy[0][0].Equals(0);
            isBoy[1][0].Equals(0);
            age[1][0].Equals(0);

            for (int family = 0; family < FAMILIES; family++)
            {
                sum(isBoy[family]).Equals(3);
                sum(boyAge[family]).Equals(sum(girlAge[family]));
                sum2(boyAge[family]).Equals(sum2(girlAge[family]));
            }
            sum(age).Equals(60);

            Solver solver = new DefaultSolver(net);
            for (solver.Start(); solver.WaitNext(); solver.Resume())
            {
                Solution solution = solver.Solution;
                for (int family = 0; family < FAMILIES; family++)
                {
                    Console.Out.Write("Family " + family + ": ");
                    for (int child = 0; child < CHILDREN; child++)
                    {
                        int _isBoy = solution.GetIntValue(isBoy[family][child]);
                        int _age = solution.GetIntValue(age[family][child]);
                        Console.Out.Write(_isBoy != 0 ? "Boy  " : "Girl ");
                        Console.Out.Write(_age + "  ");
                    }
                    Console.Out.WriteLine();
                }
            }
            Console.ReadLine();
        }

        [STAThread]
        public static void Main(String[] args)
        {
            solve();
        }
    }
}