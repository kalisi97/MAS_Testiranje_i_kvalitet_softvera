using Microsoft.Data.Tools.Schema.Sql.UnitTesting;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace TestProcedureInsertMesto
{
    [TestClass()]
    public class SqlServerTestProcedureInsertMesto : SqlDatabaseTestClass
    {

        public SqlServerTestProcedureInsertMesto()
        {
            InitializeComponent();
        }

        [TestInitialize()]
        public void TestInitialize()
        {
            base.InitializeTest();
        }
        [TestCleanup()]
        public void TestCleanup()
        {
            base.CleanupTest();
        }

        #region Designer support code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_PROC_INSERT_MESTOTest_TestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqlServerTestProcedureInsertMesto));
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition rowCountCondition1;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_PROC_INSERT_MESTOTest_PosttestAction;
            this.dbo_PROC_INSERT_MESTOTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            dbo_PROC_INSERT_MESTOTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            rowCountCondition1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            dbo_PROC_INSERT_MESTOTest_PosttestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            // 
            // dbo_PROC_INSERT_MESTOTest_TestAction
            // 
            dbo_PROC_INSERT_MESTOTest_TestAction.Conditions.Add(rowCountCondition1);
            resources.ApplyResources(dbo_PROC_INSERT_MESTOTest_TestAction, "dbo_PROC_INSERT_MESTOTest_TestAction");
            // 
            // dbo_PROC_INSERT_MESTOTestData
            // 
            this.dbo_PROC_INSERT_MESTOTestData.PosttestAction = dbo_PROC_INSERT_MESTOTest_PosttestAction;
            this.dbo_PROC_INSERT_MESTOTestData.PretestAction = null;
            this.dbo_PROC_INSERT_MESTOTestData.TestAction = dbo_PROC_INSERT_MESTOTest_TestAction;
            // 
            // rowCountCondition1
            // 
            rowCountCondition1.Enabled = true;
            rowCountCondition1.Name = "rowCountCondition1";
            rowCountCondition1.ResultSet = 1;
            rowCountCondition1.RowCount = 1;
            // 
            // dbo_PROC_INSERT_MESTOTest_PosttestAction
            // 
            resources.ApplyResources(dbo_PROC_INSERT_MESTOTest_PosttestAction, "dbo_PROC_INSERT_MESTOTest_PosttestAction");
        }

        #endregion


        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        #endregion

        [TestMethod()]
        public void dbo_PROC_INSERT_MESTOTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_PROC_INSERT_MESTOTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }
        private SqlDatabaseTestActions dbo_PROC_INSERT_MESTOTestData;
    }
}
