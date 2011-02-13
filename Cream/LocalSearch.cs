/*
* @(#)LocalSearch.cs
*/
using System;

namespace  Cream
{
	
	/// <summary> A super class of local search solvers, and also
	/// an implementation of a random walk solver.
	/// Local search is an iterative procedure.
	/// It first finds an initial soluttion, and iteratively
	/// make a small change
	/// </summary>
	/// <seealso cref="Solver">
	/// </seealso>
	/// <since> 1.0
	/// </since>
	/// <version>  1.0, 01/12/08
	/// </version>
    /// <author>  Based on Java Solver by : Naoyuki Tamura (tamura@kobe-u.ac.jp) 
    ///           C#: Ali Hmer (Hmer200a@uregina.ca)
	/// </author>
	public class LocalSearch:Solver
	{
        private bool _newCandidate;
        private Solution _candidate;
        private double _exchangeRate = 0.5;
		
		/// <summary> Sets a GetCandidate solution for a next iteration.</summary>
        // <param name="Candidate">the GetCandidate
		virtual public Solution Candidate
		{
            get
            {
                return _candidate;
            }
			set
			{
				lock (this)
				{
					if (value != null)
					{
						_candidate = value;
						_newCandidate = true;
					}
				}
			}
			
		}
		public double ExchangeRate
		{
			get
			{
				return _exchangeRate;
			}
			
			set
			{
				_exchangeRate = value;
			}
			
		}
		public long iterTimeout = 5000;
		protected internal int iteration;
		protected internal DefaultSolver solver;
		
		/// <summary> Constructs a random-walk solver for the given network.
		/// This constructor is equivalent to <tt>LocalSearch(network, Default, null)</tt>.
		/// </summary>
		/// <param name="network">the constraint network
		/// </param>
		public LocalSearch(Network network):this(network, Default, null)
		{
		}
		
		/// <summary> Constructs a random-walk solver for the given network and option.
		/// This constructor is equivalent to <tt>LocalSearch(network, option, null)</tt>.
		/// </summary>
		/// <param name="network">the constraint network
		/// </param>
		/// <param name="option">the option for search strategy
		/// </param>
		public LocalSearch(Network network, int option):this(network, option, null)
		{
		}
		
		/// <summary> Constructs a random-walk solver for the given network and name.
		/// This constructor is equivalent to <tt>LocalSearch(network, Default, name)</tt>.
		/// </summary>
		/// <param name="network">the constraint network
		/// </param>
		/// <param name="name">the name of the solver
		/// </param>
		public LocalSearch(Network network, String name):this(network, Default, name)
		{
		}
		
		/// <summary> Constructs a random-walk solver for the given network, option, and name.</summary>
		/// <param name="network">the constraint network
		/// </param>
		/// <param name="option">the option for search strategy, or Default for default search strategy
		/// </param>
		/// <param name="name">the name of the solver, or <tt>null</tt> for a default name
		/// </param>
		public LocalSearch(Network network, int option, String name):base(network, option, name)
		{
		}
		
		protected internal virtual Solution GetCandidate()
		{
			lock (this)
			{
				if (_newCandidate)
				{
					_newCandidate = false;
					return _candidate;
				}
			    if (solution != null)
			    {
			        return solution;
			    }
			    return bestSolution;
			}
		}
		
		public override void  Stop()
		{
			lock (this)
			{
				if (solver != null)
					solver.Stop();
				base.Stop();
			}
		}
		
		protected internal virtual void  StartSearch()
		{
			var thisSolverStrategy = SolverStrategy;
			solver = new DefaultSolver(network, option) {SolverStrategy = thisSolverStrategy};
			solution = solver.FindFirst();
		}

		protected internal virtual void NextSearch()
		{
			solution = GetCandidate();
			Code code = solution.Code;
			System.Collections.IList operations = code.Operations();
			while (operations.Count > 0)
			{
				var i = (int) (operations.Count * SupportClass.Random.NextDouble());
			    object tempObject = operations[i];
				operations.RemoveAt(i);
				var op = (Operation) tempObject;
				code.To = network;
				op.ApplyTo(network);
				Solution sol = solver.FindBest(iterTimeout);
				if (sol == null)
					continue;
				solution = sol;
				return ;
			}
			solution = null;
		}
		
		protected internal virtual void  EndSearch()
		{
			solver = null;
		}
		
		public override void  Run()
		{
			iteration = 0;
			ClearBest();
			var thisSolverStrategy = SolverStrategy;
			StartSearch();
			SolverStrategy = thisSolverStrategy;
			
			// StartSearch(SolverStrategy);
			while (!Aborted && solution != null)
			{
				iteration++;
				Success();
				if (Aborted)
					break;
				NextSearch();
				if (Aborted)
					break;
				//if (solution == null)
				//{
				//    solution = bestSolution;
				//}
			}
			Fail();
			EndSearch();
		}
	}
}