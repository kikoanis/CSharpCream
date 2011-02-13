using System;

namespace  Cream
{
	
	public class Serialized:Constraint
	{
		private readonly Variable[] _v;
		private readonly int[] _l;
		private int[] _order;

        public Serialized(Network net, Variable[] v, int[] l)
            : this(net, v,l, ConstraintTypes.Hard)
        {
        }

        public Serialized(Network net, Variable[] v, int[] l, ConstraintTypes cType)
            : this(net, v, l, cType, 0)
        {
        }

        public Serialized(Network net, Variable[] v, int[] l, ConstraintTypes cType, int weight)
            : base(net, cType, weight)
        {
            _v = new Variable[v.Length];
            v.CopyTo(_v, 0);
            _l = new int[l.Length];
            l.CopyTo(_l, 0);
            _order = null;
        }

        protected internal override Constraint Copy(Network net)
		{
			return new Serialized(net, Copy(_v, net), _l);
		}
		
		protected internal class SerializedCondition:Condition
		{
			private sealed class AnonymousClassComparator : System.Collections.IComparer
			{
				public AnonymousClassComparator(SerializedCondition enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(SerializedCondition enclosingInstance)
				{
					EnclosingInstance = enclosingInstance;
				}

				private SerializedCondition EnclosingInstance { get; set; }

			    public int Compare(Object o1, Object o2)
				{
					int k1 = ((int[]) o1)[1];
					int k2 = ((int[]) o2)[1];
					return (k1 < k2)?- 1:((k1 == k2)?0:1);
				}
			}
			private void  InitBlock(Serialized enclosingInstance)
			{
				EnclosingInstance = enclosingInstance;
			}

		    override public Network To
			{
				set
				{
					var s = (Serialized) value.GetConstraint(index);
					if (_code == null)
					{
						s._order = null;
					}
					else
					{
						s._order = new int[_code.Length];
						for (int i = 0; i < s._order.Length; i++)
						{
							s._order[i] = _code[i][0];
						}
					}
				}
				
			}

		    public Serialized EnclosingInstance { get; set; }

		    private readonly int[][] _code;
			
			public SerializedCondition(Serialized enclosingInstance)
			{
				InitBlock(enclosingInstance);
				index = EnclosingInstance.Index;
				_code = new int[EnclosingInstance._v.Length][];
				for (int i = 0; i < EnclosingInstance._v.Length; i++)
				{
					_code[i] = new int[3];
				}
				for (int i = 0; i < _code.Length; i++)
				{
					Domain d = EnclosingInstance._v[i].Domain;
					_code[i][0] = i;
					_code[i][1] = ((IntDomain) d).Value();
					_code[i][2] = EnclosingInstance._l[i];
				}
				System.Collections.IComparer comp = new AnonymousClassComparator(this);
				Array.Sort(_code, comp);
			}
			
			public override System.Collections.IList Operations()
			{
				System.Collections.IList operations = new System.Collections.ArrayList();
				for (int i = 0; i < _code.Length - 1; i++)
				{
					if (_code[i][1] + _code[i][2] == _code[i + 1][1])
					{
						// adjacent
						Operation op = new Swap(EnclosingInstance, index, i, i + 1);
						operations.Add(op);
					}
				}
				return operations;
			}
		}
		
		private class Swap:Operation
		{
			private void  InitBlock(Serialized enclosingInstance)
			{
				EnclosingInstance = enclosingInstance;
			}

			private Serialized EnclosingInstance { get; set; }

			private readonly int _index;
			private readonly int _i;
			private readonly int _j;
			
			public Swap(Serialized enclosingInstance, int index, int i, int j)
			{
				InitBlock(enclosingInstance);
				_index = index;
				_i = i;
				_j = j;
			}
			
			public override void  ApplyTo(Network network)
			{
				var s = (Serialized) network.GetConstraint(_index);
				int t = s._order[_i]; s._order[_i] = s._order[_j]; s._order[_j] = t;
			}
			
			public override bool IsTaboo(Operation op)
			{
				if (!(op is Swap))
					return false;
				var swap = (Swap) op;
				return _index == swap._index && _i == swap._i && _j == swap._j;
			}
		}
		
		protected internal override void  ClearCondition()
		{
			_order = null;
		}
		
		protected internal override Condition ExtractCondition()
		{
			return new SerializedCondition(this);
		}
		
		protected internal override bool IsModified()
		{
			return IsModified(_v);
		}
		
		private bool SatisfySequential(Trail trail)
		{
			if (_order == null)
				return true;
			for (int k = 0; k < _order.Length - 1; k++)
			{
				int i = _order[k];
				int j = _order[k + 1];
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
		
		private bool SatisfySerialized(Trail trail)
		{
			for (int i = 0; i < _v.Length; i++)
			{
				for (int j = 0; j < _v.Length; j++)
				{
					if (i == j)
						continue;
					var d0 = (IntDomain) _v[i].Domain;
					var d1 = (IntDomain) _v[j].Domain;
					int diffMin = d1.Maximum() - _l[i] + 1;
					int diffMax = d1.Minimum() + _l[j] - 1;
					if (diffMin <= diffMax)
					{
						d0 = d0.Delete(diffMin, diffMax);
						if (d0.Empty)
							return false;
                        if (trail != null)
                        {
                            _v[i].UpdateDomain(d0, trail);
                        }
					}
				}
			}
			return true;
		}

        protected internal override bool IsSatisfied()
        {
            return Satisfy(null);
        }
        protected internal override bool Satisfy(Trail trail)
		{
			if (!SatisfySequential(trail))
				return false;
			return SatisfySerialized(trail);
		}
		
		public override String ToString()
		{
			return "Serialized(" + ToString(_v) + "," + ToString(_l) + ")";
		}
	}
}