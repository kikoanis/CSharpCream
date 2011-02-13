#region Includes
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ClassLibrary;
#endregion

///////////////////////////////////////////////////////////////////////////////
// Copyright 2010 (c) by Class Library All Rights Reserved.
//  
// Project:      
// Module:       PropertyNamesTest.cs
// Description:  Tests for the Property Names class in the ClassLibrary assembly.
//  
// Date:       Author:           Comments:
// 21/08/2010 5:09 PM  Ali     Module created.
///////////////////////////////////////////////////////////////////////////////
namespace ClassLibraryTest
{

    /// <summary>
    /// Tests for the Property Names Class
    /// Documentation: Nested class to provide strongly-typed access to mapping names (for NHibernate Queries, etc.)
    /// </summary>
    [TestFixture(Description="Tests for Property Names")]
    public class PropertyNamesTest
    {
        #region Class Variables
        private PropertyNames //STATIC CLASS DELETE THIS LINE = null;
        #endregion

        #region Setup/Teardown

        /// <summary>
        /// Code that is run once for a suite of tests
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {

        }

        /// <summary>
        /// Code that is run once after a suite of tests has finished executing
        /// </summary>
        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {

        }

        /// <summary>
        /// Code that is run before each test
        /// </summary>
        [SetUp]
        public void Initialize()
        {
            //New instance of Property Names
            _propertyNames = new PropertyNames();
        }

        /// <summary>
        /// Code that is run after each test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {
            //TODO:  Put dispose in here for //STATIC CLASS DELETE THIS LINE or delete this line
        }
        #endregion

        #region Property Tests

//No public properties were found. No tests are generated for non-public scoped properties.

        #endregion

        #region Method Tests

//No public methods were found. No tests are generated for non-public scoped methods.

        #endregion

    }
}
