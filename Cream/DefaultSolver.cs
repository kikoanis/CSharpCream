// --------------------------------------------------------------------------------------------------------------------
// <copyright company="pc" file="DefaultSolver.cs">
//   2009
// </copyright>
// <summary>
//   A branch-and-bound solver.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Cream
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// A branch-and-bound solver.
	/// </summary>
	/// <seealso cref="Solver">
	/// </seealso>
	/// <since>1.0</since>
	/// <version>1.0, 01/12/08</version>
	/// <author>Based on Java Solver by : <c>Naoyuki</c> Tamura (tamura@kobe-u.ac.jp)
	/// C#: Ali Hmer (Hmer200a@uregina.ca)</author>
	public class DefaultSolver : Solver
	{
		#region Fields

		/// <summary>
		/// protected internal long timeSpent;
		/// </summary>
		private readonly IEnumerable<Constraint> _softConstraints;

		/// <summary>
		/// trail object
		/// </summary>
		protected Trail trail = new Trail();

		/// <summary>
		/// variable for equal soft constraints only
		/// </summary>
		private readonly IEnumerable<int> _valuesWhichHavePreferences;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultSolver"/> class.  Constructs a branch-and-bound solver for the given network.
		/// This constructor is equivalent to <tt>DefaultSolver(network, Default, null)</tt>.
		/// </summary>
		/// <param name="network">
		/// the constraint network
		/// </param>
		public DefaultSolver(Network network)
			: this(network, Default, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultSolver"/> class.  Constructs a branch-and-bound solver for the given network, options, and name.
		/// </summary>
		/// <param name="network">
		/// the constraint network
		/// </param>
		/// <param name="options">
		/// the options for search strategy, or Default for default search strategy
		/// </param>
		/// <param name="name">
		/// the name of the solver, or <tt>null</tt> for a default name
		/// </param>
		public DefaultSolver(Network network, int options, string name)
			: base(network, options, name)
		{
			// get all soft constraints
			_softConstraints = network.Constraints.Cast<Constraint>()
				.Where(soft => soft.CType == ConstraintTypes.Soft).ToArray();

			// get all values that have equal preferences
			_valuesWhichHavePreferences = _softConstraints
				.Where(soft => soft is Equals).Cast<Equals>()
				.Select(s => ((IntDomain)s.Vars[1].Domain).Minimum())
				.Distinct().
				Union(_softConstraints
						.Where(soft => soft is NotEquals).Cast<NotEquals>()
						.Select(s => ((IntDomain)s.Vars[1].Domain).Minimum())
						.Distinct()).ToArray();

			GenerateOnlySameOrBetterWeightedSolutions = false;
			Random = new Random();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultSolver"/> class.  Constructs a branch-and-bound solver for the given network and options.
		/// This constructor is equivalent to <tt>DefaultSolver(network, options, null)</tt>.
		/// </summary>
		/// <param name="network">
		/// the constraint network
		/// </param>
		/// <param name="options">
		/// the options for search strategy
		/// </param>
		public DefaultSolver(Network network, int options)
			: this(network, options, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultSolver"/> class.  Constructs a branch-and-bound solver for the given network and name.
		/// This constructor is equivalent to <tt>DefaultSolver(network, Default, name)</tt>.
		/// </summary>
		/// <param name="network">
		/// the constraint network
		/// </param>
		/// <param name="name">
		/// the name of the solver
		/// </param>
		public DefaultSolver(Network network, string name)
			: this(network, Default, name)
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets BestSolutionWeight.
		/// </summary>
		public int BestSolutionWeight { get; set; }


		public Random Random { get; set; }

		#endregion

		#region Public Methods

		/// <summary>
		/// gets value from soft constraints
		/// </summary>
		/// <param name="v0">
		/// The v0 (which is a of type variable).
		/// </param>
		/// <returns>
		/// int value represents value with soft constraints
		/// </returns>
		public int GetValueWithSoftConstraint(Variable v0)
		{
			if (Random == null)
			{
				Random = new Random();
			}
			// get V0's domain
			var d0 = (IntDomain)v0.Domain;

			// if there are soft constraints
			if (_softConstraints.Count() > 0)
			{
				// get the distinct values of the variables associated with all  
				// Equal soft constraints with V0
				var equalSoftConstraintsWithV0 =
					_softConstraints
						.Where(soft => soft is Equals).Cast<Equals>()
						.Where(cons => (cons.Vars[0] == v0))
						.Where(vars => vars.Vars[0].Domain.Size() > 1)
						.Where(cons => v0.Domain.Contains(((IntDomain)cons.Vars[1].Domain).Minimum()))
						.Select(doms => ((IntDomain)doms.Vars[1].Domain).Minimum())
						.Distinct().ToArray();

				// if there are only one just return it
				if (equalSoftConstraintsWithV0.Count() == 1)
				{
					return equalSoftConstraintsWithV0.ElementAt(0);
				}

				// get variables that are alreay assigned
				var varsDomainAlreadyAssigned = network.Variables.Cast<Variable>()
					.Where(vars => vars.Domain.Size() == 1 && !vars.IsValueType)

					// Size == 1 means already assigned
					// Where(vars => ((IntDomain) vars.Domain).Min() != 0)  ///???
					.Select(vars => ((IntDomain)vars.Domain).Minimum())
					.Where(vars => equalSoftConstraintsWithV0.Contains(vars)).ToArray();

				var counter = new int[equalSoftConstraintsWithV0.Count()];

				// if there are values assoiciated with v0 that already assigned
				if (varsDomainAlreadyAssigned.Count() > 0)
				{
					var counterForAssigned = 0;
					foreach (var cd in equalSoftConstraintsWithV0)
					{
						var cd1 = cd;
						for (var i = 0; i < varsDomainAlreadyAssigned.Where(vr => vr == cd1).Count(); i++)
						{
							counter[counterForAssigned]++;
						}
						counterForAssigned++;
					}
				}

				// to choose the value that least has been assigned from preferences with v0
				if (counter.Count() > 0)
				{
					IList<int> minIndexes = new List<int> { 0 };

					// int minIndex = 0;
					var minValue = counter[0];
					for (var i = 1; i < counter.Length; i++)
					{
						if (counter[i] < minValue)
						{
							minIndexes = new List<int> { i };
							minValue = counter[i];
						}
						else if (counter[i] == minValue)
						{
							minIndexes.Add(i);
						}
					}
					var index = Random.Next(0, minIndexes.Count);
					return equalSoftConstraintsWithV0.ElementAt(minIndexes[index]);
				}

				// Don't return values that have preferences
				// if they are minimum in domain
				// as they will be needed later or the preference is not equal 
				// which is not desirable anyway
				// unless nothing left in domain except those values
				var domainValues = d0.GetElements();
				IList<int> tempDomainValues = new List<int>(domainValues);
				foreach (var domainValue in from domainValue in tempDomainValues
				                            let value = domainValue
				                            where _valuesWhichHavePreferences.Any(val => value == val)
				                            select domainValue)
				{
					domainValues.Remove(domainValue);
				}

				if (domainValues.Count != 0)
				{
					// return domainValues.First();
					
					return domainValues[Random.Next(0, domainValues.Count)];
				}
			}

			// return one of the domain values randomly if there are no equal soft constrians
			// or you reached here by flow
			var elements = d0.GetElements();
			return elements[Random.Next(0, elements.Count)];

			// return d0.Min();
		}

		/// <summary>
		/// Run method
		/// </summary>
		public override void Run()
		{
			ClearBest();
			trail = new Trail();
			Solve(0);
			trail.Undo(0);
			Fail();
		}

		/// <summary>
		/// Selects a varaible for the current stage
		/// </summary>
		/// <returns>
		/// a variable of type Variable
		/// </returns>
		public virtual Variable SelectVariable()
		{
			Variable v = null;
			if (IsOption(Minimize) || IsOption(Maximize))
			{
				v = InfVariable();

				// v = MinimumSizeVariable();
			}

			// try to select the variable with highest weight on soft constraints
			// if there are soft constraints
			if (IsSoftConstraintsUsed())
			{
					// var varsAlreadyAssigned = (network.Variables.Cast<Variable>()
					// Where(vars => vars.Domain.Size() == 1 && vars.IsValueType == false)
					// Select(vars => new { var0 = vars }));//((IntDomain)vars.Domain).Min()));
				var variablesWhichHavePreferencesAndVar0HasNotBeenAssigned =
					//softConstraints
					//    .Where(soft => soft is Equals).Cast<Equals>() // the constraint is soft equal
					//    .Where(vars => vars.Vars[0].Domain.Size() > 1) // the first var is not assigned yet
					//    .Select(doms => new { var0 = doms.Vars[0], var1 = doms.Vars[1], weight = doms.Weight })
					//    .Distinct()
					//    .Union(
					//    softConstraints
					//        .Where(soft => soft is NotEquals).Cast<NotEquals>() // the constraint is soft not equal
					//        .Where(vars => vars.Vars[0].Domain.Size() > 1) // the first var is not assigned yet
					//        .Select(doms => new { var0 = doms.Vars[0], var1 = doms.Vars[1], weight = doms.Weight })
					//        .Distinct())
					//    .OrderBy(w => w.weight).ToArray(); // the most weighted first
						_softConstraints
						.Where(soft => soft is Equals).Cast<Equals>() // the constraint is soft equal
						.Where(vars => vars.Vars[0].Domain.Size() > 1) // the first var is not assigned yet
						.Select(doms => new { var0 = doms.Vars[0], type = "Equal", weight = doms.Weight })
						.Union(
						_softConstraints
							.Where(soft => soft is NotEquals).Cast<NotEquals>() // the constraint is soft not equal
							.Where(vars => vars.Vars[0].Domain.Size() > 1) // the first var is not assigned yet
							.Select(doms => new { var0 = doms.Vars[0], type = "NotEqual", weight = doms.Weight })
							)
                        .GroupBy(vars => new { vars.var0, vars.type })
						.Select(vars => new {vars.Key.var0, vars.Key.type, count = vars.Count()})
						.OrderByDescending(w => w.count).ToArray(); // the most weighted first

					if (variablesWhichHavePreferencesAndVar0HasNotBeenAssigned.Count() > 0)
					{
						v = variablesWhichHavePreferencesAndVar0HasNotBeenAssigned.First().var0;
					}
			}

			return v ?? MinimumSizeVariable();
		}

		private bool IsSoftConstraintsUsed()
		{
			return (SolverStrategy == StrategyMethod.Soft) && _softConstraints.Count() > 0;
		}

		/// <summary>
		/// Start method
		/// </summary>
		/// <param name="timeout">
		/// The timeout.
		/// </param>
		public override void Start(long timeout)
		{
			BestSolutionWeight = -1;
			base.Start(timeout);
		}

		#endregion

		#region Protected Methods

		/// <summary>
		/// InfVariable Method
		/// </summary>
		/// <returns>
		/// a variable of type Variable
		/// </returns>
		protected internal virtual Variable InfVariable()
		{
			Variable min = null;
			int infMin = Int32.MaxValue;
			IList<Variable> vars = network.Variables.Cast<Variable>().ToList();

			// we are only concerned about integer domains which size > 1 and they are not value type
			IEnumerable<Variable> nonAssignedVars =
				vars.Where(var => (var.Domain is IntDomain) && (var.Domain.sizeField > 1) && !var.IsValueType);
			foreach (Variable v in nonAssignedVars)
			{
				Domain d = v.Domain;

				int inf = ((IntDomain)d).Minimum();
				if (inf < infMin)
				{
					min = v;
					infMin = inf;
				}
			}

			return min;
		}

		/// <summary>
		/// MinimumSizeVariable method
		/// </summary>
		/// <returns>
		/// a variable of type Variable
		/// </returns>
		protected internal virtual Variable MinimumSizeVariable()
		{
			Variable min = null;
			IOrderedEnumerable<Variable> minimumSizeVariable =
				network.Variables.Cast<Variable>().Where(vars => vars.Domain.Size() > 1)
					.OrderBy(vars => vars.Domain.Size());
			if (minimumSizeVariable.Count() > 0)
			{
				min = minimumSizeVariable.First();
			}

			// int minSize = Int32.MaxValue;
			// foreach (Variable v in network.Variables)
			// {
			// int Size = v.Domain.Size();
			// if (1 < Size && Size <= minSize)
			// {
			// vMin = v;
			// minSize = Size;
			// }
			// }
			return min;
		}

		/// <summary>
		/// Modified constraints
		/// </summary>
		/// <returns>
		/// IList represents modified constraints
		/// </returns>
		protected IList ModifiedConstraints()
		{
			IEnumerable<Constraint> modifiedList = from cons in network.Constraints.Cast<Constraint>()
												   where cons.IsModified()
												   select cons;
			IList list = modifiedList.ToList();
			IEnumerable<Variable> vs = from vars in network.Variables.Cast<Variable>()
									   select vars;
			foreach (Variable v in vs)
			{
				v.ClearModified();
			}

			return list;
		}

		/// <summary>
		/// The Satisfy process will get rid of the domains items that are satisfied
		/// with the constraints
		/// </summary>
		/// <returns>
		/// boolean value represents whether the constraints are satisfied or not
		/// </returns>
		protected internal virtual bool Satisfy()
		{
			while (!Aborted)
			{
				var hardConstraints = ModifiedConstraints().Cast<Constraint>().Where(cons => cons.CType == ConstraintTypes.Hard);
				if (hardConstraints.Any(c => !c.Satisfy(trail)))
				{
					return false; // hard constraint broken track back
				}

				if (network.Variables.Cast<Variable>().Where(vars => vars.IsModified()).Count() == 0)
				{
					// all constraints are satisfied and no more modified variables 
					break;
				}
				
			}
			return true;
		}

		/// <summary>
		/// Solves the specified level.
		/// </summary>
		/// <param name="level">
		/// The level.
		/// </param>
		protected internal virtual void Solve(int level)
		{

			var objective = network.Objective;
			while (!Aborted)
			{
				if (IsOption(Minimize))
				{
					if (bestValue < IntDomain.MaxValue)
					{
						var d = DeleteDomainFromBestValueToMaxValue(objective);
						if (d.Empty)
						{
							break;
						}
						objective.UpdateDomain(d, trail);
					}
				}
				else if (IsOption(Maximize))
				{
					if (bestValue > IntDomain.MinValue)
					{
						var d = DeleteDomainFromMinValueToBestValue(objective);
						if (d.Empty)
						{
							break;
						}
						objective.UpdateDomain(d, trail);
					}
				}

				var sat = Satisfy();
				if (Aborted || !sat)
				{
					break;
				}
				var v0 = SelectVariable();
				if (v0 == null)
				{
					// if it is the sameweight or better of the best solution 
					GetGeneratedSolution();
					break;
				}

				if (v0.Domain is IntDomain)
				{
					var d = (IntDomain)v0.Domain;
					switch (SolverStrategy)
					{
						case StrategyMethod.Soft:
							var val = GetValueWithSoftConstraint(v0);
							if (!Aborted)
							{
								var t0 = trail.Size();
								v0.UpdateDomain(new IntDomain(val), trail);
								Solve(level + 1);
								trail.Undo(t0);
							}

							if (!Aborted)
							{
								v0.UpdateDomain(d.Delete(val), trail);
								continue;
							}

							break;

						case StrategyMethod.Step:
							var value = d.Minimum();
							if (!Aborted)
							{
								var t0 = trail.Size();
								v0.UpdateDomain(new IntDomain(value), trail);
								Solve(level + 1);
								trail.Undo(t0);
							}

							if (!Aborted)
							{
								v0.UpdateDomain(d.Delete(value), trail);
								continue;
							}

							break;

						case StrategyMethod.Enum:
							var iter = v0.Domain.Elements();
							while (!Aborted && iter.MoveNext())
							{
								var t0 = trail.Size();
								v0.UpdateDomain((Domain)iter.Current, trail);
								Solve(level + 1);
								trail.Undo(t0);
							}

							break;

						case StrategyMethod.Bisect:
							int mid;
							if (d.Minimum() + 1 == d.Maximum())
							{
								mid = d.Minimum();
							}
							else
								mid = (d.Minimum() + d.Maximum()) / 2;
							if (!Aborted)
							{
								var t0 = trail.Size();
								v0.UpdateDomain(d.CapInterval(d.Minimum(), mid), trail);
								Solve(level + 1);
								trail.Undo(t0);
							}

							if (!Aborted)
							{
								var t0 = trail.Size();
								v0.UpdateDomain(d.CapInterval(mid + 1, d.Maximum()), trail);
								Solve(level + 1);
								trail.Undo(t0);
							}

							break;
					}
				}
				else
				{
					var iter = v0.Domain.Elements();
					while (!Aborted && iter.MoveNext())
					{
						var t0 = trail.Size();
						// Domain d1 = v0.Domain;
						v0.UpdateDomain((Domain)iter.Current, trail);
						// if (v0.Domain.Elements() != d1.Elements())
						Solve(level + 1);
						trail.Undo(t0);
					}
				}

				break;
			}
		}

		private IntDomain DeleteDomainFromMinValueToBestValue(Variable objective)
		{
			var d = (IntDomain)objective.Domain;
			d = d.Delete(IntDomain.MinValue, bestValue);
			return d;
		}

		private IntDomain DeleteDomainFromBestValueToMaxValue(Variable objective)
		{
			var d = (IntDomain)objective.Domain;
			d = d.Delete(bestValue, IntDomain.MaxValue);
			return d;
		}

		private void GetGeneratedSolution()
		{
			var solutionWeight = GetSolutionWeight();

			// timeSpentToCheck = DateTime.Now.Ticks - timeSpentToCheck;
			if ((!IsBetterSolution(solutionWeight) || SolverStrategy != StrategyMethod.Soft) &&
			    SolverStrategy == StrategyMethod.Soft) return;
			solution = new Solution(network) {Weight = solutionWeight, Time = GetElapsedTime()};
			//Console.WriteLine("solution: " + solution);
			//File.AppendAllText("c:\\test.out", "---------------------------------------\n");
			//File.AppendAllText("c:\\test.out", network.ToString());
			Success();
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// gets solution weight
		/// </summary>
		/// <returns>
		/// an integer
		/// </returns>
		private int GetSolutionWeight()
		{
			var totalWeight = _softConstraints.Where(c => c.IsSatisfied()).Sum(c => c.Weight);

			BestSolutionWeight = totalWeight < BestSolutionWeight ? BestSolutionWeight : totalWeight;
			return totalWeight;
		}

		/// <summary>
		/// a method to check if it's a better solution
		/// </summary>
		/// <param name="totalWeight">
		/// The total weight.
		/// </param>
		/// <returns>
		/// boolean value
		/// </returns>
		private bool IsBetterSolution(int totalWeight)
		{
			if (GenerateOnlySameOrBetterWeightedSolutions)
			{
				if (totalWeight < BestSolutionWeight && BestSolutionWeight != -1)
				{
					return false; // total weight of this partial solution

					// is less then total weight of the best solution 
					// track back
				}

				return true;
			}

			return true;
		}

		#endregion
	}
}