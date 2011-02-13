using System;
using System.Windows.Forms;
using Cream;

namespace SudokuGame
{
    public partial class SudokuMainForm : Form
    {
        internal static int[][] sol = new int[9][];
        internal int lastRowIndex=-1, lastColIndex=-1;
        internal static int[][][] ex = new[]//  1 
                                   {new[]
                                        {new[]{1, 2, 3, 0, 0, 0, 0, 0, 0}, 
                                                new[]{4, 5, 6, 0, 0, 0, 0, 0, 0}, 
                                                new[]{7, 8, 9, 0, 0, 0, 0, 0, 0}, 
                                                new[]{0, 0, 0, 0, 0, 0, 0, 0, 0}, 
                                                new int[]{0, 0, 0, 0, 0, 0, 0, 0, 0}, 
                                                new int[]{0, 0, 0, 0, 0, 0, 0, 0, 0}, 
                                                new int[]{0, 0, 0, 0, 0, 0, 0, 0, 0}, 
                                                new int[]{0, 0, 0, 0, 0, 0, 0, 0, 0}, 
                                                new int[]{0, 0, 0, 0, 0, 0, 0, 0, 0}}, 
                                   //  2
                                     new int[][]{new int[]{0, 9, 0, 0, 0, 0, 0, 1, 0}, 
                                                 new int[]{8, 0, 4, 0, 2, 0, 3, 0, 7}, 
                                                 new int[]{0, 6, 0, 9, 0, 7, 0, 2, 0}, 
                                                 new int[]{0, 0, 5, 0, 3, 0, 1, 0, 0}, 
                                                 new int[]{0, 7, 0, 5, 0, 1, 0, 3, 0}, 
                                                 new int[]{0, 0, 3, 0, 9, 0, 8, 0, 0}, 
                                                 new int[]{0, 2, 0, 8, 0, 5, 0, 6, 0}, 
                                                 new int[]{1, 0, 7, 0, 6, 0, 4, 0, 9}, 
                                                 new int[]{0, 3, 0, 0, 0, 0, 0, 8, 0}}, 
                                   //  3 
                                     new int[][]{new int[]{3, 7, 0, 1, 0, 0, 8, 2, 0}, 
                                                 new int[]{0, 0, 0, 7, 0, 8, 0, 0, 0}, 
                                                 new int[]{0, 8, 4, 0, 0, 9, 0, 5, 7}, 
                                                 new int[]{9, 0, 0, 8, 7, 0, 1, 0, 0}, 
                                                 new int[]{5, 0, 7, 0, 0, 0, 4, 0, 3}, 
                                                 new int[]{0, 0, 8, 0, 1, 5, 0, 0, 2}, 
                                                 new int[]{6, 3, 0, 5, 0, 0, 2, 4, 0}, 
                                                 new int[]{0, 0, 0, 4, 0, 3, 0, 0, 0}, 
                                                 new int[]{0, 4, 5, 0, 0, 1, 0, 7, 9}},
                                   //  4 
                                     new int[][]{new int[]{0, 0, 2, 7, 0, 0, 0, 0, 6}, 
                                                 new int[]{9, 0, 0, 0, 0, 0, 7, 2, 4}, 
                                                 new int[]{8, 4, 0, 0, 5, 6, 0, 0, 0}, 
                                                 new int[]{0, 0, 1, 0, 0, 0, 0, 5, 9}, 
                                                 new int[]{2, 0, 4, 8, 0, 0, 0, 0, 0}, 
                                                 new int[]{0, 0, 0, 6, 9, 7, 2, 0, 0}, 
                                                 new int[]{7, 5, 0, 0, 8, 0, 0, 3, 0}, 
                                                 new int[]{0, 8, 0, 3, 0, 0, 1, 0, 0}, 
                                                 new int[]{0, 0, 0, 0, 0, 0, 6, 9, 0}}, 
                                   //  5
                                     new int[][]{new int[]{6, 3, 1, 0, 0, 8, 0, 0, 0}, 
                                                 new int[]{5, 0, 0, 0, 0, 7, 0, 0, 0}, 
                                                 new int[]{9, 0, 0, 4, 1, 2, 0, 0, 0}, 
                                                 new int[]{0, 0, 8, 0, 0, 0, 1, 2, 3}, 
                                                 new int[]{0, 0, 2, 0, 0, 0, 4, 0, 0}, 
                                                 new int[]{3, 4, 5, 0, 0, 0, 6, 0, 0}, 
                                                 new int[]{0, 0, 0, 5, 8, 6, 0, 0, 4}, 
                                                 new int[]{0, 0, 0, 1, 0, 0, 0, 0, 8}, 
                                                 new int[]{0, 0, 0, 7, 0, 0, 3, 6, 1}}, 
                                   //  6
                                     new int[][]{new int[]{7, 0, 0, 0, 3, 0, 0, 0, 1}, 
                                                 new int[]{0, 4, 0, 7, 0, 6, 0, 3, 0}, 
                                                 new int[]{0, 0, 1, 0, 0, 0, 2, 0, 0}, 
                                                 new int[]{0, 3, 0, 2, 0, 7, 0, 6, 0}, 
                                                 new int[]{2, 0, 0, 0, 6, 0, 0, 0, 9}, 
                                                 new int[]{0, 8, 0, 5, 0, 3, 0, 2, 0}, 
                                                 new int[]{0, 0, 4, 0, 0, 0, 7, 0, 0}, 
                                                 new int[]{0, 1, 0, 3, 0, 9, 0, 8, 0}, 
                                                 new int[]{9, 0, 0, 0, 2, 0, 0, 0, 6}}, 
                                   //  7
                                     new int[][]{new int[]{0, 0, 0, 1, 2, 6, 0, 0, 0}, 
                                                 new int[]{0, 0, 9, 0, 0, 0, 1, 0, 0}, 
                                                 new int[]{0, 5, 1, 0, 0, 0, 4, 2, 0}, 
                                                 new int[]{4, 0, 0, 8, 0, 9, 0, 0, 3}, 
                                                 new int[]{1, 0, 0, 0, 6, 0, 0, 0, 2}, 
                                                 new int[]{9, 0, 0, 5, 1, 2, 0, 0, 8}, 
                                                 new int[]{0, 8, 4, 0, 5, 0, 2, 3, 0}, 
                                                 new int[]{0, 0, 0, 0, 8, 0, 0, 0, 0}, 
                                                 new int[]{0, 0, 3, 2, 4, 1, 8, 0, 0}}, 
                                   //  8
                                     new int[][]{new int[]{0, 9, 0, 0, 0, 0, 0, 0, 6}, 
                                                 new int[]{0, 1, 0, 4, 0, 3, 0, 0, 9}, 
                                                 new int[]{2, 0, 0, 6, 0, 0, 7, 4, 0}, 
                                                 new int[]{7, 0, 5, 0, 0, 4, 0, 0, 0}, 
                                                 new int[]{0, 0, 0, 0, 0, 7, 0, 0, 2}, 
                                                 new int[]{0, 4, 0, 0, 0, 1, 0, 0, 0}, 
                                                 new int[]{0, 5, 6, 8, 0, 0, 9, 0, 3}, 
                                                 new int[]{0, 3, 0, 7, 5, 0, 0, 1, 0}, 
                                                 new int[]{4, 0, 9, 0, 0, 0, 8, 2, 0}}, 
                                   //  9
                                     new int[][]{new int[]{0, 0, 0, 0, 0, 0, 1, 5, 0}, 
                                                 new int[]{0, 0, 0, 0, 0, 1, 0, 0, 4}, 
                                                 new int[]{0, 2, 8, 0, 0, 7, 0, 0, 6}, 
                                                 new int[]{6, 0, 0, 4, 0, 0, 9, 7, 0}, 
                                                 new int[]{8, 0, 0, 6, 0, 9, 0, 0, 5}, 
                                                 new int[]{0, 1, 4, 0, 0, 5, 0, 0, 3}, 
                                                 new int[]{3, 0, 0, 9, 0, 0, 2, 8, 0}, 
                                                 new int[]{4, 0, 0, 2, 0, 0, 0, 0, 0}, 
                                                 new int[]{0, 5, 7, 0, 0, 0, 0, 0, 0}}, 
                                   //  10
                                     new int[][]{new int[]{0, 8, 4, 0, 0, 0, 6, 0, 0},    // extarHard 4					 
				                                 new int[]{0, 0, 0, 0, 3, 0, 0, 7, 0},
				                                 new int[]{3, 0, 0, 9, 0, 6, 2, 0, 0},
				                                 new int[]{0, 0, 0, 0, 0, 0, 0, 0, 0},
				                                 new int[]{0, 0, 0, 1, 0, 0, 0, 0, 0},
				                                 new int[]{0, 0, 0, 0, 0, 0, 9, 0, 5},
				                                 new int[]{0, 0, 0, 2, 0, 8, 0, 0, 6},
				                                 new int[]{0, 3, 0, 0, 1, 0, 0, 0, 0},
				                                 new int[]{0, 0, 1, 0, 0, 0, 5, 2, 0}}};

