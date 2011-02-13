namespace  Cream
{
	
	public abstract class Operation
	{
		protected internal int index = - 1;
		
		public abstract void  ApplyTo(Network network);
		
		public abstract bool IsTaboo(Operation op);
	}
}