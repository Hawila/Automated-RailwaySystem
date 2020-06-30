using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Permissions;
using System.Net.Sockets;
using System.Net;

namespace MainStationV0._0
{
    public partial class Form1 : Form
    {
        int[] flagscope = new int[5];
        int flagintiate;
        List<int> wflag = new List<int>(20);
        List<int> prev_warning_trainid = new List<int>();
        List<int> prev_warning_semid = new List<int>();
        int flag_comm = 0;
        byte[] buffer;
        String dest;
        Socket sck;
        EndPoint epLocal, epRemote;
        SqlConnection connection1;
        string connetionString1;
        string connetionString = "Data Source=DESKTOP-I7LBVJ6;Initial Catalog=main_DB;Trusted_Connection=Yes";
        private int changeCount = 0;
        private const string tableName = "Inventory";
        private const string statusMessage = "{0} changes have occurred.";
        private DataSet dataToWatch = null;
        private SqlCommand command_dep = null;
        string query = "SELECT SemStatus FROM main_DB.dbo.semaphore_main;";
        int t1_flag,t2_flag,t3_flag,t4_flag;
        Timer[] allTimers = new Timer[5];
        SqlConnection connection;
        Panel[] khalifa = new Panel[12];
        Panel[] sultana = new Panel[13];
        List<int> levelcrossing = new List<int>();
        List<int> localsem = new List<int>();
        List<int> lst = new List<int>();
        List<int> trainId = new List<int>();
        List<int> trainStatus = new List<int>();
        List<int> trainPath_id = new List<int>();
        List<int> pathId = new List<int>();
        List<int> xat = new List<int>();
        List<int> y_loc = new List<int>();
        List<int> speed = new List<int>();
        List<int> semaId = new List<int>();
        List<int> semaState = new List<int>();
        List<int> BlockID = new List<int>();
        List<int> activest = new List<int>();
        List<String> source = new List<string>();
        List<String> dest1 = new List<string>();
        List<String> path = new List<string>();
        List<int> xLocm = new List<int>();
        List<int> idblocklist = new List<int>();
        List<int> Xloctrain = new List<int>();
        List<int> ylocation = new List<int>();
        PictureBox[] levelcross = new PictureBox[8];
        Panel[] Z = new Panel[21];
        Panel[] X = new Panel[20];
        Panel[] B = new Panel[20];
        Panel[] C = new Panel[20];
        Panel[] zz = new Panel[7];
        Panel[] p0 = new Panel[14];
        Panel[] p1 = new Panel[40];
        Panel[] p2 = new Panel[40];
        Panel[] p3 = new Panel[40];
        Panel[] p4 = new Panel[40];
        Panel[] p5 = new Panel[40];
        Panel[] p6 = new Panel[40];
        Panel[] p7 = new Panel[40];
        Panel[] p8 = new Panel[40];
        Panel[] p9 = new Panel[40];
        Panel[] p10 = new Panel[40];
        Panel[] p11 = new Panel[40];
        Panel[] p12 = new Panel[40];
        Panel[] p13 = new Panel[40];
        Panel[] p14 = new Panel[40];
        Panel[] p15 = new Panel[40];
        Panel[] p16 = new Panel[40];
        Panel[] p17 = new Panel[40];
        Panel[] p18 = new Panel[40];
        Panel[] p19 = new Panel[40];
        Panel[] p20 = new Panel[40];
        List<int> p0id = new List<int>();
        List<int> p1id = new List<int>();
        List<int> p2id = new List<int>();
        List<int> p3id = new List<int>();
        List<int> p4id = new List<int>();
        List<int> p5id = new List<int>();
        List<int> p6id = new List<int>();
        List<int> p7id = new List<int>();
        List<int> p8id = new List<int>();
        List<int> p9id = new List<int>();
        List<int> p10id = new List<int>();
        List<int> p11id = new List<int>();
        List<int> p12id = new List<int>();
        List<int> p13id = new List<int>();
        List<int> p14id = new List<int>();
        List<int> p15id = new List<int>();
        List<int> p16id = new List<int>();
        List<int> p17id = new List<int>();
        List<int> p18id = new List<int>();
        List<int> p19id = new List<int>();
        
        public struct block
        {
            String bName;
            Panel sSema;
            Panel eSema;
            Boolean isclear;
            public block(String name, Panel s, Panel e)
            {
                this.bName = name;
                this.sSema = s;
                this.eSema = e;
                this.isclear = false;
            }
        }
        public block[] DBpathBlocks = new block[6];
        public block[] path0 = new block[6];
        public block[] path1 = new block[6];
        public block[] path2 = new block[8];
        public block[] path5 = new block[8];
        public block[] path4 = new block[9];
        public block[] path6 = new block[9];
        public block[] path7 = new block[9];
        public block[] path8 = new block[9];
        public block[] path9 = new block[9];
        public block[] path10 = new block[9];
        public block[] path11 = new block[9];
        public block[] path12 = new block[9];
        public block[] path13 = new block[9];
        public block[] path14 = new block[9];
        public block[] path15 = new block[9];
        public block[] path16 = new block[9];
        public block[] path17 = new block[9];
        public block[] path18 = new block[9];
        public block[] path19 = new block[9];
        public block[] path20 = new block[9];

        
        public Form1()
        {
            InitializeComponent();
            if (CanRequestNotifications())
            {
                //textBox1.Text = "true";
                // MessageBox.Show(" true ");
                Console.WriteLine("trueee");
            }
        }
        private void MessageCallBack(IAsyncResult aResult)
        {
            try
            {
                byte[] receivedData = new byte[1500];
                receivedData = (byte[])aResult.AsyncState;
                Console.WriteLine(receivedData);
                ASCIIEncoding aEncoding = new ASCIIEncoding();
                string receivedMessage = aEncoding.GetString(receivedData);
                //listMessage.Items.Add(dest + " :" + receivedMessage);

                buffer = new byte[1500];
                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallBack), buffer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void RoadLayout_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath tanta = new GraphicsPath();
            Pen blue = new Pen(Color.Blue, 8);
            Pen blue2 = new Pen(Color.Blue, 6);
            Pen green = new Pen(Color.Green, 5);
            Pen orange = new Pen(Color.Orange, 5);
            Pen white = new Pen(Color.White, 3);
            Pen black = new Pen(Color.Black, 3);
            SolidBrush whiteb = new SolidBrush(Color.White);
            SolidBrush blackb = new SolidBrush(Color.Black);

            tanta.StartFigure();
            tanta.AddLine(new Point(0, 300), new Point(1400, 300));

            tanta.StartFigure();
            tanta.AddLine(new Point(0, 375), new Point(1400, 375));

            blue.LineJoin = LineJoin.Bevel;
            e.Graphics.DrawPath(blue, tanta);

            tanta.StartFigure();
            tanta.AddLine(new Point(50, 300), new Point(200, 180));
            tanta.AddLine(new Point(200, 180), new Point(790, 180));
            tanta.AddLine(new Point(790, 180), new Point(900, 300));
            e.Graphics.DrawPath(blue, tanta);

            tanta.StartFigure();
            tanta.AddLine(new Point(100, 300), new Point(220, 210)); //desuq so8r
            tanta.AddLine(new Point(220, 210), new Point(780, 210));
            tanta.AddLine(new Point(780, 210), new Point(865, 300));
            e.Graphics.DrawPath(blue, tanta);

            tanta.StartFigure();
            tanta.AddLine(new Point(900, 375), new Point(1060, 505)); //tanta_menof benha
            tanta.AddLine(new Point(1060, 505), new Point(1400, 505));
            //tanta.AddLine(new Point(1180, 505), new Point(1250, 375));
            e.Graphics.DrawPath(blue, tanta);

            tanta.StartFigure();
            tanta.AddLine(new Point(940, 375), new Point(1060, 475)); ///tanta menof banha so8yr
            tanta.AddLine(new Point(1060, 475), new Point(1400, 475));
            //tanta.AddLine(new Point(1160, 475), new Point(1220, 375));
            e.Graphics.DrawPath(blue, tanta);

            tanta.StartFigure();
            tanta.AddLine(new Point(480, 300), new Point(537, 375)); //small curve
           // tanta.AddLine(new Point(491, 375), new Point(703, 375));
            tanta.AddLine(new Point(590, 375), new Point(650, 300));
            e.Graphics.DrawPath(blue, tanta);

            e.Graphics.DrawRectangle(white, 50, 290, 8, 18); //damnhur
            e.Graphics.FillRectangle(whiteb, 50, 290, 8, 18);
            e.Graphics.DrawRectangle(white, 300, 170, 8, 20); //desuq
            e.Graphics.FillRectangle(whiteb, 300, 170, 8, 20);
            e.Graphics.DrawRectangle(white, 680, 170, 8, 20); //qulin
            e.Graphics.FillRectangle(whiteb, 680, 170, 8, 20);
            e.Graphics.DrawRectangle(white, 550, 170, 8, 20); //qulin
            e.Graphics.FillRectangle(whiteb, 550, 170, 8, 20);
            /*e.Graphics.DrawRectangle(white, 300, 290, 8, 20); //etay
            e.Graphics.FillRectangle(whiteb, 300, 290, 8, 20);
            e.Graphics.DrawRectangle(white, 550, 290, 8, 18); //kafr el zyat
            e.Graphics.FillRectangle(whiteb, 550, 290, 8, 18);*/
            e.Graphics.DrawRectangle(white, 890, 290, 8, 18); //tanta
            e.Graphics.FillRectangle(whiteb, 890, 290, 8, 18);
            e.Graphics.DrawRectangle(white, 1100, 290, 8, 18); //been tanta w benha
            e.Graphics.FillRectangle(whiteb, 1100, 290, 8, 18);
            e.Graphics.DrawRectangle(white, 1140, 470, 6, 20);// mnof
            e.Graphics.FillRectangle(whiteb, 1140, 470, 6, 20);

            e.Graphics.DrawRectangle(white, 1250, 290, 8, 10); //Benha
            e.Graphics.FillRectangle(whiteb, 1250, 290, 8, 10);
            e.Graphics.DrawEllipse(white, 16, 260, 52, 24);
            e.Graphics.FillEllipse(whiteb, 16, 260, 52, 24);

            e.Graphics.DrawEllipse(white, 890, 260, 42, 24);
            e.Graphics.FillEllipse(whiteb, 890, 260, 42, 24);

            e.Graphics.DrawEllipse(white, 1230, 260, 42, 24);
            e.Graphics.FillEllipse(whiteb, 1230, 260, 42, 24);


            e.Graphics.DrawEllipse(white, 1125, 515, 42, 18);
            e.Graphics.FillEllipse(whiteb, 1125, 515, 42, 18);

            using (Font myFont = new Font("Arial", 8, FontStyle.Bold))
            {
                e.Graphics.DrawString("Damnhur", myFont, Brushes.Green, new Point(17, 264));
                e.Graphics.DrawString("Tanta", myFont, Brushes.Green, new Point(894, 266));
                e.Graphics.DrawString("Benha", myFont, Brushes.Green, new Point(1234, 266));
                e.Graphics.DrawString("Mnof", myFont, Brushes.Green, new Point(1130, 515));
            }

            e.Graphics.RotateTransform(-28.0f);
            e.Graphics.DrawRectangle(white, 10, 260, 8, 20); // el jazery been damnhur w desuq 
            e.Graphics.FillRectangle(whiteb, 10, 260, 8, 20);
            

            e.Graphics.RotateTransform(60.0f);
            e.Graphics.FillRectangle(whiteb, 765, -280, 8, 20); //saft torab
            e.Graphics.FillRectangle(whiteb, 765, -280, 8, 20);
            e.Graphics.FillRectangle(whiteb, 1020, -180, 8, 20); //between tanta and mnof
            e.Graphics.FillRectangle(whiteb, 1020, -180, 8, 20);

        }

