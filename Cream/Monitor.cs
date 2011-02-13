using System;
using System.Drawing;
using System.Collections;
namespace  Cream
{
	
	[Serializable]
	public class Monitor:System.Windows.Forms.Form
	{
        delegate void SetTextCallback(string text);
        
        private sealed class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(Monitor enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Monitor enclosingInstance)
			{
				EnclosingInstance = enclosingInstance;
			}

        	private Monitor EnclosingInstance { get; set; }

            public void  ActionPerformed(Object eventSender, EventArgs e)
			{
				EnclosingInstance.Dispose();
			}
		}
		private sealed class AnonymousClassActionListener1
		{
		    public AnonymousClassActionListener1(Monitor enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Monitor enclosingInstance)
			{
				EnclosingInstance = enclosingInstance;
			}
//			private Monitor enclosingInstance;

			private Monitor EnclosingInstance { get; set; }

		    public static void  ActionPerformed(Object eventSender, EventArgs e)
			{
				Environment.Exit(0);
			}
		}
		private long _startTime;
		private ArrayList _solvers;
        private Hashtable _solverData;
		private int _currentX;
		private int _xmin;
		private int _xmax;
		private int _ymin;
		private int _ymax;
		
		private Image _image;
		private long _prevPaintTime;
		private const int TopMargin = 100;
		private const int BotMargin = 50;
		private const int LeftMargin = 50;
		private const int RightMargin = 50;
		private double _xscale;
		private double _yscale;
		private readonly Color[] _color = new[]{Color.Red, Color.Blue, Color.FromArgb(0, 128, 0), Color.FromArgb(0, 128, 128), Color.Magenta, Color.Green, Color.FromArgb(128, 128, 0), Color.Pink};
		
		public Monitor()
		{
			Init();
			Size = new Size(800, 600);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
			MaximizeBox = true;
			var close = new System.Windows.Forms.MenuItem("Close");
			close.Click += new AnonymousClassActionListener(this).ActionPerformed;
			SupportClass.CommandManager.CheckCommand(close);
			var quit = new System.Windows.Forms.MenuItem("Quit");
			quit.Click += AnonymousClassActionListener1.ActionPerformed;
			SupportClass.CommandManager.CheckCommand(quit);
			var menu = new System.Windows.Forms.MenuItem("Window");
			menu.MenuItems.Add(close);
			menu.MenuItems.Add(quit);
			var mb = new System.Windows.Forms.MainMenu();
			mb.MenuItems.Add(menu);
			Menu = mb;
			//setsetBackground(Color.White);
			Invalidate();
			Visible = true;
		}
		
		public void  Init()
		{
			lock (this)
			{
				_startTime = (DateTime.Now.Ticks - 621355968000000000) / 10000;
				_solvers = new ArrayList();
				_solverData = new Hashtable();
				_currentX = 0;
				SetX(0, 10);
				_ymin = Int32.MaxValue;
				_ymax = Int32.MinValue;
			}
		}
		
		public virtual void  SetX(int xMIN, int xMAX)
		{
			lock (this)
			{
				_xmin = Math.Max(0, xMIN);
				_xmax = Math.Max(_xmin + 60, xMAX);
			}
		}
		
		public virtual void  ADD(Solver solver)
		{
			lock (this)
			{
				_solvers.Add(solver);
			}
		}
		
		public virtual void  ADDData(Solver solver, int y)
		{
			lock (this)
			{
				long t0 = (DateTime.Now.Ticks - 621355968000000000) / 10000;
				var x = (int) ((t0 - _startTime) / 1000);
				int j = x - _xmin;
				if (x < _xmin)
					return ;
				if (x > _xmax)
				{
					_xmax = _xmin + 4 * j / 3;
					if (_xmax % 60 != 0)
						_xmax += 60 - _xmax % 60;
				}
				_currentX = Math.Max(_currentX, x);
				_ymin = Math.Min(_ymin, y);
				_ymax = Math.Max(_ymax, y);
				var data = (Int32[]) _solverData[solver];
				if (data == null)
				{
					data = new Int32[_xmax - _xmin];
					_solverData.Add(solver, data);
				}
				if (j >= data.Length)
				{
					var newData = new Int32[4 * j / 3];
					for (int i = 0; i < data.Length; i++)
						newData[i] = data[i];
					data = newData;
					_solverData.Add(solver, data);
				}
				data[j] = y;
				if (_image == null || t0 - _prevPaintTime >= 1000)
				{
					RefreshThis("d");
					_prevPaintTime = t0;
				}
			}
		}
		
