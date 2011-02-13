using System;
using System.Drawing;
using System.Windows.Forms;
using Cream;

namespace FourColor
{
/*    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
    }
}

/*
* @(#)Form1.java
*/
/*namespace examples
{*/
	
	/// <summary> Four color problem.
	/// See Martin Gardner: Mathematical Games, Scientific American, April, 1975.
	/// 
	/// </summary>
	/// <author>  Naoyuki Tamura (tamura@kobe-u.ac.jp)
	/// </author>
	//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
	[Serializable]
	public partial class Form1:Form
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(Form1 enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Form1 thisInstance)
			{
				this.enclosingInstance = thisInstance;
			}
			private Form1 enclosingInstance;
			public Form1 Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(Object event_sender, EventArgs e)
			{
				Enclosing_Instance.actionNext();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener1
		{
			public AnonymousClassActionListener1(Form1 enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Form1 thisInstance)
			{
				this.enclosingInstance = thisInstance;
			}
			private Form1 enclosingInstance;
			public Form1 Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(Object event_sender, EventArgs e)
			{
				Enclosing_Instance.actionQuit();
			}
		}
		private const long serialVersionUID = - 8807342308085600468L;
		
		internal static int[][] map = new int[][]{new int[]{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, new int[]{2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 11, 11, 1}, new int[]{2, 12, 12, 12, 13, 13, 13, 13, 14, 14, 14, 14, 15, 15, 15, 15, 16, 16, 16, 16, 17, 17, 17, 17, 18, 18, 18, 18, 19, 19, 19, 19, 20, 20, 20, 11, 1}, new int[]{2, 12, 12, 21, 21, 21, 22, 22, 22, 22, 23, 23, 23, 23, 24, 24, 24, 24, 25, 25, 25, 25, 26, 26, 26, 26, 27, 27, 27, 27, 28, 28, 28, 20, 20, 11, 1}, new int[]{2, 12, 12, 21, 21, 29, 29, 29, 30, 30, 30, 30, 31, 31, 31, 31, 32, 32, 32, 32, 33, 33, 33, 33, 34, 34, 34, 34, 35, 35, 35, 28, 28, 20, 20, 11, 1}, new int[]{2, 12, 12, 21, 21, 29, 29, 36, 36, 36, 37, 37, 37, 37, 38, 38, 38, 38, 39, 39, 39, 39, 40, 40, 40, 40, 41, 41, 41, 35, 35, 28, 28, 20, 20, 11, 1}, new int[]{2, 12, 12, 21, 21, 29, 29, 36, 36, 42, 42, 42, 43, 43, 43, 43, 44, 44, 44, 44, 45, 45, 45, 45, 46, 46, 46, 41, 41, 35, 35, 28, 28, 20, 20, 11, 1}, new int[]{2, 12, 12, 21, 21, 29, 29, 36, 36, 42, 42, 47, 47, 47, 48, 48, 48, 48, 49, 49, 49, 49, 50, 50, 50, 46, 46, 41, 41, 35, 35, 28, 28, 20, 20, 11, 1}, new int[]{2, 12, 12, 21, 21, 29, 29, 36, 36, 42, 42, 47, 47, 51, 51, 51, 52, 52, 52, 52, 53, 53, 53, 50, 50, 46, 46, 41, 41, 35, 35, 28, 28, 20, 20, 11, 1}, new int[]{2, 12, 12, 21, 21, 29, 29, 36, 36, 42, 42, 47, 47, 51, 51, 54, 54, 54, 55, 55, 55, 53, 53, 50, 50, 46, 46, 41, 41, 35, 35, 28, 28, 20, 20, 11, 1}, new int[]{2, 12, 12, 21, 21, 29, 29, 36, 36, 42, 42, 47, 47, 51, 51, 54, 54, 56, 56, 55, 55, 53, 53, 50, 50, 46, 46, 41, 41, 35, 35, 28, 28, 20, 20, 11, 1}, new int[]{2, 57, 58, 58, 59, 59, 60, 60, 61, 61, 62, 62, 63, 63, 64, 64, 65, 56, 56, 66, 67, 67, 68, 68, 69, 69, 70, 70, 71, 71, 72, 72, 73, 73, 1, 1, 1}, new int[]{2, 57, 58, 58, 59, 59, 60, 60, 61, 61, 62, 62, 63, 63, 64, 64, 65, 65, 66, 66, 67, 67, 68, 68, 69, 69, 70, 70, 71, 71, 72, 72, 73, 73, 1, 1, 1}, new int[]{2, 57, 58, 
			58, 59, 59, 60, 60, 61, 61, 62, 62, 63, 63, 64, 64, 64, 74, 74, 67, 67, 67, 68, 68, 69, 69, 70, 70, 71, 71, 72, 72, 73, 73, 1, 1, 1}, new int[]{2, 57, 58, 58, 59, 59, 60, 60, 61, 61, 62, 62, 63, 63, 63, 75, 75, 75, 76, 76, 76, 68, 68, 68, 69, 69, 70, 70, 71, 71, 72, 72, 73, 73, 1, 1, 1}, new int[]{2, 57, 58, 58, 59, 59, 60, 60, 61, 61, 62, 62, 62, 77, 77, 77, 77, 78, 78, 79, 79, 79, 79, 69, 69, 69, 70, 70, 71, 71, 72, 72, 73, 73, 1, 1, 1}, new int[]{2, 57, 58, 58, 59, 59, 60, 60, 61, 61, 61, 80, 80, 80, 80, 81, 81, 81, 82, 82, 82, 83, 83, 83, 83, 70, 70, 70, 71, 71, 72, 72, 73, 73, 1, 1, 1}, new int[]{2, 57, 58, 58, 59, 59, 60, 60, 60, 84, 84, 84, 84, 85, 85, 85, 85, 86, 86, 87, 87, 87, 87, 88, 88, 88, 88, 71, 71, 71, 72, 72, 73, 73, 1, 1, 1}, new int[]{2, 57, 58, 58, 59, 59, 59, 89, 89, 89, 89, 90, 90, 90, 90, 91, 91, 91, 92, 92, 92, 93, 93, 93, 93, 94, 94, 94, 94, 72, 72, 72, 73, 73, 1, 1, 1}, new int[]{2, 57, 58, 58, 58, 95, 95, 95, 95, 96, 96, 96, 96, 97, 97, 97, 97, 98, 98, 99, 99, 99, 99, 100, 100, 100, 100, 101, 101, 101, 101, 73, 73, 73, 1, 1, 1}, new int[]{2, 57, 57, 102, 102, 102, 102, 103, 103, 103, 103, 104, 104, 104, 104, 105, 105, 105, 106, 106, 106, 107, 107, 107, 107, 108, 108, 108, 108, 109, 109, 109, 109, 1, 1, 1, 1}, new int[]{2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2}};
		
		internal static int[][] neighbors = new int[][]{new int[]{1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 20, 73, 109, 110}, new int[]{2, 1, 3, 12, 57, 102, 103, 104, 105, 106, 110}, new int[]{3, 1, 2, 4, 12, 13}, new int[]{4, 1, 3, 5, 13, 14}, new int[]{5, 1, 4, 6, 14, 15}, new int[]{6, 1, 5, 7, 15, 16}, new int[]{7, 1, 6, 8, 16, 17}, new int[]{8, 1, 7, 9, 17, 18}, new int[]{9, 1, 8, 10, 18, 19}, new int[]{10, 1, 9, 11, 19, 20}, new int[]{11, 1, 10, 20}, new int[]{12, 2, 3, 13, 21, 57, 58}, new int[]{13, 3, 4, 12, 14, 21, 22}, new int[]{14, 4, 5, 13, 15, 22, 23}, new int[]{15, 5, 6, 14, 16, 23, 24}, new int[]{16, 6, 7, 15, 17, 24, 25}, new int[]{17, 7, 8, 16, 18, 25, 26}, new int[]{18, 8, 9, 17, 19, 26, 27}, new int[]{19, 9, 10, 18, 20, 27, 28}, new int[]{20, 1, 10, 11, 19, 28, 73}, new int[]{21, 12, 13, 22, 29, 58, 59}, new int[]{22, 13, 14, 21, 23, 29, 30}, new int[]{23, 14, 15, 22, 24, 30, 31}, new int[]{24, 15, 16, 23, 25, 31, 32}, new int[]{25, 16, 17, 24, 26, 32, 33}, new int[]{26, 17, 18, 25, 27, 33, 34}, new int[]{27, 18, 19, 26, 28, 34, 35}, new int[]{28, 19, 20, 27, 35, 72, 73}, new int[]{29, 21, 22, 30, 36, 59, 60}, new int[]{30, 22, 23, 29, 31, 36, 37}, new int[]{31, 23, 24, 30, 32, 37, 38}, new int[]{32, 24, 25, 31, 33, 38, 39}, new int[]{33, 25, 26, 32, 34, 39, 40}, new int[]{34, 26, 27, 33, 35, 40, 41}, new int[]{35, 27, 28, 34, 41, 71, 72}, new int[]{36, 29, 30, 37, 42, 60, 61}, new int[]{37, 30, 31, 36, 38, 42, 43}, new int[]{38, 31, 32, 37, 39, 43, 44}, new int[]{39, 32, 33, 38, 40, 44, 45}, new int[]{40, 33, 34, 39, 41, 45, 46}, new int[]{41, 34, 35, 40, 46, 70, 71}, new int[]{42, 36, 37, 43, 47, 61, 62}, new int[]{43, 37, 38, 42, 44, 47, 48}, new int[]{44, 38, 39, 43, 45, 48, 49}, new int[]{45, 39, 40, 44, 46, 49, 50}, new int[]{46, 40, 41, 45, 50, 69, 70}, new int[]{47, 42, 43, 48, 51, 62, 63}, new int[]{48, 43, 44, 47, 49, 51, 52}, new int[]{49, 44, 45, 48, 50, 52, 53}, new int[]{50, 45, 46, 49, 53, 68, 69}, new int[]{51, 47, 48, 52, 54, 63, 64}, new int[]{52, 48, 49, 51, 53, 54, 55}, new 
			int[]{53, 49, 50, 52, 55, 67, 68}, new int[]{54, 51, 52, 55, 56, 64, 65}, new int[]{55, 52, 53, 54, 56, 66, 67}, new int[]{56, 54, 55, 65, 66}, new int[]{57, 2, 12, 58, 102}, new int[]{58, 12, 21, 57, 59, 95, 102}, new int[]{59, 21, 29, 58, 60, 89, 95}, new int[]{60, 29, 36, 59, 61, 84, 89}, new int[]{61, 36, 42, 60, 62, 80, 84}, new int[]{62, 42, 47, 61, 63, 77, 80}, new int[]{63, 47, 51, 62, 64, 75, 77}, new int[]{64, 51, 54, 63, 65, 74, 75}, new int[]{65, 54, 56, 64, 66, 74}, new int[]{66, 55, 56, 65, 67, 74}, new int[]{67, 53, 55, 66, 68, 74, 76}, new int[]{68, 50, 53, 67, 69, 76, 79}, new int[]{69, 46, 50, 68, 70, 79, 83}, new int[]{70, 41, 46, 69, 71, 83, 88}, new int[]{71, 35, 41, 70, 72, 88, 94}, new int[]{72, 28, 35, 71, 73, 94, 101}, new int[]{73, 1, 20, 28, 72, 101, 109}, new int[]{74, 64, 65, 66, 67, 75, 76}, new int[]{75, 63, 64, 74, 76, 77, 78}, new int[]{76, 67, 68, 74, 75, 78, 79}, new int[]{77, 62, 63, 75, 78, 80, 81}, new int[]{78, 75, 76, 77, 79, 81, 82}, new int[]{79, 68, 69, 76, 78, 82, 83}, new int[]{80, 61, 62, 77, 81, 84, 85}, new int[]{81, 77, 78, 80, 82, 85, 86}, new int[]{82, 78, 79, 81, 83, 86, 87}, new int[]{83, 69, 70, 79, 82, 87, 88}, new int[]{84, 60, 61, 80, 85, 89, 90}, new int[]{85, 80, 81, 84, 86, 90, 91}, new int[]{86, 81, 82, 85, 87, 91, 92}, new int[]{87, 82, 83, 86, 88, 92, 93}, new int[]{88, 70, 71, 83, 87, 93, 94}, new int[]{89, 59, 60, 84, 90, 95, 96}, new int[]{90, 84, 85, 89, 91, 96, 97}, new int[]{91, 85, 86, 90, 92, 97, 98}, new int[]{92, 86, 87, 91, 93, 98, 99}, new int[]{93, 87, 88, 92, 94, 99, 100}, new int[]{94, 71, 72, 88, 93, 100, 101}, new int[]{95, 58, 59, 89, 96, 102, 103}, new int[]{96, 89, 90, 95, 97, 103, 104}, new int[]{97, 90, 91, 96, 98, 104, 105}, new int[]{98, 91, 92, 97, 99, 105, 106}, new int[]{99, 92, 93, 98, 100, 106, 107}, new int[]{100, 93, 94, 99, 101, 107, 108}, new int[]{101, 72, 73, 94, 100, 108, 109}, new int[]{102, 2, 57, 58, 95, 103}, new int[]{103, 2, 95, 96, 102, 104}, new int[]{104, 2, 96, 97, 103, 105}, new int[]{105
			, 2, 97, 98, 104, 106}, new int[]{106, 2, 98, 99, 105, 107, 110}, new int[]{107, 99, 100, 106, 108, 110}, new int[]{108, 100, 101, 107, 109, 110}, new int[]{109, 1, 73, 101, 108, 110}, new int[]{110, 1, 2, 106, 107, 108, 109}};
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'Form1Panel' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.awt.Panel' and 'System.Windows.Forms.Panel' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		[Serializable]
		internal class Form1Panel:Panel
		{
			private void  InitBlock(Form1 thisInstance)
			{
				this.enclosingInstance = thisInstance;
			}
			private Form1 enclosingInstance;
			//UPGRADE_NOTE: Synchronized keyword was removed from method 'setColor'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
			virtual public int[] Color
			{
				set
				{
					lock (this)
					{
						this.color = value;
					}
				}
				
			}
			public Form1 Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = - 871639673567464376L;
			
			internal int[][] map;
			
			internal int[] color;
			
			internal Color[] ct = new[]{System.Drawing.Color.LightGray, System.Drawing.Color.White, System.Drawing.Color.Orange, System.Drawing.Color.Cyan, System.Drawing.Color.Green};
			
			internal Form1Panel(Form1 enclosingInstance, int[][] map)
			{
				InitBlock(enclosingInstance);
				this.map = map;
			}
			
			protected override void  OnPaint(PaintEventArgs g_EventArg)
			{
				Graphics g = null;
				if (g_EventArg != null)
					g = g_EventArg.Graphics;
				if (map == null)
					return ;
				int h = Size.Height - 10;
				int w = Size.Width - 10;
				int M = map.Length;
				int N = map[0].Length;
				int cell_h = h / M;
				int cell_w = w / N;
				int y0 = (Size.Height - M * cell_h) / 2;
				int x0 = (Size.Width - N * cell_w) / 2;
				int x, y, c;
				if (color != null)
				{
					for (int i = 0; i < M; ++i)
					{
						for (int j = 0; j < N; ++j)
						{
							x = x0 + j * cell_w;
							y = y0 + i * cell_h;
							c = color[map[i][j]];
							SupportClass.GraphicsManager.Manager.SetColor(g, ct[c]);
						    if (g != null) g.FillRectangle(SupportClass.GraphicsManager.Manager.GetPaint(g), x, y, cell_w, cell_h);
						}
					}
				}
				SupportClass.GraphicsManager.Manager.SetColor(g, System.Drawing.Color.Black);
			    if (g != null) g.DrawRectangle(SupportClass.GraphicsManager.Manager.GetPen(g), x0, y0, N * cell_w, M * cell_h);
			    for (int i = 0; i < M; ++i)
				{
					for (int j = 0; j < N; ++j)
					{
						x = x0 + j * cell_w;
						y = y0 + i * cell_h;
						if (i < M - 1 && map[i][j] != map[i + 1][j])
						{
						    if (g != null)
						        g.DrawLine(SupportClass.GraphicsManager.Manager.GetPen(g), x, y + cell_h, x + cell_w, y + cell_h);
						}
						if (j < N - 1 && map[i][j] != map[i][j + 1])
						{
						    if (g != null)
						        g.DrawLine(SupportClass.GraphicsManager.Manager.GetPen(g), x + cell_w, y, x + cell_w, y + cell_h);
						}
					}
				}
			}
		}
		
		internal Form1Panel form1Panel;
		
		internal Button nextButton;
		
		internal Button quitButton;
		
		internal static int WAIT;
		
		internal static int NEXT = 1;
		
		internal static int QUIT = 2;
		
		internal int state = WAIT;
		
		public Form1(int[][] map)
		{
			//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_int_int'"
			Size = new Size(400, 400);
			//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
			//UPGRADE_ISSUE: Constructor 'java.awt.BorderLayout.BorderLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtBorderLayout'"
			/*
			setLayout(new BorderLayout());*/
			form1Panel = new Form1Panel(this, map);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javalangString_javaawtComponent'"
			Controls.Add(form1Panel);
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.awt.Panel' and 'System.Windows.Forms.Panel' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			Panel p = new Panel();
		    Button temp_Button = new Button();
			temp_Button.Text = "Next";
			nextButton = temp_Button;
			nextButton.Click += new AnonymousClassActionListener(this).actionPerformed;
			SupportClass.CommandManager.CheckCommand(nextButton);
			Button temp_Button2;
			temp_Button2 = new Button();
			temp_Button2.Text = "Quit";
			quitButton = temp_Button2;
			quitButton.Click += new AnonymousClassActionListener1(this).actionPerformed;
			SupportClass.CommandManager.CheckCommand(quitButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			p.Controls.Add(nextButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			p.Controls.Add(quitButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javalangString_javaawtComponent'"
			Controls.Add(p);
			//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			Visible = true;
		}
		
		private void  setSolution(Solution solution, IntVariable[] region)
		{
			int n = region.Length;
			int[] color = new int[n + 1];
			for (int i = 0; i < n; i++)
			{
				IntDomain d = (IntDomain) solution.GetDomain(region[i]);
				if (d.Size() == 1)
				{
					color[i + 1] = d.Value();
				}
				else
				{
					color[i + 1] = 0;
				}
			}
			form1Panel.Color = color;
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			form1Panel.Refresh();
		}
		
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'setStateWait'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		private void  setStateWait()
		{
			lock (this)
			{
				state = WAIT;
			}
		}
		
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'actionNext'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		private void  actionNext()
		{
			lock (this)
			{
				state = NEXT;
				System.Threading.Monitor.PulseAll(this);
			}
		}
		
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'actionQuit'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		private void  actionQuit()
		{
			lock (this)
			{
				state = QUIT;
				System.Threading.Monitor.PulseAll(this);
			}
		}
		
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'waitAction'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		private void  waitAction()
		{
			lock (this)
			{
				while (state == WAIT)
				{
					try
					{
						System.Threading.Monitor.Wait(this);
					}
					catch (System.Threading.ThreadInterruptedException e)
					{
                        throw new Exception(e.Message);
					}
				}
			}
		}
		
		protected override void  OnPaint(PaintEventArgs g_EventArg)
		{
			Graphics g = null;
			if (g_EventArg != null)
				g = g_EventArg.Graphics;
			//UPGRADE_ISSUE: Method 'java.awt.Container.paintComponents' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainerpaintComponents_javaawtGraphics'"
            
            //this.Container.Add(g);
           
			//paintComponents(g);
		}

		public Form1()
		{
            InitializeComponent();
            new Form1(map);
            //Form1 fc = new Form1(map);
            Network net = new Network();
            int n = neighbors.Length;
            IntVariable[] region = new IntVariable[n];
            for (int i = 0; i < n; i++)
                region[i] = new IntVariable(net, 1, 4);
            for (int i = 0; i < n; i++)
            {
                IntVariable v = region[neighbors[i][0] - 1];
                for (int j = 1; j < neighbors[i].Length; ++j)
                    if (neighbors[i][0] < neighbors[i][j])
                        v.NotEquals(region[neighbors[i][j] - 1]);
            }
            Solver solver = new DefaultSolver(net);
            solver.Start();
            //while (true)
            {
                //fc.setStateWait();
                //fc.waitAction();
                //if (fc.state == QUIT)
                 //   break;
                if (!solver.WaitNext())
                   // break;
                setSolution(solver.Solution, region);
                solver.Resume();
                
            }
            solver.Stop();
		}
	}
}


