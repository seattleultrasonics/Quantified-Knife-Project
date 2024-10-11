using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.IO;
using System.Xml;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using System.Windows;
using System.Text.RegularExpressions;

namespace QKPRobot
{
    public partial class QKPRobotGUI : Form
    {

        RuishanScale scale;
        Robot robot;
        CutTestController TestController;
        CutTestDataSet CutTestDS;
        private BindingSource bindingSource;
        string seriesName = "";
        private double lastXValue;
        public bool Slice = true;
        public string autosavePath = @"C:\Users\scott\Desktop\AutoSaves";
        public bool PauseUpdates = false;

        public QKPRobotGUI()
        {
            InitializeComponent();
            robot = new Robot("192.168.1.210");
            comboBoxCOMPortScale.DataBindings.Add("SelectedItem", Properties.Settings.Default, "serialPortScale", true, DataSourceUpdateMode.OnPropertyChanged);
            PopulateCOMPortList();
            
            string portname = Properties.Settings.Default["serialPortScale"].ToString();

            if (Properties.Settings.Default["serialPortScale"] != null && !portname.Trim().Equals(""))
            {
                
                scale = new RuishanScale(portname);
                scale.Open();
            }

            CutTestDS = new CutTestDataSet();
            TestController = new CutTestController(robot, scale, CutTestDS);
            BindSavedSettings();

            InitializeChart();

            
            tmrGatherData.Start();
            //tmrAutoSave.Start();
            //scale.Tare();

        }

        private void BindSavedSettings()
        {
            //CutTestDS.SaveDataSetToMemory();
            CutTestDS.LoadDataSetFromMemory();

            tbBladeID.DataBindings.Add("Text", Properties.Settings.Default, "BladeID", false, DataSourceUpdateMode.OnPropertyChanged);
            tbSample.DataBindings.Add("Text", Properties.Settings.Default, "Sample", false, DataSourceUpdateMode.OnPropertyChanged);
            tbTrialNum.DataBindings.Add("Text", Properties.Settings.Default, "Trial", false, DataSourceUpdateMode.OnPropertyChanged);
            tbZEnd.DataBindings.Add("Text", Properties.Settings.Default, "ZEnd", false, DataSourceUpdateMode.OnPropertyChanged);
            double z;
            if (double.TryParse(Properties.Settings.Default.ZEnd, out z))
                TestController.ZMinimum = z;


        }

        private void chart1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var chartArea = chart1.ChartAreas[0];
                double xValue = chartArea.AxisX.PixelPositionToValue(e.X);

                // Store the X coordinate in a class-level variable
                lastXValue = xValue;

