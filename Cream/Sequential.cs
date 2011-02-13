using System;

namespace  Cream
{
    public class Sequential:Constraint
    {
        private readonly Variable[] _v;
        private readonly int[] _l;

        public Sequential(Network net, Variable[] v, int[] l)
            : this(net, v, l, ConstraintTypes.Hard)
        {
        }

        public Sequential(Network net, Variable[] v, int[] l, ConstraintTypes cType)
            : this(net, v, l, cType, 0)
        {
        }

        public Sequential(Network net, Variable[] v, int[] l, ConstraintTypes cType, int weight)
            : base(net, cType, weight)
        {
            _v = new Variable[v.Length];
            v.CopyTo(_v, 0);
            _l = new int[l.Length];
            l.CopyTo(_l, 0);
        }

        protected internal override Constraint Copy(Network net)
        {
            return new Sequential(net, Copy(_v, net), _l);
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
            for (int i = 0; i < _v.Length - 1; i++)
            {
                int j = i + 1;
                var d0 = (IntDomain) _v[i].Domain;
                var d1 = (IntDomain) _v[j].Domain;
                int diffMin = d1.Maximum() - _l[i] + 1;
                int diffMax = d0.Minimum() + _l[i] - 1;
                d0 = d0.Delete(diffMin, IntDomain.MaxValue);
                if (d0.Empty)
                    return false;
                d1 = d1.Delete(IntDomain.MinValue, diffMax);
                if (d1.Empty)
                    return false;
                if (trail != null)
                {
                    _v[i].UpdateDomain(d0, trail);
                    _v[j].UpdateDomain(d1, trail);
                }
            }
            return true;
        }
		
        public override String ToString()
        {
            return "Sequential(" + _v + "," + ToString(_l) + ")";
        }
    }
}