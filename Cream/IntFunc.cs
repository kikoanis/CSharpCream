using System;

namespace  Cream
{
	
	public class IntFunc:Constraint
	{
		public const int Negate = 0;
		public const int Abs = 1;
		public const int Sign = 2;
		private readonly int _arith;
		private readonly Variable[] _v;

        public IntFunc(Network net, int a, Variable v0, Variable v1)
            : this(net, a, new[] { v0, v1 })
        {
        }

        public IntFunc(Network net, int a, Variable v0, int x1)
            : this(net, a, v0, new IntVariable(net, x1))
        {
        }

        public IntFunc(Network net, int a, int x0, Variable v1)
            : this(net, a, new IntVariable(net, x0), v1)
        {
        }

		public IntFunc(Network net, int a, Variable v0, Variable v1, ConstraintTypes cType)
            : this(net, a, new[] { v0, v1 }, cType)
        {
        }

        public IntFunc(Network net, int a, Variable v0, int x1, ConstraintTypes cType)
            : this(net, a, v0, new IntVariable(net, x1), cType)
        {
        }

        public IntFunc(Network net, int a, int x0, Variable v1, ConstraintTypes cType)
            : this(net, a, new IntVariable(net, x0), v1, cType)
        {
        }

        private IntFunc(Network net, int a, Variable[] v, ConstraintTypes cType = ConstraintTypes.Hard)
            : this(net, a, v, cType, 0)
        {
        }

        public IntFunc(Network net, int a, Variable v0, Variable v1, ConstraintTypes cType, int weight)
            : this(net, a, new[] { v0, v1 }, cType, weight)
        {
        }

        public IntFunc(Network net, int a, Variable v0, int x1, ConstraintTypes cType, int weight)
            : this(net, a, v0, new IntVariable(net, x1), cType, weight)
        {
        }

        public IntFunc(Network net, int a, int x0, Variable v1, ConstraintTypes cType, int weight)
            : this(net, a, new IntVariable(net, x0), v1, cType, weight)
        {
        }

        private IntFunc(Network net, int a, Variable[] v, ConstraintTypes cType, int weight)
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
			return new IntFunc(net, _arith, Copy(_v, net));
		}
		
		protected internal override bool IsModified()
		{
			return IsModified(_v);
		}
		
		private static bool SatisfyNEGATE(Variable v0, Variable v1, Trail trail)
		{
			var d0 = (IntDomain) v0.Domain;
			var d1 = (IntDomain) v1.Domain;
			
			if (d1.Size() == 1)
			{
				// v0 = -v1
				int value = - d1.Value();
				if (!d0.Contains(value))
					return false;
                if (d0.Size() > 1)
                {
                    v0.UpdateDomain(new IntDomain(value), trail);
                }
			    return true;
			}
		    if (d0.Size() == 1)
		    {
		        // v1 = -v0
		        int value = - d0.Value();
		        if (!d1.Contains(value))
		            return false;
		        if (d1.Size() > 1)
                    if (trail != null)
                    {
                        v1.UpdateDomain(new IntDomain(value), trail);
                    }
		        return true;
		    }

		    // v0 = -v1
			d0 = d0.CapInterval(- d1.Maximum(), - d1.Minimum());
			if (d0.Empty)
				return false;
            if (trail != null)
            {
                v0.UpdateDomain(d0, trail);
            }
		    // v1 = -v0
			d1 = d1.CapInterval(- d0.Maximum(), - d0.Minimum());
			if (d1.Empty)
				return false;
            if (trail != null)
            {
                v1.UpdateDomain(d1, trail);
            }
		    return true;
		}
		