                //contextMenu.Show(chart1, e.Location);
            }
        }


        private void InitializeChart()
        {

            if (chart2 != null)
            {
                BindDataToChart();
            }
            dgSummary.UserDeletingRow += summaryDataGridView_UserDeletingRow;

        }

        private void AddNewSeries(string seriesName, double xValue, double yValue)
        {
            // Create a new series
            Series newSeries = new Series(seriesName)
            {
                ChartType = SeriesChartType.FastLine
            };
            newSeries.Points.AddXY(xValue, yValue);
            chart2.Series.Add(newSeries);

        }

        private void BindDataToChart()
        {
            chart2.DataBindCrossTable(
                CutTestDS.CutTestRawData.AsEnumerable(),          // IEnumerable
                "Trial",                               // Group by Series Name
                "PosZ",                                 // X Value Member
                "Force",                                     // Y Value Members
                ""                                           // Other Members (Empty)
            );

            CutTestDS.CutTestRawData.RowChanged += CutTestDS_RowChanged;

            

            // Update the ChartType for each new series
            foreach (Series series in chart2.Series)
            {
                series.ChartType = SeriesChartType.FastLine;
                series.BorderWidth = 5;
            }

            dgSummary.DataSource = CutTestDS.CutTestSummaryData;

        }

        private void summaryDataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            // Get the BladeID, Sample, and Trial from the row being deleted
            var bladeID = e.Row.Cells["BladeID"].Value.ToString();
            var sample = e.Row.Cells["Sample"].Value.ToString();
            var trial = e.Row.Cells["Trial"].Value.ToString();
            PauseUpdates = true;
            CutTestDS.DeleteSeries(bladeID, sample, trial);
            PauseUpdates = false;
            RedrawChart();
        }

        private void CutTestDS_RowChanged(object sender, EventArgs e)
        {
            if(!PauseUpdates)
            { 
            this.Invoke(new MethodInvoker(() =>
            {
                RedrawChart();
            }));
            }
        }

        private void RedrawChart()
        {
            chart2.Series.Clear();
            chart2.DataBindCrossTable(
                CutTestDS.CutTestRawData.AsEnumerable(),          // IEnumerable
                "SeriesName",                               // Group by Series Name
                "PosZ",                                 // X Value Member
                "Force",                                     // Y Value Members
                ""                                           // Other Members (Empty)
            );
            foreach (Series series in chart2.Series)
            {
                series.ChartType = SeriesChartType.FastLine;
                series.BorderWidth = 5;
            }
            dgSummary.Update();
        }

        private void PopulateCOMPortList()
        {
            string[] ports = SerialPort.GetPortNames();
            comboBoxCOMPortScale.Items.AddRange(ports);
            if (ports.Length > 0)
            {

            }
            //comboBoxCOMPorts.SelectedIndex = 0;
            else
                MessageBox.Show("No COM ports found on this computer.");
        }

        private void btnConnectScale_Click(object sender, EventArgs e)
        {
            if (comboBoxCOMPortScale.SelectedItem != null)
            {
                string selectedPort = comboBoxCOMPortScale.SelectedItem.ToString();
                //scale = new ArduinoScale(selectedPort);
                scale = new RuishanScale(selectedPort);
                scale.Open();
                TestController = new CutTestController(robot, scale, CutTestDS);
            }
            else
            {
                MessageBox.Show("Please select a COM port from the list.");
            }
        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            if (scale != null && !bgwRunCutTest.IsBusy)
            {
                lblForce.Text = scale.ToString();
            }
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CutTestDS.SaveDataSetToMemory();
            Properties.Settings.Default.Save();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        public Color GenerateRandomColor()
        {
            Random random = new Random();
            // Generate a random integer between 0 and 255 for each color component
            int red = random.Next(256);
            int green = random.Next(256);
            int blue = random.Next(256);

            // Create a Color object from the RGB components
            return Color.FromArgb(red, green, blue);
        }


        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileSaver saver = new FileSaver();
            string filePath = saver.SaveFileAs();
            CutTestDS.SaveAs(filePath);
            saveToolStripMenuItem.Enabled = true;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    ExcelImporter importer = new ExcelImporter();
                    PauseUpdates = true;
                    importer.ImportDataTablesFromExcel(filePath, CutTestDS.CutTestRawData, CutTestDS.CutTestSummaryData);
                    PauseUpdates = false;
                    RedrawChart();

                }
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to clear all data?", "Clear Data", MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
            {
                try
                {
                    CutTestDS.ClearData();
                }
                catch (Exception ex) { }
            }
            else
            {
                // Optional: Handle the Cancel action here if needed.
            }
        }



        private void btnRun_Click(object sender, EventArgs e)
        {
            if (scale.Mass != 0)
            {
                DialogResult r = MessageBox.Show("Did you tare the scale?");
                if (scale.Mass == 0)
                {

                }
            }
            TestController.BladeID = tbBladeID.Text;
            TestController.Sample = tbSample.Text;
            TestController.Trial = int.Parse(tbTrialNum.Text);

            if (!bgwRunCutTest.IsBusy)
            {
                bgwRunCutTest.RunWorkerAsync();
               
            }
            else
            {
                bgwRunCutTest.CancelAsync();
               
            }

        }

        private void btn_Stop(object sender, MouseEventArgs e)
        {
            robot.Stop();
            /*
            Console.WriteLine("Stopping");
            XArmAPI.set_mode(5); //set to cartesian velocity control mode
            XArmAPI.set_state(0);
            float[] stop = { 0, 0, 0, 0, 0, 0 };
            XArmAPI.vc_set_cartesian_velocity(stop, false, -1);
            */

        }

        private void btnDown_Click(object sender, MouseEventArgs e)
        {
            robot.MoveZ(-25);
        }

        private void btnUp_Click(object sender, MouseEventArgs e)
        {
            robot.MoveZ(25);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            robot.Stop();
            if (bgwRunCutTest.IsBusy)
            {
                bgwRunCutTest.CancelAsync();
            }
            if (bgwZHome.IsBusy)
            {
                bgwZHome.CancelAsync();
            }
        }

        private void btnZmin_Click(object sender, EventArgs e)
        {
            bgwZHome.RunWorkerAsync();

        }

        private void fileToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (CutTestDS._filePath == null)
            {
                saveToolStripMenuItem.Enabled = false;
            }
            else
            {
                saveToolStripMenuItem.Enabled = true;
            }
        }

        private void bgwRunCutTest_DoWork(object sender, DoWorkEventArgs e)
        {
            if (Slice)
            {
                TestController.Slice();
            }
            else
            {
                TestController.PushCut();
            }
        }

        private void bgwRunCutTest_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            TestController.dataSet.UpdateSummaryTable();
            btnSlice.Text = "Slice";
            //auto increment test number
            TestController.Trial++;
            tbTrialNum.Text = TestController.Trial.ToString();
        }

        private void btnSlice_Click(object sender, EventArgs e)
        {
            Slice = true;
            if (TestController.CheckScaleZeroed())
            {
                TestController.SetTrialInfo(tbBladeID.Text, tbSample.Text, int.Parse(tbTrialNum.Text));

                if (!bgwRunCutTest.IsBusy)
                {
                    bgwRunCutTest.RunWorkerAsync();
                    btnSlice.Text = "Stop";
                }
                else
                {
                    bgwRunCutTest.CancelAsync();
                    btnSlice.Text = "Slice";
                }
            }
            
        }

        private void bgwZHome_DoWork(object sender, DoWorkEventArgs e)
        {

            double zEnd = TestController.FindZMin();
            this.Invoke(new MethodInvoker(() =>
            {
                tbZEnd.Text = zEnd.ToString("F2");
                CutTestDS.SaveDataSetToMemory();
                Properties.Settings.Default.Save();
            }));
        }

        private void btnTare_Click(object sender, EventArgs e)
        {
            //scale.Tare();
        }

        private void btnGoToStart_Click(object sender, EventArgs e)
        {
            TestController.GoToStart();
        }

        private void tmrGatherData_Tick(object sender, EventArgs e)
        {
            if (TestController.TestRunning)
            {
                TestController.dataSet.CutTestRawData.Rows.Add(TestController.GetCurrentData());
            }
        }

        private void btnPushCut_Click(object sender, EventArgs e)
        {
            Slice = false;
            if (TestController.CheckScaleZeroed())
            {
                TestController.SetTrialInfo(tbBladeID.Text, tbSample.Text, int.Parse(tbTrialNum.Text));

                if (!bgwRunCutTest.IsBusy)
                {
                    bgwRunCutTest.RunWorkerAsync();
                    
                }
                else
                {
                    bgwRunCutTest.CancelAsync();
                    
                }
            }

        }

        private void tmrAutoSave_Tick(object sender, EventArgs e)
        {
            FileSaver saver = new FileSaver();
            string filePath = autosavePath + DateTime.Now.ToShortTimeString() + ".xml";
            CutTestDS.SaveAs(filePath);
        }

        private void btnZPose_Click(object sender, EventArgs e)
        {
            TestController.GoToSliceEndAngleforZ();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CutTestDS.Save();
            
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(CutTestDS._filePath == null)
            {
                saveToolStripMenuItem.Enabled = false;
            }
            else
            {
                saveToolStripMenuItem.Enabled = true;
            }
        }

        private void btnPushCutGoToStart_Click(object sender, EventArgs e)
        {
            TestController.GoToPushCutStart();
        }

        private void tbZEnd_Leave(object sender, EventArgs e)
        {
            try
            {
                TestController.ZMinimum = double.Parse(tbZEnd.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }

        private void btnSetPushStart_Click(object sender, EventArgs e)
        {
            TestController.PushCutStartingPose[2] = robot.Position.Z;
            MessageBox.Show("Push Cut start set to " + String.Format(TestController.PushCutStartingPose[2].ToString(), "F2"));
        }
    }

}
