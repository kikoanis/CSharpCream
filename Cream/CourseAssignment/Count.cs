using System;
using System.Text;

namespace Cream.CourseAssignment
{
    public class Count:Constraint
    {
        private readonly Variable[] v;

        public Count(Network net, Variable[] v)
            : base(net)
        {
            this.v = new Variable[v.Length];
            v.CopyTo(this.v, 0);
        }

        public Count(Network net, Variable[] v, ConstraintTypes cType)
            : base(net, cType)
        {
            this.v = new Variable[v.Length];
            v.CopyTo(this.v, 0);
        }

        public Count(Network net)
            : base(net)
        {
        }

        public Count(Network net, ConstraintTypes cType)
            : base(net, cType)
        {
        }

        protected internal override Constraint Copy(Network net)
        {
            return new Count(net, Copy(v, net));
        }

        protected internal override bool IsModified()
        {
            return IsModified(v);
        }

        protected internal override bool IsSatisfied()
        {
            return Satisfy(null);
        }
        protected internal override bool Satisfy(Trail trail)
        {
            var net = (CourseNetwork) Network;
            var prof = new int[net.Professors.Count];
            foreach (Variable var in v)
            {
                if (!var.IsValueType)
                {
                    Domain d = var.Domain;
                    if (d.Size() != 1)
                        continue;
                    prof[(int) d.Element()]++;
                    if (prof[(int) d.Element()] > ((Professor) net.Professors[(int) d.Element()]).Courses)
                    {
                        return false;
                    }
                    if (prof[(int) d.Element()] == ((Professor) net.Professors[(int) d.Element()]).Courses)
                    {
                        foreach (Variable va in v)
                        {
                            if ((va != var) && (va.Domain.Size() != 1))
                            {
                                var newD = (IntDomain) va.Domain;
                                newD = (IntDomain) newD.Delete(d.Element());
                                if (trail != null)
                                {
                                    va.UpdateDomain(newD, trail);
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }

        public override String ToString()
        {
            var str = new StringBuilder();
        	//var profCount = 0;
        	//var totlaCount = 0;
            foreach (Professor professor in ((CourseNetwork)Network).Professors)
            {

                if (professor.Index != 0)
                {
                    if (professor.Courses == 0)
                    {
                        str.Append(professor + " has no courses to be assigned \n");
                    }
                    else
                    {
                    	//profCount++;
                    	//totlaCount += professor.Courses;
						if (professor.Courses == 1)
						{
							str.Append(professor + " can be assigned just 1 course \n");
						}
						else
						{
							str.Append(professor + " has a maximum of " + professor.Courses.ToString() +
							           " courses to be assigned \n");
						}
                    }
                }
            }
        	//str.Append("Count was found on " + profCount + " professor and the total count courses is " + totlaCount);
            return str.ToString();
        }
    }
}
