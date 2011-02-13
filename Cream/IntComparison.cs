using System;

namespace Cream
{

	public class IntComparison : Constraint
	{

		#region Constants

		public const int Ge = 2;

		public const int Gt = 3;

		public const int Le = 0;

		public const int Lt = 1;

		#endregion

		#region Fields

		private readonly int _comparison;

		private readonly Variable[] _v;

		#endregion

		#region Constructors

		public IntComparison(Network net, int comp, Variable v0, int x1)
			: this(net, comp, v0, new IntVariable(net, x1))
		{
		}

		public IntComparison(Network net, int comp, Variable v0, Variable v1, ConstraintTypes cType)
			: this(net, comp, new[] { v0, v1 }, cType)
		{
		}

		public IntComparison(Network net, int comp, int x0, Variable v1)
			: this(net, comp, new IntVariable(net, x0), v1)
		{
		}

		public IntComparison(Network net, int comp, Variable v0, int x1, ConstraintTypes cType, int weight)
			: this(net, comp, v0, new IntVariable(net, x1), cType, weight)
		{
		}

		private IntComparison(Network net, int comp, Variable[] v, ConstraintTypes cType = ConstraintTypes.Hard)
			: this(net, comp, v, cType, 0)
		{
		}

		private IntComparison(Network net, int comp, Variable[] v, ConstraintTypes cType, int weight)
			: base(net, cType, weight)
		{
			_comparison = comp;
			_v = v;
		}

		public IntComparison(Network net, int comp, int x0, Variable v1, ConstraintTypes cType, int weight)
			: this(net, comp, new IntVariable(net, x0), v1, cType, weight)
		{
		}

		public IntComparison(Network net, int comp, Variable v0, int x1, ConstraintTypes cType)
			: this(net, comp, v0, new IntVariable(net, x1), cType)
		{
		}

		public IntComparison(Network net, int comp, int x0, Variable v1, ConstraintTypes cType)
			: this(net, comp, new IntVariable(net, x0), v1, cType)
		{
		}

		public IntComparison(Network net, int comp, Variable v0, Variable v1)
			: this(net, comp, new[] { v0, v1 })
		{
		}

		public IntComparison(Network net, int comp, Variable v0, Variable v1, ConstraintTypes cType, int weight)
			: this(net, comp, new[] { v0, v1 }, cType, weight)
		{
		}

		#endregion

		#region Properties

		public Variable[] Vars
		{
			get
			{
				return _v;
			}
		}

		#endregion

		#region Public Methods

		public override String ToString()
		{
			String c = "";
			switch (_comparison)
			{

				case Le:
					c = "Le"; break;

				case Lt:
					c = "Lt"; break;

				case Ge:
					c = "Ge"; break;

				case Gt:
					c = "Gt"; break;
			}
			return "IntComparison(" + c + "," + ToString(_v) + ")";
		}

		#endregion

		#region Protected Methods

		protected internal override Constraint Copy(Network net)
		{
			return new IntComparison(net, _comparison, Copy(_v, net));
		}

		protected internal override bool IsModified()
		{
			return IsModified(_v);
		}

		protected internal override bool IsSatisfied()
		{
			return Satisfy(null);
		}

		protected internal override bool Satisfy(Trail trail)
		{
			switch (_comparison)
			{

				case Le:
					return SatisfyLE(_v[0], _v[1], trail);

				case Lt:
					return SatisfyLT(_v[0], _v[1], trail);

				case Ge:
					return SatisfyLE(_v[1], _v[0], trail);

				case Gt:
					return SatisfyLT(_v[1], _v[0], trail);
			}
			return false;
		}

		#endregion

		#region Private Methods

		private static bool SatisfyLE(Variable v0, Variable v1, Trail trail)
		{
			var d0 = (IntDomain)v0.Domain;
			var d1 = (IntDomain)v1.Domain;
			d0 = d0.CapInterval(IntDomain.MinValue, d1.Maximum());
			if (d0.Empty)
				return false;
			if (trail != null)
			{
				v0.UpdateDomain(d0, trail);
			}
			d1 = d1.CapInterval(d0.Minimum(), IntDomain.MaxValue);
			if (d1.Empty) 
				return false;
			if (trail != null)
			{
				v1.UpdateDomain(d1, trail);
			}
			return true;
		}

		private static bool SatisfyLT(Variable v0, Variable v1, Trail trail)
		{
			var d0 = (IntDomain)v0.Domain;
			var d1 = (IntDomain)v1.Domain;
			d0 = d0.CapInterval(IntDomain.MinValue, d1.Maximum() - 1);
			if (d0.Empty)
				return false;
			if (trail != null)
			{
				v0.UpdateDomain(d0, trail);
			}
			d1 = d1.CapInterval(d0.Minimum() + 1, IntDomain.MaxValue);
			if (d1.Empty)
				return false;
			if (trail != null)
			{
				v1.UpdateDomain(d1, trail);
			}
			return true;
		}

		#endregion

	}
}