        public SudokuMainForm()
        {
            InitializeComponent();
        }

        private void SudokuMainForm_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void t88_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                TextBox b = (TextBox)(sender);
                if (!(((TextBox) (sender)).ReadOnly))
                {
                    if ((!((e.KeyChar > '0') && (e.KeyChar <= '9'))) && (e.KeyChar != 8))
                    {
                        e.KeyChar = (char) 0;
                    }
                    if ((((TextBox) (sender)).Text.Length == 1) && (e.KeyChar != 8))
                    {
                        ((TextBox) (sender)).Text = "";
                        //e.KeyChar = (char)0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetBtn_Click(sender, e);
            sudoku(ex[comboBox1.SelectedIndex]);
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    string name = "t" + Convert.ToChar(i + 48) + Convert.ToChar(j + 48);
                    TextBox d = (TextBox)panel1.Controls[panel1.Controls.IndexOfKey(name)];
                    d.BackColor = System.Drawing.Color.White;
                    if (ex[comboBox1.SelectedIndex][i][j] != 0)
                    {
                        d.Text = Convert.ToString(ex[comboBox1.SelectedIndex][i][j]);
                        d.ReadOnly = true;
                    }
                    else
                    {
                        d.Text = "";
                        d.ReadOnly = false;
                    }
                }
            }
        }