		private static bool SatisfyABS(Variable v0, Variable v1, Trail trail)
		{
			var d0 = (IntDomain) v0.Domain;
			var d1 = (IntDomain) v1.Domain;
			
			if (d1.Size() == 1)
			{
				// v0 = Abs(v1)
				int value = Math.Abs(d1.Value());
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
		        // Abs(v1) = v0
		        int value = d0.Value();
		        if (value < 0)
		        {
		            return false;
		        }
		        if (value == 0)
		        {
		            if (!d1.Contains(value))
		                return false;
		            if (d1.Size() > 1)
                        if (trail != null)
                        {
                            v1.UpdateDomain(new IntDomain(value), trail);
                        }
		            return true;
		        }
		        if (d1.Contains(value) && d1.Contains(- value))
		        {
		            if (d1.Size() > 2)
		            {
		                value = Math.Abs(value);
		                d1 = new IntDomain(- value, value);
		                d1 = d1.Delete(- value + 1, value - 1);
                        if (trail != null)
                        {
                            v1.UpdateDomain(d1, trail);
                        }
		            }
		            return true;
		        }
		        if (d1.Contains(value))
		        {
		            if (d1.Size() > 1)
                        if (trail != null)
                        {
                            v1.UpdateDomain(new IntDomain(value), trail);
                        }
		            return true;
		        }
		        if (d1.Contains(- value))
		        {
		            if (d1.Size() > 1)
                        if (trail != null)
                        {
                            v1.UpdateDomain(new IntDomain(-value), trail);
                        }
		            return true;
		        }
		        return false;
		    }

		    int min;
			int max;
			// v0 = Abs(v1)
			if (d1.Minimum() >= 0)
			{
				min = d1.Minimum();
				max = d1.Maximum();
			}
			else if (d1.Maximum() <= 0)
			{
				min = - d1.Maximum();
				max = - d1.Minimum();
			}
			else
			{
				min = 0;
				max = Math.Max(- d1.Minimum(), d1.Maximum());
			}
			d0 = d0.CapInterval(min, max);
			if (d0.Empty)
				return false;
            if (trail != null)
            {
                v0.UpdateDomain(d0, trail);
            }
		    // Abs(v1) = v0
			min = d0.Minimum();
			max = d0.Maximum();
			d1 = d1.CapInterval(- max, max);
			if (d1.Empty)
				return false;
			if (min > 0)
				d1 = d1.Delete(- min + 1, min - 1);
            if (trail != null)
            {
                v1.UpdateDomain(d1, trail);
            }

		    return true;
		}
		
		private static bool SatisfySIGN(Variable v0, Variable v1, Trail trail)
		{
			var d0 = (IntDomain) v0.Domain;
			var d1 = (IntDomain) v1.Domain;
			
			if (d1.Size() == 1)
			{
				// v0 = Sign(v1)
				int sign = 0;
				if (d1.Value() < 0)
				{
					sign = - 1;
				}
				else if (d1.Value() > 0)
				{
					sign = 1;
				}
				if (!d0.Contains(sign))
					return false;
				if (d0.Size() > 1)
                    if (trail != null)
                    {
                        v0.UpdateDomain(new IntDomain(sign), trail);
                    }
			    return true;
			}
		    if (d0.Size() == 1)
		    {
		        // Sign(v1) = v0
		        int sign = d0.Value();
		        if (sign < 0)
		        {
		            if (d1.Maximum() >= 0)
		            {
		                d1 = d1.CapInterval(IntDomain.MinValue, - 1);
		                if (d1.Empty)
		                    return false;
                        if (trail != null)
                        {
                            v1.UpdateDomain(d1, trail);
                        }
		            }
		            return true;
		        }
		        if (sign == 0)
		        {
		            if (!d1.Contains(0))
		                return false;
		            if (d1.Size() > 1)
                        if (trail != null)
                        {
                            v1.UpdateDomain(new IntDomain(0), trail);
                        }
		            return true;
		        }
		        if (sign > 0)
		        {
		            if (d1.Minimum() <= 0)
		            {
		                d1 = d1.CapInterval(1, IntDomain.MaxValue);
		                if (d1.Empty)
		                    return false;
                        if (trail != null)
                        {
                            v1.UpdateDomain(d1, trail);
                        }
		            }
		            return true;
		        }
		        return false;
		    }

		    // v0 = Sign(v1)
			if (!(- 1 <= d0.Minimum() && d0.Maximum() <= 1))
			{
				d0 = d0.CapInterval(- 1, 1);
			}
			if (d1.Minimum() >= 0)
				d0 = d0.Delete(- 1);
			if (!d1.Contains(0))
				d0 = d0.Delete(0);
			if (d1.Maximum() <= 0)
				d0 = d0.Delete(1);
			
			// Sign(v1) = v0
			if (!d0.Contains(- 1))
			{
				if (d1.Minimum() < 0)
					d1 = d1.CapInterval(0, IntDomain.MaxValue);
			}
			if (!d0.Contains(0))
			{
				d1 = d1.Delete(0);
			}
			if (!d0.Contains(1))
			{
				if (d1.Maximum() > 0)
					d1 = d1.CapInterval(IntDomain.MinValue, 0);
			}
			if (d0.Empty)
				return false;
			if (d1.Empty)
				return false;
            if (trail != null)
            {
                v0.UpdateDomain(d0, trail);
                v1.UpdateDomain(d1, trail);
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
				case Negate: 
					return SatisfyNEGATE(_v[0], _v[1], trail);
				
				case Abs: 
					return SatisfyABS(_v[0], _v[1], trail);
				
				case Sign: 
					return SatisfySIGN(_v[0], _v[1], trail);
				}
			return false;
		}
		
		public override String ToString()
		{
			String a = "";
			switch (_arith)
			{
				
				case Negate: 
					a = "Negate"; break;
				
				case Abs: 
					a = "Abs"; break;
				
				case Sign: 
					a = "Sign"; break;
				}
			return "IntFunc(" + a + "," + ToString(_v) + ")";
		}
	}
}