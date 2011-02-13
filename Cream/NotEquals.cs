using System;

namespace  Cream
{
	
	public class NotEquals:Constraint
	{
		private readonly Variable[] _v;

        public NotEquals(Network net, Variable v0, Variable v1)
            : this(net, new[] { v0, v1 })
        {
        }

        public NotEquals(Network net, Variable[] va)
            : this(net, va, ConstraintTypes.Hard)
        {
        }

        public NotEquals(Network net, Variable v0, Variable v1, ConstraintTypes cType)
            : this(net, new[] { v0, v1 }, cType)
        {
        }

        public NotEquals(Network net, Variable v0, Variable v1, ConstraintTypes cType, int weight)
            : this(net, new[] { v0, v1 }, cType, weight)
        {
        }

        public NotEquals(Network net, Variable[] va, ConstraintTypes cType)
            : this(net, va, cType, 0)
        {
        }

        public NotEquals(Network net, Variable[] va, ConstraintTypes cType, int weight)
            : base(net, cType, weight)
        {
            _v = new Variable[va.Length];
            va.CopyTo(_v, 0);
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
			return new NotEquals(net, Copy(_v, net), IsHard?ConstraintTypes.Hard:ConstraintTypes.Soft, Weight);
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
            for (int i = 0; i < _v.Length; i++)
            {
                Domain d = _v[i].Domain;
                if (d.Size() != 1)
                    continue;
                Object elem = d.Element();
                for (int j = 0; j < _v.Length; j++)
                {
                    if (i == j)
                        continue;
                    Domain d1 = _v[j].Domain.Delete(elem);
                    if (d1.Empty)
                        return false;
                    if (trail != null)
                    {
                        _v[j].UpdateDomain(d1, trail);
                    }
                }
            }
            return true;
		}
		
		public override String ToString()
		{
            return "NotEquals(" + ToString(_v) + " " + CType + ")";
		}
	}
}