        private void t88_Enter(object sender, EventArgs e)
        {
            if ((lastRowIndex != -1) && (lastColIndex!=-1))
            {
                string name = "t" + Convert.ToChar(lastRowIndex + 48) + Convert.ToChar(lastColIndex + 48);
                TextBox d = (TextBox)panel1.Controls[panel1.Controls.IndexOfKey(name)];
                d.BackColor = System.Drawing.Color.White;

            }
            ((TextBox)sender).BackColor = System.Drawing.Color.SkyBlue;
            if (sender is TextBox)
            {
                string s = ((TextBox) sender).Name;
                lastRowIndex = Convert.ToInt16(s[1].ToString());
                lastColIndex = Convert.ToInt16(s[2].ToString());
            }
        }


        public void sudoku(int[][] v0)
        {
            Network net = new Network();
            int n = 9;
            IntVariable[][] v = new IntVariable[n][];
            for (int i = 0; i < n; i++)
            {
                v[i] = new IntVariable[n];
            }
            IntVariable[] vs = new IntVariable[n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (v0[i][j] == 0)
                        v[i][j] = new IntVariable(net, 1, n);
                    else
                        v[i][j] = new IntVariable(net, v0[i][j]);
                }
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    vs[j] = v[i][j];
                new NotEquals(net, vs);
            }
            for (int j = 0; j < n; j++)
            {
                for (int i = 0; i < n; i++)
                    vs[i] = v[i][j];
                new NotEquals(net, vs);
            }
            for (int i0 = 0; i0 < n; i0 += 3)
            {
                for (int j0 = 0; j0 < n; j0 += 3)
                {
                    int k = 0;
                    for (int i = i0; i < i0 + 3; i++)
                        for (int j = j0; j < j0 + 3; j++)
                            vs[k++] = v[i][j];
                    new NotEquals(net, vs);
                }
            }
            Solver solver = new DefaultSolver(net);
            Int64 timer = DateTime.Now.Ticks;
            //for (solver.start(); solver.waitNext(); solver.resume())
            //solver.start();
            //solver.waitNext();
            {
                Solution solution = solver.FindFirst();
                sol = new int[9][];

                for (int i = 0; i < n; i++)
                {
                    sol[i] = new int[9];
                    for (int j = 0; j < n; j++)
                    {
                        sol[i][j] = solution.GetIntValue(v[i][j]);
                    }
                }
            }
            timer = DateTime.Now.Ticks - timer;
            solver.Stop();
            Console.WriteLine("Time = " + timer / 10000);
        }

        private void SolveBtn_Click(object sender, EventArgs e)
        {
            int n = 9;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    string name = "t" + Convert.ToChar(i + 48) + Convert.ToChar(j + 48);
                    TextBox d = (TextBox)panel1.Controls[panel1.Controls.IndexOfKey(name)];
                    d.Text = Convert.ToString(sol[i][j]);
                    d.BackColor = System.Drawing.Color.White;
                    d.ReadOnly = true;
                }
            }
        }

        private void HintBtn_Click(object sender, EventArgs e)
        {
            if ((lastRowIndex != -1) && (lastColIndex != -1))
            {
                string name = "t" + Convert.ToChar(lastRowIndex + 48) + Convert.ToChar(lastColIndex + 48);
                TextBox d = (TextBox)panel1.Controls[panel1.Controls.IndexOfKey(name)];
                d.Text = Convert.ToString(sol[lastRowIndex][lastColIndex]);
                d.BackColor = System.Drawing.Color.SkyBlue;
            }

        }

        private void CheckBtn_Click(object sender, EventArgs e)
        {
            for (int i=0; i<9; i++)
            {
                for (int j=0; j<9; j++)
                {
                    string name = "t" + Convert.ToChar(i + 48) + Convert.ToChar(j + 48);
                    TextBox d = (TextBox)panel1.Controls[panel1.Controls.IndexOfKey(name)];
                    if ((d.ReadOnly != true) && (d.Text != ""))
                    {

                        if (!d.Text.Equals(Convert.ToString(sol[i][j].ToString())))
                        {
                            d.BackColor = System.Drawing.Color.Red;
                        }
                    }

                    
                }
            }
        }

        private void t00_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }


        
    }
}
