/*
* @(#)Domain.cs
*/
using System;

namespace  Cream
{
	
	/// <summary> Domains.
	/// This is an abstract class for domains.
	/// A domain ...
	/// </summary>
	/// <seealso cref="Variable">
	/// </seealso>
	/// <since> 1.0
	/// </since>
	/// <version>  1.0, 01/12/08
	/// </version>
    /// <author>  Based on Java Solver by : Naoyuki Tamura (tamura@kobe-u.ac.jp) 
    ///           C#: Ali Hmer (Hmer200a@uregina.ca)
	/// </author>
	public abstract class Domain : ICloneable
	{
		virtual public bool Empty
		{
			get
			{
				return Size() == 0;
			}
			
		}
		protected internal int sizeField;
		
		public virtual int Size()
		{
			return sizeField;
		}
		
		public abstract Object Clone();
		
		public abstract bool Equals(Domain d);
		
		public abstract System.Collections.IEnumerator Elements();
		
		public abstract Object Element();
		
		public abstract bool Contains(Object o);
		
		public abstract Domain Insert(Object o);
		
		public abstract Domain Delete(Object o);
		
		public abstract Domain Cap(Domain d);
		
		public abstract Domain Cup(Domain d);
		
		public abstract Domain Difference(Domain d);
	}
}