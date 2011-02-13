// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="SolutionProvider.cs" company="PC">
// Copy right Ali Hmer 2008-2009  
// </copyright>
// <summary>
//   Defines the SolutionProvider type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace DataAccessLayer
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using ClassLibrary;
	using Cream;
	using Cream.CourseAssignment;
	using NHibernate;
	using NHibernate.Criterion;
	using System.Linq;
	using Solution = ClassLibrary.Solution;
	using SolverProfessor = Cream.CourseAssignment.Professor;
	using SolverSolution = Cream.Solution;

	/// <summary>
	/// Class SolutionProvider
	/// </summary>
	public class SolutionProvider : GenericDataProvider<Solution>
	{
		#region Constants

		#endregion

		#region Fields

		/// <summary>
		/// <see cref="IList"/> notes
		/// </summary>
		private IList[] _notes;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="SolutionProvider"/> class.
		/// </summary>
		public SolutionProvider()
		{
			MaxNumberOfSolutions = 100;
			TimeOut = 10000;
			DefaultSolverStrategy = Solver.StrategyMethod.Soft;
			GenerateBetterSolutions = false;
			DistributeEvenly = true;
			UsedMethod = Settings.SolvingMethods.BranchAndBound;
			GenerateOnlySameOrBetterWeightedSolutions = false;
			ConstraintsStrings = new List<string>();
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets ConstraintsStrings.
		/// </summary>
		public IList<string> ConstraintsStrings { get; private set; }

		/// <summary>
		/// Gets or sets the default solver strategy.
		/// </summary>
		/// <value>The default solver strategy.</value>
		public Solver.StrategyMethod DefaultSolverStrategy { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [distribute evenly].
		/// </summary>
		/// <value><c>true</c> if [distribute evenly]; otherwise, <c>false</c>.</value>
		public bool DistributeEvenly { get; set; }

		/// <summary>
		/// Gets the feedback message.
		/// </summary>
		/// <value>The feedback message.</value>
		public string FeedbackMessage { get; private set; }

		/// <summary>
		/// Gets or sets a value indicating whether [generate better solutions].
		/// </summary>
		/// <value>
		/// <c>true</c> if [generate better solutions]; otherwise, <c>false</c>.
		/// </value>
		public bool GenerateBetterSolutions { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [generate only same or better weighted solutions].
		/// </summary>
		/// <value>
		/// <c>true</c> if [generate only same or better weighted solutions]; otherwise, <c>false</c>.
		/// </value>
		public bool GenerateOnlySameOrBetterWeightedSolutions { get; set; }

		/// <summary>
		/// Gets or sets the max number of solutions.
		/// </summary>
		/// <value>The max number of solutions.</value>
		public long MaxNumberOfSolutions { get; set; }

		/// <summary>
		/// Gets or sets Network.
		/// </summary>
		public Network Network { get; set; }

		/// <summary>
		/// Gets or sets the no of generated solutions.
		/// </summary>
		/// <value>The no of generated solutions.</value>
		public int NoOfGeneratedSolutions { get; set; }

		/// <summary>
		/// Gets or sets the time out.
		/// </summary>
		/// <value>The time out.</value>
		public long TimeOut { get; set; }

		/// <summary>
		/// Gets or sets the time spent.
		/// </summary>
		/// <value>The time spent.</value>
		public long TimeSpent { get; set; }

		/// <summary>
		/// Gets or sets the used method.
		/// </summary>
		/// <value>The used method.</value>
		public Settings.SolvingMethods UsedMethod { get; set; }

		public bool UsePreferencesApproach { get; set; }

		#endregion

		#region Public Methods

		/// <summary>
		/// Adds the solution.
		/// </summary>
		/// <param name="solution">The solution.</param>
		/// <returns>an integer represent the solution ID</returns>
		/// <exception cref="HibernateException"><c>HibernateException</c>.</exception>
		public int AddSolution(Solution solution)
		{
			// solution.Validate();
			using (var tx = Session.BeginTransaction())
			{
				try
				{
					var newID = (int)Session.Save(solution);
					Session.Flush();
					tx.Commit();
					return newID;
				}
				catch (HibernateException)
				{
					tx.Rollback();
					throw;
				}
			}
		}

		/// <summary>
		/// Adds the solutions list.
		/// </summary>
		/// <param name="solutions">The solutions.</param>
		/// <exception cref="HibernateException"><c>HibernateException</c>.</exception>
		public void AddSolutionsList(IList<Solution> solutions)
		{
			Session.SetBatchSize(1000);
			using (var tx = Session.BeginTransaction())
			{
				try
				{
					foreach (var solution in solutions)
					{
					    Session.Save(solution);
					}
				    tx.Commit();
				}
				catch (HibernateException)
				{
					tx.Rollback();
					throw;
				}
			}
		}

		/// <summary>
		/// Deletes all solutions.
		/// </summary>
		/// <exception cref="HibernateException"><c>HibernateException</c>.</exception>
		public void DeleteAllSolutions()
		{
			using (var tx = Session.BeginTransaction())
			{
				try
				{
					IQuery deleteAll = Session.CreateSQLQuery("Delete from solution");
					deleteAll.ExecuteUpdate();
					Session.Flush();
					tx.Commit();
				}
				catch (HibernateException)
				{
					tx.Rollback();
					throw;
				}
			}
		}

		/// <summary>
		/// Generates the solutions.
		/// </summary>
		/// <returns>
		/// The generate solutions.
		/// </returns>
		public bool GenerateSolutions()
		{
			var net = new CourseNetwork();

			// IList OldProofessors = new ArrayList();
			// foreach (var prof in net.Professors) {
			//    OldProofessors.Add(prof);
			// }

			// var RandomArray = GetRandomArray(net.Professors.Count);

			// for (int i = 0; i < net.Professors.Count; i++) {
			//    net.Professors[i] = OldProofessors[RandomArray[i]];
			// }
			var allProfs = new ProfessorProvider().GetAllProfessors();
			var professors = new SolverProfessor[allProfs.Count];
			var unassignedIndex = -1;
			var position = 0;
			foreach (var prof in allProfs)
			{
				var m = prof.NoOfCourses;
				var s = prof.FullName;
				professors[position] = new SolverProfessor(net, m, s);
				if (prof.UnassignedProf)
				{
					unassignedIndex = position;
				}

				position++;
			}

			//-----------------
			var allCourses = new CourseProvider().GetAllCourses();
			var noOfCourses = allCourses.Count;
			var course = new IntVariable[noOfCourses];
			var timeSlots = new int[allCourses.Count][];
			var dow = new string[allCourses.Count];
			var max = net.Professors.Count - 1;

			for (var i = 0; i < allCourses.Count; i++)
			{
				course[i] = new IntVariable(net, 0, max, allCourses[i].CourseName);
				timeSlots[i] = ParseTimeSlot(allCourses[i].TimeSlot);
				dow[i] = allCourses[i].DaysOfWeek;
			    if (allCourses[i].AssignedProfessor == null) continue;
			    var c = 0;
			    for (var j = 0; j < allProfs.Count; j++)
			    {
			        var pf = allProfs[j];
			        if (pf.ProfId == allCourses[i].AssignedProfessor.ProfId)
			        {
			            c = j;
			            break;
			        }
			    }

			    ConstraintsStrings.Add("Course " + course[i] + " must be taught by " + allCourses[i].AssignedProfessor.FullName +
			                           " (Hard Constraint)");
			    course[i].Equals(c);
			}

			var allPreferences = new PreferencesProvider().GetAllPreferences();
			foreach (var preference in allPreferences)
			{
				var preference1 = preference;
				var cr =
					course.Select(p => p).Where(p => p.Name == preference1.Id.Course.CourseName).First();

				for (var i = 0; i < professors.Length; i++)
				{
					if (preference.Id.Professor.FullName != professors[i].Name) continue;
					if (preference.PreferenceType == Preference.PreferenceTypes.Equal)
					{
						ConstraintsStrings.Add("Course " + cr + " is preferred to " + professors[i] + " (Soft Constraint)");
						cr.Equals(i, ConstraintTypes.Soft, preference.Weight);
						break;
					}
					if (preference.PreferenceType != Preference.PreferenceTypes.NotEqual) continue;
					ConstraintsStrings.Add("Course " + cr + " is not preferred to " + professors[i] + " (Soft Constraint)");
					cr.NotEquals(i, ConstraintTypes.Soft, preference.Weight);
					break;
				}
			}

			CreateNotEqualConstraint(allCourses.Count, dow, timeSlots, course);
			new Count(net, course);
			foreach (var s in from constraint in net.Constraints.OfType<Count>()
			                  select constraint.ToString().Split('\n')
			                  into str from s in str where !string.IsNullOrEmpty(s) select s)
			{
				ConstraintsStrings.Add(s + " (Hard Constraint)");
			}

			var sum = 0;
			if (net.Professors != null)
			{
				sum = net.Professors.Cast<SolverProfessor>().Sum(p => p.Courses);
			}

			//bool proceed = true;
			var goOn = true;
			if (sum < noOfCourses)
			{
				if (unassignedIndex > -1)
				{
					professors[unassignedIndex].Courses = noOfCourses - sum;
				}
				else
				{
					FeedbackMessage = "Number of courses is more than the professors' courses..\n" +
					                  "You should assign Unassigned professor!";
					var solProv = new SolutionProvider();
					solProv.DeleteAllSolutions();
					return false;
				}
			}
			else if (DistributeEvenly)
			{
				// must distribute evenly
				if (sum > noOfCourses)
				{
					FeedbackMessage = "Count Constraint was relaxed";
					var tempNoOfCourses = 0;
					if (net.Professors != null)
					{
						foreach (SolverProfessor professor in net.Professors)
						{
							professor.Courses = 0;
						}

						// try to pick randomly instead of going one by one.
						IList<int> list = new List<int>();
						var random = new Random();
						while (tempNoOfCourses <= noOfCourses && goOn)
						{
							int ran;
							while (true)
							{
								ran = random.Next(0, net.Professors.Count);
								int ran1 = ran;
								var isThereSimilarItems = list.Where(n => n == ran1);
								if (isThereSimilarItems.Count()> 0)
								{
									continue;
								}
								list.Add(ran);
								break;
							}
							if (list.Count == net.Professors.Count) // clear when list is full
							{
								list = new List<int>();
							}
							var professor = (SolverProfessor)net.Professors[ran];
							//foreach (SolverProfessor professor in net.Professors)
							{
								if (professor.Courses < professor.RealNoOfCourses)
								{
									professor.Courses++;
									tempNoOfCourses++;
									if (tempNoOfCourses >= noOfCourses)
									{
										goOn = false;
										break;
									}
								}
							}
						}
						foreach (var professor in
							net.Professors.Cast<SolverProfessor>().Where(professor => professor.Name.Trim() != "Not Assigned".Trim() && professor.Courses == 0))
						{
							professor.Courses = 1;
						}
					}
				}
				else
				{
					FeedbackMessage = "Count Constraint was not relaxed";
				}
			}
			else if (sum != noOfCourses)
			{
				FeedbackMessage = "No. of courses should be equal to the number of courses assigned to professors";
				var solProv = new SolutionProvider();
				solProv.DeleteAllSolutions();
				return false;
			}

			Network = net;
			Solve(net, course, noOfCourses);
			return true;
		}

		/// <summary>
		/// Gets all professors assigned courses.
		/// </summary>
		/// <param name="solutionNo">
		/// The solution No.
		/// </param>
		/// <returns>
		/// All professors assigned solutions as sums
		/// </returns>
		public IList<object[]> GetAllProfessorsAssignedCourses(int solutionNo)
		{
			var top = Session.CreateQuery(@"select s.Professor, count(s.Professor) 
                   from Solution s
                   where s.SolutionNo =:solutionNo 
                   group by s.Professor
                   order by s.Professor")
				.SetParameter("solutionNo", solutionNo)
				.List<object[]>();
			var allProfs = new ProfessorProvider().GetAllProfessors();
			foreach (var prof in from prof in allProfs
			                     where prof.FullName.Trim() != "Not Assigned".Trim()
			                     let found = top.Any(item => (string) item[0] == prof.FullName)
			                     where !found
			                     select prof)
			{
				top.Add(new object[]{prof.FullName, (long)0});
			}
			return top;
		}

		/// <summary>
		/// Gets all solutions.
		/// </summary>
		/// <returns>List of all solutions</returns>
		public IList GetAllSolutions()
		{
			var results =
				Session.CreateCriteria(typeof(Solution)).SetProjection(
					Projections.ProjectionList().Add(Projections.GroupProperty("SolutionNo"))).AddOrder(
					Order.Asc("SolutionNo")).List();
			return results;

			// DetachedCriteria solutions = DetachedCriteria.For<Solution>()
			////    .SetProjection(Projections.ProjectionList()
			////                       .Add(Projections.GroupProperty("SolutionNo")));
			////return FindByDetachedCriteria(solutions); 
			////IQuery allSolutions = session.CreateQuery("select from Solution u group by u.SolutionNo ");
			// var allSolutions = session.CreateSQLQuery("select solutionno from solution group by solutionno");

			// return allSolutions.List<Solution>();
		}

		/// <summary>
		/// Solutions the by solution no.
		/// </summary>
		/// <param name="solutionNo">
		/// The solution no.
		/// </param>
		/// <returns>
		/// One solution
		/// </returns>
		public IList<Solution> SolutionsBySolutionNo(int solutionNo)
		{
			var solutions = DetachedCriteria.For<Solution>()
				.Add(Restrictions.Eq("SolutionNo", solutionNo));

			// .AddOrder(Order.Asc(sort));
			return FindByDetachedCriteria(solutions);
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Creates the not equal constraint.
		/// </summary>
		/// <param name="n">The n represents number of courses.</param>
		/// <param name="dow">The day of week.</param>
		/// <param name="timeSlots">The time slots.</param>
		/// <param name="course">The course.</param>
		private void CreateNotEqualConstraint(int n, string[] dow, int[][] timeSlots, IntVariable[] course)
		{
			for (var first = 0; first < n; first++)
			{
				for (var second = first + 1; second < n; second++)
				{
					for (var m = 0; m < dow[first].Length; m++)
					{
						var c = dow[first][m];
						if (!dow[second].Contains(c.ToString())) continue;
						if (timeSlots[first][0] != timeSlots[second][0])
						{
							if ((timeSlots[first][0] < timeSlots[second][0]) &&
							    (timeSlots[first][1] > timeSlots[second][0]))
							{
								ConstraintsStrings.Add("Whoever teaches course " + course[first] + " cannot teach course " + course[second] +
								                       " (Hard Constraint)");
								ConstraintsStrings.Add("Whoever teaches course " + course[second] + " cannot teach course " + course[first] +
								                       " (Hard Constraint)");
								course[first].NotEquals(course[second]);
								break;
							}

							if ((timeSlots[first][0] < timeSlots[second][1]) &&
							    (timeSlots[first][1] > timeSlots[second][1]))
							{
								ConstraintsStrings.Add("Whoever teaches Course " + course[first] + " cannot teach course " + course[second] +
								                       " (Hard Constraint)");
								ConstraintsStrings.Add("Whoever teaches Course " + course[second] + " cannot teach course " + course[first] +
								                       " (Hard Constraint)");
								course[first].NotEquals(course[second]);
								break;
							}
						}
						else
						{
							ConstraintsStrings.Add("Whoever teaches course " + course[first] + " cannot teach course " + course[second] +
							                       " (Hard Constraint)");
							ConstraintsStrings.Add("Whoever teaches course " + course[second] + " cannot teach course " + course[first] +
							                       " (Hard Constraint)");
							course[first].NotEquals(course[second]);
							break;
						}
					}
				}
			}
		}

		/// <summary>
		/// Parses the time slot.
		/// </summary>
		/// <param name="ts">The time slot.</param>
		/// <returns>array of integers represents time slots</returns>
		private static int[] ParseTimeSlot(string ts)
		{
			while (ts.IndexOf(' ') >= 0)
			{
				ts = ts.Remove(ts.IndexOf(' '), 1);
			}

			var timeSlot = new int[2];
			var s = ts.Split('-');
			for (var j = 0; j < 2; j++)
			{
				var d = s[j];
				var f = d.Split(':');
				int i1 = Convert.ToInt16(f[0]); // hour of starting time slot
				var h = f[1];
				var k1 = Convert.ToInt16(h.Substring(0, 1)) * 10;
				int k2 = Convert.ToInt16(h.Substring(1, 1));
				var i2 = k1 + k2;
				var l = String.Concat(h[2], h[3]);
				if (l.ToUpper().Equals("PM") && (i1 != 12))
				{
					i1 += 12;
				}

				timeSlot[j] = (i1 * 100) + i2;
			}

			return timeSlot;
		}

		/// <summary>
		/// Solves the specified net.
		/// </summary>
		/// <param name="net">The network.</param>
		/// <param name="course">The course.</param>
		/// <param name="noOfCourses">The no of courses.</param>
		private void Solve(CourseNetwork net, IntVariable[] course, int noOfCourses)
		{
			Solver solver;
			switch (UsedMethod)
			{
				case Settings.SolvingMethods.BranchAndBound:
					solver = new DefaultSolver(net, Solver.Default);
					break;
				case Settings.SolvingMethods.IterativeBranchAndBound:
					solver = new IterativeBranchAndBoundSearch(net, Solver.Minimize);

					// MaxNumberOfSolutions = 1;
					break;
				case Settings.SolvingMethods.Taboo:
					solver = new TabooSearch(net, Solver.Minimize);
					MaxNumberOfSolutions = 1;
					break;
				case Settings.SolvingMethods.RandomWalk:
					solver = new LocalSearch(net, Solver.Minimize);

					// MaxNumberOfSolutions = 1;
					break;
				case Settings.SolvingMethods.SimulatedAnnealing:
					net.Objective = course[0];

					// MaxNumberOfSolutions = 1;
					solver = new SimulatedAnneallingSearch(net, Solver.Minimize);
					break;
				default:
					solver = new DefaultSolver(net, Solver.Default);
					break;
			}

			solver.GenerateOnlySameOrBetterWeightedSolutions = GenerateOnlySameOrBetterWeightedSolutions;
			TimeSpent = 0;
			var count = 1;

			var solProv = new SolutionProvider();
			solProv.DeleteAllSolutions();

			_notes = new IList[MaxNumberOfSolutions];
			SolverSolution bestSolution = null;

			IList<Solution> allSolutions = new List<Solution>();

			solver.SolverStrategy = UsePreferencesApproach?Solver.StrategyMethod.Soft:Solver.StrategyMethod.Step;
			for (solver.Start(TimeOut); solver.WaitNext(); solver.Resume())
			{
				var sol = solver.Solution;
				if (count == 1)
				{
					bestSolution = sol;
				}
				else
				{
					if (bestSolution != null)
					{
						if (sol.Weight > bestSolution.Weight)
						{
							bestSolution = sol;
						}
					}
				}

				_notes[count - 1] = new ArrayList { "Weight= " + sol.Weight + "\n" };
				for (var i = 0; i < net.Professors.Count; i++)
				{
					int i1 = i;
					var pcount = (from object t in net.Variables where !((Variable) t).IsValueType select t).Count(t => sol.GetIntValue(((Variable) t)) == i1);

					if (pcount < ((SolverProfessor)net.Professors[i]).RealNoOfCourses)
					{
						_notes[count - 1].Add("Prof. " + net.Professors[i] +
											 " not consistent.. needs " +
											 (((SolverProfessor)net.Professors[i]).RealNoOfCourses - pcount) +
											 " assignment(s) more!!" + "\n");
					}
				}

				Console.Out.WriteLine();
				var spent = 0;
				for (var i = 0; i < noOfCourses; i++)
				{
					var solution = new Solution
									   {
										   SolutionNo = count,
										   Weight = sol.Weight,
										   Course = course[i].Name,
										   Time = sol.Time
									   };
					var index = sol.GetIntValue(course[i]);
					solution.Professor = ((SolverProfessor)net.Professors[index]).Name;
					allSolutions.Add(solution);
					spent = (int)solution.Time;
				}

				TimeSpent += spent;

				count++;
				if (count == MaxNumberOfSolutions + 1)
				{
					break;
				}

				Console.Out.WriteLine(count);
			}

			NoOfGeneratedSolutions = count - 1;
			solProv.AddSolutionsList(allSolutions);
			Console.WriteLine(bestSolution);

			Console.WriteLine(TimeSpent);
			if (TimeSpent == 0)
			{
				FeedbackMessage = (count - 1) + " Solution(s) found in " + TimeSpent + " MS";
			}
			else
			{
				FeedbackMessage = (count - 1) + " Solution(s) found in " + (TimeSpent / 10000.0) + " second(s)";
			}
		}

		#endregion
	}
}
