/*
* @(#)ISolutionHandler.cs
*/
namespace  Cream
{
	
	/// <summary> An interface for solution handlers.
	/// A solution handler is invoked by {@linkplain Solver a solver}.
	/// See {@link Solver} for example programs.
	/// </summary>
	/// <seealso cref="Solver">
	/// </seealso>
	/// <seealso cref="Solution">
	/// </seealso>
	/// <since> 1.0
	/// </since>
	/// <version>  1.0, 01/12/08
	/// </version>
    /// <author>  Based on Java Solver by : Naoyuki Tamura (tamura@kobe-u.ac.jp) 
    ///           C#: Ali Hmer (Hmer200a@uregina.ca)
	/// </author>
	public interface ISolutionHandler
	{
		
		/// <summary> This method is called for each sol and
		/// at the end of search (<tt>sol</tt> is set to <tt>null</tt>).
		/// </summary>
		/// <param name="solver">the solver
		/// </param>
		/// <param name="sol">the solution or <tt>null</tt> at the end of search
		/// </param>
		void  Solved(Solver solver, Solution sol);
	}
}