        private void RefreshThis(String text)
        {
            if (InvokeRequired)
            {
                var d = new SetTextCallback(RefreshThis);
                Invoke(d, new object[] { text });
            }
            else
            {
                Refresh();
            }
            
        }
		private int Wpos(int x)
		{
			return LeftMargin + (int) (_xscale * (x - _xmin));
		}
		
		private int Hpos(int y)
		{
			return TopMargin + (int) (_yscale * (_ymax - y));
		}
		
		private void  DrawLine(Graphics g, int x0, int y0, int x1, int y1)
		{
			g.DrawLine(SupportClass.GraphicsManager.Manager.GetPen(g), Wpos(x0), Hpos(y0), Wpos(x1), Hpos(y1));
		}
		
		private void  UpdateImage(int width, int height)
		{
			lock (this)
			{
				_image = new Bitmap(width, height);
				int w = width - (LeftMargin + RightMargin);
				int h = height - (TopMargin + BotMargin);
				if (w <= 0 || h <= 0)
					return ;
				if (_xmin >= _xmax || _ymin >= _ymax)
					return ;
				_xscale = w / (double) (_xmax - _xmin);
				_yscale = h / (double) (_ymax - _ymin);
				Graphics g = Graphics.FromImage(_image);
				// x-axis
				/////////////g.setColor(Color.LightGray);
				DrawLine(g, _xmin, _ymin, _xmax, _ymin);
				DrawLine(g, _xmin, _ymax, _xmax, _ymax);
				////////////////////g.setColor(Color.Black);
				g.DrawString(Convert.ToString(_xmax), SupportClass.GraphicsManager.Manager.GetFont(g), SupportClass.GraphicsManager.Manager.GetBrush(g), Wpos(_xmax), Hpos(_ymin) + BotMargin / 4 - SupportClass.GraphicsManager.Manager.GetFont(g).GetHeight());
				// y-axis
				DrawLine(g, _xmin, _ymin, _xmin, _ymax);
// ReSharper disable PossibleLossOfFraction
				g.DrawString(Convert.ToString(_ymin), SupportClass.GraphicsManager.Manager.GetFont(g), SupportClass.GraphicsManager.Manager.GetBrush(g), LeftMargin / 3, Hpos(_ymin) + 5 - SupportClass.GraphicsManager.Manager.GetFont(g).GetHeight());
// ReSharper restore PossibleLossOfFraction
// ReSharper disable PossibleLossOfFraction
				g.DrawString(Convert.ToString(_ymax), SupportClass.GraphicsManager.Manager.GetFont(g), SupportClass.GraphicsManager.Manager.GetBrush(g), LeftMargin / 3, Hpos(_ymax) + 5 - SupportClass.GraphicsManager.Manager.GetFont(g).GetHeight());
// ReSharper restore PossibleLossOfFraction
				g.DrawString("time=" + _currentX, SupportClass.GraphicsManager.Manager.GetFont(g), SupportClass.GraphicsManager.Manager.GetBrush(g), Wpos(_xmin), Hpos(_ymin) + BotMargin / 2 - SupportClass.GraphicsManager.Manager.GetFont(g).GetHeight());
				
				for (int i = 0; i < _solvers.Count; i++)
				{
					var solver = (Solver) _solvers[i];
                    
					var data = (Int32?[]) _solverData[solver];
					if (data == null)
						continue;
					SupportClass.GraphicsManager.Manager.SetColor(g, _color[i % _color.Length]);
					String msg = solver + "=" + solver.BestValue;
					g.DrawString(msg, SupportClass.GraphicsManager.Manager.GetFont(g), SupportClass.GraphicsManager.Manager.GetBrush(g), Wpos(_xmin) + 100 * (i + 1), Hpos(_ymin) + BotMargin / 2 - SupportClass.GraphicsManager.Manager.GetFont(g).GetHeight());
					int? x0 = - 1;
					int? y0 = - 1;
					for (int j = 0; j < data.Length; j++)
					{
						if (data[j] == null)
							continue;
						int? x = _xmin + j;
						int? y = data[j];
						if (x0 >= 0)
						{
							if (y0 != null) if (y != null) DrawLine(g, (Int32)x0, (Int32)y0, (Int32)x, (Int32)y);
						}
						x0 = x;
						y0 = y;
					}
				}
			}
		}
		
		public void  Update(Graphics g)
		{
			Size size = Size;
			UpdateImage(size.Width, size.Height);
			g.DrawImage(_image, 0, 0);
		}
		
		protected override void  OnPaint(System.Windows.Forms.PaintEventArgs gEventArg)
		{
			Graphics g = null;
			if (gEventArg != null)
				g = gEventArg.Graphics;
			if (_image != null)
			{
			    if (g != null) g.DrawImage(_image, 0, 0);
			}
		}
	}
}