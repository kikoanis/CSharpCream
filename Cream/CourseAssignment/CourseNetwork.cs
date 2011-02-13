using System;

namespace Cream.CourseAssignment
{
    public class CourseNetwork : Network
    {
        
        private System.Collections.IList professors;

        public CourseNetwork()
		{
			Professors = new System.Collections.ArrayList();
		}
        public System.Collections.IList Professors
        {
            get
            {
                return professors;
            }
            set
            {
                professors = value;
            }
        }
        /// <summary> Adds a profCourse to this network.
        /// If the profCourse is already in the nework, this invocation has no effect.
        /// </summary>
        /// <param name="pc">the profCourse to be added
        /// </param>
        /// <returns> the variable itself
        /// </returns>
        /// <throws>  NullPointerException if <tt>v</tt> is <tt>null</tt> </throws>
        /// <throws>  IllegalArgumentException if <tt>v</tt> is already added to another network </throws>
        protected internal virtual Professor Add(Professor pc)
        {
            if (!professors.Contains(pc))
            {
                if (pc.Index >= 0)
                {
                    throw new ArgumentException();
                }
                pc.Index = professors.Count;
                professors.Add(pc);
            }
            return pc;
        }

		/// <summary> Returns a Copy of this network.
		/// The new network has the same structure as the original network.
		/// </summary>
		/// <returns> a Copy of this network
		/// </returns>
		public override Object Clone()
		{
			var net = new CourseNetwork();
			var vs = Variables.GetEnumerator();
			while (vs.MoveNext())
			{
				var v = (Variable)vs.Current;
				Variable v1 = v.Copy(net);
				if (v.Index != v1.Index)
					throw new ArgumentException();
			}
			var vp = Professors.GetEnumerator();
			while (vp.MoveNext())
			{
				var v = (Professor)vp.Current;
				Professor v1 = v.Copy(net);
				if (v.Index != v1.Index)
					throw new ArgumentException();
			}
			var cs = Constraints.GetEnumerator();
			while (cs.MoveNext())
			{
				var c = (Constraint)cs.Current;
				Constraint c1 = c.Copy(net);
				if (c.Index != c1.Index)
					throw new ArgumentException();
			}
			if (Objective != null)
			{
				net.Objective = net.GetVariable(Objective.Index);
			}
			return net;
		}
		

		
    }
}
