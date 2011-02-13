/*
* @(#)ParallelSolver.cs
*/
using System;

namespace  Cream
{
	
	public class ParallelSolver:Solver, ISolutionHandler
	{
		protected internal Solver[] solvers;
		
		public ParallelSolver(Solver[] solvers):this(solvers, null)
		{
		}
		
		public ParallelSolver(Solver[] solvers, String name):base(null, None, name)
		{
			this.solvers = solvers;
			network = solvers[0].network;
			option = solvers[0].option;
		}
		
		public override void  SetMonitor(Monitor mon)
		{
			foreach (Solver t in solvers)
			{
				t.SetMonitor(mon);
			}
		}

		public override void  Stop()
		{
			lock (this)
			{
				foreach (Solver t in solvers)
				{
					t.Stop();
				}
				base.Stop();
			}
		}
		
		public virtual void  Solved(Solver solver, Solution sol)
		{
			lock (this)
			{
				if (Aborted || sol == null)
				{
					return ;
				}
				int oldBestValue = bestValue;
				solution = sol;
				Success();
				if (!(solver is LocalSearch))
					return ;
				if (network.Objective == null)
					return ;
				int objectiveIntValue = sol.ObjectiveIntValue;
				if (!IsBetter(objectiveIntValue, oldBestValue))
				{
					double rate;
					{
						rate = ((LocalSearch) solver).ExchangeRate;
					}
					if (SupportClass.Random.NextDouble() < rate)
					{
						//System.out.println(header + "Get " + best);
						((LocalSearch) solver).Candidate = bestSolution;
					}
				}
			}
		}
		
		private void  AllStart()
		{
			foreach (Solver t in solvers)
			{
				lock (t)
				{
					{
						t.Start(this, totalTimeout);
					}
				}
			}
		}

		private void  AllJoin()
		{
			// Add synchronized statement
			// Modified by Muneyuki Kawatani 05/12/09
			lock (solvers)
			{
				foreach (Solver t in solvers)
				{
					lock (t)
					{
						{
							t.Join();
						}
					}
				}
			}
		}
		
		public override void  Run()
		{
			ClearBest();
			AllStart();
			AllJoin();
			Fail();
		}
	}
}