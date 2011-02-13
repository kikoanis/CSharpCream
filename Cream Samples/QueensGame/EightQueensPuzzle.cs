using System;
using System.Windows.Forms;
using Cream;

namespace QueensGame
{
    public partial class QueenPuzzle : Form
    {
        private int[][] sol = new int[92][];
        private int solIndex;
        internal void queens(int n)
        {
            var c = 0;
            var net = new Network();
            var q = new IntVariable[n];
            var u = new IntVariable[n];
            var d = new IntVariable[n];
            for (var i = 0; i < n; ++i)
            {
                q[i] = new IntVariable(net, 1, n);
                u[i] = q[i].Add(i);
                d[i] = q[i].Subtract(i);
            }
            new NotEquals(net, q);
            new NotEquals(net, u);
            new NotEquals(net, d);
            Solver solver = new DefaultSolver(net);
            for (solver.Start(); solver.WaitNext(); solver.Resume())
            {
                Solution solution = solver.Solution;
                sol[c] = new int[8];
                for (int i = 0; i < n; i++)
                {
					var s = solution.GetIntValue(q[i]);
                    sol[c][i] = solution.GetIntValue(q[i]);
                }
                c++;
            }
            solver.Stop();
        }
        public QueenPuzzle()
        {
            InitializeComponent();
        }

        private void QueenPuzzle_Load(object sender, EventArgs e)
        {
            queens(8);
            label1.Text = "";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            if (p.Image != null)
            {
                FirstSol.Enabled = true;
                NextSol.Enabled = true;
                PrevSol.Enabled = true;
                LastSol.Enabled = true;
                p.Image = null;
                label1.Text = "";
                label3.Text = "";
            }
            else
            {
                if (checkValidity(sender))
                {
                    p.Image = pictureBox1.Image;
                    FirstSol.Enabled = true;
                    NextSol.Enabled = true;
                    PrevSol.Enabled = true;
                    LastSol.Enabled = true;
                    label3.Text = "";
                    label1.Text = "";
                    bool allfound = true;
                    for (int col =0; col<8; col++)
                    {
                        bool rowfound = false;
                        for (int row=0; row<8; row++)
                        {
                            string name = "p" + Convert.ToChar(row + 48) + Convert.ToChar(col + 48);
                            PictureBox p1 = (PictureBox)Controls[Controls.IndexOfKey(name)];
                            if (p1.Image != null)
                            {
                                rowfound = true;
                                break;
                            }
                        }
                        if (!rowfound)
                        {
                            allfound = false;
                            break;
                        }
                    }
                    if (allfound)
                    {
                        label3.Text = "You've found a solution!!!";
                    }
                }
            }
        }

        private bool checkValidity(object sender)
        {
            label1.Text = "";
            if (sender is PictureBox)
            {
                PictureBox pOrg = (PictureBox) sender;
                string s = ((PictureBox) sender).Name;
                int RowIndex = Convert.ToInt16(s[1].ToString());
                int ColIndex = Convert.ToInt16(s[2].ToString());
                for (int col = 0; col < 8; col++)
                {
                    string name = "p" + Convert.ToChar(RowIndex + 48) + Convert.ToChar(col + 48);
                    PictureBox p = (PictureBox) Controls[Controls.IndexOfKey(name)];
                    if (p != pOrg)
                    {
                        if (p.Image != null)
                        {
                            label1.Text = "Invalid Move!! try again";
                            return false;
                        }
                    }
                }
                for (int row = 0; row < 8; row++)
                {
                    string name = "p" + Convert.ToChar(row + 48) + Convert.ToChar(ColIndex + 48);
                    PictureBox p = (PictureBox) Controls[Controls.IndexOfKey(name)];
                    if (p != pOrg)
                    {
                        if (p.Image != null)
                        {
                            label1.Text = "Invalid Move!! try another one";
                            return false;
                        }
                    }
                }
                int dif = RowIndex - ColIndex;
                int sum = RowIndex + ColIndex;
                for (int i=0; i< 8; i++)
                {
                    if ((i - dif >= 0) && (i - dif < 8))
                    {
                        string name = "p" + Convert.ToChar(i + 48) + Convert.ToChar(i - dif + 48);
                        PictureBox p = (PictureBox)Controls[Controls.IndexOfKey(name)];
                        if (p != pOrg)
                        {
                            if (p.Image != null)
                            {
                                label1.Text = "Invalid Move!! try another one";
                                return false;
                            }
                        }
                    }
                    if ((sum - i >= 0) && (sum - i < 8))
                    {
                        string name = "p" + Convert.ToChar(i + 48) + Convert.ToChar(sum -i + 48);
                        PictureBox p = (PictureBox)Controls[Controls.IndexOfKey(name)];
                        if (p != pOrg)
                        {
                            if (p.Image != null)
                            {
                                label1.Text = "Invalid Move!! try another one";
                                return false;
                            }
                        }
                    }

                }
            }
            return true;
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            FirstSol.Enabled = true;
            NextSol.Enabled = true;
            PrevSol.Enabled = true;
            LastSol.Enabled = true;
            label1.Text = "";
            label3.Text = "";
            for (int i=0; i<8; i++)
            {
                for (int j=0; j<8; j++)
                {
                    string name = "p" + Convert.ToChar(i + 48) + Convert.ToChar(j + 48);
                    PictureBox p = (PictureBox)Controls[Controls.IndexOfKey(name)];
                    p.Image = null;
                }
            }
        }

        private void getSolution(object sender, EventArgs e)
        {
            ResetBtn_Click(sender, e);
            label3.Text = "Solution: "+Convert.ToString(solIndex + 1)+"/92";
            for (int col = 0; col < 8; col++)
            {
                int RowIndex = sol[solIndex][col];
                string name = "p" + Convert.ToChar(RowIndex - 1 + 48) + Convert.ToChar(col + 48);
                PictureBox p = (PictureBox)Controls[Controls.IndexOfKey(name)];
                p.Image = pictureBox1.Image;

            }
        }

        private void FirstSol_Click(object sender, EventArgs e)
        {
            solIndex = 0;
            getSolution(sender, e);
            PrevSol.Enabled = false;
            FirstSol.Enabled = false;
            NextSol.Enabled = true;
            LastSol.Enabled = true;
        }

        private void PrevSol_Click(object sender, EventArgs e)
        {
            solIndex --;
            getSolution(sender, e);
            if (solIndex == 0)
            {
                PrevSol.Enabled = false;
                FirstSol.Enabled = false;
            }
            NextSol.Enabled = true;
            LastSol.Enabled = true;
        }

        private void NextSol_Click(object sender, EventArgs e)
        {
            solIndex++;
            getSolution(sender, e);
            if (solIndex == 91)
            {
                NextSol.Enabled = false;
                LastSol.Enabled = false;
            }
            FirstSol.Enabled = true;
            PrevSol.Enabled = true;
        }

        private void LastSol_Click(object sender, EventArgs e)
        {
            solIndex = 91;
            getSolution(sender, e);
            PrevSol.Enabled = true;
            FirstSol.Enabled = true;
            NextSol.Enabled = false;
            LastSol.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

      
     
    }
}
