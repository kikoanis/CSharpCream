using System;
using System.Collections;
using System.Linq;

namespace Cream
{

	public class TabooSearch : LocalSearch
	{
		#region Fields

		protected internal Operation[] Taboo;

		protected internal int TabooI;

		public int TabooLength = 16;

		#endregion

		#region Constructors

		public TabooSearch(Network network, int option, String name)
			: base(network, option, name)
		{
			ExchangeRate = 0.8;
		}

		public TabooSearch(Network network, int option)
			: this(network, option, null)
		{
		}

		public TabooSearch(Network network)
			: this(network, Default, null)
		{
		}

		public TabooSearch(Network network, String name)
			: this(network, Default, name)
		{
		}

		#endregion

		#region Protected Methods

		protected internal virtual void AddTaboo(Operation op)
		{
			Taboo[TabooI] = op;
			TabooI = (TabooI + 1) % Taboo.Length;
		}

		protected internal virtual void ClearTaboo()
		{
			Taboo = new Operation[TabooLength];
			for (int i = 0; i < Taboo.Length; i++)
			{
				Taboo[i] = null;
			}
			TabooI = 0;
		}

		protected internal virtual bool IsTaboo(Operation op, Operation[] localTaboo)
		{
			if (localTaboo == null)
				return false;
			return localTaboo.Any(t => t != null && op.IsTaboo(t));
		}

		protected internal override void NextSearch()
		{
			Operation locallyBestOp = null;
			Solution locallyBestSol = null;
			int locallyBest = IntDomain.MaxValue;
			solution = GetCandidate();
			Code code = solution.Code;
			while (!Aborted)
			{
				IEnumerator ops = code.Operations().GetEnumerator();
				while (ops.MoveNext() && !Aborted)
				{
					var op = (Operation)ops.Current;
					if (IsTaboo(op, Taboo))
					{
						continue;
					}
					code.To = network;
					op.ApplyTo(network);
					Solution sol = solver.FindBest(iterTimeout);
					if (sol == null)
					{
						continue;
					}
					int objectiveIntValue = sol.ObjectiveIntValue;
					if (!IsBetter(objectiveIntValue, locallyBest))
					{
						continue;
					}
					locallyBest = objectiveIntValue;
					locallyBestOp = op;
					locallyBestSol = sol;
				}
				if (locallyBestOp != null)
				{
					break;
				}
				ClearTaboo();
			}
			code.To = network;
			if (locallyBestOp != null)
			{
				locallyBestOp.ApplyTo(network);
			}

			solution = locallyBestSol;
			AddTaboo(locallyBestOp);
		}

		protected internal override void StartSearch()
		{
			ClearTaboo();
			var solverStrategy = SolverStrategy;
			solver = new DefaultSolver(network, option) {SolverStrategy = solverStrategy};
			solution = solver.FindFirst();
		}

		#endregion

	}
}