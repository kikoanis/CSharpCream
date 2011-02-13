/*
* @(#)Museum.cs
* Museum puzzle by Nikoli
* http://www.nikoli.co.jp/puzzles/32/
*/
using System;
using Cream;

public class Museum
{
    internal static String[][] puzzle = null;
    internal static int m;
    internal static int n;
    internal static Network net = null;
    internal static IntVariable[][] v = null;
    internal static IntVariable[][] _vsum = null;
    internal static IntVariable[][] _hsum = null;

    internal static bool parse(System.IO.StreamReader inString)
    {
        puzzle = null;
        try
        {
            String line;
            do
            {
                line = inString.ReadLine();
                if (line == null)
                    return false;
            }
            while (!line.Equals("begin"));
            line = inString.ReadLine();
            if (line == null)
                return false;
            String[] tokens = line.Split(" ".ToCharArray(),StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length != 3 || !tokens[0].Equals("size"))
                return false;
            m = Int32.Parse(tokens[1]);
            n = Int32.Parse(tokens[2]);
            if (m <= 0 || n <= 0)
                return false;
            puzzle = new String[m][];
            for (int i = 0; i < m; i++)
            {
                line = inString.ReadLine();
                puzzle[i] = line.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (puzzle.Length != n)
                    return false;
            }
            line = inString.ReadLine();
            if (!line.Equals("end"))
                return false;
        }
        catch (System.IO.IOException)
        {
            return false;
        }
        return true;
    }

    internal static IntVariable vsum(int i, int j)
    {
        while (i - 1 >= 0 && puzzle[i - 1][j].Equals("-"))
            i--;
        if (_vsum[i][j] == null)
        {
            _vsum[i][j] = v[i][j];
            for (int k = i + 1; k < m && v[k][j] != null; k++)
                _vsum[i][j] = _vsum[i][j].Add(v[k][j]);
            _vsum[i][j].Le(1);
            
        }
        return _vsum[i][j];
    }

    internal static IntVariable hsum(int i, int j)
    {
        while (j - 1 >= 0 && puzzle[i][j - 1].Equals("-"))
            j--;
        if (_hsum[i][j] == null)
        {
            _hsum[i][j] = v[i][j];
            for (int k = j + 1; k < n && v[i][k] != null; k++)
                _hsum[i][j] = _hsum[i][j].Add(v[i][k]);
            _hsum[i][j].Le(1);
            
        }
        return _hsum[i][j];
        //else
        //{
        //    return null;
        //}
    }

    internal static IntVariable adjacentSum(int i, int j)
    {
        IntVariable s = new IntVariable(net, 0);
        if (i - 1 >= 0 && v[i - 1][j] != null)
            s = s.Add(v[i - 1][j]);
        if (i + 1 < m && v[i + 1][j] != null)
            s = s.Add(v[i + 1][j]);
        if (j - 1 >= 0 && v[i][j - 1] != null)
            s = s.Add(v[i][j - 1]);
        if (j + 1 < n && v[i][j + 1] != null)
            s = s.Add(v[i][j + 1]);
        return s;
    }

    internal static void setProblem()
    {
        net = new Network();

        // Set constraint variables
        v = new IntVariable[m][];
        for (int i = 0; i < m; i++)
        {
            v[i] = new IntVariable[n];
        }
        _vsum = new IntVariable[m][];
        for (int i2 = 0; i2 < m; i2++)
        {
            _vsum[i2] = new IntVariable[n];
        }
        _hsum = new IntVariable[m][];
        for (int i3 = 0; i3 < m; i3++)
        {
            _hsum[i3] = new IntVariable[n];
        }
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                v[i][j] = null;
                if (puzzle[i][j].Equals("-"))
                {
                    v[i][j] = new IntVariable(net, 0, 1);
                }
                _vsum[i][j] = null;
                _hsum[i][j] = null;
            }
        }
        // Set constraints
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (puzzle[i][j].Equals("-"))
                {
                    IntVariable vsum_ = vsum(i, j);
                    IntVariable hsum_ = hsum(i, j);
                    vsum_.Add(hsum_).Subtract(v[i][j]).Ge(1);
                }
                else if (puzzle[i][j].Equals("^\\d$"))
                {
                    int sum = Int32.Parse(puzzle[i][j]);
                    IntVariable asum = adjacentSum(i, j);
                    asum.Equals(sum);
                }
            }
        }
    }

    internal static void printSolution(Solution solution)
    {
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                String s;
                if (v[i][j] == null)
                {
                    s = puzzle[i][j];
                }
                else
                {
                    int x = solution.GetIntValue(v[i][j]);
                    s = (x == 0) ? "-" : "*";
                }
                Console.Out.Write(s + " ");
            }
            Console.Out.WriteLine();
        }
        Console.Out.WriteLine();
    }

    internal static void solve()
    {
        setProblem();

        Solver solver = new DefaultSolver(net);
        for (solver.Start(); solver.WaitNext(); solver.Resume())
        {
            Solution solution = solver.Solution;
            printSolution(solution);
        }
        solver.Stop();
    }

    [STAThread]
    public static void Main(String[] args)
    {
        System.IO.StreamReader inString = new System.IO.StreamReader(new System.IO.StreamReader(Console.OpenStandardInput(), System.Text.Encoding.Default).BaseStream, new System.IO.StreamReader(Console.OpenStandardInput(), System.Text.Encoding.Default).CurrentEncoding);
        while (parse(inString))
        {
            solve();
        }
        Console.ReadLine();
    }
}

/* Sample data

begin
size   7 7
- - - - X - -
0 - 2 - - - -
- - - 4 - X -
- 0 - - - 1 - 
- X - X - - - 
- - - - 3 - X
- - 1 - - - -
end
*/