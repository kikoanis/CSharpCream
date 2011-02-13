using System;

namespace  Cream
{
	
	public class IterativeBranchAndBoundSearch:LocalSearch
	{
		virtual public long IterationTimeout
		{
			set
			{
				iterTimeout = value;
			}
			
		}
		virtual public double ClearRate
		{
			set
			{
				_clearRate = value;
			}
			
		}
		private double _clearRate = 0.8;
		
		public IterativeBranchAndBoundSearch(Network network):this(network, Default, null)
		{
		}
		
		public IterativeBranchAndBoundSearch(Network network, int option):this(network, option, null)
		{
		}
		
		public IterativeBranchAndBoundSearch(Network network, String name):this(network, Default, name)
		{
		}
		
		public IterativeBranchAndBoundSearch(Network network, int option, String name):base(network, option, name)
		{
			ExchangeRate = 0.8;
		}
		

		protected internal virtual void  BranchAndBoundSearch()
		{
			if (Aborted)
				return ;
			for (solver.Start(iterTimeout); solver.WaitNext(); solver.Resume())
			{
				solution = solver.Solution;
				Success();
				if (Aborted)
					break;
			}
			solver.Stop();
			solution = solver.BestSolution;
		}
		
		protected internal override void  StartSearch()
		{
			var thisSolvertrateg = SolverStrategy;
			solver = new DefaultSolver(network, option) {SolverStrategy = thisSolvertrateg};
			//solution = solver.FindFirst();
			//solution = solver.FindBest(IterTimeout);
			BranchAndBoundSearch();
		}

		protected internal override void NextSearch()
		{
			if (Aborted)
				return ;
			solution = GetCandidate();
			Code code = solution.Code;
			code = (Code) code.Clone();
			Condition[] conditions = code.conditions;
			for (int i = 0; i < conditions.Length; i++)
			{
				if (SupportClass.Random.NextDouble() < _clearRate)
				{
					conditions[i] = null;
				}
			}
			code.To = network;
			BranchAndBoundSearch();
		}
	}
}