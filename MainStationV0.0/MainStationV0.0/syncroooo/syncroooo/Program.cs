using System;
using System.Data.SqlClient;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;

namespace syncroooo
{
    class Program
    {
        private static void syncr()
        {
            String scopeName="mahy3";
            //String scopeName1 = "khalifa1";
            SqlConnection sqlServerConn = new SqlConnection("Data Source=DESKTOP-5NLB5GK;Initial Catalog=SmallStation;User ID=sa;Password=1234");
            SqlConnection sqlServerConn2 = new SqlConnection("Data Source=DESKTOP-5NLB5GK;Initial Catalog=train;User ID=sa;Password=1234");

            DbSyncScopeDescription myScope = new DbSyncScopeDescription(scopeName);
            DbSyncTableDescription semaphore = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.synTrain", sqlServerConn);
            //DbSyncTableDescription Product = SqlSyncDescriptionBuilder.GetDescriptionForTable("Salest", sqlServerConn);

                    // Add the tables from above to the scope
            myScope.Tables.Add(semaphore);
            //myScope.Tables.Add(Product);

            /*DbSyncScopeDescription myScope1 = new DbSyncScopeDescription(scopeName1);
            DbSyncTableDescription semaphore1 = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.semaphore", sqlServerConn2);
            //DbSyncTableDescription Product = SqlSyncDescriptionBuilder.GetDescriptionForTable("Salest", sqlServerConn);

            // Add the tables from above to the scope
            myScope1.Tables.Add(semaphore1);
            //myScope.Tables.Add(Product);*/




            SqlSyncScopeProvisioning sqlServerProv = new SqlSyncScopeProvisioning(sqlServerConn, myScope);
            if (!sqlServerProv.ScopeExists(scopeName))
                // Apply the scope provisioning.
                sqlServerProv.Apply();

            /*sqlServerProv = new SqlSyncScopeProvisioning(sqlServerConn2, myScope);
            if (!sqlServerProv.ScopeExists(scopeName))
                // Apply the scope provisioning.
                sqlServerProv.Apply();*/

            SqlSyncScopeProvisioning sqlServerProv2 = new SqlSyncScopeProvisioning(sqlServerConn2, myScope);
            if (!sqlServerProv2.ScopeExists(scopeName))
                // Apply the scope provisioning.
                sqlServerProv2.Apply();
            /*sqlServerProv2 = new SqlSyncScopeProvisioning(sqlServerConn2, myScope1);
            if (!sqlServerProv2.ScopeExists(scopeName1))
                // Apply the scope provisioning.
                sqlServerProv2.Apply();*/
        }
        private static void syncrazure()
        {
            String scopeName = "small";
            SqlConnection sqlServerConn ;
            SqlConnection sqlAzureConn ;
            String LocalSQLServerConnectionString = "Data Source=DESKTOP-5NLB5GK;Initial Catalog=small;User ID=sa;Password=1234";
            String RemoteSQLAzureConnectionString = "Data Source=projtrain.database.windows.net;initial catalog=azure_db;User ID=train;password=Proj1234;";

            using (sqlServerConn = new SqlConnection(LocalSQLServerConnectionString))
            {
                using (sqlAzureConn = new SqlConnection(RemoteSQLAzureConnectionString))
                {
                    DbSyncScopeDescription myScope = new DbSyncScopeDescription(scopeName);
                   // DbSyncScopeDescription myScope1 = new DbSyncScopeDescription(scopeName2);

                    //DbSyncTableDescription lc_azure = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.semaphore_syn", sqlAzureConn);
                  //  DbSyncTableDescription sem_azure = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.mahy", sqlAzureConn);
                    
                    //DbSyncTableDescription lc_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.levelcrossing", sqlServerConn);
                    DbSyncTableDescription sem_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.semaphore", sqlServerConn);

                    // DbSyncTableDescription train_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.Strain", sqlServerConn);
                    DbSyncTableDescription block_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.trainblock", sqlServerConn);

                    /*DbSyncTableDescription semaphore_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.semaphore", sqlServerConn);
                    DbSyncTableDescription train2_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.train", sqlServerConn);
                    DbSyncTableDescription trainblock_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.trainblock", sqlServerConn);*/



                    // Add the tables from above to the scope
                    // myScope.Tables.Add(lc_azure);
                    // myScope.Tables.Add(lc_small);
                    // myScope.Tables.Add(sem_azure);
                    myScope.Tables.Add(sem_small);
                    //myScope.Tables.Add(train_small);
                    myScope.Tables.Add(block_small);
                   /* myScope.Tables.Add(semaphore_small);
                    myScope.Tables.Add(train2_small);
                    myScope.Tables.Add(trainblock_small);*/
                    SqlSyncScopeProvisioning sqlServerProv = new SqlSyncScopeProvisioning(sqlServerConn, myScope);
                    if (!sqlServerProv.ScopeExists(scopeName))
                        // Apply the scope provisioning.
                        sqlServerProv.Apply();
                    // Setup SQL Database for sync
                    SqlSyncScopeProvisioning sqlAzureProv = new SqlSyncScopeProvisioning(sqlAzureConn, myScope);
                    if (!sqlAzureProv.ScopeExists(scopeName))
                        // Apply the scope provisioning.
                        sqlAzureProv.Apply();

                    /*myScope1.Tables.Add(lc_azure);
                    sqlServerProv = new SqlSyncScopeProvisioning(sqlServerConn, myScope1);
                    if (!sqlServerProv.ScopeExists(scopeName2))
                        // Apply the scope provisioning.
                        sqlServerProv.Apply();
                    // Setup SQL Database for sync
                    sqlAzureProv = new SqlSyncScopeProvisioning(sqlAzureConn, myScope1);
                    if (!sqlAzureProv.ScopeExists(scopeName2))
                        // Apply the scope provisioning
                        sqlAzureProv.Apply();*/
                }
            }
        }
        private static void syncrazure2()
        {
            String scopeName = "train";
            SqlConnection sqlServerConn;
            SqlConnection sqlAzureConn;
            String RemoteSQLAzureConnectionString = "Data Source=DESKTOP-5NLB5GK;Initial Catalog=train;User ID=sa;Password=1234";
            String LocalSQLServerConnectionString = "Data Source=projtrain.database.windows.net;initial catalog=azure_db;User ID=train;password=Proj1234;";

            using (sqlServerConn = new SqlConnection(LocalSQLServerConnectionString))
            {
                using (sqlAzureConn = new SqlConnection(RemoteSQLAzureConnectionString))
                {
                    DbSyncScopeDescription myScope = new DbSyncScopeDescription(scopeName);
                    // DbSyncScopeDescription myScope1 = new DbSyncScopeDescription(scopeName2);

                    //DbSyncTableDescription lc_azure = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.semaphore_syn", sqlAzureConn);
                    //  DbSyncTableDescription sem_azure = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.mahy", sqlAzureConn);

                    //DbSyncTableDescription lc_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.levelcrossing", sqlServerConn);
                    DbSyncTableDescription sem_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.semaphore", sqlServerConn);
                   

                    // DbSyncTableDescription train_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.Strain", sqlServerConn);
                    DbSyncTableDescription block_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.trainblock", sqlServerConn);

                    /*DbSyncTableDescription semaphore_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.semaphore", sqlServerConn);
                    DbSyncTableDescription train2_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.train", sqlServerConn);
                    DbSyncTableDescription trainblock_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.trainblock", sqlServerConn);*/



                    // Add the tables from above to the scope
                    // myScope.Tables.Add(lc_azure);
                    // myScope.Tables.Add(lc_small);
                    // myScope.Tables.Add(sem_azure);
                    myScope.Tables.Add(sem_small);
                    //myScope.Tables.Add(train_small);
                    myScope.Tables.Add(block_small);
                    /* myScope.Tables.Add(semaphore_small);
                     myScope.Tables.Add(train2_small);
                     myScope.Tables.Add(trainblock_small);*/
                    SqlSyncScopeProvisioning sqlServerProv = new SqlSyncScopeProvisioning(sqlServerConn, myScope);
                    if (!sqlServerProv.ScopeExists(scopeName))
                        // Apply the scope provisioning.
                        sqlServerProv.Apply();
                    // Setup SQL Database for sync
                    SqlSyncScopeProvisioning sqlAzureProv = new SqlSyncScopeProvisioning(sqlAzureConn, myScope);
                    if (!sqlAzureProv.ScopeExists(scopeName))
                        // Apply the scope provisioning.
                        sqlAzureProv.Apply();

                    /*myScope1.Tables.Add(lc_azure);
                    sqlServerProv = new SqlSyncScopeProvisioning(sqlServerConn, myScope1);
                    if (!sqlServerProv.ScopeExists(scopeName2))
                        // Apply the scope provisioning.
                        sqlServerProv.Apply();
                    // Setup SQL Database for sync
                    sqlAzureProv = new SqlSyncScopeProvisioning(sqlAzureConn, myScope1);
                    if (!sqlAzureProv.ScopeExists(scopeName2))
                        // Apply the scope provisioning
                        sqlAzureProv.Apply();*/
                }
            }
        }
        private static void syncrazure3()
        {
            String scopeName = "main";
            SqlConnection sqlServerConn;
            SqlConnection sqlAzureConn;
            String RemoteSQLAzureConnectionString = "Data Source=DESKTOP-I7LBVJ6;Initial Catalog=main_DB;Trusted_Connection=Yes";
            String LocalSQLServerConnectionString = "Data Source=projtrain.database.windows.net;initial catalog=db_v;User ID=train;password=Proj1234;";

            using (sqlServerConn = new SqlConnection(LocalSQLServerConnectionString))
            {
                using (sqlAzureConn = new SqlConnection(RemoteSQLAzureConnectionString))
                {
                    DbSyncScopeDescription myScope = new DbSyncScopeDescription(scopeName);
                    // DbSyncScopeDescription myScope1 = new DbSyncScopeDescription(scopeName2);

                    //DbSyncTableDescription lc_azure = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.semaphore_syn", sqlAzureConn);
                    //  DbSyncTableDescription sem_azure = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.mahy", sqlAzureConn);

                    //DbSyncTableDescription lc_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.levelcrossing", sqlServerConn);
                    DbSyncTableDescription sem_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.semaphore_small", sqlServerConn);
                    // DbSyncTableDescription train_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.Strain", sqlServerConn);
                    DbSyncTableDescription block_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.trainblock_small", sqlServerConn);
                    /*DbSyncTableDescription semaphore_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.semaphore", sqlServerConn);
                    DbSyncTableDescription train2_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.train", sqlServerConn);
                    DbSyncTableDescription trainblock_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.trainblock", sqlServerConn);*/



                    // Add the tables from above to the scope
                    // myScope.Tables.Add(lc_azure);
                    // myScope.Tables.Add(lc_small);
                    // myScope.Tables.Add(sem_azure);
                    myScope.Tables.Add(sem_small);
                    //myScope.Tables.Add(train_small);
                    myScope.Tables.Add(block_small);
                    /* myScope.Tables.Add(semaphore_small);
                     myScope.Tables.Add(train2_small);
                     myScope.Tables.Add(trainblock_small);*/
                    SqlSyncScopeProvisioning sqlServerProv = new SqlSyncScopeProvisioning(sqlServerConn, myScope);
                    if (!sqlServerProv.ScopeExists(scopeName))
                        // Apply the scope provisioning.
                        sqlServerProv.Apply();
                    // Setup SQL Database for sync
                    SqlSyncScopeProvisioning sqlAzureProv = new SqlSyncScopeProvisioning(sqlAzureConn, myScope);
                    if (!sqlAzureProv.ScopeExists(scopeName))
                        // Apply the scope provisioning.
                        sqlAzureProv.Apply();

                    /*myScope1.Tables.Add(lc_azure);
                    sqlServerProv = new SqlSyncScopeProvisioning(sqlServerConn, myScope1);
                    if (!sqlServerProv.ScopeExists(scopeName2))
                        // Apply the scope provisioning.
                        sqlServerProv.Apply();
                    // Setup SQL Database for sync
                    sqlAzureProv = new SqlSyncScopeProvisioning(sqlAzureConn, myScope1);
                    if (!sqlAzureProv.ScopeExists(scopeName2))
                        // Apply the scope provisioning
                        sqlAzureProv.Apply();*/
                }
            }
        }
        private static void syncrazure4()
        {
            String scopeName = "central";
            SqlConnection sqlServerConn;
            SqlConnection sqlAzureConn;
            String RemoteSQLAzureConnectionString = "Data Source=DESKTOP-5NLB5GK;Initial Catalog=central;User ID=sa;Password=1234";
            String LocalSQLServerConnectionString = "Data Source=projtrain.database.windows.net;initial catalog=azure_db;User ID=train;password=Proj1234;";

            using (sqlServerConn = new SqlConnection(LocalSQLServerConnectionString))
            {
                using (sqlAzureConn = new SqlConnection(RemoteSQLAzureConnectionString))
                {
                    DbSyncScopeDescription myScope = new DbSyncScopeDescription(scopeName);
                    // DbSyncScopeDescription myScope1 = new DbSyncScopeDescription(scopeName2);

                    //DbSyncTableDescription lc_azure = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.semaphore_syn", sqlAzureConn);
                    //  DbSyncTableDescription sem_azure = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.mahy", sqlAzureConn);
                    //DbSyncTableDescription lc_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.levelcrossing", sqlServerConn);
                    DbSyncTableDescription sem_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.semaphore", sqlServerConn);
                    // DbSyncTableDescription train_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.Strain", sqlServerConn);
                    DbSyncTableDescription block_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.trainblock", sqlServerConn);
                    /*DbSyncTableDescription semaphore_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.semaphore", sqlServerConn);
                    DbSyncTableDescription train2_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.train", sqlServerConn);
                    DbSyncTableDescription trainblock_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.trainblock", sqlServerConn);*/



                    // Add the tables from above to the scope
                    // myScope.Tables.Add(lc_azure);
                    // myScope.Tables.Add(lc_small);
                    // myScope.Tables.Add(sem_azure);
                    myScope.Tables.Add(sem_small);
                    //myScope.Tables.Add(train_small);
                    myScope.Tables.Add(block_small);
                    /* myScope.Tables.Add(semaphore_small);
                     myScope.Tables.Add(train2_small);
                     myScope.Tables.Add(trainblock_small);*/
                    SqlSyncScopeProvisioning sqlServerProv = new SqlSyncScopeProvisioning(sqlServerConn, myScope);
                    if (!sqlServerProv.ScopeExists(scopeName))
                        // Apply the scope provisioning.
                        sqlServerProv.Apply();
                    // Setup SQL Database for sync
                    SqlSyncScopeProvisioning sqlAzureProv = new SqlSyncScopeProvisioning(sqlAzureConn, myScope);
                    if (!sqlAzureProv.ScopeExists(scopeName))
                        // Apply the scope provisioning.
                        sqlAzureProv.Apply();

                    /*myScope1.Tables.Add(lc_azure);
                    sqlServerProv = new SqlSyncScopeProvisioning(sqlServerConn, myScope1);
                    if (!sqlServerProv.ScopeExists(scopeName2))
                        // Apply the scope provisioning.
                        sqlServerProv.Apply();
                    // Setup SQL Database for sync
                    sqlAzureProv = new SqlSyncScopeProvisioning(sqlAzureConn, myScope1);
                    if (!sqlAzureProv.ScopeExists(scopeName2))
                        // Apply the scope provisioning
                        sqlAzureProv.Apply();*/
                }
            }
        }
        private static void syncrazure5()
        {
            String scopeName = "main1";
            SqlConnection sqlServerConn;
            SqlConnection sqlAzureConn;
            String LocalSQLServerConnectionString = "Data Source=DESKTOP-I7LBVJ6;Initial Catalog=main_DB;Trusted_Connection=Yes";
            String RemoteSQLAzureConnectionString = "Data Source=projtrain.database.windows.net;initial catalog=db_v;User ID=train;password=Proj1234;";

            using (sqlServerConn = new SqlConnection(LocalSQLServerConnectionString))
            {
                using (sqlAzureConn = new SqlConnection(RemoteSQLAzureConnectionString))
                {
                    DbSyncScopeDescription myScope = new DbSyncScopeDescription(scopeName);
                    // DbSyncScopeDescription myScope1 = new DbSyncScopeDescription(scopeName2);

                    //DbSyncTableDescription lc_azure = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.semaphore_syn", sqlAzureConn);
                    //  DbSyncTableDescription sem_azure = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.mahy", sqlAzureConn);

                    //DbSyncTableDescription lc_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.levelcrossing", sqlServerConn);
                   // DbSyncTableDescription sem_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.semaphore", sqlServerConn);
                    // DbSyncTableDescription train_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.Strain", sqlServerConn);
                   // DbSyncTableDescription block_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.trainblock", sqlServerConn);
                    DbSyncTableDescription sem_main = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.semaphore_main", sqlServerConn);
                    DbSyncTableDescription block_main = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.trainblock_main", sqlServerConn);
                    /*DbSyncTableDescription semaphore_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.semaphore", sqlServerConn);
                    DbSyncTableDescription train2_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.train", sqlServerConn);
                    DbSyncTableDescription trainblock_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.trainblock", sqlServerConn);*/



                    // Add the tables from above to the scope
                    // myScope.Tables.Add(lc_azure);
                    // myScope.Tables.Add(lc_small);
                    // myScope.Tables.Add(sem_azure);
                    myScope.Tables.Add(sem_main);
                    //myScope.Tables.Add(train_small);
                    myScope.Tables.Add(block_main);
                    /* myScope.Tables.Add(semaphore_small);
                     myScope.Tables.Add(train2_small);
                     myScope.Tables.Add(trainblock_small);*/
                    SqlSyncScopeProvisioning sqlServerProv = new SqlSyncScopeProvisioning(sqlServerConn, myScope);
                    if (!sqlServerProv.ScopeExists(scopeName))
                        // Apply the scope provisioning.
                        sqlServerProv.Apply();
                    // Setup SQL Database for sync
                    SqlSyncScopeProvisioning sqlAzureProv = new SqlSyncScopeProvisioning(sqlAzureConn, myScope);
                    if (!sqlAzureProv.ScopeExists(scopeName))
                        // Apply the scope provisioning.
                        sqlAzureProv.Apply();

                    /*myScope1.Tables.Add(lc_azure);
                    sqlServerProv = new SqlSyncScopeProvisioning(sqlServerConn, myScope1);
                    if (!sqlServerProv.ScopeExists(scopeName2))
                        // Apply the scope provisioning.
                        sqlServerProv.Apply();
                    // Setup SQL Database for sync
                    sqlAzureProv = new SqlSyncScopeProvisioning(sqlAzureConn, myScope1);
                    if (!sqlAzureProv.ScopeExists(scopeName2))
                        // Apply the scope provisioning
                        sqlAzureProv.Apply();*/
                }
            }
        }
        private static void syncrazure6()
        {
            String scopeName = "central1";
            SqlConnection sqlServerConn;
            SqlConnection sqlAzureConn;
            String RemoteSQLAzureConnectionString = "Data Source=DESKTOP-5NLB5GK;Initial Catalog=central;User ID=sa;Password=1234";
            String LocalSQLServerConnectionString = "Data Source=projtrain.database.windows.net;initial catalog=azure_db;User ID=train;password=Proj1234;";

            using (sqlServerConn = new SqlConnection(LocalSQLServerConnectionString))
            {
                using (sqlAzureConn = new SqlConnection(RemoteSQLAzureConnectionString))
                {
                    DbSyncScopeDescription myScope = new DbSyncScopeDescription(scopeName);
                    // DbSyncScopeDescription myScope1 = new DbSyncScopeDescription(scopeName2);

                    //DbSyncTableDescription lc_azure = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.semaphore_syn", sqlAzureConn);
                    //  DbSyncTableDescription sem_azure = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.mahy", sqlAzureConn);

                    //DbSyncTableDescription lc_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.levelcrossing", sqlServerConn);
                    // DbSyncTableDescription sem_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.semaphore", sqlServerConn);
                    // DbSyncTableDescription train_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.Strain", sqlServerConn);
                    // DbSyncTableDescription block_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.trainblock", sqlServerConn);
                    DbSyncTableDescription sem_main = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.semaphore1", sqlServerConn);
                    DbSyncTableDescription block_main = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.trainblock1", sqlServerConn);
                    /*DbSyncTableDescription semaphore_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.semaphore", sqlServerConn);
                    DbSyncTableDescription train2_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.train", sqlServerConn);
                    DbSyncTableDescription trainblock_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.trainblock", sqlServerConn);*/



                    // Add the tables from above to the scope
                    // myScope.Tables.Add(lc_azure);
                    // myScope.Tables.Add(lc_small);
                    // myScope.Tables.Add(sem_azure);
                    myScope.Tables.Add(sem_main);
                    //myScope.Tables.Add(train_small);
                    myScope.Tables.Add(block_main);
                    /* myScope.Tables.Add(semaphore_small);
                     myScope.Tables.Add(train2_small);
                     myScope.Tables.Add(trainblock_small);*/
                    SqlSyncScopeProvisioning sqlServerProv = new SqlSyncScopeProvisioning(sqlServerConn, myScope);
                    if (!sqlServerProv.ScopeExists(scopeName))
                        // Apply the scope provisioning.
                        sqlServerProv.Apply();
                    // Setup SQL Database for sync
                    SqlSyncScopeProvisioning sqlAzureProv = new SqlSyncScopeProvisioning(sqlAzureConn, myScope);
                    if (!sqlAzureProv.ScopeExists(scopeName))
                        // Apply the scope provisioning.
                        sqlAzureProv.Apply();

                    /*myScope1.Tables.Add(lc_azure);
                    sqlServerProv = new SqlSyncScopeProvisioning(sqlServerConn, myScope1);
                    if (!sqlServerProv.ScopeExists(scopeName2))
                        // Apply the scope provisioning.
                        sqlServerProv.Apply();
                    // Setup SQL Database for sync
                    sqlAzureProv = new SqlSyncScopeProvisioning(sqlAzureConn, myScope1);
                    if (!sqlAzureProv.ScopeExists(scopeName2))
                        // Apply the scope provisioning
                        sqlAzureProv.Apply();*/
                }
            }
        }
        private static void syncrazure7()
        {
            String scopeName = "train2";
            SqlConnection sqlServerConn;
            SqlConnection sqlAzureConn;
            String LocalSQLServerConnectionString = "Data Source=DESKTOP-5NLB5GK;Initial Catalog=train;User ID=sa;Password=1234";
            String RemoteSQLAzureConnectionString = "Data Source=projtrain.database.windows.net;initial catalog=azure_db;User ID=train;password=Proj1234;";

            using (sqlServerConn = new SqlConnection(LocalSQLServerConnectionString))
            {
                using (sqlAzureConn = new SqlConnection(RemoteSQLAzureConnectionString))
                {
                    DbSyncScopeDescription myScope = new DbSyncScopeDescription(scopeName);
                    DbSyncTableDescription train_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.train", sqlServerConn);
                 
                    myScope.Tables.Add(train_small);
                 
                    SqlSyncScopeProvisioning sqlServerProv = new SqlSyncScopeProvisioning(sqlServerConn, myScope);
                    if (!sqlServerProv.ScopeExists(scopeName))
                        // Apply the scope provisioning.
                        sqlServerProv.Apply();
                    // Setup SQL Database for sync
                    SqlSyncScopeProvisioning sqlAzureProv = new SqlSyncScopeProvisioning(sqlAzureConn, myScope);
                    if (!sqlAzureProv.ScopeExists(scopeName))
                        // Apply the scope provisioning.
                        sqlAzureProv.Apply();
                }
            }
        }
        private static void syncrazure8()
        {
            String scopeName = "small2";
            SqlConnection sqlServerConn;
            SqlConnection sqlAzureConn;
            String RemoteSQLAzureConnectionString = "Data Source=DESKTOP-5NLB5GK;Initial Catalog=small;User ID=sa;Password=1234";
            String LocalSQLServerConnectionString = "Data Source=projtrain.database.windows.net;initial catalog=azure_db;User ID=train;password=Proj1234;";

            using (sqlServerConn = new SqlConnection(LocalSQLServerConnectionString))
            {
                using (sqlAzureConn = new SqlConnection(RemoteSQLAzureConnectionString))
                {
                    DbSyncScopeDescription myScope = new DbSyncScopeDescription(scopeName);
                    DbSyncTableDescription train_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.train", sqlServerConn);

                    myScope.Tables.Add(train_small);

                    SqlSyncScopeProvisioning sqlServerProv = new SqlSyncScopeProvisioning(sqlServerConn, myScope);
                    if (!sqlServerProv.ScopeExists(scopeName))
                        // Apply the scope provisioning.
                        sqlServerProv.Apply();
                    // Setup SQL Database for sync
                    SqlSyncScopeProvisioning sqlAzureProv = new SqlSyncScopeProvisioning(sqlAzureConn, myScope);
                    if (!sqlAzureProv.ScopeExists(scopeName))
                        // Apply the scope provisioning.
                        sqlAzureProv.Apply();
                }
            }
        }
        private static void syncrazure9()
        {
            String scopeName = "main2";
            SqlConnection sqlServerConn;
            SqlConnection sqlAzureConn;
            String RemoteSQLAzureConnectionString = "Data Source=DESKTOP-I7LBVJ6;Initial Catalog=main_DB;Trusted_Connection=Yes";
            String LocalSQLServerConnectionString = "Data Source=projtrain.database.windows.net;initial catalog=db_v;User ID=train;password=Proj1234;";

            using (sqlServerConn = new SqlConnection(LocalSQLServerConnectionString))
            {
                using (sqlAzureConn = new SqlConnection(RemoteSQLAzureConnectionString))
                {
                    DbSyncScopeDescription myScope = new DbSyncScopeDescription(scopeName);
                    DbSyncTableDescription train_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.train", sqlServerConn);

                    myScope.Tables.Add(train_small);

                    SqlSyncScopeProvisioning sqlServerProv = new SqlSyncScopeProvisioning(sqlServerConn, myScope);
                    if (!sqlServerProv.ScopeExists(scopeName))
                        // Apply the scope provisioning.
                        sqlServerProv.Apply();
                    // Setup SQL Database for sync
                    SqlSyncScopeProvisioning sqlAzureProv = new SqlSyncScopeProvisioning(sqlAzureConn, myScope);
                    if (!sqlAzureProv.ScopeExists(scopeName))
                        // Apply the scope provisioning.
                        sqlAzureProv.Apply();
                }
            }
        }
        private static void syncrazure10()
        {
            String scopeName = "central2";
            SqlConnection sqlServerConn;
            SqlConnection sqlAzureConn;
            String RemoteSQLAzureConnectionString = "Data Source=DESKTOP-5NLB5GK;Initial Catalog=central;User ID=sa;Password=1234";
            String LocalSQLServerConnectionString = "Data Source=projtrain.database.windows.net;initial catalog=azure_db;User ID=train;password=Proj1234;";

            using (sqlServerConn = new SqlConnection(LocalSQLServerConnectionString))
            {
                using (sqlAzureConn = new SqlConnection(RemoteSQLAzureConnectionString))
                {
                    DbSyncScopeDescription myScope = new DbSyncScopeDescription(scopeName);
                    DbSyncTableDescription train_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.train", sqlServerConn);

                    myScope.Tables.Add(train_small);

                    SqlSyncScopeProvisioning sqlServerProv = new SqlSyncScopeProvisioning(sqlServerConn, myScope);
                    if (!sqlServerProv.ScopeExists(scopeName))
                        // Apply the scope provisioning.
                        sqlServerProv.Apply();
                    // Setup SQL Database for sync
                    SqlSyncScopeProvisioning sqlAzureProv = new SqlSyncScopeProvisioning(sqlAzureConn, myScope);
                    if (!sqlAzureProv.ScopeExists(scopeName))
                        // Apply the scope provisioning.
                        sqlAzureProv.Apply();
                }
            }
        }
        private static void syncr1()
        {
            String scopeName = "synct1";
            //String scopeName2 = "azure1";
            SqlConnection sqlServerConn;
            SqlConnection sqlAzureConn;
            String LocalSQLServerConnectionString = "Data Source=DESKTOP-5NLB5GK;Initial Catalog=train;User ID=sa;Password=1234";
            String RemoteSQLAzureConnectionString = "Data Source=projtrain.database.windows.net;initial catalog=G_project;User ID=train;password=Proj1234;";

            using (sqlServerConn = new SqlConnection(LocalSQLServerConnectionString))
            {
                using (sqlAzureConn = new SqlConnection(RemoteSQLAzureConnectionString))
                {
                    DbSyncScopeDescription myScope = new DbSyncScopeDescription(scopeName);
                   // DbSyncScopeDescription myScope1 = new DbSyncScopeDescription(scopeName2);

                   // DbSyncTableDescription lc_azure = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.sem", sqlAzureConn);
                    DbSyncTableDescription sem_azure = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.synf", sqlAzureConn);

                    //DbSyncTableDescription lc_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.levelcrossing", sqlServerConn);
                   // DbSyncTableDescription sem_small = SqlSyncDescriptionBuilder.GetDescriptionForTable("dbo.sem", sqlServerConn);

                    // Add the tables from above to the scope
                    // myScope.Tables.Add(lc_azure);
                    // myScope.Tables.Add(lc_small);
                    //myScope.Tables.Add(sem_small);
                    myScope.Tables.Add(sem_azure);
                   

                    SqlSyncScopeProvisioning sqlServerProv = new SqlSyncScopeProvisioning(sqlServerConn, myScope);
                    if (!sqlServerProv.ScopeExists(scopeName))
                        // Apply the scope provisioning.
                        sqlServerProv.Apply();
                    // Setup SQL Database for sync
                    SqlSyncScopeProvisioning sqlAzureProv = new SqlSyncScopeProvisioning(sqlAzureConn, myScope);
                    if (!sqlAzureProv.ScopeExists(scopeName))
                        // Apply the scope provisioning.
                        sqlAzureProv.Apply();

                   /* myScope1.Tables.Add(lc_azure);
                    sqlServerProv = new SqlSyncScopeProvisioning(sqlServerConn, myScope1);
                    if (!sqlServerProv.ScopeExists(scopeName2))
                        // Apply the scope provisioning.
                        sqlServerProv.Apply();
                    // Setup SQL Database for sync
                    sqlAzureProv = new SqlSyncScopeProvisioning(sqlAzureConn, myScope1);
                    if (!sqlAzureProv.ScopeExists(scopeName2))
                        // Apply the scope provisioning
                        sqlAzureProv.Apply();*/
                }
            }
        }
        static void Main(string[] args)
        {
            // syncr();
            //syncrazure();
            //syncrazure2();
            syncrazure3();
            //syncrazure4();
            syncrazure5();
           // syncrazure6();
            //syncrazure7();
            //syncrazure8();
            syncrazure9();
            //syncrazure10();
        }
    }
}