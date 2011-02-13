using System;
using System.Windows.Forms;
using QueensGame;
using SudokuGame;

namespace MainMenu
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QueenPuzzle qg = new QueenPuzzle();
            qg.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SudokuMainForm sg = new SudokuMainForm();
            sg.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
