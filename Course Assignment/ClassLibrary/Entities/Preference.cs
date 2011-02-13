// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Preference.cs" company="U of R">
//   Ali Hmer 2009
// </copyright>
// <summary>
//   Defines the Preference type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ClassLibrary
{
	using System;
	using System.IO;
	using System.Xml.Serialization;
	using Interfaces;

	/// <summary>
	/// Class preference
	/// </summary>
	[Serializable]
	public partial class Preference : ICloneable, IEntity
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Preference"/> class.
		/// </summary>
		/// <param name="id">
		/// The id. for preference id
		/// </param>
		public Preference(PreferencesId id)
		{
			Id = id;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Preference"/> class.
		/// </summary>
		/// <param name="id">The preference id.</param>
		/// <param name="weight">The preference weight.</param>
		public Preference(PreferencesId id, int weight)
		{
			Id = id;
			Weight = weight;
			PreferenceType = PreferenceTypes.Equal;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Preference"/> class.
		/// </summary>
		public Preference()
		{
			Id = new PreferencesId
			     	{
			     		Course = new Course(), Professor = new Professor()
			     	};
			Weight = 0;
			PreferenceType = PreferenceTypes.Equal;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the id.
		/// </summary>
		/// <value>The id for preference.</value>
		public virtual PreferencesId Id
		{
			get; set;
		}

		/// <summary>
		/// Gets or sets the version.
		/// </summary>
		/// <value>The version.</value>
		public virtual int Version
		{
			get; set;
		}

		/// <summary>
		/// Gets or sets the weight.
		/// </summary>
		/// <value>The weight.</value>
		public virtual int Weight
		{
			get; set;
		}

		/// <summary>
		/// Gets or sets the type of the preference.
		/// </summary>
		/// <value>The type of the preference.</value>
		public virtual PreferenceTypes PreferenceType
		{
			get; set;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>
		/// A new object that is a copy of this instance.
		/// </returns>
		public virtual object Clone()
		{
			return MemberwiseClone();
		}

		/// <summary>
		/// Method for entity class deserialization from XML file. Does not change this object content but returns another deserialized object instance
		/// </summary>
		/// <typeparam name="Preference">
		/// the generc type of type Preference
		/// </typeparam>
		/// <param name="xmlFilePath">
		/// Path of the XML file to read from.
		/// </param>
		/// <returns>
		/// Preference object restored from XML file
		/// </returns>
		public virtual Preference DeserializeFromFile<Preference>(string xmlFilePath)
		{
			Preference result;

			var seriliaser = new XmlSerializer(GetType());
			using (TextReader txtReader = new StreamReader(xmlFilePath))
			{
				result = (Preference)seriliaser.Deserialize(txtReader);
				txtReader.Close();
			}

			return result;
		}

		/// <summary>
		/// local implementation of Equals based on unique value members
		/// </summary>
		/// <param name="obj">
		/// An object represents the object to be compared.
		/// </param>
		/// <returns>
		/// a boolean value represents wether the object is equal or not.
		/// </returns>
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}

			Preference castObj;
			try
			{
				castObj = (Preference)obj;
			}
			catch (Exception)
			{
				return false;
			}

			return (castObj != null) &&
				   Id.Equals(castObj.Id);
		}

		/// <summary>
		/// local implementation of GetHashCode based on unique value members
		/// </summary>
		/// <returns>
		/// A hash code.
		/// </returns>
		public override int GetHashCode()
		{
			int hash = 57;
			hash = 27 * hash * Id.GetHashCode();
			return hash;
		}

		/// <summary>
		/// Method for entity class serialization to XML file
		/// </summary>
		/// <param name="xmlFilePath">Path of the XML file to write to. Will be overwritten if already exists.</param>
		public virtual void SerializeToFile(string xmlFilePath)
		{
			var seriliaser = new XmlSerializer(GetType());
			using (TextWriter txtWriter = new StreamWriter(xmlFilePath))
			{
				seriliaser.Serialize(txtWriter, this);
				txtWriter.Close();
			}
		}

		#endregion

		/// <summary>
		/// Nested class to provide strongly-typed access to property names (for .NET databinding, etc.)
		/// </summary>
		public static class MappingNames
		{
			/// <summary>
			/// Mapping name for ID
			/// </summary>
			public const string Id = "Id";

			/// <summary>
			/// Mapping name for preference
			/// </summary>
			public const string Preference = "Preference";

			/// <summary>
			/// Mapping Name for version
			/// </summary>
			public const string Version = "Version";

			/// <summary>
			/// Mapping name for weight
			/// </summary>
			public const string Weight = "Weight";

			/// <summary>
			/// Mapping name for preference type
			/// </summary>
			public const string PreferenceType = "Preference Type";
		}

		/// <summary>
		/// Nested class to provide strongly-typed access to mapping names (for NHibernate Queries, etc.)
		/// </summary>
		public static class PropertyNames
		{
			/// <summary>
			/// Property name for ID
			/// </summary>
			public const string Id = "Id";

			/// <summary>
			/// Property name for preference
			/// </summary>
			public const string Preference = "Preference";

			/// <summary>
			/// Property name for version
			/// </summary>
			public const string Version = "Version";

			/// <summary>
			/// Property name for  weight
			/// </summary>
			public const string Weight = "Weight";

			/// <summary>
			/// Property name for preference type
			/// </summary>
			public const string PreferenceType = "Preference Type";
		}
	}
}