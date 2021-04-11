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
    public class SqlServerTestTriger : SqlDatabaseTestClass
    {

        public SqlServerTestTriger()
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
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_TriggerVelikoSlovoTest_TestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqlServerTestTriger));
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.NotEmptyResultSetCondition notEmptyResultSetCondition1;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition1;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_TriggerVelikoSlovoTest_PosttestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_TriggerVelikoSlovoTest_PretestAction;
            this.dbo_TriggerVelikoSlovoTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            dbo_TriggerVelikoSlovoTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            notEmptyResultSetCondition1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.NotEmptyResultSetCondition();
            scalarValueCondition1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            dbo_TriggerVelikoSlovoTest_PosttestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            dbo_TriggerVelikoSlovoTest_PretestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            // 
            // dbo_TriggerVelikoSlovoTest_TestAction
            // 
            dbo_TriggerVelikoSlovoTest_TestAction.Conditions.Add(notEmptyResultSetCondition1);
            dbo_TriggerVelikoSlovoTest_TestAction.Conditions.Add(scalarValueCondition1);
            resources.ApplyResources(dbo_TriggerVelikoSlovoTest_TestAction, "dbo_TriggerVelikoSlovoTest_TestAction");
            // 
            // notEmptyResultSetCondition1
            // 
            notEmptyResultSetCondition1.Enabled = true;
            notEmptyResultSetCondition1.Name = "notEmptyResultSetCondition1";
            notEmptyResultSetCondition1.ResultSet = 1;
            // 
            // scalarValueCondition1
            // 
            scalarValueCondition1.ColumnNumber = 1;
            scalarValueCondition1.Enabled = true;
            scalarValueCondition1.ExpectedValue = "Novo mesto";
            scalarValueCondition1.Name = "scalarValueCondition1";
            scalarValueCondition1.NullExpected = false;
            scalarValueCondition1.ResultSet = 1;
            scalarValueCondition1.RowNumber = 1;
            // 
            // dbo_TriggerVelikoSlovoTest_PosttestAction
            // 
            resources.ApplyResources(dbo_TriggerVelikoSlovoTest_PosttestAction, "dbo_TriggerVelikoSlovoTest_PosttestAction");
            // 
            // dbo_TriggerVelikoSlovoTest_PretestAction
            // 
            resources.ApplyResources(dbo_TriggerVelikoSlovoTest_PretestAction, "dbo_TriggerVelikoSlovoTest_PretestAction");
            // 
            // dbo_TriggerVelikoSlovoTestData
            // 
            this.dbo_TriggerVelikoSlovoTestData.PosttestAction = dbo_TriggerVelikoSlovoTest_PosttestAction;
            this.dbo_TriggerVelikoSlovoTestData.PretestAction = dbo_TriggerVelikoSlovoTest_PretestAction;
            this.dbo_TriggerVelikoSlovoTestData.TestAction = dbo_TriggerVelikoSlovoTest_TestAction;
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
        public void dbo_TriggerVelikoSlovoTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_TriggerVelikoSlovoTestData;
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
        private SqlDatabaseTestActions dbo_TriggerVelikoSlovoTestData;
    }
}
