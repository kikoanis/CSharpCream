using System;

namespace  Cream
{
	
	public class IntArith:Constraint
	{
		public const int Add = 0;
		public const int Subtract = 1;
		public const int MULTIPLY = 2;
		// public static final int DIVIDE = 3;
		// public static final int MOD = 4;
		// public static final int POW = 5;
		public const int MAX = 6;
		public const int MIN = 7;
		private readonly int _arith;
		private readonly Variable[] _v;

        public IntArith(Network net, int a, Variable v0, Variable v1, Variable v2)
            : this(net, a, new[] { v0, v1, v2 })
        {
        }

        public IntArith(Network net, int a, Variable v0, Variable v1, int x2)
            : this(net, a, v0, v1, new IntVariable(net, x2))
        {
        }

        public IntArith(Network net, int a, Variable v0, int x1, Variable v2)
            : this(net, a, v0, new IntVariable(net, x1), v2)
        {
        }

        public IntArith(Network net, int a, int x0, Variable v1, Variable v2)
            : this(net, a, new IntVariable(net, x0), v1, v2)
        {
        }

		public IntArith(Network net, int a, Variable v0, Variable v1, Variable v2, ConstraintTypes cType)
            : this(net, a, new[] { v0, v1, v2 }, cType)
        {
        }

        public IntArith(Network net, int a, Variable v0, Variable v1, int x2, ConstraintTypes cType)
            : this(net, a, v0, v1, new IntVariable(net, x2), cType)
        {
        }

        public IntArith(Network net, int a, Variable v0, int x1, Variable v2, ConstraintTypes cType)
            : this(net, a, v0, new IntVariable(net, x1), v2, cType)
        {
        }

        public IntArith(Network net, int a, int x0, Variable v1, Variable v2, ConstraintTypes cType)
            : this(net, a, new IntVariable(net, x0), v1, v2, cType)
        {
        }

        private IntArith(Network net, int a, Variable[] v, ConstraintTypes cType = ConstraintTypes.Hard)
            : this(net, a, v, cType, 0)
        {
        }

        public IntArith(Network net, int a, Variable v0, Variable v1, Variable v2, ConstraintTypes cType, int weight)
            : this(net, a, new[] { v0, v1, v2 }, cType, weight)
        {
        }

        public IntArith(Network net, int a, Variable v0, Variable v1, int x2, ConstraintTypes cType, int weight)
            : this(net, a, v0, v1, new IntVariable(net, x2), cType, weight)
        {
        }

        public IntArith(Network net, int a, Variable v0, int x1, Variable v2, ConstraintTypes cType, int weight)
            : this(net, a, v0, new IntVariable(net, x1), v2, cType, weight)
        {
        }

        public IntArith(Network net, int a, int x0, Variable v1, Variable v2, ConstraintTypes cType, int weight)
            : this(net, a, new IntVariable(net, x0), v1, v2, cType, weight)
        {
        }

        private IntArith(Network net, int a, Variable[] v, ConstraintTypes cType, int weight)
            : base(net, cType, weight)
        {
            _arith = a;
            _v = v;
        }

        public Variable[] Vars
        {
            get
            {
                return _v;
            }
        }
        
        protected internal override Constraint Copy(Network net)
		{
			return new IntArith(net, _arith, Copy(_v, net));
		}
		
		protected internal override bool IsModified()
		{
			return IsModified(_v);
		}
		
