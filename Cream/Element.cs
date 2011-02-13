/*
* Element.java
*/
using System;
namespace Cream
{
	
	/// <summary> Element constraints.
	/// 
	/// </summary>
	/// <since> 1.4
	/// </since>
	/// <version>  1.4
	/// </version>
    /// <author>  Original java solver: Naoyuki Tamura (tamura@kobe-u.ac.jp)
    ///           C Sharp Solver: Ali Hmer (hmer200a@uregina.ca)
    /// </author>
	public class Element:Constraint
	{
		private readonly Variable _v0;
		private readonly Variable _v1;
		private readonly Variable[] _v;
		
		/// <summary> Adds an Element constraint meaning <tt>v0 == v[v1]</tt> 
		/// to the network.
		/// 
		/// </summary>
		/// <param name="net">the network
		/// </param>
		/// <param name="v0">integer variable for the result
		/// </param>
		/// <param name="v1">integer variable for the index
		/// </param>
		/// <param name="v">the array of integer variables
		/// </param>
        public Element(Network net, Variable v0, Variable v1, Variable[] v)
            : this(net, v0, v1, v, ConstraintTypes.Hard)
        {
        }

        public Element(Network net, Variable v0, Variable v1, Variable[] v, ConstraintTypes cType)
            : this(net, v0, v1, v, cType, 0)
        {
        }

        public Element(Network net, Variable v0, Variable v1, Variable[] v, ConstraintTypes cType, int weight)
            : base(net, cType, weight)
        {
            _v0 = v0;
            _v1 = v1;
            _v = (Variable[])v.Clone();
        }
		
		protected override internal Constraint Copy(Network net)
		{
			return new Element(net, Copy(_v0, net), Copy(_v1, net), Copy(_v, net));
		}
		
		protected override internal bool IsModified()
		{
			return _v0.IsModified() || _v1.IsModified() || IsModified(_v);
		}

        protected internal override bool IsSatisfied()
        {
            return Satisfy(null);
        }
		protected override internal bool Satisfy(Trail trail)
		{
			int n = _v.Length;
			// limit the domain of v1 to 0..n-1
			var d1 = (IntDomain) _v1.Domain;
			d1 = d1.CapInterval(0, n - 1);
			if (d1.Empty)
				return false;
			// get the possible range of v[i] as Min..Max
			int min = IntDomain.MaxValue;
			int max = IntDomain.MinValue;
			for (int i = 0; i < n; i++)
			{
				if (d1.Contains(i))
				{
					var d = (IntDomain) _v[i].Domain;
					min = Math.Min(min, d.Minimum());
					max = Math.Max(max, d.Maximum());
				}
			}
			if (min > max)
				return false;
			// limit the domain of v0 to Min..Max
			var d0 = (IntDomain) _v0.Domain;
			d0 = d0.CapInterval(min, max);
			if (d0.Empty)
				return false;
			// Delete impossible indices from v1
			for (int i = 0; i < n; i++)
			{
				if (d1.Contains(i))
				{
					var d = (IntDomain) _v[i].Domain;
					if (d0.CapInterval(d.Minimum(), d.Maximum()).Empty)
					{
						d1 = d1.Delete(i);
					}
				}
			}
			if (d1.Empty)
				return false;
			// propagate v0 to v[v1] when v1 is determined
			if (d1.Size() == 1)
			{
				int i = d1.Value();
				var d = (IntDomain) _v[i].Domain;
				d0 = (IntDomain) d.Cap(d0);
				if (d0.Empty)
					return false;
                if (trail != null)
                {
                    _v[i].UpdateDomain(d0, trail);
                }
			}
            if (trail != null)
            {
                _v0.UpdateDomain(d0, trail);
                _v1.UpdateDomain(d1, trail);
            }
		    return true;
		}
		
		public override String ToString()
		{
			return "Element(" + _v0 + "," + _v1 + "," + ToString(_v) + ")";
		}
	}
}