        private void damnhor_desuq_tanta(Panel p, int i)
        {
            if (trainStatus[i - 1] == 1)
            {
                set_Sem_ray7_tanta(p.Location.X, p0);
                int x = p.Location.X;
                int ny = ((-4 * x) + 1700) / 5;
                int ny2 = ((12 * x) - 7500) / 11;
                if (p.Location.X < 48)
                {
                    p.Location = new Point(p.Location.X + 2, p.Location.Y);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 1 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();

                } //y sabta l7d damnhur
                if (p.Location.X >= 50 && p.Location.X < 206)
                {


                    p.Location = new Point(p.Location.X + 2, ny - 16);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 1 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();

                }
                if (p.Location.X >= 206 && p.Location.X < 790)
                {


                    p.Location = new Point(p.Location.X + 2, p.Location.Y);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main='" + 1 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();

                }
                if (p.Location.X >= 790 && p.Location.X < 900)
                {
                    if (p.Location.X >= 824) { set_Sem_damn_verti2(p.Location.X + 70, p0); }
                    p.Location = new Point(p.Location.X + 2, ny2 - 16);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main='" + 2 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X >= 900)
                {
                    p.Location = new Point(913, 290);
                    p.Visible = false;
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main='" + 2 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close(); p.Visible = false;
                }
            }

        }
        private void damnhour_desuq_benha(Panel p, int i)
        {
            if (trainStatus[i - 1] == 1)
            {
                set_Sem_ray7_tanta(p.Location.X, p0);
                int x = p.Location.X;
                int ny = ((-4 * x) + 1700) / 5;
                int ny2 = ((12 * x) - 7500) / 11;
                if (p.Location.X < 50)
                {
                    p.Location = new Point(p.Location.X + 2, p.Location.Y);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 1 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();

                } //y sabta l7d damnhur
                if (p.Location.X >= 50 && p.Location.X < 206)
                {

                    p.Location = new Point(p.Location.X + 2, ny - 16);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 1 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();

                }
                if (p.Location.X >= 206 && p.Location.X < 790)
                {

                    p.Location = new Point(p.Location.X + 2, p.Location.Y);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main='" + 1 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();

                }
                if (p.Location.X >= 790 && p.Location.X < 900)
                {

                    p.Location = new Point(p.Location.X + 2, ny2 - 16);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main='" + 2 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X >= 900 && p.Location.X <= 1250)
                {
                    set_Sem1(p.Location.X, p2);
                    p.Location = new Point(p.Location.X + 4, p.Location.Y);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE pathID = '" + i + "'", connection);
                }
                if (p.Location.X >= 1250)
                {
                    p.Location = new Point(1250, 290);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE pathID = '" + i + "'", connection);
                    p.Visible = false;
                }
            }
        }
        private void damnhou_etay_tanta(Panel p, int i)
        {
            /*for(int ii=0;ii<15;ii++)
            {
                if(p.Location.X==p1[ii].Location.X+11)
                {
                    set_Sem(p.Location.X, p1);
                }
                else if(p.Location.X == p1[ii].Location.X + 12)
                {
                    set_Sem(p.Location.X, p1);
                }
            }*/
            if (trainStatus[i - 1] == 1)
            {
                if (p.Location.X < 900)
                {
                    p.Location = new Point(p.Location.X + 2, 290);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET  xLoc_main= '" + p.Location.X + "' WHERE ID = '" + 1 + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X > 450 && p.Location.X <= 900)
                {
                    //SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 2 + "' WHERE ID = '" + i + "'", connection);
                    //SqlDataReader reader = cmd.ExecuteReader();
                    // reader.Close();
                }
                if (p.Location.X >= 900)
                {
                    //p.Location = new Point(900, 290);
                    //SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection); p.Visible = false;
                    //p.Visible = false;
                }
            }
        }
        private void damnhou_etay_benha(Panel p, int i)
        {
            if (trainStatus[i - 1] == 1)
            {
                //set_Sem(p.Location.X, p3);
                if (p.Location.X <= 1250)
                {
                    p.Location = new Point(p.Location.X + 2, 290);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 1 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X > 450 && p.Location.X < 900)
                {
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 2 + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X > 900 && p.Location.X < 1400)
                {
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 3 + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }

                if (p.Location.X >= 1250)
                {
                    p.Location = new Point(1250, 290);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 3 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close(); p.Visible = false;
                }
            }
        }
        private void damnhou_etay_menof(Panel p, int i)
        {
            if (trainStatus[i - 1] == 1)
            {
                //set_Sem(p.Location.X, p5);
                int x = p.Location.X;
                int ny = ((14 * x) - 7480) / 16;
                int y33 = ((13 * x) - 5700) / 16;

                if (p.Location.X < 900)
                {
                    p.Location = new Point(p.Location.X + 2, 290);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X == 900)
                {
                    p.Location = new Point(900, 375);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 2 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X >= 900 && p.Location.X < 1060)
                {
                    p.Location = new Point(p.Location.X + 2, y33);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 2 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X >= 1060 && p.Location.X <= 1140)
                {
                    p.Location = new Point(p.Location.X + 2, p.Location.Y);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 4 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X >= 1140)
                {
                    p.Location = new Point(1140, p.Location.Y);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 4 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close(); p.Visible = false;
                }
                if (p.Location.X > 450 && p.Location.X < 900)
                {
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 2 + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
            }
        }
        private void damnhor_desuq_menof(Panel p, int i)
        {
            if (trainStatus[i - 1] == 1)
            {
                if (trainStatus[i - 1] == 1)
                {
                    set_Sem_ray7_menof(p.Location.X, p4);
                    int x = p.Location.X;
                    int ny = ((-4 * x) + 1700) / 5;
                    int ny2 = ((12 * x) - 7500) / 11;
                    int ny3 = ((14 * x) - 7480) / 16;
                    int y33 = ((13 * x) - 5700) / 16;
                    if (p.Location.X < 48)
                    {
                        p.Location = new Point(p.Location.X + 2, p.Location.Y);
                        SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 1 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                        SqlDataReader reader = cmd.ExecuteReader();
                        reader.Close();
                    } //y sabta l7d damnhur
                    if (p.Location.X >= 48 && p.Location.X < 206)
                    {
                        p.Location = new Point(p.Location.X + 2, ny - 16);
                        SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 1 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                        SqlDataReader reader = cmd.ExecuteReader();
                        reader.Close();
                    }
                    if (p.Location.X >= 206 && p.Location.X < 790)
                    {
                        p.Location = new Point(p.Location.X + 2, p.Location.Y);
                        SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 1 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                        SqlDataReader reader = cmd.ExecuteReader();
                        reader.Close();
                    }
                    if (p.Location.X >= 790 && p.Location.X < 900)
                    {

                        p.Location = new Point(p.Location.X + 2, ny2 - 16);
                        SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 2 + "' ,xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                        SqlDataReader reader = cmd.ExecuteReader();
                        reader.Close();
                    }
                    if (p.Location.X == 900)
                    {
                        p.Location = new Point(900, 375);
                        SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 2 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                        SqlDataReader reader = cmd.ExecuteReader();
                        reader.Close();
                    }
                    if (p.Location.X >= 900 && p.Location.X <= 1060)
                    {
                        //p.Location = new Point(p.Location.X + 2, ny3 - 2);
                        p.Location = new Point(p.Location.X + 2, y33 - 2);
                        SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 2 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                        SqlDataReader reader = cmd.ExecuteReader();
                        reader.Close();
                    }
                    if (p.Location.X >= 1060 && p.Location.X <= 1140)
                    {
                        p.Location = new Point(p.Location.X + 2, p.Location.Y);
                        SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 4 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                        SqlDataReader reader = cmd.ExecuteReader();
                        reader.Close();
                    }
                    if (p.Location.X >= 1140)
                    {
                        p.Location = new Point(1140, p.Location.Y);
                        SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 4 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                        SqlDataReader reader = cmd.ExecuteReader();
                        reader.Close();
                    }
                    if (p.Location.X > 450 && p.Location.X < 900)
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 2 + "' WHERE ID = '" + i + "'", connection);
                        SqlDataReader reader = cmd.ExecuteReader();
                        reader.Close();
                    }
                }

            }
        }
        private void tanta_benha(Panel p, int i)
        {
            if (trainStatus[i - 1] == 1)
            {
                // set_Sem(p.Location.X, p6);
                int x = p.Location.X;
                if (p.Location.X <= 1250)
                {
                    p.Location = new Point(p.Location.X + 2, p.Location.Y);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 4 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
            }
        }
        private void tanta_menof(Panel p, int i)
        {
            if (trainStatus[i - 1] == 1)
            {
                int x = p.Location.X;
                int y33 = ((13 * x) - 5700) / 16;

                if (p.Location.X == 900)
                {
                    p.Location = new Point(900, 375);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 1 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X >= 900 && p.Location.X <= 1060)
                {
                    p.Location = new Point(p.Location.X + 2, y33);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 4 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X >= 1060 && p.Location.X <= 1140)
                {
                    p.Location = new Point(p.Location.X + 2, p.Location.Y);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 4 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
            }
        }
        private void tanta_desuq_damnhour(Panel p, int i)
        {
            if (trainStatus[i - 1] == 1)
            {
                int x = p.Location.X;
                int ny = ((18 * x) - 10470) / 17;
                int ny2 = ((-9 * x) + 4920) / 14;
                if (p.Location.X <= 900 && p.Location.X > 865)
                {
                    p.Location = new Point(p.Location.X - 4, 310);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 2 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X <= 865 && p.Location.X > 780)
                {
                    p.Location = new Point(p.Location.X - 8, ny);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 2 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X <= 780 && p.Location.X >= 220)
                {
                    p.Location = new Point(p.Location.X - 4, 200);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 2 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X <= 220 && p.Location.X > 50)
                {
                    p.Location = new Point(p.Location.X - 6, ny2);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 1 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
            }
        }
        private void tanta_etay_damnhor(Panel p, int i)
        {
            if (trainStatus[i - 1] == 1)
            {
                int x = p.Location.X;
                if (p.Location.X >= 50)
                {
                    p.Location = new Point(p.Location.X - 6, 310);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 2 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X < 450 && p.Location.X < 50)
                {
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 1 + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close(); p.Visible = false;
                }
            }
        }
        private void tanta_menof_benha(Panel p, int i)
        {
            if (trainStatus[i - 1] == 1)
            {
                int x = p.Location.X;
                int ny = ((14 * x) - 7480) / 16;
                int ny3 = -2 * x + 2820;
                int ny33 = ((13 * x) - 18875) / -7;
                int y33 = ((13 * x) - 5700) / 16;

                if (p.Location.X == 900)
                {
                    p.Location = new Point(900, 375);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 2 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X >= 900 && p.Location.X < 1060)
                {
                    p.Location = new Point(p.Location.X + 2, y33);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 2 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X >= 1060 && p.Location.X < 1180)
                {
                    p.Location = new Point(p.Location.X + 2, p.Location.Y);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 4 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X >= 1180 && p.Location.X <= 1250)
                {
                    p.Location = new Point(p.Location.X + 2, ny33);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 3 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
            }
        }
        private void benha_menof(Panel p, int i)
        {
            if (trainStatus[i - 1] == 1)
            {
                //set_Sem(p.Location.X, p7);
                int x = p.Location.X;
                int ny = ((-10 * x) + 14450) / 6;
                if (p.Location.X > 1230) { p.Location = new Point(p.Location.X - 6, 375); }
                if (p.Location.X > 1160 && p.Location.X <= 1230)
                {
                    p.Location = new Point(p.Location.X - 4, ny);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 3 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X >= 1140 && p.Location.X <= 1160)
                {
                    p.Location = new Point(p.Location.X - 4, 465);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 4 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
            }
        }
        private void benha_menof_tanta(Panel p, int i)
        {
            if (trainStatus[i - 1] == 1)
            {
                int x = p.Location.X;
                int ny = ((-10 * x) + 14450) / 6;
                int ny2 = ((10 * x) - 4900) / 12;
                if (p.Location.X > 1236) { p.Location = new Point(p.Location.X - 6, 365); }
                if (p.Location.X > 1160 && p.Location.X <= 1236)
                {
                    p.Location = new Point(p.Location.X - 4, ny);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 3 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X > 1060 && p.Location.X <= 1160)
                {
                    p.Location = new Point(p.Location.X - 6, 465);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 4 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X > 930 && p.Location.X <= 1060)
                {
                    p.Location = new Point(p.Location.X - 6, ny2);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 4 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                /*if (p.Location.X > 900 && p.Location.X <= 930)
                {
                    p.Location = new Point(p.Location.X - 6, 365);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 2 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close(); 
                }*/
            }
        }
        private void benha_tanta(Panel p, int i)
        {
            if (trainStatus[i - 1] == 1)
            {
                set_Sem_rag3(p.Location.X, p14);
                int x = p.Location.X;
                if (p.Location.X > 900)
                {
                    p.Location = new Point(p.Location.X - 6, 375);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 3 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (x < 1000 && x >= 900)
                {
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 2 + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
            }
        }
        private void benha_desuq_damnh(Panel p, int i)
        {
            if (trainStatus[i - 1] == 1)
            {
                set_Sem_rag3_desuq(p.Location.X, p15);
                int x = p.Location.X;
                if (p.Location.X > 865)
                {
                    p.Location = new Point(p.Location.X - 6, 365);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 3 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                int ny = ((18 * x) - 10470) / 17;
                int ny2 = ((-9 * x) + 4920) / 14;
                if (p.Location.X <= 865 && p.Location.X > 780)
                {
                    p.Location = new Point(p.Location.X - 8, ny);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 2 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X <= 780 && p.Location.X > 220)
                {
                    p.Location = new Point(p.Location.X - 6, 200);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 2 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X <= 220 && p.Location.X > 50)
                {
                    p.Location = new Point(p.Location.X - 7, ny2);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 1 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close(); p.Visible = false;
                }
            }
        }
        private void benha_etay_damnh(Panel p, int i)
        {
            if (trainStatus[i - 1] == 1)
            {
                int x = p.Location.X;
                if (p.Location.X >= 50)
                {
                    p.Location = new Point(p.Location.X - 7, 365);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X > 450 && p.Location.X <= 900)
                {
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 2 + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X < 450 && p.Location.X >= 50)
                {
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 1 + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X > 900 && p.Location.X <= 1250)
                {
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main= '" + 4 + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
            }
        }
        private void mennof_benha(Panel p, int i)
        {
            if (trainStatus[i - 1] == 1)
            {
                int x = p.Location.X;
                int ny3 = ((13 * x) - 18875) / -7;
                if (p.Location.X >= 1140 && p.Location.X < 1180)
                {
                    p.Location = new Point(p.Location.X + 4, 495);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main='" + 4 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X >= 1180 && p.Location.X <= 1250)
                {
                    p.Location = new Point(p.Location.X + 4, ny3 - 20);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main='" + 3 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
            }
        }
        private void menof_tanta(Panel p, int i)
        {
            if (trainStatus[i - 1] == 1)
            {
                int x = p.Location.X;
                int ny2 = ((10 * x) - 4900) / 12;
                if (p.Location.X > 1060 && p.Location.X <= 1140)
                {
                    p.Location = new Point(p.Location.X - 6, 465);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main='" + 4 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X > 930 && p.Location.X <= 1060)
                {
                    p.Location = new Point(p.Location.X - 6, ny2);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main='" + 4 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X > 900 && p.Location.X <= 930)
                {
                    p.Location = new Point(p.Location.X - 6, 365);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main='" + 3 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
            }
        }
        private void menof_desuq_damnhor(Panel p, int i)
        {
            if (trainStatus[i - 1] == 1)
            {
                int x = p.Location.X;
                int ny = ((18 * x) - 10470) / 17;
                int ny3 = ((-9 * x) + 4920) / 14;
                int ny2 = ((10 * x) - 4900) / 12;
                if (p.Location.X > 1060 && p.Location.X <= 1140)
                {
                    p.Location = new Point(p.Location.X - 6, 465);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main='" + 4 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X > 940 && p.Location.X <= 1060)
                {
                    p.Location = new Point(p.Location.X - 6, ny2);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main='" + 4 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X > 865 && p.Location.X <= 940)
                {
                    p.Location = new Point(p.Location.X - 6, 365);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main='" + 2 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X <= 865 && p.Location.X > 780)
                {
                    p.Location = new Point(p.Location.X - 8, ny);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main='" + 2 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X <= 780 && p.Location.X > 220)
                {
                    p.Location = new Point(p.Location.X - 6, 200);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main='" + 2 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X <= 220 && p.Location.X > 50)
                {
                    p.Location = new Point(p.Location.X - 7, ny3);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main='" + 1 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
            }
        }
        private void menof_etay_damnhor(Panel p, int i)
        {
            if (trainStatus[i - 1] == 1)
            {
                int x = p.Location.X;
                int ny2 = ((10 * x) - 4900) / 12;
                if (p.Location.X > 1060 && p.Location.X <= 1140)
                {
                    p.Location = new Point(p.Location.X - 6, 465);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main='" + 4 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X > 940 && p.Location.X <= 1060)
                {
                    p.Location = new Point(p.Location.X - 6, ny2);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main='" + 2 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
                if (p.Location.X > 50 && p.Location.X <= 940)
                {
                    p.Location = new Point(p.Location.X - 7, 365);
                    SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET ID_main='" + 1 + "', xLoc_main = '" + p.Location.X + "', yLoc_main = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                }
            }
        }
        private void altnerativeway(Panel p, int i) {
            int x = p.Location.X;
            int y1 = ((75 * x) - 18900) / 57;
            int y2 = ((-75 * x) + 66750) / 60;
            if (p.Location.X > 50 && p.Location.X < 480)
            {
                p.Location = new Point(p.Location.X +2, 290);
                SqlCommand cmd = new SqlCommand("UPDATE mainDB.dbo.train SET idAuth='" + 4 + "', xLoc = '" + p.Location.X + "', yLoc = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Close();
            }
            if (p.Location.X >= 480 && p.Location.X <= 537)
            {
                p.Location = new Point(p.Location.X +2, y1);
                Console.WriteLine(p.Location.Y);
                SqlCommand cmd = new SqlCommand("UPDATE mainDB.dbo.train SET idAuth='" + 4 + "', xLoc = '" + p.Location.X + "', yLoc = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Close();
            }
            if (p.Location.X >= 537 && p.Location.X < 590)
            {
                p.Location = new Point(p.Location.X +2, 365);
                SqlCommand cmd = new SqlCommand("UPDATE mainDB.dbo.train SET idAuth='" + 4 + "', xLoc = '" + p.Location.X + "', yLoc = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Close();
            }
            if (p.Location.X >= 590 && p.Location.X <= 650)
            {
                p.Location = new Point(p.Location.X +2, y2);
                SqlCommand cmd = new SqlCommand("UPDATE mainDB.dbo.train SET idAuth='" + 4 + "', xLoc = '" + p.Location.X + "', yLoc = '" + p.Location.Y + "' WHERE ID = '" + i + "'", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Close();
            }


        }
   
        private void A1_Timer_Tick(object sender, EventArgs e)
        {
            if (t1_flag == 17)
            {

                damnhor_desuq_tanta(A1, 1);

                //  SqlCommand cmd = new SqlCommand("UPDATE mainDB.dbo.train SET xLoc = '" + A1.Location.X + "', yLoc = '" + A2.Location.Y + "' WHERE ID = '" + A1 + "'", connection);
            }
            if (t1_flag == 18) { damnhou_etay_tanta(A1, 1); }
            if (t1_flag == 19) { damnhour_desuq_benha(A1, 1); }
            if (t1_flag == 20) { damnhou_etay_benha(A1, 1); }
            if (t1_flag == 21) { damnhor_desuq_menof(A1, 1); }
            if (t1_flag == 22) { damnhou_etay_menof(A1, 1); }
            if (t1_flag == 23) { tanta_benha(A1, 1); }
            if (t1_flag == 24) { tanta_menof(A1, 1); }
            if (t1_flag == 25) { tanta_desuq_damnhour(A1, 1); }
            if (t1_flag == 26) { tanta_etay_damnhor(A1, 1); }
            if (t1_flag == 27) { tanta_menof_benha(A1, 1); }
            if (t1_flag == 28) { benha_menof(A1, 1); }
            if (t1_flag == 29) { benha_menof_tanta(A1, 1); }
            if (t1_flag == 30) { benha_tanta(A1, 1); }
            if (t1_flag == 31) { benha_desuq_damnh(A1, 1); }
            if (t1_flag == 32) { benha_etay_damnh(A1, 1); }
            if (t1_flag == 33) { mennof_benha(A1, 1); }
            if (t1_flag == 34) { menof_tanta(A1, 1); }
            if (t1_flag == 35) { menof_desuq_damnhor(A1, 1); }
            if (t1_flag == 36) { menof_etay_damnhor(A1, 1); }
            if (t1_flag == 37) { altnerativeway(A1, 1); }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            levelcross[0] = green_1;
            levelcross[1] = red_1;
            levelcross[2] = green_2;
            levelcross[3] = red_2;
            levelcross[4] = green_3;
            levelcross[5] = red_3;
            levelcross[6] = green_5;
            levelcross[7] = red_5;
            for (int iz = 0; iz < 5; iz++)
                flagscope[iz] = 0;
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            /* try
             {
                 connetionString1 = "Data Source=projtrain.database.windows.net;initial catalog=db_v;User ID=train;password=Proj1234;";
                 connection1 = new SqlConnection(connetionString1);
                 connection1.Open();

             }
             catch (SqlException e1)
             {
                 Console.WriteLine(e.ToString());
             }*/
            for (int iii = 0; iii < 25; iii++)
            {
                prev_warning_trainid.Add(-1);
                prev_warning_semid.Add(-1);
            }
            allTimers[0] = A1_Timer;
            allTimers[1] = Train2;
            allTimers[2] = Train3;
            allTimers[3] = Train4;
            khalifa[0] = Z2; khalifa[1] = Z3; khalifa[2] = Z4; khalifa[3] = Z5; khalifa[4] = Z6; khalifa[5] = Z7; khalifa[6] = Z8; khalifa[7] = Z9;
            khalifa[8] = Z10; khalifa[9] = Z11; khalifa[10] = Z12; khalifa[11] = Z13;

            sultana[0] = X18; sultana[1] = X17; sultana[2] = X16; sultana[3] = X15; sultana[4] = X14; sultana[5] = X13;
            sultana[6] = X12; sultana[7] = X11; sultana[8] = X10; sultana[9] = X9; sultana[10] = X8; sultana[11] = X7;


            Z[0] = Z0; Z[1] = Z1; Z[2] = Z2; Z[3] = Z3; Z[4] = Z4; Z[5] = Z5; Z[6] = Z6; Z[7] = Z7; Z[8] = Z8; Z[9] = Z9;
            Z[10] = Z10; Z[11] = Z11; Z[12] = Z12; Z[13] = Z13; Z[14] = Z14; Z[15] = Z15; Z[16] = Z16; Z[17] = Z17;
            Z[18] = Z18; Z[19] = Z19; Z[20] = Z20;

            X[0] = X1; X[1] = X2; X[2] = X3; X[3] = X4; X[4] = X5; X[5] = X6; X[6] = X7; X[7] = X8; X[8] = X9;
            X[9] = X10; X[10] = X11; X[11] = X12; X[12] = X13; X[13] = X14; X[14] = X15; X[15] = X16; X[16] = X17;
            X[17] = X18; X[18] = X19; X[19] = X20;

            B[0] = B0; B[1] = B1; B[2] = B2; B[3] = B3; B[4] = B4; B[5] = B5; B[6] = B6; B[7] = B7; B[8] = B8; B[9] = B9;
            B[10] = B10; B[11] = B11; B[12] = B12; B[13] = B13; B[14] = B14; B[15] = B15; B[16] = B16; B[17] = B17;

            C[0] = C0; C[1] = C1; C[2] = C2; C[3] = C3; C[4] = C4; C[5] = C5; C[6] = C6; C[7] = C7; C[8] = C8; C[9] = C9;
            C[10] = C10; C[11] = C11; C[12] = C12; C[13] = C13; C[14] = C14;

            p0[0] = B0; p0[1] = B1; p0[2] = B2; p0[3] = B4; p0[4] = B6; p0[5] = B8; p0[6] = B10;
            p0[7] = B12; p0[8] = B13;p0[9] = Z14; //desuqtanta

            p1[0] = Z0; p1[1] = Z1; p1[2] = Z2; p1[3] = Z3; p1[4] = Z4; p1[5] = Z5; p1[6] = Z6; p1[7] = Z7; p1[8] = Z8; p1[9] = Z9;
            p1[10] = Z10; p1[11] = Z11; p1[12] = Z12; p1[13] = Z13; p1[14] = Z14;//etaytanta

            p2[0] = B0; p2[1] = B1; p2[2] = B2; p2[3] = B4; p2[4] = B6; p2[5] = B8; p2[6] = B10;
            p2[7] = B12; p2[8] = B13; p2[9] = Z14; p2[10] = Z15; p2[11] = Z16; p2[12] = Z17; p2[13] = Z18; p2[14] = Z19; p2[15] = Z20;//desuqbenh

            p3[0] = Z0; p3[1] = Z1; p3[2] = Z2; p3[3] = Z3; p3[4] = Z4; p3[5] = Z5; p3[6] = Z6; p3[7] = Z7; p3[8] = Z8; p3[9] = Z9;
            p3[10] = Z10; p3[11] = Z11; p3[12] = Z12; p3[13] = Z13; p3[14] = Z14; p3[15] = Z15; p3[16] = Z16; p3[17] = Z17;
            p3[18] = Z18; p3[19] = Z19; p3[20] = Z20;//etaybenha

            p4[0] = B0; p4[1] = B1; p4[2] = B2; p4[3] = B4; p4[4] = B6; p4[5] = B8; p4[6] = B10;
            p4[7] = B12; p4[8] = B13; p4[9] = Z14; p4[10] = C4; p4[11] = C3; p4[12] = C2; p4[13] = C1; p4[14] = C50;
            p4[15] = C51; p4[16] = C52; //desumenof //desuqmenof

            p5[0] = Z0; p5[1] = Z1; p5[2] = Z2; p5[3] = Z3; p5[4] = Z4; p5[5] = Z5; p5[6] = Z6; p5[7] = Z7; p5[8] = Z8; p5[9] = Z9;
            p5[10] = Z10; p5[11] = Z11; p5[12] = Z12; p5[13] = Z13; p5[14] = Z14; p4[10] = C4; p4[11] = C3; p4[12] = C2; p4[13] = C1; p4[14] = C50;
            p4[15] = C51; p5[16] = C52;  //etaymenof

            p6[0] = Z14; p6[1] = Z15; p6[2] = Z16; p6[3] = Z17; p6[4] = Z18; p6[5] = Z19; p6[6] = Z20;//tanta benha

            p7[0] = C4; p7[1] = C3; p7[2] = C2; p7[3] = C1; p7[4] = C50;
            p7[5] = C51; p7[6] = C52; //tanta menof

            p8[0] = panel10; p8[1] = C5; p8[2] = C6; p8[3] = C7; p8[4] = C9;
            p8[5] = C11; p8[6] = C13; p8[7] = panel58; p8[8] = C14;///tanta desuq

            p17[0] = panel4; p17[1] = C0; p17[2] = B17; p17[3] = B16; p17[4] = B15;
            p17[5] = B14; //menof tanta

            p14[0] = X1; p14[1] = X2; p14[2] = X3; p14[3] = X4; p14[4] = X5;
            p14[5] = X6; //benha tanta


            p15[0] = X1; p15[1] = X2; p15[2] = X3; p15[3] = X4; p15[4] = X5;
            p15[5] = X6; p15[6] = panel10; p15[7] = C5; p15[8] = C6; p15[9] = C7; p15[10] = C9;
            p15[11] = C11; p15[12] = C13; p15[13] = panel58; p15[14] = C14;// benha damnhour

            p20[0] = Z1;p20[1] = X19;p20[2] = X20;










            block b = new block("DT0", Z0, Z3);
            block b1 = new block("DT1", Z3, Z6);
            block b2 = new block("DT2", Z6, Z9);
            block b3 = new block("DT3", Z9, Z12);
            block b4 = new block("DT4", Z12, Z15);
            block b5 = new block("DT5", Z15, Z18);
            block s0 = new block("BD0", X1, X4);
            block s1 = new block("BD1", X4, X7);
            block s2 = new block("BD2", X7, X10);
            block s3 = new block("BD3", X10, X13);
            block s4 = new block("BD4", X13, X16);
            block s5 = new block("BD5", X16, X19);

            block d = new block("DB0", B0, B3);
            block d1 = new block("DB1", B3, B6);
            block d2 = new block("DB2", B6, B9);
            block d3 = new block("DB3", B9, B12);
            block d4 = new block("DB4", B14, B17);

            block f = new block("BD0", C0, C3);
            block f1 = new block("BD0", C5, C8);
            block f2 = new block("BD0", C8, C11);
            block f3 = new block("BD0", C11, C14);
            zz[0] = X1; zz[1] = X4; zz[2] = X7; zz[3] = X10; zz[4] = X13; zz[5] = X16; zz[6] = Z19;

            
            try
            {
                String s = "Data Source=DESKTOP-I7LBVJ6;Initial Catalog=main_DB;Trusted_Connection=Yes";
                connection = new SqlConnection(s);
                connection.Open();
            }
            catch (SqlException e2)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadLine();
            SqlCommand xloc = new SqlCommand("select xLoc_main from main_DB.dbo.train", connection);
            SqlDataReader xread = xloc.ExecuteReader();
            int i = 0;
            while (xread.Read())
            {
                xLocm.Add(xread.GetInt32(0));
                Console.WriteLine(xLocm[0]);
                i++;
            }
            xread.Close();
          

            SqlCommand active = new SqlCommand("select active from main_DB.dbo.train", connection);
            SqlDataReader active_reader = active.ExecuteReader();
            int uu = 0;
            while (active_reader.Read())
            {
                activest.Add(active_reader.GetInt32(0));
                uu++;
            }
            active_reader.Close();

            SqlCommand levelc_state = new SqlCommand("select LcStatus from main_DB.dbo.levelcrossing_main", connection);
            SqlDataReader leve_reader = levelc_state.ExecuteReader();
            while (leve_reader.Read())
            {
                levelcrossing.Add(leve_reader.GetInt32(0));
            }
            leve_reader.Close();

            SqlCommand sem_state = new SqlCommand("select semStatus from main_DB.dbo.semaphore_small", connection);
            SqlDataReader sem_reader = sem_state.ExecuteReader();
            int u = 0;
            while (sem_reader.Read())
            {
                semaState.Add( sem_reader.GetInt32(0));
                u++;
            }
            sem_reader.Close();
            SqlCommand localsem_state = new SqlCommand("select semStatus from main_DB.dbo.semaphore_main", connection);
            SqlDataReader localsem_reader = localsem_state.ExecuteReader();
            int uuu = 0;
            while (localsem_reader.Read())
            {
                localsem.Add( localsem_reader.GetInt32(0));
                uuu++;
            }
            localsem_reader.Close();

            SqlCommand train_status = new SqlCommand("select trainStatus from main_DB.dbo.train", connection);
            SqlDataReader status_read = train_status.ExecuteReader();
            int o = 0;
            while (status_read.Read())
            {
              
                trainStatus.Add( status_read.GetInt32(0));
                o++;
            }
            status_read.Close();

            SqlCommand train_ids = new SqlCommand("select ID from main_DB.dbo.train", connection);
            SqlDataReader id_read = train_ids.ExecuteReader();
            while (id_read.Read())
            {
                trainId.Add(id_read.GetInt32(0));
            }
            id_read.Close();

            SqlCommand block_id = new SqlCommand("select block_id from main_DB.dbo.train", connection);
            SqlDataReader block_read = block_id.ExecuteReader();
            while (block_read.Read())
            {
                BlockID.Add(block_read.GetInt32(0));
                //Console.WriteLine(BlockID[0]);
            }
            block_read.Close();

            SqlCommand xtrain = new SqlCommand("select xLoc_main from main_DB.dbo.train", connection);
            SqlDataReader xx_read = xtrain.ExecuteReader();
            while (xx_read.Read())
            {
                Xloctrain.Add(xx_read.GetInt32(0));
            }
            xx_read.Close();


            SqlCommand ytrain = new SqlCommand("select yLoc_main from main_DB.dbo.train", connection);
            SqlDataReader yy_read = xtrain.ExecuteReader();
            while (yy_read.Read())
            {
                ylocation.Add(yy_read.GetInt32(0));
            }
            yy_read.Close();


            SqlCommand xLocation_q = new SqlCommand("select x_start_main from main_DB.dbo.trainblock_main", connection);
            SqlDataReader xLocation_read = xLocation_q.ExecuteReader();
            int x = 0;
            while (xLocation_read.Read())
            {
                xat.Add( xLocation_read.GetInt32(0));

                x++;
            }
            xLocation_read.Close();

            SqlCommand yLocation_q = new SqlCommand("select y_start_main from main_DB.dbo.trainblock_main", connection);
            SqlDataReader yLocation_read = yLocation_q.ExecuteReader();
            int y = 0;
            while (yLocation_read.Read())
            {
                y_loc.Add( yLocation_read.GetInt32(0));
                y++;
            }
            yLocation_read.Close();


            SqlCommand trainpath = new SqlCommand("select path_id from main_DB.dbo.train ", connection);
            SqlDataReader trainpath_reader = trainpath.ExecuteReader();
            int q = 0;
            while (trainpath_reader.Read())
            {
                trainPath_id.Add( trainpath_reader.GetInt32(0));
                q++;
            }
            trainpath_reader.Close();
            SqlCommand trainpath1 = new SqlCommand("select speed from main_DB.dbo.train ", connection);
            SqlDataReader trainpath1_reader = trainpath1.ExecuteReader();
            int q1 = 0;
            while (trainpath1_reader.Read())
            {
                speed.Add(trainpath1_reader.GetInt32(0));
                q1++;
            }
            trainpath1_reader.Close();
            SqlCommand blocklist = new SqlCommand("select ID from main_DB.dbo.trainblock_main ", connection);
            SqlDataReader idblock_reader = blocklist.ExecuteReader();
            int qq1 = 0;
            while (idblock_reader.Read())
            {
                idblocklist.Add(idblock_reader.GetInt32(0));
               // Console.WriteLine("asasasas"+idblocklist[qq1]);
                qq1++;
            }
            idblock_reader.Close();

           
            
            intiateTrain();
            /*int count = 0;
            while (count < trainId.Count)
            {

                if (trainStatus[count] == 0)
                {
                    //allTimers[count].Stop();
                    count++;
                }
                else
                {
                    moveTrains(trainId[count], Xloctrain[count], ylocation[count], trainStatus[count], trainPath_id[count]);
                    count++;
                }


            }*/
           //action_dep();
        }


        private void intiateTrain(){
           
            for (int i = 0; i < 2; i++) {
                Console.WriteLine("kjdhkfhkjfhkjdf" + BlockID[0]);
                int bl=-1;
                if (flagscope[i]==0)
                {
                    if (trainStatus[i] == 1)
                    {
                        for (int j = 0; j < idblocklist.Count; j++)
                        {
                           // Console.WriteLine("kjdhkfhkjfhkjdf" + BlockID[i]);

                            if (idblocklist[j] == BlockID[i])
                            {
                               //Console.WriteLine("kjdhkfhkjfhkjdf" + BlockID[i]);
                                bl = j;
                            }
                        }
                        if(bl!=-1)
                        {
                            flagscope[i] = 1;
                            if (trainId[i] == 1)
                            {

                                A1.Location = new Point(xat[bl], y_loc[bl]);
                                A1.Visible = true;
                                A1_Timer.Interval = speed[i];
                                t1_flag = trainPath_id[i];
                                A1_Timer.Start();
                            }
                            else if (trainId[i] == 2)
                            {
                                Train2P.Visible = true;
                                Train2P.Location = new Point(xat[bl], y_loc[bl]);
                                t2_flag = trainPath_id[i];
                                Train2.Start();

                            }
                            else if (trainId[i] == 3)
                            {
                                Train3P.Visible = true;
                                Train3P.Location = new Point(xat[bl], y_loc[bl]);
                                t3_flag = trainPath_id[i];
                                Train3.Start();
                            }
                            else if (trainId[i] == 4)
                            {
                                Train4P.Visible = true;
                                Train4P.Location = new Point(xat[bl], y_loc[bl]);
                                t4_flag = trainPath_id[i];
                                Train4.Start();

                            }
                        }
                    }
                }
                
                
                /*else if (trainStatus[i] == 0) {
                    allTimers[i].Stop();
                }*/
            }

        }

        private void panel47_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DB_sync_Tick(object sender, EventArgs e)
        {
            SqlCommand train_status = new SqlCommand("select trainStatus from main_DB.dbo.train", connection);
            SqlDataReader status_read = train_status.ExecuteReader();
            int i = 0;
            while (status_read.Read())
            {
                //Console.WriteLine("dasasdasdasd");
                trainStatus.Insert(i, status_read.GetInt32(0));
                i++;
            }
            status_read.Close();

            SqlCommand train_ids = new SqlCommand("select ID from main_DB.dbo.train", connection);
            SqlDataReader id_read = train_ids.ExecuteReader();
            int rrrr = 0;
            while (id_read.Read())
            {
                trainId.Insert(rrrr,id_read.GetInt32(0));
                rrrr++;
            }
            id_read.Close();

            SqlCommand block_id = new SqlCommand("select block_id from main_DB.dbo.train", connection);
            SqlDataReader block_read = block_id.ExecuteReader();
            int yyyy = 0;
            while (block_read.Read())
            {
                // BlockID.Insert(yyyy,block_read.GetInt32(0));
                BlockID[yyyy] = block_read.GetInt32(0);
               // Console.WriteLine(BlockID[0]);
                yyyy++;
            }
            block_read.Close();



               


            SqlCommand trainpath = new SqlCommand("select path_id from main_DB.dbo.train ", connection);
            SqlDataReader trainpath_reader = trainpath.ExecuteReader();
            int q = 0;
            while (trainpath_reader.Read())
            {
                trainPath_id.Insert(q, trainpath_reader.GetInt32(0));
                q++;
            }
            trainpath_reader.Close();
            SqlCommand trainpath1 = new SqlCommand("select speed from main_DB.dbo.train ", connection);
            SqlDataReader trainpath1_reader = trainpath1.ExecuteReader();
            int q1 = 0;
            while (trainpath1_reader.Read())
            {
                speed.Insert(q1, trainpath1_reader.GetInt32(0));
                q1++;
            }
            trainpath1_reader.Close();
            int count = 0;
            /*
           while (count < trainId.Count)
            {

                if (trainStatus[count] == 0)
                {
                    allTimers[count].Stop();
                    count++;
                }
                else
                {
                    moveTrains(trainId[count], xat[count], y_loc[count], trainStatus[count], trainPath_id[count]);
                    count++;
                }
            }*/
        }

        
        private void moveTrains(int id, int x_loc, int y_loc, int trainstatus, int pathid)
        {
            if (id == 1 && trainstatus == 1)
            {
                A1.Visible = true;
                A1.Location = new Point(x_loc, y_loc);
                //Console.WriteLine(x_loc);
                t1_flag = pathid;
                A1_Timer.Start();
            }
            if (id == 2 && trainstatus == 1)
            {
                Train2P.Visible = true;
                Train2P.Location = new Point(x_loc, y_loc);
                t2_flag = pathid;
                Train2.Start();
            }
            if (id == 3 && trainstatus == 1)
            {
                Train3P.Visible = true;
                Train3P.Location = new Point(x_loc, y_loc);
                t3_flag = pathid;
                Train3.Start();
            }
            if (id == 4 && trainstatus == 1)
            {
                Train4P.Visible = true;
                Train4P.Location = new Point(x_loc, y_loc);
                t4_flag = pathid;
                Train4.Start();
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void semSync_Tick(object sender, EventArgs e)
        {

            SqlCommand sem_state = new SqlCommand("select semStatus from main_DB.dbo.semaphore_small", connection);
            SqlDataReader sem_reader = sem_state.ExecuteReader();
            int u = 0;
            while (sem_reader.Read())
            {
                semaState.Insert(u, sem_reader.GetInt32(0));
                u++;
            }
            sem_reader.Close();
            for (int i = 0; i < 12; i++)
            {
                if (semaState[i] == 2) { khalifa[i].BackColor = Color.Yellow; }
                else if (semaState[i] == 1) { khalifa[i].BackColor = Color.Green; }
                else { khalifa[i].BackColor = Color.Red; }
            }
            for (int i = 13; i < 25; i++)
            {
                if (semaState[i] == 2) { sultana[i-13].BackColor = Color.Yellow; }
                else if (semaState[i] == 1) { sultana[i-13].BackColor = Color.Green; }
                else { sultana[i-13].BackColor = Color.Red; }
            }
            /*for (int i = 0; i < 9; i++) {
                if (semaState[i] == 2) { p0[i].BackColor = Color.Yellow; }
                else if (semaState[i] == 1) { p0[i].BackColor = Color.Green; }
                else if (semaState[i] == 4) { p0[ i].BackColor = Color.Gray; }
                else { p0[i].BackColor = Color.Red; }
            }
            for (int i = 21; i < 41; i++)
            {
                if (semaState[i] == 2) { X[i-21].BackColor = Color.Yellow; }
                else if (semaState[i] == 1) { X[i-21].BackColor = Color.Green; }
                else if (semaState[i] == 4) { X[i-21].BackColor = Color.Gray; }
                else { X[i].BackColor = Color.Red; }
            }
            for (int i = 41; i <= 58; i++)
            {
                if (semaState[i] == 2) { B[i - 41].BackColor = Color.Yellow; }
                else if (semaState[i] == 1) { B[i - 41].BackColor = Color.Green; }
                else if (semaState[i] == 4) { B[i - 41].BackColor = Color.Gray; }
                else { B[i].BackColor = Color.Red; }
            }
            for (int i = 59; i <= 73; i++)
            {
                if (semaState[i] == 2) { C[i - 59].BackColor = Color.Yellow; }
                else if (semaState[i] == 1) { C[i - 59].BackColor = Color.Green; }
                else if (semaState[i] == 4) { C[i - 59].BackColor = Color.Gray; }
                else { C[i].BackColor = Color.Red; }
            }*/

        }

        private void Train3_Tick(object sender, EventArgs e)
        {

            if (t3_flag == 17) { damnhor_desuq_tanta(Train3P, 3); }
            if (t3_flag == 18) { damnhou_etay_tanta(Train3P, 3); }
            if (t3_flag == 19) { damnhour_desuq_benha(Train3P, 3); }
            if (t3_flag == 20) { damnhou_etay_benha(Train3P, 3); }
            if (t3_flag == 21) { damnhor_desuq_menof(Train3P, 3); }
            if (t3_flag == 22) { damnhou_etay_menof(Train3P, 3); }
            if (t3_flag == 23) { tanta_benha(Train3P, 3); }
            if (t3_flag == 24) { tanta_menof(Train3P, 3); }
            if (t3_flag == 25) { tanta_desuq_damnhour(Train3P, 3); }
            if (t3_flag == 26) { tanta_etay_damnhor(Train3P, 3); }
            if (t3_flag == 27) { tanta_menof_benha(Train3P, 3); }
            if (t3_flag == 28) { benha_menof(Train3P, 3); }
            if (t3_flag == 29) { benha_menof_tanta(Train3P, 3); }
            if (t3_flag == 30) { benha_tanta(Train3P, 3); }
            if (t3_flag == 31) { benha_desuq_damnh(Train3P, 3); }
            if (t3_flag == 32) { benha_etay_damnh(Train3P, 3); }
            if (t3_flag == 33) { mennof_benha(Train3P, 3); }
            if (t3_flag == 34) { menof_tanta(Train3P, 3); }
            if (t3_flag == 35) { menof_desuq_damnhor(Train3P, 3); }
            if (t3_flag == 36) { menof_etay_damnhor(Train3P, 3); }
            if (t2_flag == 37) { altnerativeway(Train2P, 3); }
        }

        private void Train4_Tick(object sender, EventArgs e)
        {
            if (t4_flag == 17) { damnhor_desuq_tanta(Train4P, 4); }
            if (t4_flag == 18) { damnhou_etay_tanta(Train4P, 4); }
            if (t4_flag == 19) { damnhour_desuq_benha(Train4P, 4); }
            if (t4_flag == 20) { damnhou_etay_benha(Train4P, 4); }
            if (t4_flag == 21) { damnhor_desuq_menof(Train4P, 4); }
            if (t4_flag == 22) { damnhou_etay_menof(Train4P, 4); }
            if (t4_flag == 23) { tanta_benha(Train4P, 4); }
            if (t4_flag == 24) { tanta_menof(Train4P, 4); }
            if (t4_flag == 25) { tanta_desuq_damnhour(Train4P, 4); }
            if (t4_flag == 26) { tanta_etay_damnhor(Train4P, 4); }
            if (t4_flag == 27) { tanta_menof_benha(Train4P, 4); }
            if (t4_flag == 28) { benha_menof(Train4P, 4); }
            if (t4_flag == 29) { benha_menof_tanta(Train4P, 4); }
            if (t4_flag == 30) { benha_tanta(Train4P, 4); }
            if (t4_flag == 31) { benha_desuq_damnh(Train4P, 4); }
            if (t4_flag == 32) { benha_etay_damnh(Train4P, 4); }
            if (t4_flag == 33) { mennof_benha(Train4P, 4); }
            if (t4_flag == 34) { menof_tanta(Train4P, 4); }
            if (t4_flag == 35) { menof_desuq_damnhor(Train4P, 4); }
            if (t4_flag == 36) { menof_etay_damnhor(Train4P, 4); }
            if (t4_flag == 37) { altnerativeway(Train4P, 4); }
        }

        private void Sts_Tick(object sender, EventArgs e)
        {
            /*for (int i = 0; i < trainId.Count; i++) {
                if (trainStatus[i] == 0)
                {
                    if (trainId[i] == 1) { A1_Timer.Stop(); }
                    else if (trainId[i] == 2) { Train2.Stop(); }
                    //allTimers[i].Stop();
                }
            }*/
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (comboBox1.SelectedIndex == 0) {
                /*int s=0;
                for (int i = 0; i < trainId.Count; i++)
                {
                    if (trainId[i] == 1)
                    {
                        s = i;
                    }
                }*/
                if (trainPath_id[0] == 2 || trainPath_id[0] == 3 || trainPath_id[0] == 5)
                {
                    if (Xloctrain[0] > 20)
                    {
                        Route2.Visible = true;
                        routerag3.Visible = false;
                    }
                }
            
            }
        }

        private void localSem_Tick(object sender, EventArgs e)
        {
            SqlCommand localsem_state = new SqlCommand("select semStatus from main_DB.dbo.semaphore_main", connection);
            SqlDataReader localsem_reader = localsem_state.ExecuteReader();
            int uuu = 0;
            while (localsem_reader.Read())
            {
                localsem.Insert(uuu, localsem_reader.GetInt32(0));
                uuu++;
            }
            localsem_reader.Close();
            for (int i = 0; i < 9; i++)
            {
                if (localsem[i] == 2) { p0[i].BackColor = Color.Yellow; }
                else if (localsem[i] == 1) { p0[i].BackColor = Color.Green; }
                else if (localsem[i] == 4) { p0[i].BackColor = Color.Gray; }
                else { p0[i].BackColor = Color.Red; }
            }
            for (int i = 9; i < 16; i++)
            {
                if (localsem[i] == 2) { p6[i-9].BackColor = Color.Yellow; }
                else if (localsem[i] == 1) { p6[i - 9].BackColor = Color.Green; }
                else if (localsem[i] == 4) { p6[i - 9].BackColor = Color.Gray; }
                else { p6[i-9].BackColor = Color.Red; }
            }
            for (int i = 16; i < 23; i++)
            {
                if (localsem[i] == 2) { p7[i - 16].BackColor = Color.Yellow; }
                else if (localsem[i] == 1) { p7[i - 16].BackColor = Color.Green; }
                else if (localsem[i] == 4) { p7[i - 16].BackColor = Color.Gray; }
                else { p7[i - 16].BackColor = Color.Red; }
            }
            for (int i = 23; i < 29; i++)
            {
                if (localsem[i] == 2) { p17[i - 23].BackColor = Color.Yellow; }
                else if (localsem[i] == 1) { p17[i - 23].BackColor = Color.Green; }
                else if (localsem[i] == 4) { p17[i - 23].BackColor = Color.Gray; }
                else { p17[i - 23].BackColor = Color.Red; }
            }
            for (int i = 29; i < 35; i++)
            {
                if (localsem[i] == 2) { p14[i - 29].BackColor = Color.Yellow; }
                else if (localsem[i] == 1) { p14[i - 29].BackColor = Color.Green; }
                else if (localsem[i] == 4) { p14[i - 29].BackColor = Color.Gray; }
                else { p14[i - 29].BackColor = Color.Red; }
            }
            for (int i = 36; i < 45; i++)
            {
                if (localsem[i] == 2) { p8[i - 36].BackColor = Color.Yellow; }
                else if (localsem[i] == 1) { p8[i - 36].BackColor = Color.Green; }
                else if (localsem[i] == 4) { p8[i - 36].BackColor = Color.Gray; }
                else { p8[i-36].BackColor = Color.Red; }
            }
            for (int i = 45; i <48; i++)
            {
                if (localsem[i] == 2) { p20[i - 45].BackColor = Color.Yellow; }
                else if (localsem[i] == 1) { p20[i - 45].BackColor = Color.Green; }
                else if (localsem[i] == 4) { p20[i - 45].BackColor = Color.Gray; }
                else { p20[i - 45].BackColor = Color.Red; }
            }

        }

        private void Route_btn_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0 && Route2.SelectedIndex == 0) {
                t1_flag = 37;

                //SqlCommand cmd = new SqlCommand("UPDATE MainDBFinal.dbo.Strain SET pathID = '" + 37 + "' WHERE ID = '" + 1 + "'", connection);
                //SqlDataReader reader = cmd.ExecuteReader();
                //reader.Close();
            }
        }

        private void Train2_Tick(object sender, EventArgs e)
        {

            if (t2_flag == 17) { damnhor_desuq_tanta(Train2P, 2); }
            if (t2_flag == 18) { damnhou_etay_tanta(Train2P, 2); }
            if (t2_flag == 19) { damnhour_desuq_benha(Train2P, 2); }
            if (t2_flag == 20) { damnhou_etay_benha(Train2P, 2); }
            if (t2_flag == 21) { damnhor_desuq_menof(Train2P, 2); }
            if (t2_flag == 22) { damnhou_etay_menof(Train2P, 2); }
            if (t2_flag == 23) { tanta_benha(Train2P, 2); }
            if (t2_flag == 24) { tanta_menof(Train2P, 2); }
            if (t2_flag == 25) { tanta_desuq_damnhour(Train2P, 2); }
            if (t2_flag == 26) { tanta_etay_damnhor(Train2P, 2); }
            if (t2_flag == 27) { tanta_menof_benha(Train2P, 2); }
            if (t2_flag == 28) { benha_menof(Train2P, 2); }
            if (t2_flag == 29) { benha_menof_tanta(Train2P, 2); }
            if (t2_flag == 30) { benha_tanta(Train2P, 2); }
            if (t2_flag == 31) { benha_desuq_damnh(Train2P, 2); }
            if (t2_flag == 32) { benha_etay_damnh(Train2P, 2); }
            if (t2_flag == 33) { mennof_benha(Train2P, 2); }
            if (t2_flag == 34) { menof_tanta(Train2P, 2); }
            if (t2_flag == 35) { menof_desuq_damnhor(Train2P, 2); }
            if (t2_flag == 36) { menof_etay_damnhor(Train2P, 2); }
            if (t2_flag == 37) { altnerativeway(Train2P, 2); }
        }
        private void set_Sem(int xtrain,Panel[] p)
        {
             int status;
            SqlCommand cmd;
            int semr = (int)Math.Floor(xtrain / 65.0);
            notification.Items.Add(semr);
            SqlDataReader DR;
            //////////////check y
            if (semr > 0)
            {
                int by1 = p[semr].Location.Y;
                int bx1 = p[semr].Location.X;
                status = 0;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                if (semr > 1)
                {
                    int by2 = p[semr - 1].Location.Y;
                    int bx2 = p[semr-1].Location.X;
                    status = 2;
                    cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                    DR = cmd.ExecuteReader();
                    DR.Close();
                }
                if (semr > 2)
                {
                    if (p[semr - 2].BackColor!=Color.Red)
                    {
                        int by3 = p[semr - 2].Location.Y;
                        int bx3 = p[semr - 2].Location.X;
                        status = 1;
                        cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                        DR = cmd.ExecuteReader();
                        DR.Close();
                    }

                }
            }


        }
        private void set_Sem1(int xtrain, Panel[] p)
        {
            int status;
            SqlCommand cmd;
            int semr = (int)Math.Floor(xtrain / 65.0)-5;
            notification.Items.Add(semr);
            SqlDataReader DR;
            //////////////check y
            if (semr > 0)
            {
                int by1 = p[semr].Location.Y;
                int bx1 = p[semr].Location.X;
                status = 0;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                if (semr > 1)
                {
                    int by2 = p[semr - 1].Location.Y;
                    int bx2 = p[semr - 1].Location.X;
                    status = 2;
                    cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                    DR = cmd.ExecuteReader();
                    DR.Close();
                }
                if (semr > 2)
                {
                    if (p[semr - 2].BackColor != Color.Red)
                    {
                        int by3 = p[semr - 2].Location.Y;
                        int bx3 = p[semr - 2].Location.X;
                        status = 1;
                        cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                        DR = cmd.ExecuteReader();
                        DR.Close();
                    }

                }
            }


        }
        private bool CanRequestNotifications()
        {
            try
            {
                SqlClientPermission perm = new SqlClientPermission(PermissionState.Unrestricted);
                perm.Demand();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void RoadLayout_Click(object sender, EventArgs e)
        {

        }

        private void ok_rout_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("UPDATE main_DB.dbo.train SET path_id = '" + Convert.ToInt32(routID.Text) + "' WHERE ID = '" + Convert.ToInt32(train_idd.Text) + "'", connection);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Close();

            /*cmd = new SqlCommand("UPDATE main_DB.dbo.train SET path_id = '" + Convert.ToInt32(routID.Text) + "' WHERE ID = '" + Convert.ToInt32(train_idd.Text) + "'", connection1);
            reader = cmd.ExecuteReader();
            reader.Close();*/
        }

        private void radCheckedDropDownList1_ItemCheckedChanged(object sender, Telerik.WinControls.UI.RadCheckedListDataItemEventArgs e)
        {
            if (e.Item.Checked == true)
            {
                /* textlocalip.Text = "192.168.1.3";
                 textlocalport.Text = "1000";*/
                SqlDataReader reader2;
                SqlCommand cmd3;
                //id = "No50";
                List<string> lst1 = new List<string>();
                string query5;
                dest = e.Item.Text;
                if (e.Item.Text == "main")
                {
                    /*query5 = "SELECT stationIp FROM dbo.addressIP WHERE stationName = '" + e.Item.Text + "';";
                    cmd3 = new SqlCommand(query5, connection);
                    reader2 = cmd3.ExecuteReader();
                    while (reader2.Read())
                    {
                        textremoteip.Text = reader2.GetString(0);
                    }
                    reader2.Close();
                    query5 = "SELECT stationPort FROM dbo.addressIP WHERE stationName = '" + e.Item.Text + "';";
                    cmd3 = new SqlCommand(query5, connection);
                    reader2 = cmd3.ExecuteReader();
                    while (reader2.Read())
                    {
                        textremoteport.Text = reader2.GetString(0);
                    }
                    reader2.Close();*/
                    textremoteip.Text = "172.20.10.2";
                    textremoteport.Text = "1001";
                    textlocalip.Text = "172.20.10.3";
                    textlocalport.Text = "1000";
                }
                else if (e.Item.Text == "small")
                {
                    /*query5 = "SELECT stationIp FROM dbo.addressIP WHERE stationName = '" + e.Item.Text + "';";
                    cmd3 = new SqlCommand(query5, connection);
                    reader2 = cmd3.ExecuteReader();
                    while (reader2.Read())
                    {
                        textremoteip.Text = reader2.GetString(0);
                    }

                    reader2.Close();
                    query5 = "SELECT stationPort FROM dbo.addressIP WHERE stationName = '" + e.Item.Text + "';";
                    cmd3 = new SqlCommand(query5, connection);
                    reader2 = cmd3.ExecuteReader();
                    while (reader2.Read())
                    {
                        textremoteport.Text = reader2.GetString(0);
                    }
                    reader2.Close();*/
                    /*textremoteip.Text = "172.20.10.2";
                    textremoteport.Text = "1001";
                    textlocalip.Text = "172.20.10.3";
                    textlocalport.Text = "1001";*/
                    textremoteip.Text = "10.106.193.180";
                    textremoteport.Text = "1000";
                    textlocalip.Text = "10.109.136.18";
                    textlocalport.Text = "1000";

                }
                else if (e.Item.Text == "train")
                {
                    textremoteip.Text = "172.20.10.4";
                    textremoteport.Text = "2000";
                    textlocalip.Text = "172.20.10.3";
                    textlocalport.Text = "2001";
                }
                else if (e.Item.Text == "central")
                {
                    textremoteip.Text = "172.20.10.4";
                    textremoteport.Text = "2000";
                    textlocalip.Text = "172.20.10.3";
                    textlocalport.Text = "2001";
                }
                groupBox1.Text = e.Item.Text;
                Console.ReadLine();
                try
                {
                    epLocal = new IPEndPoint(IPAddress.Parse(textlocalip.Text), Convert.ToInt32(textlocalport.Text));
                    sck.Bind(epLocal);
                    epRemote = new IPEndPoint(IPAddress.Parse(textremoteip.Text), Convert.ToInt32(textremoteport.Text));
                    sck.Connect(epRemote);
                    buffer = new byte[1500];
                    sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallBack), buffer);
                    flag_comm = 1;
                }
                catch
                {
                    MessageBox.Show("connection is closed, notify for message");
                }

            }
        }

        private void buttonSend_Click_1(object sender, EventArgs e)
        {
            ASCIIEncoding aEncoding = new ASCIIEncoding();
            byte[] sendingMessage = new byte[1500];
            sendingMessage = aEncoding.GetBytes(textMessage.Text);
            SqlDataReader insert;
            SqlCommand cmd_insert;
            if (dest == "main")
            {
                cmd_insert = new SqlCommand("INSERT INTO queue_message(main_id,sender,msgDatetime, msgData) VALUES('2','main','" + DateTime.Now + "','" + textMessage.Text + "');", connection);
                insert = cmd_insert.ExecuteReader();
                insert.Close();
            }
            else if (dest == "small")
            {
                cmd_insert = new SqlCommand("INSERT INTO queue_message(small_id,sender,msgDatetime, msgData) VALUES('4','main','" + DateTime.Now + "','" + textMessage.Text + "');", connection);
                insert = cmd_insert.ExecuteReader();
                insert.Close();
            }
            else if (dest == "train")
            {
                cmd_insert = new SqlCommand("INSERT INTO queue_message(train_id,sender,msgDatetime, msgData) VALUES('5','main','" + DateTime.Now + "','" + textMessage.Text + "');", connection);
                insert = cmd_insert.ExecuteReader();
                insert.Close();
            }
            else if (dest == "central")
            {
                cmd_insert = new SqlCommand("INSERT INTO queue_message(central,sender,msgDatetime, msgData) VALUES('1','main','" + DateTime.Now + "','" + textMessage.Text + "');", connection);
                insert = cmd_insert.ExecuteReader();
                insert.Close();
            }
            if (flag_comm == 1)
            {
                sck.Send(sendingMessage);
                listMessage.Items.Add("main: " + textMessage.Text);
                textMessage.Text = "";
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel11.Visible = true;
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {

        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {

            ISynchronizeInvoke i = (ISynchronizeInvoke)this;
            if (i.InvokeRequired)
            {
                OnChangeEventHandler tempDelegate = new OnChangeEventHandler(dependency_OnChange);
                object[] args = { sender, e };
                i.BeginInvoke(tempDelegate, args);
                return;
            }
            SqlDependency dependency = (SqlDependency)sender;
            dependency.OnChange -= dependency_OnChange;
            ++changeCount;
            label6.Text = String.Format(statusMessage, changeCount);
            notification.Items.Clear();
            notification.Items.Add("Info:   " + e.Info.ToString());
            notification.Items.Add("Source: " + e.Source.ToString());
            notification.Items.Add("Type:   " + e.Type.ToString());
            GetData();
        }
        private void GetData()
        {
            dataToWatch.Clear();
            command_dep.Notification = null;
            SqlDependency dependency = new SqlDependency(command_dep);
            dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
            using (SqlDataAdapter adapter = new SqlDataAdapter(command_dep))
            {
                adapter.Fill(dataToWatch, tableName);
            }
        }
        private void action_dep()
        {
            changeCount = 0;
            // label1.Text = String.Format(statusMessage, changeCount);
            string query_not = "SELECT main_id FROM dbo.queue_message;";
            SqlDependency.Stop(connetionString);
            SqlDependency.Start(connetionString);
            if (connection == null)
            {
                connection = new SqlConnection(connetionString);
            }
            if (command_dep == null)
            {
                command_dep = new SqlCommand(query_not, connection);
                SqlParameter prm = new SqlParameter("@Quantity", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.DbType = DbType.Int32;
                prm.Value = 100;
                command_dep.Parameters.Add(prm);
            }
            if (dataToWatch == null)
            {
                dataToWatch = new DataSet();
            }
            GetData();
        }
        private void set_Sem_damn_verti(int xtrain, Panel[] p)
        {
            int status;
            SqlCommand cmd;
            int semr = (int)Math.Floor(xtrain / 56.0);
            
            SqlDataReader DR;
            //////////////check y
            if (semr > 0)
            {
                int by1 = p[semr].Location.Y;
                int bx1 = p[semr].Location.X;
                status = 0;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                if (semr > 1)
                {
                    int by2 = p[semr - 1].Location.Y;
                    int bx2 = p[semr - 1].Location.X;
                    status = 2;
                    cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                    DR = cmd.ExecuteReader();
                    DR.Close();
                }
                if (semr > 2)
                {
                    if (p[semr - 2].BackColor != Color.Red)
                    {
                        int by3 = p[semr - 2].Location.Y;
                        int bx3 = p[semr - 2].Location.X;
                        status = 1;
                        cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                        DR = cmd.ExecuteReader();
                        DR.Close();
                    }

                }
            }


        }
        private void set_Sem_damn_nor(int xtrain, Panel[] p)
        {
            int status;
            SqlCommand cmd;
            int semr = (int)Math.Floor(xtrain / 130.0);
            
            SqlDataReader DR;
            //////////////check y
            if (semr > 0)
            {
                int by1 = p[semr].Location.Y;
                int bx1 = p[semr].Location.X;
                status = 0;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                if (semr > 1)
                {
                    int by2 = p[semr -1].Location.Y;
                    int bx2 = p[semr -1].Location.X;
                    status = 2;
                    cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                    DR = cmd.ExecuteReader();
                    DR.Close();
                }
                if (semr > 2)
                {
                    if (p[semr -2].BackColor != Color.Red)
                    {
                        int by3 = p[semr -2].Location.Y;
                        int bx3 = p[semr -2].Location.X;
                        status = 1;
                        cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                        DR = cmd.ExecuteReader();
                        DR.Close();
                    }

                }
            }


        }
        private void set_Sem_damn_verti2(int xtrain, Panel[] p)
        {
            if (xtrain <= 940)
            {
                int status;
                SqlCommand cmd;
                int semr = (int)Math.Floor(xtrain  / 46.0) - 11;

                SqlDataReader DR;
                //////////////check y
                if (semr > 0)
                {
                    int by1 = p[semr - 1].Location.Y;
                    int bx1 = p[semr - 1].Location.X;
                    status = 0;
                    cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                    DR = cmd.ExecuteReader();
                    DR.Close();
                    if (semr > 1)
                    {
                        int by2 = p[semr - 2].Location.Y;
                        int bx2 = p[semr - 2].Location.X;
                        status = 2;
                        cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                        DR = cmd.ExecuteReader();
                        DR.Close();
                    }
                    if (semr > 2)
                    {
                        if (p[semr - 3].BackColor != Color.Red)
                        {
                            int by3 = p[semr - 3].Location.Y;
                            int bx3 = p[semr - 3].Location.X;
                            status = 1;
                            cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                            DR = cmd.ExecuteReader();
                            DR.Close();
                        }

                    }
                }
            }


        }
        private void set_Sem_ray7_tanta(int xtrain, Panel[] p)
        {
            if (xtrain < 173 && xtrain > 127)
            {
                int status;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[0].Location.Y;
                int bx1 = p[0].Location.X;
                status = 0;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 231 && xtrain > 173)
            {
                int status;
                int statusred;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[0].Location.Y;
                int bx1 = p[0].Location.X;
                int by2 = p[1].Location.Y;
                int bx2 = p[1].Location.X;
                status = 2;
                statusred = 0;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 361 && xtrain > 231)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[0].Location.Y;
                int bx1 = p[0].Location.X;
                int by2 = p[1].Location.Y;
                int bx2 = p[1].Location.X;
                int by3 = p[2].Location.Y;
                int bx3 = p[2].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 491 && xtrain > 361)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[1].Location.Y;
                int bx1 = p[1].Location.X;
                int by2 = p[2].Location.Y;
                int bx2 = p[2].Location.X;
                int by3 = p[3].Location.Y;
                int bx3 = p[3].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 621 && xtrain > 491)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[2].Location.Y;
                int bx1 = p[2].Location.X;
                int by2 = p[3].Location.Y;
                int bx2 = p[3].Location.X;
                int by3 = p[4].Location.Y;
                int bx3 = p[4].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 751 && xtrain > 621)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[3].Location.Y;
                int bx1 = p[3].Location.X;
                int by2 = p[4].Location.Y;
                int bx2 = p[4].Location.X;
                int by3 = p[5].Location.Y;
                int bx3 = p[5].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 835 && xtrain > 751)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[4].Location.Y;
                int bx1 = p[4].Location.X;
                int by2 = p[5].Location.Y;
                int bx2 = p[5].Location.X;
                int by3 = p[6].Location.Y;
                int bx3 = p[6].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 911 && xtrain > 835)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[5].Location.Y;
                int bx1 = p[5].Location.X;
                int by2 = p[6].Location.Y;
                int bx2 = p[6].Location.X;
                int by3 = p[7].Location.Y;
                int bx3 = p[7].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 985 && xtrain > 911)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[6].Location.Y;
                int bx1 = p[6].Location.X;
                int by2 = p[7].Location.Y;
                int bx2 = p[7].Location.X;
                int by3 = p[8].Location.Y;
                int bx3 = p[8].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }


        }

        private void intiateTimer_Tick(object sender, EventArgs e)
        {
            intiateTrain();
        }

        private void levelCrossing_Tick(object sender, EventArgs e)
        {
            SqlCommand levelcl_state = new SqlCommand("select LcStatus from main_DB.dbo.levelcrossing_small", connection);
            SqlDataReader level_reader = levelcl_state.ExecuteReader();
            while (level_reader.Read()) {
                if()//// read mn el small ya 5alifa <3 xD

            }
            SqlCommand levelc_state = new SqlCommand("select LcStatus from main_DB.dbo.levelcrossing_main", connection);
            SqlDataReader leve_reader = levelc_state.ExecuteReader();
            int uuu = 0;
            while (leve_reader.Read())
            {
                levelcrossing.Insert(uuu, leve_reader.GetInt32(0));
              //  if (levelcrossing[uuu] < 4)
              //  {
                    if (levelcrossing[uuu] == 0)
                    {
                        levelcross[uuu].Visible = true;
                        levelcross[uuu + 1].Visible = false;
                    }
                    else
                    {
                        levelcross[uuu].Visible = false;
                        levelcross[uuu + 1].Visible = true;
                    }
              //  }
                /*else {
                    if (levelcrossing[uuu] == 0)
                    {
                        levelcross[2*uuu].Visible = true;
                        levelcross[2*uuu + 1].Visible = false;
                    }
                    else
                    {
                        levelcross[2*uuu].Visible = false;
                        levelcross[2*uuu + 1].Visible = true;
                    }
                }*/
                Console.WriteLine(levelcrossing[uuu]);
                uuu += 2;
            }
            leve_reader.Close();
           /* if (levelcrossing[1] == 1) { red_1.Visible = false; }
            else if (levelcrossing[1] == 0)
            {
                red_1.Visible = true;
                green_1.Visible = false;
            }*/
        }

        private void set_Sem_rag3(int xtrain, Panel[] p)
        {
            if (xtrain < 1260&&xtrain>1195)
            {
                int status;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[0].Location.Y;
                int bx1 = p[0].Location.X;
                status = 0;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 1195 && xtrain > 1130)
            {
                int status;
                int statusred;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[0].Location.Y;
                int bx1 = p[0].Location.X;
                int by2 = p[1].Location.Y;
                int bx2 = p[1].Location.X;
                status = 2;
                statusred = 0;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 1130 && xtrain > 1065)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[0].Location.Y;
                int bx1 = p[0].Location.X;
                int by2 = p[1].Location.Y;
                int bx2 = p[1].Location.X;
                int by3 = p[2].Location.Y;
                int bx3 = p[2].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 1065 && xtrain > 1000)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[1].Location.Y;
                int bx1 = p[1].Location.X;
                int by2 = p[2].Location.Y;
                int bx2 = p[2].Location.X;
                int by3 = p[3].Location.Y;
                int bx3 = p[3].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 1000 && xtrain > 935)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[2].Location.Y;
                int bx1 = p[2].Location.X;
                int by2 = p[3].Location.Y;
                int bx2 = p[3].Location.X;
                int by3 = p[4].Location.Y;
                int bx3 = p[4].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 935 && xtrain > 870)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[3].Location.Y;
                int bx1 = p[3].Location.X;
                int by2 = p[4].Location.Y;
                int bx2 = p[4].Location.X;
                int by3 = p[5].Location.Y;
                int bx3 = p[5].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }


        }
        private void set_Sem_ray7_menof(int xtrain, Panel[] p)
        {
            if (xtrain < 173 && xtrain > 127)
            {
                int status;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[0].Location.Y;
                int bx1 = p[0].Location.X;
                status = 0;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 231 && xtrain > 173)
            {
                int status;
                int statusred;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[0].Location.Y;
                int bx1 = p[0].Location.X;
                int by2 = p[1].Location.Y;
                int bx2 = p[1].Location.X;
                status = 2;
                statusred = 0;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 361 && xtrain > 231)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[0].Location.Y;
                int bx1 = p[0].Location.X;
                int by2 = p[1].Location.Y;
                int bx2 = p[1].Location.X;
                int by3 = p[2].Location.Y;
                int bx3 = p[2].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 491 && xtrain > 361)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[1].Location.Y;
                int bx1 = p[1].Location.X;
                int by2 = p[2].Location.Y;
                int bx2 = p[2].Location.X;
                int by3 = p[3].Location.Y;
                int bx3 = p[3].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 621 && xtrain > 491)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[2].Location.Y;
                int bx1 = p[2].Location.X;
                int by2 = p[3].Location.Y;
                int bx2 = p[3].Location.X;
                int by3 = p[4].Location.Y;
                int bx3 = p[4].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 751 && xtrain > 621)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[3].Location.Y;
                int bx1 = p[3].Location.X;
                int by2 = p[4].Location.Y;
                int bx2 = p[4].Location.X;
                int by3 = p[5].Location.Y;
                int bx3 = p[5].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 835 && xtrain > 751)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[4].Location.Y;
                int bx1 = p[4].Location.X;
                int by2 = p[5].Location.Y;
                int bx2 = p[5].Location.X;
                int by3 = p[6].Location.Y;
                int bx3 = p[6].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 911 && xtrain > 835)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[5].Location.Y;
                int bx1 = p[5].Location.X;
                int by2 = p[6].Location.Y;
                int bx2 = p[6].Location.X;
                int by3 = p[7].Location.Y;
                int bx3 = p[7].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 951 && xtrain > 911)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[6].Location.Y;
                int bx1 = p[6].Location.X;
                int by2 = p[7].Location.Y;
                int bx2 = p[7].Location.X;
                int by3 = p[8].Location.Y;
                int bx3 = p[8].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 993 && xtrain > 951)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[8].Location.Y;
                int bx1 = p[8].Location.X;
                int by2 = p[9].Location.Y;
                int bx2 = p[9].Location.X;
                int by3 = p[10].Location.Y;
                int bx3 = p[10].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 1050 && xtrain > 993)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[9].Location.Y;
                int bx1 = p[9].Location.X;
                int by2 = p[10].Location.Y;
                int bx2 = p[10].Location.X;
                int by3 = p[11].Location.Y;
                int bx3 = p[11].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 1116 && xtrain > 1050)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[10].Location.Y;
                int bx1 = p[10].Location.X;
                int by2 = p[11].Location.Y;
                int bx2 = p[11].Location.X;
                int by3 = p[12].Location.Y;
                int bx3 = p[12].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 1181 && xtrain > 1116)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[11].Location.Y;
                int bx1 = p[11].Location.X;
                int by2 = p[12].Location.Y;
                int bx2 = p[12].Location.X;
                int by3 = p[13].Location.Y;
                int bx3 = p[13].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 1246 && xtrain > 1181)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[12].Location.Y;
                int bx1 = p[12].Location.X;
                int by2 = p[13].Location.Y;
                int bx2 = p[13].Location.X;
                int by3 = p[14].Location.Y;
                int bx3 = p[14].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 1311 && xtrain > 1246)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[13].Location.Y;
                int bx1 = p[13].Location.X;
                int by2 = p[14].Location.Y;
                int bx2 = p[14].Location.X;
                int by3 = p[15].Location.Y;
                int bx3 = p[15].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain > 1311)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[14].Location.Y;
                int bx1 = p[14].Location.X;
                int by2 = p[15].Location.Y;
                int bx2 = p[15].Location.X;
                int by3 = p[16].Location.Y;
                int bx3 = p[16].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }


        }


        private void set_Sem_rag3_desuq(int xtrain, Panel[] p)
        {
            if (xtrain < 1260 && xtrain > 1195)
            {
                int status;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[0].Location.Y;
                int bx1 = p[0].Location.X;
                status = 0;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 1195 && xtrain > 1130)
            {
                int status;
                int statusred;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[0].Location.Y;
                int bx1 = p[0].Location.X;
                int by2 = p[1].Location.Y;
                int bx2 = p[1].Location.X;
                status = 2;
                statusred = 0;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 1130 && xtrain > 1065)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[0].Location.Y;
                int bx1 = p[0].Location.X;
                int by2 = p[1].Location.Y;
                int bx2 = p[1].Location.X;
                int by3 = p[2].Location.Y;
                int bx3 = p[2].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 1065 && xtrain > 1000)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[1].Location.Y;
                int bx1 = p[1].Location.X;
                int by2 = p[2].Location.Y;
                int bx2 = p[2].Location.X;
                int by3 = p[3].Location.Y;
                int bx3 = p[3].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 1000 && xtrain > 935)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[2].Location.Y;
                int bx1 = p[2].Location.X;
                int by2 = p[3].Location.Y;
                int bx2 = p[3].Location.X;
                int by3 = p[4].Location.Y;
                int bx3 = p[4].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 935 && xtrain > 818)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[3].Location.Y;
                int bx1 = p[3].Location.X;
                int by2 = p[4].Location.Y;
                int bx2 = p[4].Location.X;
                int by3 = p[5].Location.Y;
                int bx3 = p[5].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 818 && xtrain > 790)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[4].Location.Y;
                int bx1 = p[4].Location.X;
                int by2 = p[5].Location.Y;
                int bx2 = p[5].Location.X;
                int by3 = p[6].Location.Y;
                int bx3 = p[6].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 790 && xtrain > 750)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[5].Location.Y;
                int bx1 = p[5].Location.X;
                int by2 = p[6].Location.Y;
                int bx2 = p[6].Location.X;
                int by3 = p[7].Location.Y;
                int bx3 = p[7].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 750 && xtrain > 620)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[6].Location.Y;
                int bx1 = p[6].Location.X;
                int by2 = p[7].Location.Y;
                int bx2 = p[7].Location.X;
                int by3 = p[8].Location.Y;
                int bx3 = p[8].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 620 && xtrain > 490)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[7].Location.Y;
                int bx1 = p[7].Location.X;
                int by2 = p[8].Location.Y;
                int bx2 = p[8].Location.X;
                int by3 = p[9].Location.Y;
                int bx3 = p[9].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 490 && xtrain > 360)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[8].Location.Y;
                int bx1 = p[8].Location.X;
                int by2 = p[9].Location.Y;
                int bx2 = p[9].Location.X;
                int by3 = p[10].Location.Y;
                int bx3 = p[10].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 360 && xtrain > 230)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[9].Location.Y;
                int bx1 = p[9].Location.X;
                int by2 = p[10].Location.Y;
                int bx2 = p[10].Location.X;
                int by3 = p[11].Location.Y;
                int bx3 = p[11].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 230 && xtrain > 200)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[10].Location.Y;
                int bx1 = p[10].Location.X;
                int by2 = p[11].Location.Y;
                int bx2 = p[11].Location.X;
                int by3 = p[12].Location.Y;
                int bx3 = p[12].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 200 && xtrain > 166)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[11].Location.Y;
                int bx1 = p[11].Location.X;
                int by2 = p[12].Location.Y;
                int bx2 = p[12].Location.X;
                int by3 = p[13].Location.Y;
                int bx3 = p[13].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }
            if (xtrain < 166)
            {
                int status;
                int statusred;
                int statusgreen;
                SqlCommand cmd;
                SqlDataReader DR;
                int by1 = p[12].Location.Y;
                int bx1 = p[12].Location.X;
                int by2 = p[13].Location.Y;
                int bx2 = p[13].Location.X;
                int by3 = p[14].Location.Y;
                int bx3 = p[14].Location.X;
                status = 2;
                statusred = 0;
                statusgreen = 1;
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusgreen + "' WHERE xLoc_main = '" + bx1 + "' AND yLoc_main = '" + by1 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + status + "' WHERE xLoc_main = '" + bx2 + "' AND yLoc_main = '" + by2 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
                cmd = new SqlCommand("UPDATE dbo.semaphore_main SET semStatus = '" + statusred + "' WHERE xLoc_main = '" + bx3 + "' AND yLoc_main = '" + by3 + "'", connection);
                DR = cmd.ExecuteReader();
                DR.Close();
            }




        }

        private void warning_message(int typeOfWarning, int trianid, int semid)
        {
            if (typeOfWarning == 1 && prev_warning_trainid[trianid] != typeOfWarning && prev_warning_semid[semid] != semid)
            {
                prev_warning_trainid[typeOfWarning] = typeOfWarning;
                prev_warning_semid[trianid] = semid;
                warningMsg.Items.Add("Info:   " + "semaphore is green " + semid + " for" + trianid + " " + DateTime.Now);
            }
            else if (typeOfWarning == 0 && prev_warning_trainid[trianid] != typeOfWarning && prev_warning_semid[trianid] != semid)
            {
                prev_warning_trainid[typeOfWarning] = typeOfWarning;
                prev_warning_semid[trianid] = semid;
                /*prev_warning_trainid[typeOfWarning] = typeOfWarning;
                prev_warning_semid[typeOfWarning] = typeOfWarning;*/
                warningMsg.Items.Add("Info:   " + "semaphore is red, you have to stop the train. " + "for semaphore id " + semid + " for  train ID " + trianid + " " + DateTime.Now);
            }
            else if (typeOfWarning == 2 && prev_warning_trainid[trianid] != typeOfWarning && prev_warning_semid[trianid] != semid)
            {
                prev_warning_trainid[typeOfWarning] = typeOfWarning;
                prev_warning_semid[trianid] = semid;
                warningMsg.Items.Add("Info:   " + "semaphore is yellow, you have to slow your speed " + "for semaphore id " + semid + " for  train ID " + trianid + " " + DateTime.Now);
            }
            else if (typeOfWarning == 3 && prev_warning_trainid[trianid] != typeOfWarning && prev_warning_semid[trianid] != semid)
            {
                warningMsg.Items.Add("Info:   " + "semaphore out of service " + "for semaphore id " + semid + " for  train ID " + trianid + " " + DateTime.Now);
                prev_warning_trainid[typeOfWarning] = typeOfWarning;
                prev_warning_semid[trianid] = semid;
            }
            else if (typeOfWarning == 4 && prev_warning_trainid[trianid] != typeOfWarning && prev_warning_semid[trianid] != semid)
            {
                prev_warning_trainid[typeOfWarning] = typeOfWarning;
                prev_warning_semid[trianid] = semid;
                warningMsg.Items.Add("Info:   " + "level crossing is safe  " + "for semaphore id " + semid + " for  train ID " + trianid + " " + DateTime.Now);
            }
            else if (typeOfWarning == 5 && prev_warning_trainid[trianid] != typeOfWarning && prev_warning_semid[trianid] != semid)
            {
                prev_warning_trainid[typeOfWarning] = typeOfWarning;
                prev_warning_semid[trianid] = semid;
                //   MessageBox.Show("level crossing not clear please stop.", "Warning..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                warningMsg.Items.Add("Info:   " + "level crossing not clear please stop " + "for semaphore id " + semid + " for  train ID " + trianid + " " + DateTime.Now);
            }
            else if (typeOfWarning == 6 && prev_warning_trainid[trianid] != typeOfWarning && prev_warning_semid[trianid] != semid)
            {
                prev_warning_trainid[typeOfWarning] = typeOfWarning;
                prev_warning_semid[trianid] = semid;
                // camera
                warningMsg.Items.Add("Info:   " + " there is an obstacle so level crossing not clear please stop " + "for semaphore id " + semid + " for  train ID " + trianid + " " + DateTime.Now);
            }
            else if (typeOfWarning == 7 && prev_warning_trainid[trianid] != typeOfWarning && prev_warning_semid[trianid] != semid)
            {
                prev_warning_trainid[typeOfWarning] = typeOfWarning;
                prev_warning_semid[trianid] = semid;
                warningMsg.Items.Add("Info:   " + "there is a train in front of you at the same block " + "for semaphore id " + semid + " for  train ID " + trianid + " " + DateTime.Now);
            }
            else if (typeOfWarning == 8 && prev_warning_trainid[trianid] != typeOfWarning && prev_warning_semid[trianid] != semid)
            {
                prev_warning_trainid[typeOfWarning] = typeOfWarning;
                prev_warning_semid[trianid] = semid;
                warningMsg.Items.Add("Info:   " + "there is a train behind you at the same block " + "for semaphore id " + semid + " for  train ID " + trianid + " " + DateTime.Now);
            }
        }


    }



}