		private static bool SatisfyAdd(Variable v0, Variable v1, Variable v2, Trail trail)
		{
			var d0 = (IntDomain) v0.Domain;
			var d1 = (IntDomain) v1.Domain;
			var d2 = (IntDomain) v2.Domain;

		    if (d1.Size() == 1 && d2.Size() == 1)
			{
				// v0 = v1 + v2
				var value = d1.Value() + d2.Value();
				if (!d0.Contains(value))
					return false;
				if (d0.Size() > 1)
                    if (trail != null)
                    {
                        v0.UpdateDomain(new IntDomain(value), trail);
                    }
			    return true;
			}
		    if (d0.Size() == 1 && d2.Size() == 1)
		    {
		        // v1 = v0 - v2
				var value = d0.Value() - d2.Value();
				if (!d1.Contains(value))
		            return false;
		        if (d1.Size() > 1)
                    if (trail != null)
                    {
                        v1.UpdateDomain(new IntDomain(value), trail);
                    }
		        return true;
		    }
		    if (d0.Size() == 1 && d1.Size() == 1)
		    {
		        // v2 = v0 - v1
				var value = d0.Value() - d1.Value();
				if (!d2.Contains(value))
		            return false;
		        if (d2.Size() > 1)
                    if (trail != null)
                    {
                        v2.UpdateDomain(new IntDomain(value), trail);
                    }
		        return true;
		    }

		    // v0 = v1 + v2
			d0 = d0.CapInterval(d1.Minimum() + d2.Minimum(), d1.Maximum() + d2.Maximum());
			if (d0.Empty)
				return false;
            if (trail != null)
            {
                v0.UpdateDomain(d0, trail);
            }
		    // v1 = v0 - v2
			d1 = d1.CapInterval(d0.Minimum() - d2.Maximum(), d0.Maximum() - d2.Minimum());
			if (d1.Empty)
				return false;
            if (trail != null)
            {
                v1.UpdateDomain(d1, trail);
            }
		    // v2 = v0 - v1
			d2 = d2.CapInterval(d0.Minimum() - d1.Maximum(), d0.Maximum() - d1.Minimum());
			if (d2.Empty)
				return false;
            if (trail != null)
            {
                v2.UpdateDomain(d2, trail);
            }

		    return true;
		}
		
		private static int ToInt(long x)
		{
			return (int) Math.Max(IntDomain.MinValue, Math.Min(IntDomain.MaxValue, x));
		}
		
		private static int Min(int[] x)
		{
			int m = x[0];
			for (int i = 1; i < x.Length; i++)
				m = Math.Min(m, x[i]);
			return m;
		}
		
		private static int Max(int[] x)
		{
			int m = x[0];
			for (int i = 1; i < x.Length; i++)
				m = Math.Max(m, x[i]);
			return m;
		}
		
		private static IntDomain Multiply(IntDomain d0, IntDomain d1, IntDomain d2)
		{
			if (!d1.Contains(0) && !d2.Contains(0))
			{
				d0 = d0.Delete(0);
				if (d0.Empty)
					return IntDomain.emptyDomain;
			}
			var x = new[]
			        	{
			        		ToInt(d1.Minimum() * (long) d2.Minimum()),
			        		ToInt(d1.Minimum() * (long) d2.Maximum()),
			        		ToInt(d1.Maximum() * (long) d2.Minimum()),
			        		ToInt(d1.Maximum() * (long) d2.Maximum())
			        	};
			d0 = d0.CapInterval(Min(x), Max(x));
			return d0;
		}
		
		private static IntDomain Divide(IntDomain d0, IntDomain d1, IntDomain d2)
		{
			if (!d1.Contains(0))
			{
				d0 = d0.Delete(0);
				if (d0.Empty)
					return IntDomain.emptyDomain;
			}
			if (d2.Contains(0))
			{
				return d0;
			}
		    if (d2.Maximum() < 0 || 0 < d2.Minimum())
		    {
		        var x = new[]{d1.Minimum() / d2.Minimum(), d1.Maximum() / d2.Minimum(), d1.Minimum() / d2.Maximum(), d1.Maximum() / d2.Maximum()};
		        d0 = d0.CapInterval(Min(x), Max(x));
		    }
		    else
		    {
		        var x = new[]{d1.Minimum() / d2.Minimum(), d1.Maximum() / d2.Minimum(), d1.Minimum() / d2.Maximum(), d1.Maximum() / d2.Maximum(), d1.Minimum(), d1.Maximum(), - d1.Minimum(), - d1.Maximum()};
		        d0 = d0.CapInterval(Min(x), Max(x));
		    }
		    return d0;
		}
		
