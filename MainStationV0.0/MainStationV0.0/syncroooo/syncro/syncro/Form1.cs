using Microsoft.Synchronization;
using Microsoft.Synchronization.Data.SqlServer;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace syncro
{
    public partial class Form1 : Form
    {
        SqlConnection sqlServerConn;
        SqlConnection sqlAzureConn;
        //SqlConnection sqlServerConn2;
        String LocalSQLServerConnectionString = "Data Source=DESKTOP-I7LBVJ6;Initial Catalog=main_DB;Trusted_Connection=Yes";
        String RemoteSQLAzureConnectionString = "Data Source=projtrain.database.windows.net;initial catalog=db_v;User ID=train;password=Proj1234;";

        /*String LocalSQLServerConnectionString = "Data Source=DESKTOP-5NLB5GK;Initial Catalog=SmallStation;User ID=sa;Password=1234";
       // String RemoteSQLAzureConnectionString = "Data Source=DESKTOP-5NLB5GK;Initial Catalog=train;User ID=sa;Password=1234";
        String RemoteSQLAzureConnectionString = "Data Source=projtrain.database.windows.net;initial catalog=syncoooo;User ID=train;password=Proj1234;";*/

        String scopeName = "small";
        String scopeName2 = "train";
        String scopeName3 = "main";
        String scopeName4 = "central";

        // String scopeName2 = "mahy1";
        /*SqlConnection sqlServerConn;
        SqlConnection sqlServerConn1;*/

        SyncOrchestrator syncOrchestrator;
        public Form1()
        {
            InitializeComponent();
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
           
           

           
            scopeName = "main";
            RemoteSQLAzureConnectionString = "Data Source=DESKTOP-I7LBVJ6;Initial Catalog=main_DB;Trusted_Connection=Yes";
            LocalSQLServerConnectionString = "Data Source=projtrain.database.windows.net;initial catalog=db_v;User ID=train;password=Proj1234;";

            using (sqlServerConn = new SqlConnection(LocalSQLServerConnectionString))
            {
                using (sqlAzureConn = new SqlConnection(RemoteSQLAzureConnectionString))
                {
                    syncOrchestrator = new SyncOrchestrator
                    {
                        LocalProvider = new SqlSyncProvider(scopeName, sqlAzureConn),
                        RemoteProvider = new SqlSyncProvider(scopeName, sqlServerConn),
                        Direction = SyncDirectionOrder.UploadAndDownload
                    };
                    syncOrchestrator.Synchronize();
                }
            }
            
            scopeName = "main1";
            LocalSQLServerConnectionString = "Data Source=DESKTOP-I7LBVJ6;Initial Catalog=main_DB;Trusted_Connection=Yes";
            RemoteSQLAzureConnectionString = "Data Source=projtrain.database.windows.net;initial catalog=db_v;User ID=train;password=Proj1234;";

            using (sqlServerConn = new SqlConnection(LocalSQLServerConnectionString))
            {
                using (sqlAzureConn = new SqlConnection(RemoteSQLAzureConnectionString))
                {
                    syncOrchestrator = new SyncOrchestrator
                    {
                        LocalProvider = new SqlSyncProvider(scopeName, sqlAzureConn),
                        RemoteProvider = new SqlSyncProvider(scopeName, sqlServerConn),
                        Direction = SyncDirectionOrder.UploadAndDownload
                    };
                    syncOrchestrator.Synchronize();
                }
            }
            
            
            
            scopeName = "main2";
            RemoteSQLAzureConnectionString = "Data Source=DESKTOP-I7LBVJ6;Initial Catalog=main_DB;Trusted_Connection=Yes";
            LocalSQLServerConnectionString = "Data Source=projtrain.database.windows.net;initial catalog=db_v;User ID=train;password=Proj1234;";

            using (sqlServerConn = new SqlConnection(LocalSQLServerConnectionString))
            {
                using (sqlAzureConn = new SqlConnection(RemoteSQLAzureConnectionString))
                {
                    syncOrchestrator = new SyncOrchestrator
                    {
                        LocalProvider = new SqlSyncProvider(scopeName, sqlAzureConn),
                        RemoteProvider = new SqlSyncProvider(scopeName, sqlServerConn),
                        Direction = SyncDirectionOrder.UploadAndDownload
                    };
                    syncOrchestrator.Synchronize();
                }
            }
            
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            
            scopeName = "main";
            RemoteSQLAzureConnectionString = "Data Source=DESKTOP-I7LBVJ6;Initial Catalog=main_DB;Trusted_Connection=Yes";
            LocalSQLServerConnectionString = "Data Source=projtrain.database.windows.net;initial catalog=db_v;User ID=train;password=Proj1234;";

            using (sqlServerConn = new SqlConnection(LocalSQLServerConnectionString))
            {
                using (sqlAzureConn = new SqlConnection(RemoteSQLAzureConnectionString))
                {
                    syncOrchestrator = new SyncOrchestrator
                    {
                        LocalProvider = new SqlSyncProvider(scopeName, sqlAzureConn),
                        RemoteProvider = new SqlSyncProvider(scopeName, sqlServerConn),
                        Direction = SyncDirectionOrder.UploadAndDownload
                    };
                    syncOrchestrator.Synchronize();
                }
            }
          
            scopeName = "main1";
            LocalSQLServerConnectionString = "Data Source=DESKTOP-I7LBVJ6;Initial Catalog=main_DB;Trusted_Connection=Yes";
            RemoteSQLAzureConnectionString = "Data Source=projtrain.database.windows.net;initial catalog=db_v;User ID=train;password=Proj1234;";

            using (sqlServerConn = new SqlConnection(LocalSQLServerConnectionString))
            {
                using (sqlAzureConn = new SqlConnection(RemoteSQLAzureConnectionString))
                {
                    syncOrchestrator = new SyncOrchestrator
                    {
                        LocalProvider = new SqlSyncProvider(scopeName, sqlAzureConn),
                        RemoteProvider = new SqlSyncProvider(scopeName, sqlServerConn),
                        Direction = SyncDirectionOrder.UploadAndDownload
                    };
                    syncOrchestrator.Synchronize();
                }
            }
           
            scopeName = "main2";
            RemoteSQLAzureConnectionString = "Data Source=DESKTOP-I7LBVJ6;Initial Catalog=main_DB;Trusted_Connection=Yes";
            LocalSQLServerConnectionString = "Data Source=projtrain.database.windows.net;initial catalog=db_v;User ID=train;password=Proj1234;";

            using (sqlServerConn = new SqlConnection(LocalSQLServerConnectionString))
            {
                using (sqlAzureConn = new SqlConnection(RemoteSQLAzureConnectionString))
                {
                    syncOrchestrator = new SyncOrchestrator
                    {
                        LocalProvider = new SqlSyncProvider(scopeName, sqlAzureConn),
                        RemoteProvider = new SqlSyncProvider(scopeName, sqlServerConn),
                        Direction = SyncDirectionOrder.UploadAndDownload
                    };
                    syncOrchestrator.Synchronize();
                }
            }
            
        }
    }
}
