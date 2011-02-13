// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IntVariable.cs" company="U of R">
//   Copyright 2008-2009
// </copyright>
// <summary>
//   Integer variables.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Cream
{
	using System;

	/// <summary> Integer variables.</summary>
	/// <seealso cref="IntDomain">
	/// </seealso>
	/// <since> 1.0
	/// </since>
	/// <version>  1.0, 01/12/08
	/// </version>
    /// <author>  Based on Java Solver by : Naoyuki Tamura (tamura@kobe-u.ac.jp) 
    ///           C#: Ali Hmer (Hmer200a@uregina.ca)
	/// </author>
	public class IntVariable : Variable
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="IntVariable"/> class.  Constructs an integer variable of the network
		/// with an initial integer domain <tt>d</tt>
		/// and a default name.
		/// This constructor is equivalent to <tt>IntVariable(network, d, null)</tt>.
		/// </summary>
		/// <param name="net">
		/// the network
		/// </param>
		/// <param name="d">
		/// the initial integer domain
		/// </param>
		public IntVariable(Network net, Domain d) : this(net, d, null)
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="IntVariable"/> class.  Constructs an integer variable of the network
		/// with an initial integer domain <tt>d</tt>
		/// and a name specified by the parameter <tt>name</tt>.
		/// When the parameter <tt>name</tt> is <tt>null</tt>,
		/// default names (<tt>v1</tt>, <tt>v2</tt>, and so on) are used.
		/// </summary>
		/// <param name="net">
		/// the network
		/// </param>
		/// <param name="d">
		/// the initial integer domain
		/// </param>
		/// <param name="name">
		/// the name of the variable, or <tt>null</tt> for a default name
		/// </param>
		public IntVariable(Network net, Domain d, string name) : base(net, d, name)
		{
			IsValueType = false;
        }

        public IntVariable(Network net, String name):this(net, IntDomain.fullDomain, name)
        {
        }

		public IntVariable(Network net):this(net, IntDomain.fullDomain)
		{
		}
		
		public IntVariable(Network net, int value):this(net, new IntDomain(value))
		{
		    IsValueType = true;
		}
		
		public IntVariable(Network net, int value, String name):this(net, new IntDomain(value), name)
		{
            IsValueType = true;
        }
		
		public IntVariable(Network net, int lo, int hi):this(net, new IntDomain(lo, hi))
		{
		}
		
		public IntVariable(Network net, int lo, int hi, String name):this(net, new IntDomain(lo, hi), name)
		{
		}
		
		public virtual IntVariable Add(IntVariable v)
		{
			Network net = Network;
			var x = new IntVariable(net);
			// x = this + v
			new IntArith(net, IntArith.Add, x, this, v);
			return x;
		}
		
		public virtual IntVariable Add(int value)
		{
			Network net = Network;
			var x = new IntVariable(net);
			// x = this + value
			new IntArith(net, IntArith.Add, x, this, new IntVariable(net, value));
			return x;
		}
		
		public virtual IntVariable Subtract(IntVariable v)
		{
			Network net = Network;
			var x = new IntVariable(net);
			// x = this - v
			new IntArith(net, IntArith.Subtract, x, this, v);
			return x;
		}
		
		public virtual IntVariable Subtract(int value)
		{
			Network net = Network;
			var x = new IntVariable(net);
			// x = this - value
			new IntArith(net, IntArith.Subtract, x, this, new IntVariable(net, value));
			return x;
		}
		
		public virtual IntVariable Multiply(IntVariable v)
		{
			Network net = Network;
			var x = new IntVariable(net);
			// x = this * v
			new IntArith(net, IntArith.MULTIPLY, x, this, v);
			return x;
		}
		
		public virtual IntVariable Multiply(int value)
		{
			Network net = Network;
			var x = new IntVariable(net);
			// x = this * value
			new IntArith(net, IntArith.MULTIPLY, x, this, new IntVariable(net, value));
			return x;
		}
		
		public virtual IntVariable Max(IntVariable v)
		{
			Network net = Network;
			var x = new IntVariable(net);
			// x = Max(this, v)
			new IntArith(net, IntArith.MAX, x, this, v);
			return x;
		}
		
		public virtual IntVariable Max(int value)
		{
			Network net = Network;
			var x = new IntVariable(net);
			// x = Max(this, value)
			new IntArith(net, IntArith.MAX, x, this, new IntVariable(net, value));
			return x;
		}
		
		public virtual IntVariable Min(IntVariable v)
		{
			Network net = Network;
			var x = new IntVariable(net);
			// x = Min(this, v)
			new IntArith(net, IntArith.MIN, x, this, v);
			return x;
		}
		
		public virtual IntVariable Min(int value)
		{
			Network net = Network;
			var x = new IntVariable(net);
			// x = Min(this, value)
			new IntArith(net, IntArith.MIN, x, this, new IntVariable(net, value));
			return x;
		}
		
		public virtual IntVariable Negate()
		{
			Network net = Network;
			var x = new IntVariable(net);
			// x = - this
			new IntFunc(net, IntFunc.Negate, x, this);
			return x;
		}
		
		public virtual IntVariable Abs()
		{
			Network net = Network;
			var x = new IntVariable(net);
			// x = Abs(this)
			new IntFunc(net, IntFunc.Abs, x, this);
			return x;
		}
		
		public virtual IntVariable Sign()
		{
			Network net = Network;
			var x = new IntVariable(net);
			// x = Sign(this)
			new IntFunc(net, IntFunc.Sign, x, this);
			return x;
		}

        public void Equals(IntVariable v)
        {
            Equals(v, ConstraintTypes.Hard);
        }

        public void Equals(IntVariable v, ConstraintTypes cType)
        {
            Equals(v, cType, 0);
        }

        public void Equals(IntVariable v, ConstraintTypes cType, int weight)
        {
            Network net = Network;
            new Equals(net, this, v, cType, weight);
        }

        public virtual void Equals(int value)
        {
            Equals(value, ConstraintTypes.Hard);
        }

        public virtual void Equals(int value, ConstraintTypes cType)
        {
            Equals(value, cType, 0);
        }

        public virtual void Equals(int value, ConstraintTypes cType, int weight)
        {
            Network net = Network;
            new Equals(net, this, new IntVariable(net, value), cType, weight);
        }

        public virtual void NotEquals(IntVariable v)
        {
            NotEquals(v, ConstraintTypes.Hard);
        }

        public virtual void NotEquals(IntVariable v, ConstraintTypes cType)
        {
            NotEquals(v, cType, 0);
        }

        public virtual void NotEquals(IntVariable v, ConstraintTypes cType, int weight)
        {
            Network net = Network;
            new NotEquals(net, this, v, cType, weight);
        }

        public virtual void NotEquals(int value)
        {
            NotEquals(value, ConstraintTypes.Hard);
       }

        public virtual void NotEquals(int value, ConstraintTypes cType)
        {
            NotEquals(value, cType, 0);
        }

        public virtual void NotEquals(int value, ConstraintTypes cType, int weight)
        {
            Network net = Network;
            new NotEquals(net, this, new IntVariable(net, value), cType, weight);
        }

        public virtual void Le(IntVariable v)
        {
            Le(v, ConstraintTypes.Hard);
        }

        public virtual void Le(IntVariable v, ConstraintTypes cType)
        {
            Le(v, cType, 0);
        }

        public virtual void Le(IntVariable v, ConstraintTypes cType, int weight)
        {
            Network net = Network;
            new IntComparison(net, IntComparison.Le, this, v, cType, weight);
        }

        public virtual void Le(int value)
        {
            Le(value, ConstraintTypes.Hard);
        }

        public virtual void Le(int value, ConstraintTypes cType)
        {
            Le(value, cType, 0);
        }

        public virtual void Le(int value, ConstraintTypes cType, int weight)
        {
            Network net = Network;
            new IntComparison(net, IntComparison.Le, this, new IntVariable(net, value), cType, weight);
        }

        public virtual void Lt(IntVariable v)
        {
            Lt(v, ConstraintTypes.Hard);
        }

        public virtual void Lt(IntVariable v, ConstraintTypes cType)
        {
            Lt(v, cType, 0);
        }

        public virtual void Lt(IntVariable v, ConstraintTypes cType, int weight)
        {
            Network net = Network;
            new IntComparison(net, IntComparison.Lt, this, v, cType, weight);
        }

        public virtual void Lt(int value)
        {
            Lt(value, ConstraintTypes.Hard);
        }

        public virtual void Lt(int value, ConstraintTypes cType)
        {
            Lt(value, cType, 0);
        }

        public virtual void Lt(int value, ConstraintTypes cType, int weight)
        {
            Network net = Network;
            new IntComparison(net, IntComparison.Lt, this, new IntVariable(net, value), cType, weight);
        }

        public virtual void Ge(IntVariable v)
        {
            Ge(v, ConstraintTypes.Hard);
        }

        public virtual void Ge(IntVariable v, ConstraintTypes cType)
        {
            Ge(v, cType, 0);
        }

        public virtual void Ge(IntVariable v, ConstraintTypes cType, int weight)
        {
            Network net = Network;
            new IntComparison(net, IntComparison.Ge, this, v, cType, weight);
        }

        public virtual void Ge(int value)
        {
            Ge(value, ConstraintTypes.Hard);
        }

        public virtual void Ge(int value, ConstraintTypes cType)
        {
            Ge(value, cType, 0);
        }

	    public virtual void Ge(ConstraintTypes cType, int weight)
	    {
	        Ge(0, cType, weight);
	    }

	    public virtual void Ge(int value, ConstraintTypes cType, int weight)
        {
            Network net = Network;
            new IntComparison(net, IntComparison.Ge, this, new IntVariable(net, value), cType, weight);
        }

        public virtual void Gt(IntVariable v)
        {
            Gt(v, ConstraintTypes.Hard);
        }

        public virtual void Gt(IntVariable v, ConstraintTypes cType)
        {
            Gt(v, cType, 0);
        }

        public virtual void Gt(IntVariable v, ConstraintTypes cType, int weight)
        {
            Network net = Network;
            new IntComparison(net, IntComparison.Gt, this, v, cType, weight);
        }

        public virtual void Gt(int value)
        {
            Gt(value, ConstraintTypes.Hard);
        }

        public virtual void Gt(int value, ConstraintTypes cType)
        {
            Gt(value, cType, 0);
        }

        public virtual void Gt(int value, ConstraintTypes cType, int weight)
        {
            Network net = Network;
            new IntComparison(net, IntComparison.Gt, this, new IntVariable(net, value), cType, weight);
        }
    }
}