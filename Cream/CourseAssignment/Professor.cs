using System;

namespace Cream.CourseAssignment
{
    public class Professor
    {
        private int index = -1;
        private static int Count = 1;

        protected internal Network Network {get; set;}

        public String Name {get; set;}

        public int Courses { get; set; }
        
        public int RealNoOfCourses { get; set; }
        
        protected internal int Index
        {
            get
            {
                return index;
            }
            set
            {
               index = value;   
            }
        }

        public Professor(Network net): this(net, 0, null)
        {
            
        }

        public Professor(Network net, int realNumberOfCourses)
            : this(net, realNumberOfCourses, null)
        {
            
        }

        public Professor(Network net, int realNumberOfCourses, string name)
        {
            Network = net;
            Courses = realNumberOfCourses;
            RealNoOfCourses = realNumberOfCourses;
            Name = name ?? "Prof" + (Count++);
            ((CourseNetwork)Network).Add(this);
        }

		private  Professor(Network net, int realNumberOfCourses, string name, int courses)
		{
			Network = net;
			Courses = courses;
			RealNoOfCourses = realNumberOfCourses;
			Name = name ?? "Prof" + (Count++);
			((CourseNetwork)Network).Add(this);
		}

		protected internal virtual Professor Copy(Network net)
		{
			return new Professor(net, RealNoOfCourses, Name, Courses);
		}

        public override string ToString()
        {
            return Name;
            
        }

        protected internal static String ToString(Professor[] pc)
        {
            String s = "";
            if (pc != null)
            {
                String delim = "";
                for (int i = 0; i < pc.Length; i++)
                {
                    s += (delim + pc[i]);
                    delim = ",";
                }
            }
            return "{" + s + "}";
        }
    }
}
