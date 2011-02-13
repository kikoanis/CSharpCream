using System;

namespace  Cream
{
    public class Equals:Constraint
    {
        private readonly Variable[] _v;
		
        public Equals(Network net, Variable v0, Variable v1):this(net, new[]{v0, v1})
        {
        }

        public Equals(Network net, Variable v0, Variable v1, ConstraintTypes cType)
            : this(net, new[] { v0, v1 }, cType)
        {
        }

        public Equals(Network net, Variable v0, Variable v1, ConstraintTypes cType, int weight)
            : this(net, new[] { v0, v1 }, cType, weight)
        {
        }

        public Equals(Network net, Variable[] v)
            : this(net, v, ConstraintTypes.Hard)
        {
        }

        public Equals(Network net, Variable[] v, ConstraintTypes cType)
            : this(net, v, cType, 0)
        {
        }
        
        public Equals(Network net, Variable[] v, ConstraintTypes cType, int weight)
            : base(net, cType, weight)
        {
            _v = new Variable[v.Length];
            v.CopyTo(_v, 0);
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
            return new Equals(net, Copy(_v, net), IsHard?ConstraintTypes.Hard:ConstraintTypes.Soft, Weight);
        }
		
        protected internal override bool IsModified()
        {
            return IsModified(_v);
        }

        protected internal override bool IsSatisfied()
        {
            return Satisfy(null);
        }

        //public int getMostSoftSatisfiedDomain()
        //{
        //    Domain d = v[0].Domain;
        //    if (CType == ConstraintTypes.Soft)
        //    {
        //        for (int i = 1; i < v.Length; i++)
        //        {
        //            d = d.Cap(v[i].Domain);
        //            if (d.EmptyDomain)
        //                return -1;
        //        }
        //    }
        //    return ((IntDomain) d).Min();
        //}
        
        protected internal override bool Satisfy(Trail trail)
        {
            Domain d = _v[0].Domain;
            for (int i = 1; i < _v.Length; i++)
            {
                d = d.Cap(_v[i].Domain);
                if (d.Empty)
                    return false;
            }
            if (trail != null)
            {
                foreach (Variable t in _v)
                {
                	t.UpdateDomain(d, trail);
                }
            }
            return true;
        }
		
        public override String ToString()
        {
            return "Equals(" + ToString(_v) + " "+CType+")";
        }
    }
}