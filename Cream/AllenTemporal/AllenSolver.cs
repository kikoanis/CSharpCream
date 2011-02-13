// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AllenSolver.cs" company="U of R">
//   2008-2009
// </copyright>
// <summary>
//   Defines the AllenSolver type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace Cream.AllenTemporal
{
    using System.Collections;
    using Cream;

    /// <summary>
    /// Allen solver class
    /// </summary>
    public class AllenSolver : DefaultSolver
    {
        #region Fields

        /// <summary>
        /// AC3 queue list
        /// </summary>
        private IList _ac3Queue;

        /// <summary>
        /// list of disjunctive constraint
        /// </summary>
        private IList[] _disjuctiveCons;

        /// <summary>
        /// list of pc2 queue
        /// </summary>
        public IList pc2Queue;

        /// <summary>
        /// boolean value for reductionDOne
        /// </summary>
        private bool _reductionDone;

        /// <summary>
        /// list network variables
        /// </summary>
        private IList[] _varNetwork;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AllenSolver"/> class.
        /// </summary>
        /// <param name="network">
        /// The network.
        /// </param>
        public AllenSolver(Network network)
            : base(network)
        {
            Choice = (int) StrategyMethod.Step;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AllenSolver"/> class.
        /// </summary>
        /// <param name="network">
        /// The network.
        /// </param>
        /// <param name="name">
        /// The name of the problem.
        /// </param>
        internal AllenSolver(Network network, string name)
            : base(network, name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AllenSolver"/> class.
        /// </summary>
        /// <param name="network">
        /// The network.
        /// </param>
        /// <param name="options">
        /// The options.
        /// </param>
        public AllenSolver(Network network, int options)
            : base(network, options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AllenSolver"/> class.
        /// </summary>
        /// <param name="network">
        /// The network.
        /// </param>
        /// <param name="options">
        /// The options.
        /// </param>
        /// <param name="name">
        /// The name of the problem
        /// </param>
        public AllenSolver(Network network, int options, string name)
            : base(network, options, name)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets strategy method
        /// </summary>
        public int Choice { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Revise method
        /// </summary>
        /// <param name="x">
        /// The x.variable
        /// </param>
        /// <param name="y">
        /// The y.variable
        /// </param>
        /// <param name="relations">
        /// The relations.
        /// </param>
        /// <returns>
        /// boolean value
        /// </returns>
        public static bool Revise(Variable x, Variable y, int[] relations)
        {
            var d1 = (AllenDomain)x.Domain;
            var d2 = (AllenDomain)y.Domain;
            bool rev = false;
            if (d1.Empty)
            {
                return false;
            }

            if (d2.Empty)
            {
                return false;
            }

            for (int st1 = d1.Min(); st1 <= d1.Max(); st1 += d1.Step)
            {
                bool found = false;
                for (int st2 = d2.Min(); st2 <= d2.Max(); st2 += d2.Step)
                {
                    if (Compatible(st1, st2, d1.Duration, d2.Duration, relations))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    rev = true;
                    ((AllenDomain)x.Domain).Remove(st1);
                    if (x.Domain.Size() == 0)
                    {
                        break;
                    }
                }
            }

            return rev;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// This method is based on Dr Malek Mouhoub Tempor application
        /// http://www2.cs.uregina.ca/~mouhoubm/=postscript/=papers/32mouh.ps.gz
        ///  </summary>
        /// <param name="level">integer value for the level of the solver</param>
        protected internal override void Solve(int level)
        {
            // do it only once
            if (!_reductionDone)
            {
                ConvertNumericToSymbolic(); // convert from nuymeric to symbolic
                PathConsistency(); // PC2
                // AC3.1
                if (!ArcConsistency())
                {
                    return;
                }

                // ConvertNumericToSymbolic(); // convert from nuymeric to symbolic
                GetDisjunctiveConstraints(); // get all edges' disjunctive constraints
                _reductionDone = true;
            }

            Variable objective = network.Objective;
            while (!Aborted)
            {
                if (IsOption(Minimize))
                {
                    if (bestValue < IntDomain.MaxValue)
                    {
                        var d = (IntDomain)objective.Domain;
                        d = d.Delete(bestValue, IntDomain.MaxValue);
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
                        var d = (IntDomain)objective.Domain;
                        d = d.Delete(IntDomain.MinValue, bestValue);
                        if (d.Empty)
                        {
                            break;
                        }

                        objective.UpdateDomain(d, trail);
                    }
                }

                bool sat = SatisfyDisjCons();
                if (Aborted || !sat)
                {
                    break;
                }

                Variable v0 = SelectVariable();
                if (v0 == null)
                {
                    IEnumerator v = network.Variables.GetEnumerator();
                    while (v.MoveNext())
                    {
                        if (((AllenVariable)v.Current).Domain.Size() == 0)
                        {
                            break;
                        }
                    }

                    solution = new Solution(network);
                    Success();
                    break;
                }

                if (v0.Domain is AllenDomain)
                {
                    var d = (AllenDomain)v0.Domain;
                    switch (Choice)
                    {
                        case (int)StrategyMethod.Step:
                            int value = d.Min();
                            if (!Aborted)
                            {
                                int t0 = trail.Size();
                                v0.UpdateDomain(new AllenDomain(value, value + d.Duration, d.Duration, d.Step), trail);
                                Solve(level + 1);
                                trail.Undo(t0);
                            }

                            if (!Aborted)
                            {
                                v0.UpdateDomain(d.Delete(value), trail);
                                continue;
                            }

                            break;

                        case (int)StrategyMethod.Enum:
                            IEnumerator iter = v0.Domain.Elements();
                            while (!Aborted && iter.MoveNext())
                            {
                                int t0 = trail.Size();
                                v0.UpdateDomain((Domain)iter.Current, trail);
                                Solve(level + 1);
                                trail.Undo(t0);
                            }

                            break;

                        case (int)StrategyMethod.Bisect:
                            int mid;
                            if (d.Min() + 1 == d.Max())
                            {
                                mid = d.Min();
                            }
                            else
                                mid = (d.Min() + d.Max()) / 2;
                            if (!Aborted)
                            {
                                int t0 = trail.Size();
                                v0.UpdateDomain(d.CapInterval(d.Min(), mid), trail);
                                Solve(level + 1);
                                trail.Undo(t0);
                            }

                            if (!Aborted)
                            {
                                int t0 = trail.Size();
                                v0.UpdateDomain(d.CapInterval(mid + 1, d.Max()), trail);
                                Solve(level + 1);
                                trail.Undo(t0);
                            }

                            break;
                    }
                }
                else
                {
                    IEnumerator iter = v0.Domain.Elements();
                    while (!Aborted && iter.MoveNext())
                    {
                        int t0 = trail.Size();
                        v0.UpdateDomain((Domain)iter.Current, trail);
                        Solve(level + 1);
                        trail.Undo(t0);
                    }
                }

                break;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Arc consistency method
        /// </summary>
        /// <returns>
        /// boolean value
        /// </returns>
        private bool ArcConsistency()
        {
            QueueUpVarsForAC();
            int n = network.Variables.Count;
            while (_ac3Queue.Count > 0)
            {
                var obj = (object[])_ac3Queue[0];
                var x = (AllenVariable)obj[0];
                var y = (AllenVariable)obj[1];
                var relations = (int[])obj[2];
                _ac3Queue.RemoveAt(0);
                if (Revise(x, y, relations))
                {
                    if (x.Domain.Size() > 0)
                    {
                        for (int i = 0; i < n; i++)
                        {
                            var v1 = (AllenVariable)network.Variables[i];
                            if ((v1 != y) && (v1 != x))
                            {
                                var v2 = (AllenVariable)x.Clone();
                                var con = new object[3];
                                con[0] = v1; // first variable
                                con[1] = v2; // second variable
                                int[] events = GetEvents(v1, v2);
                                if (events.Length > 0)
                                {
                                    con[2] = events;
                                    _ac3Queue.Add(con); // Add to queue
                                }
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// checks for the compatibility of variables
        /// </summary>
        /// <param name="a">
        /// The a.variable
        /// </param>
        /// <param name="b">
        /// The b.variable
        /// </param>
        /// <param name="d1">
        /// The d 1.variable
        /// </param>
        /// <param name="d2">
        /// The d 2.valirable
        /// </param>
        /// <param name="relations">
        /// The relations.
        /// </param>
        /// <returns>
        /// boolean value
        /// </returns>
        private static bool Compatible(int a, int b, int d1, int d2, IEnumerable<int> relations)
        {
        	return relations.Any(t => AllenEvents.Satisfy(t, a, b, d1, d2));
        }

    	/// <summary>
        /// converts numeric to symbolic
        /// </summary>
        private void ConvertNumericToSymbolic()
        {
            IEnumerator cs = network.Constraints.GetEnumerator(); // do it only once
            IList constraintsToBeDeleted = new ArrayList();
            while (cs.MoveNext())
            {
                if (!((AllenConstraint)cs.Current).NumericToSymbolic())
                {
                    constraintsToBeDeleted.Add(cs.Current);

                    // think about it.
                    if (network.Constraints.Count == 0)
                    {
                        break;
                    }
                }
            }

            foreach (Constraint c in constraintsToBeDeleted)
            {
                network.Constraints.Remove(c);
            }
        }

        /// <summary>
        /// gets disjunctive constraints
        /// </summary>
        private void GetDisjunctiveConstraints()
        {
            int n = network.Variables.Count;
            int noOfPossibleEdges = n * (n - 1) / 2;

            // for (int i = 1; i < n; noOfPossibleEdges += i, i++) ;
            var tempDisjuctiveCons = new IList[noOfPossibleEdges];
            int c = 0;
            int realc = 0;
            for (int i = 0; i < n; i++)
            {
                var v1 = (AllenVariable)network.Variables[i];
                for (int j = i + 1; j < n; j++)
                {
                    tempDisjuctiveCons[c] = new ArrayList();
                    var v2 = (AllenVariable)network.Variables[j];
                    IEnumerator cs = network.Constraints.GetEnumerator();
                    while (cs.MoveNext())
                    {
                        var var1 = (AllenVariable)((AllenConstraint)cs.Current).Vars[0];
                        var var2 = (AllenVariable)((AllenConstraint)cs.Current).Vars[1];
                        if (((var1 == v1) && (var2 == v2)) || ((var1 == v2) && (var2 == v1)))
                        {
                            tempDisjuctiveCons[c].Add(cs.Current);
                        }
                    }

                    if (tempDisjuctiveCons[c].Count > 0)
                    {
                        realc++;
                    }

                    c++;
                }
            }

            _disjuctiveCons = new IList[realc];
            int cc = 0;
            for (int j = 0; j < c; j++)
            {
                if (tempDisjuctiveCons[j].Count > 0)
                {
                    _disjuctiveCons[cc++] = tempDisjuctiveCons[j];
                }
            }
        }

        /// <summary>
        /// gets all events
        /// </summary>
        /// <param name="v1">
        /// The v 1.variable
        /// </param>
        /// <param name="v2">
        /// The v 2.varaible
        /// </param>
        /// <returns>
        /// array of integer values
        /// </returns>
        private int[] GetEvents(Variable v1, Variable v2)
        {
            IEnumerator cs = network.Constraints.GetEnumerator();
            var temp = new int[13];
            for (int i = 0; i < 13; i++)
            {
                temp[i] = -1;
            }

            int counter = 0;
            while (cs.MoveNext())
            {
                var c = (Constraint)cs.Current;
                if ((c is AllenConstraint) &&
                    ((c as AllenConstraint).Vars[0] == v1) &&
                    ((c as AllenConstraint).Vars[1] == v2))
                {
                    var c1 = (AllenConstraint)c;
                    temp[counter++] = c1.AllenEvent;
                }
            }

            var events = new int[counter];
            for (int j = 0; j < counter; j++)
            {
                events[j] = temp[j];
            }

            return events;
        }

        /// <summary>
        /// Path consistency implementatiopn
        /// </summary>
        private void PathConsistency()
        {
            if (network.Variables.Count < 3)
            {
                return;
            }

            QueueUpVarsForPC();
            while (pc2Queue.Count > 0)
            {
                var obj = (object[])pc2Queue[0];
                var x = (AllenVariable)obj[0];
                var y = (AllenVariable)obj[1];
                int xInd = x.Index;
                int yInd = y.Index;
                pc2Queue.RemoveAt(0);
                IEnumerator kv = network.Variables.GetEnumerator();
                while (kv.MoveNext())
                {
                    var k = (AllenVariable)kv.Current;
                	if ((k == x) || k == y) continue;
                	var kInd = k.Index;

                	var objXK = (object[])_varNetwork[xInd][kInd];
                	var cxk = (int[])objXK[2];

                	var objXY = (object[])_varNetwork[xInd][yInd];
                	var cxy = (int[])objXY[2];

                	var objYK = (object[])_varNetwork[yInd][kInd];
                	var cyk = (int[])objYK[2];

                	int[] t = AllenEvents.Composition(cxy, cyk);
                	t = AllenEvents.Intersection(t, cxk);
                	if (AllenEvents.IsEqual(t, cxk) != true)
                	{
                		var con2 = (object[])_varNetwork[xInd][kInd];
                		con2[2] = t; // allen events
                		_varNetwork[xInd][kInd] = con2; // Cxk = t
                		con2 = (object[])_varNetwork[kInd][xInd];

                		// con2[0] = k;
                		// con2[1] = x;
                		con2[2] = AllenEvents.Inverse(t); // allen events
                		_varNetwork[kInd][xInd] = con2; // Ckx = t
                		var con1 = new object[2];
                		con1[0] = x; // first variable
                		con1[1] = k; // second variable
                		pc2Queue.Add(con1);
                	}

                	objXY = (object[])_varNetwork[xInd][yInd];
                	cxy = (int[])objXY[2];

                	var objKY = (object[])_varNetwork[kInd][yInd];
                	var cky = (int[])objKY[2];

                	var objKX = (object[])_varNetwork[kInd][xInd];
                	var ckx = (int[])objKX[2];
                	t = AllenEvents.Composition(ckx, cxy);
                	t = AllenEvents.Intersection(t, cky);
                	if (AllenEvents.IsEqual(t, cky) != true)
                	{
                		var con2 = (object[])_varNetwork[yInd][kInd];
                		con2[2] = AllenEvents.Inverse(t); // allen events
                		_varNetwork[yInd][kInd] = con2;  // Cxk = t
                		var con = new object[2];
                		con[0] = k; // first variable
                		con[1] = y; // second variable
                		pc2Queue.Add(con);
                	}
                }
            }

            // Remove the old constraints
            while (network.Constraints.Count > 0)
            {
                network.Constraints.RemoveAt(0);
            }

            // Add the new constraints
            for (int i = 0; i < network.Variables.Count; i++)
            {
                for (int j = i + 1; j < network.Variables.Count; j++)
                {
                    var obj1 = (object[])_varNetwork[i][j];
                    var a = (int[])obj1[2];
                	if (AllenEvents.IsEqual(a, AllenEvents.I)) continue;
                	foreach (var aa in a)
                	{
                		var var1 = (Variable)obj1[0];
                		var var2 = (Variable)obj1[1];
                		new AllenConstraint(network, aa, var1, var2);
                	}
                }
            }
        }

        /// <summary>
        /// quies varaibles for AC
        /// </summary>
        private void QueueUpVarsForAC()
        {
            int n = network.Variables.Count;
            _ac3Queue = new ArrayList();
            for (int i = 0; i < n; i++)
            {
                var v1 = (AllenVariable)network.Variables[i];
                for (int j = i + 1; j < n; j++)
                {
                    var v2 = (AllenVariable)network.Variables[j];
                    var con = new object[3];
                    con[0] = v1; // first variable
                    con[1] = v2; // second variable
                    int[] events = GetEvents(v1, v2);
                    if (events.Length > 0)
                    {
                        con[2] = events;
                        _ac3Queue.Add(con); // Add to queue
                    }

                    con = new object[3];
                    con[0] = v2;
                    con[1] = v1;
                    events = GetEvents(v2, v1);
                    if (events.Length > 0)
                    {
                        con[2] = events;
                        _ac3Queue.Add(con);
                    }
                }
            }
        }

        /// <summary>
        /// queries variables for PC
        /// </summary>
        private void QueueUpVarsForPC()
        {
            IEnumerator v1 = network.Variables.GetEnumerator();
            _varNetwork = new IList[network.Variables.Count];
            pc2Queue = new ArrayList();
            int rowC = 0;
            while (v1.MoveNext())
            {
                IEnumerator v2 = network.Variables.GetEnumerator();
                _varNetwork[rowC] = new ArrayList();
                while (v2.MoveNext())
                {
                    if (v1.Current != v2.Current)
                    {
                        var con1 = new object[2];
                        con1[0] = v1.Current; // first variable
                        con1[1] = v2.Current; // second variable
                        pc2Queue.Add(con1); // Add to queue
                        int[] events = GetEvents((Variable)v1.Current, (Variable)v2.Current);
                        events = AllenEvents.Union(events, AllenEvents.Inverse(GetEvents((Variable)v2.Current, (Variable)v1.Current)));
                        if (events.Length == 0)
                        {
                            events = AllenEvents.I; // assign I interval to events;
                        }

                        var con2 = new object[3];
                        con2[0] = con1[0];
                        con2[1] = con1[1];
                        con2[2] = events; // allen events
                        _varNetwork[rowC].Add(con2);
                    }
                    else
                    {
                        _varNetwork[rowC].Add(new object[3]);     // Add this item just to be consistent with the network
                    }
                }

                rowC++;
            }
        }

        /// <summary>
        /// Checks the consistency of the disjunctive constraints between every two varaibles in the
        /// network
        /// </summary>
        /// <returns>returns true if the disjunctive of all the constraints between every two variables
        /// is true, otherwise false.</returns>
        private bool SatisfyDisjCons()
        {
            var dc = _disjuctiveCons.GetEnumerator();
            while (dc.MoveNext())
            {
                var edgeConstraints = (ArrayList)dc.Current;
                var satisfied = edgeConstraints.Cast<Constraint>().Aggregate(false, (current, c) => current | c.Satisfy(trail));

            	if (!satisfied)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
