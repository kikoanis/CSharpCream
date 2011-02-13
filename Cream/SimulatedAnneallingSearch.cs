// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimulatedAnneallingSearch.cs" company="pc">
//   2007-2009
// </copyright>
// <summary>
//   Defines the SimulatedAnneallingSearch type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Cream
{
	using System;
	using System.Collections;

	/// <summary>
	/// SA Solving Method
	/// </summary>
	public class SimulatedAnneallingSearch : LocalSearch
	{
		/// <summary>
		/// The gamma.
		/// </summary>
		private double _gamma = 0.999;

		/// <summary>
		/// The temperature.
		/// </summary>
		private double _temperature = 100.0;

		/// <summary>
		/// Initializes a new instance of the <see cref="SimulatedAnneallingSearch"/> class.
		/// </summary>
		/// <param name="network">
		/// The network.
		/// </param>
		public SimulatedAnneallingSearch(Network network) : this(network, Default, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SimulatedAnneallingSearch"/> class.
		/// </summary>
		/// <param name="network">
		/// The network.
		/// </param>
		/// <param name="option">
		/// The option.
		/// </param>
		public SimulatedAnneallingSearch(Network network, int option) : this(network, option, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SimulatedAnneallingSearch"/> class.
		/// </summary>
		/// <param name="network">
		/// The network.
		/// </param>
		/// <param name="name">
		/// The solver name.
		/// </param>
		public SimulatedAnneallingSearch(Network network, string name) : this(network, Default, name)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SimulatedAnneallingSearch"/> class.
		/// </summary>
		/// <param name="network">
		/// The network.
		/// </param>
		/// <param name="option">
		/// The option.
		/// </param>
		/// <param name="name">
		/// The solver name.
		/// </param>
		public SimulatedAnneallingSearch(Network network, int option, string name) : base(network, option, name)
		{
			ExchangeRate = 0.05;
		}

		/// <summary>
		/// Gets or sets Temperature.
		/// </summary>
		public virtual double Temperature
		{
			get
			{
				lock (this)
				{
					return _temperature;
				}
			}

			set
			{
				lock (this)
				{
					_temperature = value;
				}
			}
		}

		/// <summary>
		/// The next search.
		/// </summary>
		protected internal override void NextSearch()
		{
			if (totalTimeout > 0)
			{
				long elapsedTime = Math.Max(1, ((DateTime.Now.Ticks - 621355968000000000) / 10000) - startTime);
				double iterationRate = (double) iteration/elapsedTime;
				var expectedIteration = (int) (iterationRate*(totalTimeout - elapsedTime));
				if (expectedIteration > 0)
				{
					_gamma = Math.Exp(Math.Log(1.0/_temperature)/expectedIteration);
					_gamma = Math.Min(0.9999, _gamma);
				}
			}

			_temperature *= _gamma;
			solution = GetCandidate();
			int objectiveIntValue = solution.ObjectiveIntValue;
			Code code = solution.Code;
			IList operations = code.Operations();
			while (operations.Count > 0)
			{
				var i = (int) (operations.Count*SupportClass.Random.NextDouble());
				object tempObject = operations[i];
				operations.RemoveAt(i);
				var op = (Operation) tempObject;
				code.To = network;
				op.ApplyTo(network);
				Solution sol = solver.FindBest(iterTimeout);
				if (sol == null)
					continue;
				double delta = sol.ObjectiveIntValue - objectiveIntValue;
				if (IsOption(Maximize))
				{
					delta = -delta;
				}

				if (delta < 0)
				{
					solution = sol;
					return;
				}

				double p = Math.Exp((-delta)/_temperature);
				if (p < SupportClass.Random.NextDouble())
					continue;
				solution = sol;
				return;
			}

			solution = null;
		}

		/// <summary>
		/// The start search.
		/// </summary>
		protected internal override void StartSearch()
		{
			base.StartSearch();

			_temperature = solution==null?0:solution.ObjectiveIntValue/10.0;
		}
	}
}