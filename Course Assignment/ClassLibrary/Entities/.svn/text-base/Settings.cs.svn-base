// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Settings.cs" company="U of R">
//   Copy right 2008-2009
// </copyright>
// <summary>
//   Defines the Settings type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ClassLibrary
{
	using System;
	using System.IO;
	using System.Linq;
	using System.Xml.Linq;

	/// <summary>
	/// Settings class
	/// </summary>
	public partial class Settings
	{

		#region Fields

		/// <summary>
		/// Solving methods strings
		/// </summary>
		public string[] SolvingMethodsStrings =
            {
                "Branch And Bound",
                "Iterative Branch And Bound",
                "Taboo",
                "Simulated Annealing",
                "Random Walk (Local Search)"
            };

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Settings"/> class.
		/// </summary>
		/// <param name="fileName">
		/// The file name.
		/// </param>
		private Settings(string fileName)
		{
			FileName = fileName;
			if (!File.Exists(FileName))
			{
				File.Create(FileName);
			}

			try
			{
				Document = XDocument.Load(FileName);
				if (Document.Root != null)
				{
					var elements = Document.Root.Elements();
					var maxBreakMinutesPerSessionElements =
						elements.Where(s => s.Name == "MaxBreakMinutesPerSession").Select(s => s.Value);
					if (maxBreakMinutesPerSessionElements.Count() > 0)
					{
						MaxBreakMinutesPerSession = Convert.ToInt32(maxBreakMinutesPerSessionElements.First());
					}

					var relaxCountConstraintElements =
						elements.Where(s => s.Name == "RelaxCountConstraint").Select(s => s.Value);
					if (relaxCountConstraintElements.Count() > 0)
					{
						RelaxCountConstraint = Convert.ToBoolean(relaxCountConstraintElements.First());
					}

					var maxNumberOfHoursPerCourseElements =
						elements.Where(s => s.Name == "MaxNumberOfHoursPerCourse").Select(s => s.Value);
					if (maxNumberOfHoursPerCourseElements.Count() > 0)
					{
						MaxNumberOfHoursPerCourse = Convert.ToInt32(maxNumberOfHoursPerCourseElements.First());
					}

					var maxNumberOfCoursesPerProfessorElements =
						elements.Where(s => s.Name == "MaxNumberOfCoursesPerProfessor").Select(s => s.Value);
					if (maxNumberOfCoursesPerProfessorElements.Count() > 0)
					{
						MaxNumberOfCoursesPerProfessor = Convert.ToInt32(maxNumberOfCoursesPerProfessorElements.First());
					}

					var numberOfPreferencesPerProfessorElements =
						elements.Where(s => s.Name == "NumberOfPreferencesPerProfessor").Select(s => s.Value);
					if (numberOfPreferencesPerProfessorElements.Count() > 0)
					{
						NumberOfPreferencesPerProfessor =
							Convert.ToInt32(numberOfPreferencesPerProfessorElements.First());
					}

					var maxNumberOfGeneratedSolutionsElements =
						elements.Where(s => s.Name == "MaxNumberOfGeneratedSolutions").Select(s => s.Value);
					if (maxNumberOfGeneratedSolutionsElements.Count() > 0)
					{
						MaxNumberOfGeneratedSolutions =
							Convert.ToInt32(maxNumberOfGeneratedSolutionsElements.First());
					}

					var solvingMethodElements =
						elements.Where(s => s.Name == "SolvingMethod").Select(s => s.Value);
					if (solvingMethodElements.Count() > 0)
					{
						SolvingMethod = (SolvingMethods)Enum.Parse(typeof(SolvingMethods), solvingMethodElements.First(), true);
					}

					var timeOutElements =
						elements.Where(s => s.Name == "TimeOut").Select(s => s.Value);
					if (timeOutElements.Count() > 0)
					{
						TimeOut = Convert.ToInt64(timeOutElements.First());
					}

					var generateOnlySameOrBetterSolutionsElements =

						elements.Where(s => s.Name == "GenerateOnlySameOrBetterWeightedSolutions").Select(s => s.Value);
					if (generateOnlySameOrBetterSolutionsElements.Count() > 0)
					{
						GenerateOnlySameOrBetterWeightedSolutions = Convert.ToBoolean(generateOnlySameOrBetterSolutionsElements.First());
					}

					var displayConstraintsDetailsElements =
						elements.Where(s => s.Name == "DisplayConstraintsDetails").Select(s => s.Value);
					if (displayConstraintsDetailsElements.Count() > 0)
					{
						DisplayConstraintsDetails = Convert.ToBoolean(displayConstraintsDetailsElements.First());
					}

					var usePreferencesApproachElements =
						elements.Where(s => s.Name == "UsePreferencesApproach").Select(s => s.Value);
					if (usePreferencesApproachElements.Count() > 0)
					{
						UsePreferencesApproach = Convert.ToBoolean(usePreferencesApproachElements.First());
					}
				}
			}
			catch
			{
				// file structure is not XML compliant or file just created, default values
				MaxBreakMinutesPerSession = 15;
				MaxNumberOfHoursPerCourse = 3;
				MaxNumberOfCoursesPerProfessor = 3;
				NumberOfPreferencesPerProfessor = 5;
				MaxNumberOfGeneratedSolutions = 10;
				RelaxCountConstraint = false;
				SolvingMethod = SolvingMethods.BranchAndBound;
				UsePreferencesApproach = true;
				GenerateOnlySameOrBetterWeightedSolutions = true;
				DisplayConstraintsDetails = true;
				TimeOut = 100000;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Settings"/> class.
		/// </summary>
		public Settings()
			: this(FullFileName)
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets a value indicating whether DisplayConstraintsDetails.
		/// </summary>
		public bool DisplayConstraintsDetails { get; set; }

		/// <summary>
		/// Gets or sets Document.
		/// </summary>
		public XDocument Document { get; set; }

		/// <summary>
		/// Gets or sets FileName.
		/// </summary>
		private string FileName { get; set; }

		/// <summary>
		/// Gets FullFileName.
		/// </summary>
		public static string FullFileName
		{
			get
			{
				return "c:\\Settings.xml";

				// return 
				//    Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).
				//        Substring(6) + "\\settings.xml";
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether GenerateOnlySameOrBetterWeightedSolutions.
		/// </summary>
		public bool GenerateOnlySameOrBetterWeightedSolutions { get; set; }

		/// <summary>
		/// Gets or sets MaxBreakMinutesPerSession.
		/// </summary>
		public int MaxBreakMinutesPerSession { get; set; }

		/// <summary>
		/// Gets or sets MaxNumberOfCoursesPerProfessor.
		/// </summary>
		public int MaxNumberOfCoursesPerProfessor { get; set; }

		/// <summary>
		/// Gets or sets MaxNumberOfGeneratedSolutions.
		/// </summary>
		public int MaxNumberOfGeneratedSolutions { get; set; }

		/// <summary>
		/// Gets or sets MaxNumberOfHoursPerCourse.
		/// </summary>
		public int MaxNumberOfHoursPerCourse { get; set; }

		/// <summary>
		/// Gets or sets NumberOfPreferencesPerProfessor.
		/// </summary>
		public int NumberOfPreferencesPerProfessor { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether RelaxCountConstraint.
		/// </summary>
		public bool RelaxCountConstraint { get; set; }

		/// <summary>
		/// Gets or sets SolvingMethod.
		/// </summary>
		public SolvingMethods SolvingMethod { get; set; }

		/// <summary>
		/// Gets or sets TimeOut.
		/// </summary>
		public long TimeOut { get; set; }

		public bool UsePreferencesApproach { get; set; }

		#endregion

		#region Public Methods

		/// <summary>
		/// Save method
		/// </summary>
		public void Save()
		{
			Save(FullFileName);
		}

		/// <summary>
		/// Saves this settings object to desired location
		/// </summary>
		/// <param name="fileName">filename path</param>
		public void Save(string fileName)
		{
			Validate();

			Document = new XDocument();
			Document.Add(new XElement(
							"SettingsFile",
							new XElement("MaxNumberOfHoursPerCourse", MaxNumberOfHoursPerCourse),
							new XElement("MaxBreakMinutesPerSession", MaxBreakMinutesPerSession),
							new XElement("MaxNumberOfCoursesPerProfessor", MaxNumberOfCoursesPerProfessor),
							new XElement("MaxNumberOfGeneratedSolutions", MaxNumberOfGeneratedSolutions),
							new XElement("NumberOfPreferencesPerProfessor", NumberOfPreferencesPerProfessor),
							new XElement("GenerateOnlySameOrBetterWeightedSolutions", GenerateOnlySameOrBetterWeightedSolutions),
							new XElement("DisplayConstraintsDetails", DisplayConstraintsDetails),
							new XElement("RelaxCountConstraint", RelaxCountConstraint),
							new XElement("SolvingMethod", SolvingMethod),
							new XElement("UsePreferencesApproach", UsePreferencesApproach),
							new XElement("TimeOut", TimeOut)));
			Document.Save(fileName);
		}

		#endregion

	}
}

