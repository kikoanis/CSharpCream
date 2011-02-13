using System;

namespace  Cream
{
	
	public class Trail
	{
		private readonly System.Collections.ArrayList _trail = new System.Collections.ArrayList();
		
		public virtual int Size()
		{
			return _trail.Count;
		}
		
		public virtual void  Push(Variable v)
		{
			var pair = new Object[]{v, v.Domain};
			_trail.Add(pair);
		}
		
		public virtual void  Undo(int size0)
		{
			for (int size = _trail.Count; size > size0; size--)
			{
				var pair = (Object[]) SupportClass.StackSupport.Pop(_trail);
				var v = (Variable) pair[0];
				v.Domain = (Domain) pair[1];
			}
		}
	}
}