		private static bool SatisfyMULTIPLY(Variable v0, Variable v1, Variable v2, Trail trail)
		{
			var d0 = (IntDomain) v0.Domain;
			var d1 = (IntDomain) v1.Domain;
			var d2 = (IntDomain) v2.Domain;
			
			if (d1.Size() == 1 && d2.Size() == 1)
			{
				// v0 = v1 * v2
				int value = ToInt(d1.Value() * (long) d2.Value());
				if (!d0.Contains(value))
					return false;
				if (d0.Size() > 1)
                    if (trail != null)
                    {
                        v0.UpdateDomain(new IntDomain(value), trail);
                    }
			    return true;
			}
		    if (d0.Size() == 1 && d2.Size() == 1)
		    {
		        // v1 = v0 / v2
		        int x = d0.Value();
		        int y = d2.Value();
		        if (y == 0)
		        {
		            return x == 0;
		        }
		        if (x % y != 0)
		        {
		            return false;
		        }
		        int value = x / y;
		        if (!d1.Contains(value))
		            return false;
		        if (d1.Size() > 1)
                    if (trail != null)
                    {
                        v1.UpdateDomain(new IntDomain(value), trail);
                    }
		        return true;
		    }
		    if (d0.Size() == 1 && d1.Size() == 1)
		    {
		        // v2 = v0 / v1
		        int x = d0.Value();
		        int y = d1.Value();
		        if (y == 0)
		        {
		            return x == 0;
		        }
		        if (x % y != 0)
		        {
		            return false;
		        }
		        int value = x / y;
		        if (!d2.Contains(value))
		            return false;
		        if (d2.Size() > 1)
                    if (trail != null)
                    {
                        v2.UpdateDomain(new IntDomain(value), trail);
                    }
		        return true;
		    }

		    d0 = Multiply(d0, d1, d2);
			if (d0.Empty)
				return false;
			d1 = Divide(d1, d0, d2);
			if (d1.Empty)
				return false;
			d2 = Divide(d2, d0, d1);
			if (d2.Empty)
				return false;
            if (trail != null)
            {
                if (d0 != v0.Domain)
                    v0.UpdateDomain(d0, trail);
                if (d1 != v1.Domain)
                    v1.UpdateDomain(d1, trail);
                if (d2 != v2.Domain)
                    v2.UpdateDomain(d2, trail);
            }
		    return true;
		}
		
		private static bool SatisfyMAX(Variable v0, Variable v1, Variable v2, Trail trail)
		{
			var d0 = (IntDomain) v0.Domain;
			var d1 = (IntDomain) v1.Domain;
			var d2 = (IntDomain) v2.Domain;
			
			if (d1.Size() == 1 && d2.Size() == 1)
			{
				// v0 = Max(v1, v2)
				int value = Math.Max(d1.Value(), d2.Value());
				if (!d0.Contains(value))
					return false;
				if (d0.Size() > 1)
                    if (trail != null)
                    {
                        v0.UpdateDomain(new IntDomain(value), trail);
                    }
			    return true;
			}
			if (d0.Size() == 1)
			{
				// Max(v1, v2) = v0
				int value = d0.Value();
				if (!d1.Contains(value) && !d2.Contains(value))
					return false;
				if (d1.Maximum() > value)
				{
					d1.CapInterval(IntDomain.MinValue, value);
					if (d1.Empty)
						return false;
                    if (trail != null)
                    {
                        v1.UpdateDomain(d1, trail);
                    }
				}
				if (d2.Maximum() > value)
				{
					d2.CapInterval(IntDomain.MinValue, value);
					if (d2.Empty)
						return false;
                    if (trail != null)
                    {
                        v2.UpdateDomain(d2, trail);
                    }
				}
				return true;
			}
			
			// v0 = Max(v1, v2)
			int min = Math.Max(d1.Minimum(), d2.Minimum());
			int max = Math.Max(d1.Maximum(), d2.Maximum());
			d0 = d0.CapInterval(min, max);
			if (d0.Empty)
				return false;
            if (trail != null)
            {
                v0.UpdateDomain(d0, trail);
            }

		    // Max(v1, v2) = v0
			if (d1.Maximum() > d0.Maximum())
				d1 = d1.CapInterval(IntDomain.MinValue, d0.Maximum());
			if (d2.Maximum() > d0.Maximum())
				d2 = d2.CapInterval(IntDomain.MinValue, d0.Maximum());
			if (d1.Maximum() < d0.Minimum())
			{
				d0 = (IntDomain) d0.Cap(d2);
				d2 = d0;
			}
			if (d2.Maximum() < d0.Minimum())
			{
				d0 = (IntDomain) d0.Cap(d1);
				d1 = d0;
			}
			if (d0.Empty)
				return false;
			if (d1.Empty)
				return false;
			if (d2.Empty)
				return false;
            if (trail != null)
            {
                v0.UpdateDomain(d0, trail);
                v1.UpdateDomain(d1, trail);
                v2.UpdateDomain(d2, trail);
            }
		    return true;
		}
		
