/*
* @(#)Network.cs
*/
using System;
using System.Collections;

namespace  Cream
{
	
	/// <summary> Constraint networks.
	/// A constraint network consists of
	/// {@linkplain Variable variables},
	/// {@linkplain Constraint constraints},
	/// and an objective variable (optional).
	/// Variables and constraints are added by <tt>Add</tt> methods:
	/// <pre>
	/// Network net = new Network();
	/// Domain d = new IntDomain(0, IntDomain.MaxValue);
	/// Variable x = new Variable(net, d);
	/// Variable y = new Variable(net, d);
	/// new NotEquals(net, x, y);
	/// </pre>
	/// <p/>
	/// Please note that any variable or any constraint can not be added to two different networks.
	/// In other words, a network can not share a variable or a constraint with another network.
	/// </summary>
	/// <seealso cref="Variable">
	/// </seealso>
	/// <seealso cref="Constraint">
	/// </seealso>
	/// <since> 1.0
	/// </since>
	/// <version>  1.0, 01/12/08
	/// </version>
    /// <author>  Based on Java Solver by : Naoyuki Tamura (tamura@kobe-u.ac.jp) - C#: Ali Hmer (Hmer200a@uregina.ca)
	/// </author>
	public class Network : ICloneable
	{
		/// <summary> Returns the objective variable of this network.</summary>
		/// <returns> the objective variable or <tt>null</tt> if the network has no objective variable
		/// </returns>
		/// <summary> Sets the objective variable of this network.
		/// If <tt>v</tt> is <tt>null</tt>,
		/// this network is set to have no objective variable.
		/// If <tt>v</tt> is not <tt>null</tt>,
		/// the objective variable is automatically added to the network.
		/// </summary>
		// <param name="v">the objective variable
		/// 

        private Variable _objective;
        private IList _variables;
        private IList _constraints;

		public Variable Objective
		{
			get
			{
				return _objective;
			}
			
			set
			{
				_objective = value;
				if (value != null)
				{
					ADD(value);
				}
			}
			
		}
		/// <summary> Returns the list of variables of this network.</summary>
		/// <returns> the list of variables
		/// </returns>
		public IList Variables
		{
			get
			{
				return _variables;
			}
			set
			{
			    _variables = value;
			}
		}
		/// <summary> Returns the list of constraints of this network.</summary>
		/// <returns> the list of constraints
		/// </returns>
		public IList Constraints
		{
			get
			{
				return _constraints;
			}
            set
            {
                _constraints = value;
            }
			
		}
		
		/// <summary> Constructs an empty constraint network.</summary>
		public Network()
		{
			Objective = null;
			Variables = new ArrayList();
			Constraints = new ArrayList();
		}
		
		/// <summary> Adds a variable to this network.
		/// If the variable is already in the nework, this invocation has no effect.
		/// </summary>
		/// <param name="v">the variable to be added
		/// </param>
		/// <returns> the variable itself
		/// </returns>
		/// <throws>  NullPointerException if <tt>v</tt> is <tt>null</tt> </throws>
		/// <throws>  IllegalArgumentException if <tt>v</tt> is already added to another network </throws>
		protected internal virtual Variable ADD(Variable v)
		{
			var list = _variables.GetEnumerator();
			bool found = false;
			while (list.MoveNext())
			{
				if (((Variable)list.Current).Name == v.Name)
				{
					found = true;
					break;
				}
			}
			//if (!variables.Contains(v))
			if (!found)
			{
				if (v.Index >= 0)
				{
					throw new ArgumentException();
				}
				v.Index = _variables.Count;
				_variables.Add(v);
			}
			return v;
		}
		
		/// <summary> Adds a constraint to this network.
		/// If the constraint is already in the nework, this invocation has no effect.
		/// Please notice that variables in the constraint are not automatically added.
		/// </summary>
		/// <param name="c">the constraint to be added
		/// </param>
		/// <returns> the constraint itself
		/// </returns>
		/// <throws>  NullPointerException if <tt>c</tt> is <tt>null</tt> </throws>
		/// <throws>  IllegalArgumentException if <tt>c</tt> is already added to another network </throws>
		protected internal virtual Constraint ADD(Constraint c)
		{
			if (!_constraints.Contains(c))
			{
				if (c.Index >= 0)
				{
					throw new ArgumentException();
				}
				c.Index = _constraints.Count;
				_constraints.Add(c);
			}
			return c;
		}
		
		/// <summary> Returns the <tt>i</tt>-th variable of this network.
		/// The index starts from 0.
		/// </summary>
		/// <param name="i">the index value of the variable to be returned
		/// </param>
		/// <returns> the <tt>i</tt>-th variable
		/// </returns>
		/// <throws>  IndexOutOfBoundsException if <tt>i</tt> is out-of-range </throws>
		public virtual Variable GetVariable(int i)
		{
			return (Variable) Variables[i];
		}
		
		/// <summary> Returns the <tt>i</tt>-th constraint of this network.
		/// The index starts from 0.
		/// </summary>
		/// <param name="i">the index value of the constraint to be returned
		/// </param>
		/// <returns> the <tt>i</tt>-th constraint
		/// </returns>
		/// <throws>  IndexOutOfBoundsException if <tt>i</tt> is out-of-range </throws>
		public virtual Constraint GetConstraint(int i)
		{
			return (Constraint) Constraints[i];
		}
		
		/// <summary> Returns a Copy of this network.
		/// The new network has the same structure as the original network.
		/// </summary>
		/// <returns> a Copy of this network
		/// </returns>
		public virtual Object Clone()
		{
			var net = new Network();
			var vs = _variables.GetEnumerator();
			while (vs.MoveNext())
			{
				var v = (Variable) vs.Current;
				Variable v1 = v.Copy(net);
				if (v.Index != v1.Index)
					throw new ArgumentException();
			}
			var cs = _constraints.GetEnumerator();
			while (cs.MoveNext())
			{
				var c = (Constraint) cs.Current;
				Constraint c1 = c.Copy(net);
				if (c.Index != c1.Index)
					throw new ArgumentException();
			}
			if (_objective != null)
			{
				net.Objective = net.GetVariable(_objective.Index);
			}
			return net;
		}
		
		/// <summary> Returns a readable string representation of this network.</summary>
		/// <returns> the readable string representation
		/// </returns>
		public override String ToString()
		{
			String s = "";
			if (_objective != null)
			{
				s += ("Objective: " + _objective.Name + "=" + _objective.Domain + "\n");
			}
			var vs = _variables.GetEnumerator();
			while (vs.MoveNext())
			{
				var v = (Variable) vs.Current;
				s += (v.Name + "=" + v.Domain + "\n");
			}
			var cs = _constraints.GetEnumerator();
			while (cs.MoveNext())
			{
				s += (cs.Current + "\n");
			}
			return s;
		}
	}
}