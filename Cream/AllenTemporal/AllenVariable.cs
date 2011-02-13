using System;
using System.Collections;

namespace Cream.AllenTemporal
{
    /// <summary> Allen variables.</summary>
    /// <seealso cref="IntDomain">
    /// </seealso>
    /// <since> 1.0
    /// </since>
    /// <version>  1.0, 01/12/08
    /// </version>
    /// <author>  Based on Java Solver by : Naoyuki Tamura (tamura@kobe-u.ac.jp) 
    ///           C#: Ali Hmer (Hmer200a@uregina.ca)
    /// </author>
    public class AllenVariable: Variable
    {
        /// <summary> Constructs an Allen variable of the network
		/// with an initial Allen domain <tt>d</tt>
		/// and a default name.
        /// This constructor is equivalent to <tt>AllenVariable(network, d, null)</tt>.
		/// </summary>
		/// <param name="net">the network
		/// </param>
		/// <param name="d">the initial Allen domain
		/// </param>
		public AllenVariable(Network net, Domain d):this(net, d, null)
		{
		}
		
		/// <summary> Constructs an Allen variable of the network
		/// with an initial allen domain <tt>d</tt>
		/// and a name specified by the parameter <tt>name</tt>.
		/// When the parameter <tt>name</tt> is <tt>null</tt>,
		/// default names (<tt>v1</tt>, <tt>v2</tt>, and so on) are used.
		/// </summary>
		/// <param name="net">the network
		/// </param>
		/// <param name="d">the initial allen domain
		/// </param>
		/// <param name="name">the name of the variable, or <tt>null</tt> for a default name
		/// </param>
        public AllenVariable(Network net, Domain d, String name)
            : base(net, d, name)
		{
		}

        public AllenVariable(Network net, String name): this(net, new AllenDomain(AllenDomain.MinValue, AllenDomain.MaxValue), name)
        {
        }

        public AllenVariable(Network net): this(net, new AllenDomain(AllenDomain.MinValue, AllenDomain.MaxValue))
        {
        }
		
		public AllenVariable(Network net, int st, int lt):base(net, new AllenDomain(st, lt))
		{
		}

        public AllenVariable(Network net, int st, int lt, int duration)
            : base(net, new AllenDomain(st, lt, duration))
        {
        }

        public AllenVariable(Network net, int st, int lt, int duration, int step)
            : base(net, new AllenDomain(st, lt, duration, step))
        {
        }
        
        public AllenVariable(Network net, int st, int lt, String name)
            : this(net, new AllenDomain(st, lt), name)
        {
        }

        public AllenVariable(Network net, int st, int lt, int duration, String name)
            : this(net, new AllenDomain(st, lt, duration), name)
        {
        }

        public AllenVariable(Network net, int st, int lt, int duration, int step, String name)
            : this(net, new AllenDomain(st, lt, duration, step), name)
        {
        }
        
        public CVarToVar[] GetConstraintsVars()
        {
            var constraints = new CVarToVar[Network.Variables.Count];
            for (int i = 0; i < Network.Variables.Count; i++)
            {
                constraints[i] = new CVarToVar(); 
            }
            IEnumerator cs = Network.Constraints.GetEnumerator();
            while (cs.MoveNext())
            {
                var ac = (AllenConstraint) cs.Current;
                if (ac.Vars[0] == this)
                {
                    constraints[ac.Vars[1].Index].ConstraintIndex = ac.AllenEvent;
                    constraints[ac.Vars[1].Index].Var1 = (AllenVariable)ac.Vars[0];
                    constraints[ac.Vars[1].Index].Var2 = (AllenVariable)ac.Vars[1];
                }
            }
            return constraints;
        }

        public virtual void Precedes(AllenVariable v)
        {
            Network net = Network;
            // this Precedes v
            if (this != v)
            {
                new AllenConstraint(net, AllenEvents.PRECEDES, this, v);
            }
            else
            {
                throw new NotImplementedException("Variable cannot event itself!!");
            }
        }

        public virtual void Precededby(AllenVariable v)
        {
            Network net = Network;
            // this Precededby v
            if (this != v)
            {
                new AllenConstraint(net, AllenEvents.PRECEDEDBY, this, v);
            }
            else
            {
                throw new NotImplementedException("Variable cannot event itself!!");
            }
        }

        public virtual void Equals(AllenVariable v)
        {
            Network net = Network;
            // this equal v
            if (this != v)
            {
                new AllenConstraint(net, AllenEvents.EQUALS, this, v);
            }
            else
            {
                throw new NotImplementedException("Variable cannot event itself!!");
            }
        }

        public virtual void Meets(AllenVariable v)
        {
            Network net = Network;
            // this meet v
            if (this != v)
            {
                new AllenConstraint(net, AllenEvents.MEETS, this, v);
            }
            else
            {
                throw new NotImplementedException("Variable cannot event itself!!");
            }
        }

        public virtual void MetBy(AllenVariable v)
        {
            Network net = Network;
            // this met by v
            if (this != v)
            {
                new AllenConstraint(net, AllenEvents.METBY, this, v);
            }
            else
            {
                throw new NotImplementedException("Variable cannot event itself!!");
            }
        }

        public virtual void During(AllenVariable v)
        {
            Network net = Network;
            // this During v
            if (this != v)
            {
                new AllenConstraint(net, AllenEvents.DURING, this, v);
            }
            else
            {
                throw new NotImplementedException("Variable cannot event itself!!");
            }
        }

        public virtual void Contains(AllenVariable v)
        {
            Network net = Network;
            // this Contains v
            if (this != v)
            {
                new AllenConstraint(net, AllenEvents.CONTAINS, this, v);
            }
            else
            {
                throw new NotImplementedException("Variable cannot event itself!!");
            }
        }

        public virtual void Starts(AllenVariable v)
        {
            Network net = Network;
            // this Starts v
            if (this != v)
            {
                new AllenConstraint(net, AllenEvents.STARTS, this, v);
            }
            else
            {
                throw new NotImplementedException("Variable cannot event itself!!");
            }
        }

        public virtual void StartedBy(AllenVariable v)
        {
            Network net = Network;
            // this started by v
            if (this != v)
            {
                new AllenConstraint(net, AllenEvents.STARTEDBY, this, v);
            }
            else
            {
                throw new NotImplementedException("Variable cannot event itself!!");
            }
        }

        public virtual void Finishes(AllenVariable v)
        {
            Network net = Network;
            // this Finishes v
            if (this != v)
            {
                new AllenConstraint(net, AllenEvents.FINISHES, this, v);
            }
            else
            {
                throw new NotImplementedException("Variable cannot event itself!!");
            }
        }

        public virtual void FinishedBy(AllenVariable v)
        {
            Network net = Network;
            // this finished By v
            if (this != v)
            {
                new AllenConstraint(net, AllenEvents.FINISHEDBY, this, v);
            }
            else
            {
                throw new NotImplementedException("Variable cannot event itself!!");
            }
        }

        public virtual void Overlaps(AllenVariable v)
        {
            Network net = Network;
            // this Overlaps v
            if (this != v)
            {
                new AllenConstraint(net, AllenEvents.OVERLAPS, this, v);
            }
            else
            {
                throw new NotImplementedException("Variable cannot event itself!!");
            }
        }

        public virtual void OverlappedBy(AllenVariable v)
        {
            Network net = Network;
            // this overlapped By v
            if (this != v)
            {
                new AllenConstraint(net, AllenEvents.OVERLAPPEDBY, this, v);
            }
            else
            {
                throw new NotImplementedException("Variable cannot event itself!!");
            }
        }
    }
}