		private static bool SatisfyMIN(Variable v0, Variable v1, Variable v2, Trail trail)
		{
			var d0 = (IntDomain) v0.Domain;
			var d1 = (IntDomain) v1.Domain;
			var d2 = (IntDomain) v2.Domain;
			
			if (d1.Size() == 1 && d2.Size() == 1)
			{
				// v0 = Min(v1, v2)
				int value = Math.Min(d1.Value(), d2.Value());
				if (!d0.Contains(value))
					return false;
				if (d0.Size() > 1)
                    if (trail != null)
                    {
                        v0.UpdateDomain(new IntDomain(value), trail);
                    }
			    return true;
			}
			if (d0.Size() == 1)
			{
				int value = d0.Value();
				if (!d1.Contains(value) && !d2.Contains(value))
					return false;
				// ???
				if (d1.Minimum() < value)
				{
					d1.CapInterval(value, IntDomain.MaxValue);
					if (d1.Empty)
						return false;
                    if (trail != null)
                    {
                        v1.UpdateDomain(d1, trail);
                    }
				}
				if (d2.Minimum() < value)
				{
					d2.CapInterval(value, IntDomain.MaxValue);
					if (d2.Empty)
						return false;
                    if (trail != null)
                    {
                        v2.UpdateDomain(d2, trail);
                    }
				}
				return true;
			}
			
			// v0 = Min(v1, v2)
			int min = Math.Min(d1.Minimum(), d2.Minimum());
			int max = Math.Min(d1.Maximum(), d2.Maximum());
			d0 = d0.CapInterval(min, max);
			if (d0.Empty)
				return false;
            if (trail != null)
            {
                v0.UpdateDomain(d0, trail);
            }
		    //
			if (d1.Minimum() < d0.Minimum())
				d1 = d1.CapInterval(d0.Minimum(), IntDomain.MaxValue);
			if (d2.Minimum() < d0.Minimum())
				d2 = d2.CapInterval(d0.Minimum(), IntDomain.MaxValue);
			if (d1.Minimum() > d0.Maximum())
			{
				d0 = (IntDomain) d0.Cap(d2);
				d2 = d0;
			}
			if (d2.Minimum() > d0.Maximum())
			{
				d0 = (IntDomain) d0.Cap(d1);
				d1 = d0;
			}
			if (d0.Empty)
				return false;
			if (d1.Empty)
				return false;
			if (d2.Empty)
				return false;
            if (trail != null)
            {
                v0.UpdateDomain(d0, trail);
                v1.UpdateDomain(d1, trail);
                v2.UpdateDomain(d2, trail);
            }
		    return true;
		}

        protected internal override bool IsSatisfied()
        {
            return Satisfy(null);
        }
		protected internal override bool Satisfy(Trail trail)
		{
			switch (_arith)
			{
				
				case Add: 
					return SatisfyAdd(_v[0], _v[1], _v[2], trail);
				
				case Subtract: 
					return SatisfyAdd(_v[1], _v[0], _v[2], trail);
				
				case MULTIPLY: 
					return SatisfyMULTIPLY(_v[0], _v[1], _v[2], trail);
				
				case MAX: 
					return SatisfyMAX(_v[0], _v[1], _v[2], trail);
				
				case MIN: 
					return SatisfyMIN(_v[0], _v[1], _v[2], trail);
				}
			return false;
		}
		
		public override String ToString()
		{
			String a = "";
			switch (_arith)
			{
				
				case Add: 
					a = "Add"; break;
				
				case Subtract: 
					a = "Subtract"; break;
				
				case MULTIPLY: 
					a = "MULTIPLY"; break;
				
				case MAX: 
					a = "Maximum"; break;
				
				case MIN: 
					a = "Minimum"; break;
				}
			return "IntArith(" + a + "," + ToString(_v) + ")";
		}
	}
}