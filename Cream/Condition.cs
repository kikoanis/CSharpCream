using System.Collections;

namespace  Cream
{
    public abstract class Condition
    {
        public abstract Network To{set;}
        protected internal int index = - 1;
		
        public abstract IList Operations();
    }
}