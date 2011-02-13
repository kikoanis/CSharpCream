// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IntDomain.cs" company="U of R">
//   Copyright 2008
// </copyright>
// <summary>
//   Defines the IntDomain type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;

namespace Cream
{
	using System;
	using System.Collections;
	using System.Collections.Generic;

	/// <summary>
	/// IntDomain Class
	/// </summary>
	public class IntDomain : Domain
	{
		#region Constants

		/// <summary>
		/// Maximum value
		/// </summary>
		public const int MaxValue = 0x3fffffff;

		/// <summary>
		/// minimum value
		/// </summary>
		public const int MinValue = unchecked((int)0xc0000001);

		#endregion

		#region Fields

		/// <summary>
		/// EmptyDomain instance
		/// </summary>
		public static IntDomain emptyDomain = new IntDomain();

		/// <summary>
		/// FullDomain Domain
		/// </summary>
		public static IntDomain fullDomain = new IntDomain(MinValue, MaxValue);

		/// <summary>
		/// list of intervals
		/// </summary>
		private readonly ArrayList _intervals = new ArrayList();

		/// <summary>
		/// maximum field
		/// </summary>
		private int _maxField;

		/// <summary>
		/// minimum field
		/// </summary>
		private int _minField;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="IntDomain"/> class.
		/// </summary>
		public IntDomain()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="IntDomain"/> class.
		/// </summary>
		/// <param name="minimum">
		/// The minimum.
		/// </param>
		/// <param name="maximum">
		/// The maximum.
		/// </param>
		public IntDomain(int minimum, int maximum)
		{
			minimum = Math.Max(minimum, MinValue);
			maximum = Math.Min(maximum, MaxValue);
			if (minimum <= maximum)
			{
				var interval = new[] { minimum, maximum };
				_intervals.Add(interval);
				sizeField = maximum - minimum + 1;
				_minField = minimum;
				_maxField = maximum;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="IntDomain"/> class.
		/// </summary>
		/// <param name="value">
		/// The value.
		/// </param>
		public IntDomain(int value)
			: this(value, value)
		{
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Capitalize domain
		/// </summary>
		/// <param name="domain">
		/// The domain.
		/// </param>
		/// <returns>
		/// A modified domain
		/// </returns>
		public override Domain Cap(Domain domain)
		{
			if (!(domain is IntDomain))
			{
				return emptyDomain;
			}

			var newD = new IntDomain();
			IntDomain d0 = this;
			var d1 = (IntDomain)domain;
			try
			{
				int i0 = 0;
				int i1 = 0;
				while (i0 < d0._intervals.Count && i1 < d1._intervals.Count)
				{
					var interval = (int[])d0._intervals[i0];
					int min0 = interval[0];
					int max0 = interval[1];
					interval = (int[])d1._intervals[i1];
					int min1 = interval[0];
					int max1 = interval[1];
					if (max0 < min1)
					{
						i0++;
						continue;
					}

					if (max1 < min0)
					{
						i1++;
						continue;
					}

					interval = new int[2];
					interval[0] = Math.Max(min0, min1);
					interval[1] = Math.Min(max0, max1);
					newD._intervals.Add(interval);
					if (max0 <= max1)
					{
						i0++;
					}

					if (max1 <= max0)
					{
						i1++;
					}
				}
			}
			catch (IndexOutOfRangeException)
			{
			}

			newD.UpdateSize();
			newD.UpdateMinMax();
			if (newD.Empty)
			{
				return emptyDomain;
			}

			return newD;
		}

		/// <summary>
		/// Captilizes interval
		/// </summary>
		/// <param name="low">
		/// The low.value
		/// </param>
		/// <param name="high">
		/// The high.value
		/// </param>
		/// <returns>
		/// a modified domain
		/// </returns>
		public virtual IntDomain CapInterval(int low, int high)
		{
			IntDomain d = this;
			if (MinValue < low)
			{
				d = d.Delete(MinValue, low - 1);
			}

			if (high < MaxValue)
			{
				d = d.Delete(high + 1, MaxValue);
			}

			return d;
		}

		/// <summary>
		/// Clones this object
		/// </summary>
		/// <returns>
		/// an object value
		/// </returns>
		public override object Clone()
		{
			var d = new IntDomain();
			try
			{
				for (int i = 0; i < _intervals.Count; ++i)
				{
					d._intervals.Add(((int[])_intervals[i]).Clone());
				}
			}
			catch (IndexOutOfRangeException)
			{
			}

			d.sizeField = sizeField;
			d._minField = _minField;
			d._maxField = _maxField;
			return d;
		}

		/// <summary>
		/// checks if a value is contained
		/// </summary>
		/// <param name="element">
		/// The element.
		/// </param>
		/// <returns>
		/// boolean value
		/// </returns>
		public virtual bool Contains(int element)
		{
			return IndexOf(element) >= 0;
		}

		/// <summary>
		/// checks if an ovjectValue is contained
		/// </summary>
		/// <param name="ovjectValue">
		/// The ovjectValue.
		/// </param>
		/// <returns>
		/// boolean value
		/// </returns>
		public override bool Contains(object ovjectValue)
		{
			if (!(ovjectValue is ValueType))
			{
				return false;
			}

			return Contains(Convert.ToInt32(ovjectValue));
		}

		/// <summary>
		/// not implemented method
		/// </summary>
		/// <param name="domain">
		/// The domain.
		/// </param>
		/// <returns>
		/// domain object
		/// </returns>
		/// <exception cref="NotImplementedException">
		/// </exception>
		public override Domain Cup(Domain domain)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Deletes values between minimum and miximum values
		/// </summary>
		/// <param name="low">
		/// The low.value
		/// </param>
		/// <param name="high">
		/// The high.value
		/// </param>
		/// <returns>
		/// IntDomain object
		/// </returns>
		public virtual IntDomain Delete(int low, int high)
		{
			if (sizeField == 0 || low > high || high < _minField || _maxField < low)
			{
				return this;
			}

			if (low == high)
			{
				return Delete(low);
			}

			var d = new IntDomain();
			try
			{
				for (int i = 0; i < _intervals.Count; i++)
				{
					var interval = (int[])_intervals[i];
					int mi = Math.Max(low, interval[0]);
					int ma = Math.Min(high, interval[1]);
					if (mi <= ma)
					{
						if (interval[0] < mi)
						{
							var period = new int[2];
							period[0] = interval[0];
							period[1] = mi - 1;
							d._intervals.Add(period);
						}

						if (ma < interval[1])
						{
							var period = new int[2];
							period[0] = ma + 1;
							period[1] = interval[1];
							d._intervals.Add(period);
						}
					}
					else
					{
						var period = new int[2];
						period[0] = interval[0];
						period[1] = interval[1];
						d._intervals.Add(period);
					}
				}
			}
			catch (IndexOutOfRangeException)
			{
			}

			d.UpdateSize();
			d.UpdateMinMax();
			return d;
		}

		/// <summary>
		/// Deletes an object from domain
		/// </summary>
		/// <param name="objectValue">
		/// The objectValue.
		/// </param>
		/// <returns>
		/// Domain instance
		/// </returns>
		public override Domain Delete(object objectValue)
		{
			if (!(objectValue is ValueType))
			{
				return this;
			}

			return Delete(Convert.ToInt32(objectValue));
		}

		/// <summary>
		/// deletes an element from domain
		/// </summary>
		/// <param name="element">
		/// The element.value
		/// </param>
		/// <returns>
		/// IntDomain object
		/// </returns>
		public virtual IntDomain Delete(int element)
		{
			if (!Contains(element))
			{
				return this;
			}

			var d = (IntDomain)Clone();
			d.Remove(element);
			return d;
		}

		/// <summary>
		/// Difference between two domains
		/// </summary>
		/// <param name="domian">
		/// The domian.
		/// </param>
		/// <returns>
		/// Domain object
		/// </returns>
		/// <exception cref="NotImplementedException">
		/// </exception>
		public override Domain Difference(Domain domian)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Elemnt methos
		/// </summary>
		/// <returns>
		///  object value
		/// </returns>
		public override object Element()
		{
			return Value();
		}

		/// <summary>
		/// Enumeration of elements
		/// </summary>
		/// <returns>
		/// Enumeration list of elements
		/// </returns>
		public override IEnumerator Elements()
		{
			IEnumerator iter = new IntDomainIterator(this);
			return iter;
		}

		/// <summary>
		/// Checks for equality qith another domain
		/// </summary>
		/// <param name="domain">
		/// The domain
		/// </param>
		/// <returns>
		/// boolean value
		/// </returns>
		public override bool Equals(Domain domain)
		{
			if (this == domain)
			{
				return true;
			}

			if (!(domain is IntDomain))
			{
				return false;
			}

			var d = (IntDomain)domain;
			if (_intervals.Count != d._intervals.Count)
			{
				return false;
			}

			try
			{
				for (int i = 0; i < _intervals.Count; i++)
				{
					var i0 = (int[])_intervals[i];
					var i1 = (int[])d._intervals[i];
					if (i0[0] != i1[0] || i0[1] != i1[1])
					{
						return false;
					}
				}
			}
			catch (IndexOutOfRangeException)
			{
			}

			return true;
		}

		/// <summary>
		/// Gets all elements of this domain
		/// </summary>
		/// <returns>
		/// list of all elements of this domain
		/// </returns>
		public IList<int> GetElements()
		{
			IList<int> elements = new List<int>();
			foreach (object t in _intervals)
			{
				var interval = (int[])t;
				for (int j = interval[0]; j <= interval[1]; j++)
				{
					elements.Add(j);
				}
			}

			return elements;
		}

		/// <summary>
		/// Inserts an element in the domain
		/// </summary>
		/// <param name="objectValue">
		/// The objectValue.
		/// </param>
		/// <returns>
		/// Domain object
		/// </returns>
		/// <exception cref="NotImplementedException">
		/// </exception>
		public override Domain Insert(object objectValue)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Maximum value of a domain
		/// </summary>
		/// <returns>
		/// integer value
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// </exception>
		public virtual int Maximum()
		{
			if (sizeField == 0)
			{
				throw new ArgumentOutOfRangeException();
			}

			return _maxField;
		}

		/// <summary>
		/// Minimum value of a domain
		/// </summary>
		/// <returns>
		/// integer value
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// </exception>
		public virtual int Minimum()
		{
			if (sizeField == 0)
			{
				throw new ArgumentOutOfRangeException();
			}

			return _minField;
		}

		/// <summary>
		/// Returns a randomly selected Element value.
		/// </summary>
		/// <returns>return a randomly selected Element value</returns>
		public int RandomElement()
		{
			var ran = new Random();
			int n = ran.Next() * sizeField;
			foreach (int[] interval in _intervals)
			{
				int m = interval[1] - interval[0] + 1;
				if (n < m)
				{
					return interval[0] + n;
				}

				n -= m;
			}

			return Minimum();
		}

		/// <summary>
		/// Removesd an element from domain
		/// </summary>
		/// <param name="element">
		/// The element.
		/// </param>
		public virtual void Remove(int element)
		{
			int i = IndexOf(element);
			if (i < 0)
			{
				return;
			}

			try
			{
				var interval = (int[])_intervals[i];
				int lo = interval[0];
				int hi = interval[1];
				if (element == lo && element == hi)
				{
					_intervals.RemoveAt(i);
				}
				else if (element == lo)
				{
					interval[0] = lo + 1;
				}
				else if (element == hi)
				{
					interval[1] = hi - 1;
				}
				else
				{
					interval[0] = element + 1;
					_intervals.Insert(i, new[] { lo, element - 1 });
				}

				sizeField--;
				UpdateMinMax();
			}
			catch (IndexOutOfRangeException)
			{
			}
		}

		/// <summary>
		/// Removes an object from domain
		/// </summary>
		/// <param name="objectValue">
		/// The objectValue.
		/// </param>
		public virtual void Remove(object objectValue)
		{
			if (!(objectValue is ValueType))
			{
				return;
			}

			Remove(Convert.ToInt32(objectValue));
		}

		/// <summary>
		/// Converts this domain to string value
		/// </summary>
		/// <returns>
		/// string value
		/// </returns>
		public override string ToString()
		{
			string s = string.Empty;
			string delim = string.Empty;
			try
			{
				foreach (var interval in _intervals.Cast<int[]>())
				{
					s += delim + ToString(interval[0]);
					if (interval[0] < interval[1])
					{
						s += "-" + ToString(interval[1]);
					}

					delim = ",";
				}
			}
			catch (IndexOutOfRangeException)
			{
			}

			if (Size() == 1)
			{
				return s;
			}

			return "{" + s + "}";
		}

		/// <summary>
		/// return the minimum value of a domian
		/// </summary>
		/// <returns>
		/// integer value
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// </exception>
		public virtual int Value()
		{
			if (Size() != 1)
			{
				throw new ArgumentOutOfRangeException();
			}

			return _minField;
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// returns an index of an object within the domain
		/// </summary>
		/// <param name="objectValue">
		/// The objectValue.
		/// </param>
		/// <returns>
		/// integer value
		/// </returns>
		private int IndexOf(object objectValue)
		{
			if (!(objectValue is ValueType))
			{
				return -1;
			}

			return IndexOf(Convert.ToInt32(objectValue));
		}

		/// <summary>
		/// Returns an index of an element within domain
		/// </summary>
		/// <param name="element">
		/// The element.
		/// </param>
		/// <returns>
		/// integer value
		/// </returns>
		private int IndexOf(int element)
		{
			if (element < _minField || _maxField < element)
			{
				return -1;
			}

			try
			{
				for (int i = 0; i < _intervals.Count; i++)
				{
					var interval = (int[])_intervals[i];
					if (element < interval[0])
					{
						return -1;
					}

					if (element <= interval[1])
					{
						return i;
					}
				}
			}
			catch (IndexOutOfRangeException)
			{
			}

			return -1;
		}

		/// <summary>
		/// returns a string if the value checked is the mimimum or maximum value
		/// </summary>
		/// <param name="element">
		/// The element.
		/// </param>
		/// <returns>
		/// string value
		/// </returns>
		private static string ToString(int element)
		{
			if (element == MinValue)
			{
				return "Min";
			}

			if (element == MaxValue)
			{
				return "Max";
			}

			return Convert.ToString(element);
		}

		/// <summary>
		/// Updates minimum and maximum values for the domain
		/// </summary>
		private void UpdateMinMax()
		{
			if (sizeField > 0)
			{
				try
				{
					var interval = (int[])_intervals[0];
					_minField = interval[0];
					interval = (int[])_intervals[_intervals.Count - 1];
					_maxField = interval[1];
				}
				catch (IndexOutOfRangeException)
				{
				}
			}
		}

		/// <summary>
		/// Updates the size of the domain
		/// </summary>
		private void UpdateSize()
		{
			sizeField = 0;
			try
			{
				foreach (object t in _intervals)
				{
					var interval = (int[])t;
					sizeField += interval[1] - interval[0] + 1;
				}
			}
			catch (IndexOutOfRangeException)
			{
			}
		}

		#endregion

		/// <summary>
		/// Private class IntDomainIterator
		/// </summary>
		private sealed class IntDomainIterator : IEnumerator
		{
			#region Fields

			/// <summary>
			/// choice field
			/// </summary>
			private int _choice;

			/// <summary>
			/// enclosing instance
			/// </summary>
			private IntDomain _enclosingInstance;

			#endregion

			#region Constructors

			/// <summary>
			/// Initializes a new instance of the <see cref="IntDomainIterator"/> class.
			/// </summary>
			/// <param name="enclosingInstance">
			/// The enclosing instance.
			/// </param>
			public IntDomainIterator(IntDomain enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}

			#endregion

			#region Properties

			/// <summary>
			/// Gets Current.
			/// </summary>
			/// <exception cref="ArgumentOutOfRangeException">
			/// </exception>
			public object Current
			{
				get
				{
					if (!MoveNext())
					{
						throw new ArgumentOutOfRangeException();
					}

					var d = new IntDomain(_choice);
					_choice++;
					return d;
				}
			}

			/// <summary>
			/// Gets Enclosing_Instance.
			/// </summary>
			private IntDomain Enclosing_Instance
			{
				get
				{
					return _enclosingInstance;
				}
			}

			#endregion

			#region Public Methods

			/// <summary>
			/// moves to next value in domain
			/// </summary>
			/// <returns>
			/// boolean value
			/// </returns>
			public bool MoveNext()
			{
				if (Enclosing_Instance.sizeField == 0)
				{
					return false;
				}

				while (_choice <= Enclosing_Instance._maxField)
				{
					if (Enclosing_Instance.Contains(_choice))
					{
						return true;
					}

					_choice++;
				}

				return false;
			}

			/// <summary>
			/// Resets iteration
			/// </summary>
			public void Reset()
			{
			}

			#endregion

			#region Private Methods

			/// <summary>
			/// Initiates block
			/// </summary>
			/// <param name="enclosingIns">
			/// The enclosing instance
			/// </param>
			private void InitBlock(IntDomain enclosingIns)
			{
				_enclosingInstance = enclosingIns;
				_choice = Enclosing_Instance._minField;
			}

			#endregion
		}
	}
}