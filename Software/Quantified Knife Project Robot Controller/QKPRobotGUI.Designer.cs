using System;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;

namespace QKPRobot
{
    partial class QKPRobotGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.bgwRunCutTest = new System.ComponentModel.BackgroundWorker();
            this.dgSummary = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbSample = new System.Windows.Forms.TextBox();
            this.tbTrialNum = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSetFrequency = new System.Windows.Forms.Button();
            this.btnConnectScale = new System.Windows.Forms.Button();
            this.comboBoxCOMPortScale = new System.Windows.Forms.ComboBox();
            this.lblPowerLive = new System.Windows.Forms.Label();
            this.lblForce = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnlSweepControls = new System.Windows.Forms.FlowLayoutPanel();
            this.tbPageZCalibrate = new System.Windows.Forms.TabControl();
            this.tabPageCutTest = new System.Windows.Forms.TabPage();
            this.btnPushCutGoToStart = new System.Windows.Forms.Button();
            this.btnPushCut = new System.Windows.Forms.Button();
            this.btnGoToStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnSlice = new System.Windows.Forms.Button();
            this.tbBladeID = new System.Windows.Forms.TextBox();
            this.lblBladeID = new System.Windows.Forms.Label();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.tbPageZ = new System.Windows.Forms.TabPage();
            this.btnSetPushStart = new System.Windows.Forms.Button();
            this.btnZPose = new System.Windows.Forms.Button();
            this.btnStop2 = new System.Windows.Forms.Button();
            this.btnDown2 = new System.Windows.Forms.Button();
            this.btnUp2 = new System.Windows.Forms.Button();
            this.btnZmin = new System.Windows.Forms.Button();
            this.tbZEnd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlChart = new System.Windows.Forms.Panel();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnTare = new System.Windows.Forms.Button();
            this.VisibleColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgButtonDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pointsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.visibleDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.bladeIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sampleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bgwZHome = new System.ComponentModel.BackgroundWorker();
            this.tmrGatherData = new System.Windows.Forms.Timer(this.components);
            this.tmrAutoSave = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgSummary)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlSweepControls.SuspendLayout();
            this.tbPageZCalibrate.SuspendLayout();
            this.tabPageCutTest.SuspendLayout();
            this.tbPageZ.SuspendLayout();
            this.pnlChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Enabled = true;
            this.tmrUpdate.Interval = 250;
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // bgwRunCutTest
            // 
            this.bgwRunCutTest.WorkerSupportsCancellation = true;
            this.bgwRunCutTest.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwRunCutTest_DoWork);
            this.bgwRunCutTest.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwRunCutTest_RunWorkerCompleted);
            // 
            // dgSummary
            // 
            this.dgSummary.AllowUserToAddRows = false;
            this.dgSummary.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgSummary.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgSummary.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgSummary.Location = new System.Drawing.Point(0, 0);
            this.dgSummary.Name = "dgSummary";
            this.dgSummary.RowHeadersWidth = 51;
            this.dgSummary.RowTemplate.Height = 40;
            this.dgSummary.Size = new System.Drawing.Size(2148, 376);
            this.dgSummary.TabIndex = 47;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(378, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 33);
            this.label1.TabIndex = 48;
            this.label1.Text = "Trial";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-1, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 33);
            this.label3.TabIndex = 50;
            this.label3.Text = "Sample";
            // 
            // tbSample
            // 
            this.tbSample.Location = new System.Drawing.Point(6, 84);
            this.tbSample.Name = "tbSample";
            this.tbSample.Size = new System.Drawing.Size(366, 41);
            this.tbSample.TabIndex = 53;
            // 
            // tbTrialNum
            // 
            this.tbTrialNum.Location = new System.Drawing.Point(382, 84);
            this.tbTrialNum.Name = "tbTrialNum";
            this.tbTrialNum.Size = new System.Drawing.Size(168, 41);
            this.tbTrialNum.TabIndex = 54;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 700);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(198, 33);
            this.label6.TabIndex = 62;
            this.label6.Text = "Set Frequency";
            // 
            // btnSetFrequency
            // 
            this.btnSetFrequency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSetFrequency.Location = new System.Drawing.Point(429, 700);
            this.btnSetFrequency.Name = "btnSetFrequency";
            this.btnSetFrequency.Size = new System.Drawing.Size(74, 37);
            this.btnSetFrequency.TabIndex = 63;
            this.btnSetFrequency.Text = "Go";
            this.btnSetFrequency.UseVisualStyleBackColor = true;
            // 
            // btnConnectScale
            // 
            this.btnConnectScale.Location = new System.Drawing.Point(337, 4);
            this.btnConnectScale.Name = "btnConnectScale";
            this.btnConnectScale.Size = new System.Drawing.Size(160, 39);
            this.btnConnectScale.TabIndex = 65;
            this.btnConnectScale.Text = "Connect";
            this.btnConnectScale.UseVisualStyleBackColor = true;
            this.btnConnectScale.Click += new System.EventHandler(this.btnConnectScale_Click);
            // 
            // comboBoxCOMPortScale
            // 
            this.comboBoxCOMPortScale.FormattingEnabled = true;
            this.comboBoxCOMPortScale.Location = new System.Drawing.Point(13, 8);
            this.comboBoxCOMPortScale.Name = "comboBoxCOMPortScale";
            this.comboBoxCOMPortScale.Size = new System.Drawing.Size(318, 40);
            this.comboBoxCOMPortScale.TabIndex = 64;
            // 
            // lblPowerLive
            // 
            this.lblPowerLive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPowerLive.AutoSize = true;
            this.lblPowerLive.Location = new System.Drawing.Point(1887, 507);
            this.lblPowerLive.Name = "lblPowerLive";
            this.lblPowerLive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblPowerLive.Size = new System.Drawing.Size(92, 33);
            this.lblPowerLive.TabIndex = 66;
            this.lblPowerLive.Text = "power";
            // 
            // lblForce
            // 
            this.lblForce.AutoSize = true;
            this.lblForce.Location = new System.Drawing.Point(503, 8);
            this.lblForce.Name = "lblForce";
            this.lblForce.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblForce.Size = new System.Drawing.Size(87, 33);
            this.lblForce.TabIndex = 46;
            this.lblForce.Text = "Force";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.dataToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(2148, 30);
            this.menuStrip1.TabIndex = 70;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 26);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.DropDownOpening += new System.EventHandler(this.fileToolStripMenuItem_DropDownOpening);
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem});
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(55, 26);
            this.dataToolStripMenuItem.Text = "Data";
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(126, 26);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 30);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pnlSweepControls);
            this.splitContainer1.Panel1.Controls.Add(this.pnlChart);
            this.splitContainer1.Panel1.Controls.Add(this.pnlTop);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgSummary);
            this.splitContainer1.Size = new System.Drawing.Size(2148, 995);
            this.splitContainer1.SplitterDistance = 616;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 72;
            // 
            // pnlSweepControls
            // 
            this.pnlSweepControls.Controls.Add(this.tbPageZCalibrate);
            this.pnlSweepControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSweepControls.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlSweepControls.Location = new System.Drawing.Point(0, 400);
            this.pnlSweepControls.Name = "pnlSweepControls";
            this.pnlSweepControls.Size = new System.Drawing.Size(2148, 216);
            this.pnlSweepControls.TabIndex = 74;
            // 
            // tbPageZCalibrate
            // 
            this.tbPageZCalibrate.Controls.Add(this.tabPageCutTest);
            this.tbPageZCalibrate.Controls.Add(this.tbPageZ);
            this.tbPageZCalibrate.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbPageZCalibrate.Location = new System.Drawing.Point(3, 3);
            this.tbPageZCalibrate.Name = "tbPageZCalibrate";
            this.tbPageZCalibrate.SelectedIndex = 0;
            this.tbPageZCalibrate.Size = new System.Drawing.Size(1600, 193);
            this.tbPageZCalibrate.TabIndex = 72;
            // 
            // tabPageCutTest
            // 
            this.tabPageCutTest.Controls.Add(this.btnPushCutGoToStart);
            this.tabPageCutTest.Controls.Add(this.btnPushCut);
            this.tabPageCutTest.Controls.Add(this.btnGoToStart);
            this.tabPageCutTest.Controls.Add(this.btnStop);
            this.tabPageCutTest.Controls.Add(this.btnSlice);
            this.tabPageCutTest.Controls.Add(this.tbBladeID);
            this.tabPageCutTest.Controls.Add(this.lblBladeID);
            this.tabPageCutTest.Controls.Add(this.btnDown);
            this.tabPageCutTest.Controls.Add(this.btnUp);
            this.tabPageCutTest.Controls.Add(this.label3);
            this.tabPageCutTest.Controls.Add(this.btnSetFrequency);
            this.tabPageCutTest.Controls.Add(this.label6);
            this.tabPageCutTest.Controls.Add(this.tbTrialNum);
            this.tabPageCutTest.Controls.Add(this.label1);
            this.tabPageCutTest.Controls.Add(this.tbSample);
            this.tabPageCutTest.Location = new System.Drawing.Point(4, 41);
            this.tabPageCutTest.Name = "tabPageCutTest";
            this.tabPageCutTest.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCutTest.Size = new System.Drawing.Size(1592, 148);
            this.tabPageCutTest.TabIndex = 0;
            this.tabPageCutTest.Text = "Trial";
            this.tabPageCutTest.UseVisualStyleBackColor = true;
            // 
            // btnPushCutGoToStart
            // 
            this.btnPushCutGoToStart.Location = new System.Drawing.Point(752, 85);
            this.btnPushCutGoToStart.Name = "btnPushCutGoToStart";
            this.btnPushCutGoToStart.Size = new System.Drawing.Size(189, 37);
            this.btnPushCutGoToStart.TabIndex = 74;
            this.btnPushCutGoToStart.Text = "Go To Start";
            this.btnPushCutGoToStart.UseVisualStyleBackColor = true;
            this.btnPushCutGoToStart.Click += new System.EventHandler(this.btnPushCutGoToStart_Click);
            // 
            // btnPushCut
            // 
            this.btnPushCut.Location = new System.Drawing.Point(752, 34);
            this.btnPushCut.Name = "btnPushCut";
            this.btnPushCut.Size = new System.Drawing.Size(189, 37);
            this.btnPushCut.TabIndex = 73;
            this.btnPushCut.Text = "Push Cut";
            this.btnPushCut.UseVisualStyleBackColor = true;
            this.btnPushCut.Click += new System.EventHandler(this.btnPushCut_Click);
            // 
            // btnGoToStart
            // 
            this.btnGoToStart.Location = new System.Drawing.Point(965, 84);
            this.btnGoToStart.Name = "btnGoToStart";
            this.btnGoToStart.Size = new System.Drawing.Size(189, 37);
            this.btnGoToStart.TabIndex = 72;
            this.btnGoToStart.Text = "Go To Start";
            this.btnGoToStart.UseVisualStyleBackColor = true;
            this.btnGoToStart.Click += new System.EventHandler(this.btnGoToStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(1316, 34);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(189, 37);
            this.btnStop.TabIndex = 68;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnSlice
            // 
            this.btnSlice.Location = new System.Drawing.Point(965, 34);
            this.btnSlice.Name = "btnSlice";
            this.btnSlice.Size = new System.Drawing.Size(189, 37);
            this.btnSlice.TabIndex = 71;
            this.btnSlice.Text = "Slice";
            this.btnSlice.UseVisualStyleBackColor = true;
            this.btnSlice.Click += new System.EventHandler(this.btnSlice_Click);
            // 
            // tbBladeID
            // 
            this.tbBladeID.Location = new System.Drawing.Point(138, 3);
            this.tbBladeID.Name = "tbBladeID";
            this.tbBladeID.Size = new System.Drawing.Size(366, 41);
            this.tbBladeID.TabIndex = 70;
            // 
            // lblBladeID
            // 
            this.lblBladeID.AutoSize = true;
            this.lblBladeID.Location = new System.Drawing.Point(3, 3);
            this.lblBladeID.Name = "lblBladeID";
            this.lblBladeID.Size = new System.Drawing.Size(120, 33);
            this.lblBladeID.TabIndex = 69;
            this.lblBladeID.Text = "Blade ID";
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(557, 84);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(189, 37);
            this.btnDown.TabIndex = 66;
            this.btnDown.Text = "Down";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnDown_Click);
            this.btnDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Stop);
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(557, 34);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(189, 37);
            this.btnUp.TabIndex = 65;
            this.btnUp.Text = "Up";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnUp_Click);
            this.btnUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Stop);
            // 
            // tbPageZ
            // 
            this.tbPageZ.Controls.Add(this.btnSetPushStart);
            this.tbPageZ.Controls.Add(this.btnZPose);
            this.tbPageZ.Controls.Add(this.btnStop2);
            this.tbPageZ.Controls.Add(this.btnDown2);
            this.tbPageZ.Controls.Add(this.btnUp2);
            this.tbPageZ.Controls.Add(this.btnZmin);
            this.tbPageZ.Controls.Add(this.tbZEnd);
            this.tbPageZ.Controls.Add(this.label4);
            this.tbPageZ.Location = new System.Drawing.Point(4, 25);
            this.tbPageZ.Name = "tbPageZ";
            this.tbPageZ.Size = new System.Drawing.Size(1592, 164);
            this.tbPageZ.TabIndex = 1;
            this.tbPageZ.Text = "Z-Calibrate";
            this.tbPageZ.UseVisualStyleBackColor = true;
            // 
            // btnSetPushStart
            // 
            this.btnSetPushStart.Location = new System.Drawing.Point(425, 101);
            this.btnSetPushStart.Name = "btnSetPushStart";
            this.btnSetPushStart.Size = new System.Drawing.Size(225, 37);
            this.btnSetPushStart.TabIndex = 75;
            this.btnSetPushStart.Text = "Set Push Start";
            this.btnSetPushStart.UseVisualStyleBackColor = true;
            this.btnSetPushStart.Click += new System.EventHandler(this.btnSetPushStart_Click);
            // 
            // btnZPose
            // 
            this.btnZPose.Location = new System.Drawing.Point(9, 55);
            this.btnZPose.Name = "btnZPose";
            this.btnZPose.Size = new System.Drawing.Size(189, 37);
            this.btnZPose.TabIndex = 74;
            this.btnZPose.Text = "Z Pose";
            this.btnZPose.UseVisualStyleBackColor = true;
            this.btnZPose.Click += new System.EventHandler(this.btnZPose_Click);
            // 
            // btnStop2
            // 
            this.btnStop2.Location = new System.Drawing.Point(210, 101);
            this.btnStop2.Name = "btnStop2";
            this.btnStop2.Size = new System.Drawing.Size(189, 37);
            this.btnStop2.TabIndex = 73;
            this.btnStop2.Text = "Stop";
            this.btnStop2.UseVisualStyleBackColor = true;
            this.btnStop2.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnDown2
            // 
            this.btnDown2.Location = new System.Drawing.Point(210, 55);
            this.btnDown2.Name = "btnDown2";
            this.btnDown2.Size = new System.Drawing.Size(189, 37);
            this.btnDown2.TabIndex = 72;
            this.btnDown2.Text = "Down";
            this.btnDown2.UseVisualStyleBackColor = true;
            this.btnDown2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnDown_Click);
            this.btnDown2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Stop);
            // 
            // btnUp2
            // 
            this.btnUp2.Location = new System.Drawing.Point(210, 5);
            this.btnUp2.Name = "btnUp2";
            this.btnUp2.Size = new System.Drawing.Size(189, 37);
            this.btnUp2.TabIndex = 71;
            this.btnUp2.Text = "Up";
            this.btnUp2.UseVisualStyleBackColor = true;
            this.btnUp2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnUp_Click);
            this.btnUp2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Stop);
            // 
            // btnZmin
            // 
            this.btnZmin.Location = new System.Drawing.Point(426, 5);
            this.btnZmin.Name = "btnZmin";
            this.btnZmin.Size = new System.Drawing.Size(225, 37);
            this.btnZmin.TabIndex = 70;
            this.btnZmin.Text = "Find Z-Min";
            this.btnZmin.UseVisualStyleBackColor = true;
            this.btnZmin.Click += new System.EventHandler(this.btnZmin_Click);
            // 
            // tbZEnd
            // 
            this.tbZEnd.Location = new System.Drawing.Point(521, 55);
            this.tbZEnd.Name = "tbZEnd";
            this.tbZEnd.Size = new System.Drawing.Size(130, 41);
            this.tbZEnd.TabIndex = 69;
            this.tbZEnd.Leave += new System.EventHandler(this.tbZEnd_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(419, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 33);
            this.label4.TabIndex = 68;
            this.label4.Text = "Z-End";
            // 
            // pnlChart
            // 
            this.pnlChart.Controls.Add(this.chart2);
            this.pnlChart.Controls.Add(this.lblPowerLive);
            this.pnlChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChart.Location = new System.Drawing.Point(0, 58);
            this.pnlChart.Name = "pnlChart";
            this.pnlChart.Padding = new System.Windows.Forms.Padding(0, 0, 0, 216);
            this.pnlChart.Size = new System.Drawing.Size(2148, 558);
            this.pnlChart.TabIndex = 73;
            // 
            // chart2
            // 
            this.chart2.BackColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisX.Title = "Position";
            chartArea1.AxisY.Title = "Force (g)";
            chartArea1.BackColor = System.Drawing.Color.Gray;
            chartArea1.Name = "ChartArea1";
            chartArea1.Position.Auto = false;
            chartArea1.Position.Height = 82.54519F;
            chartArea1.Position.Width = 100F;
            chartArea1.Position.Y = 3F;
            this.chart2.ChartAreas.Add(chartArea1);
            this.chart2.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.Name = "Legend1";
            this.chart2.Legends.Add(legend1);
            this.chart2.Location = new System.Drawing.Point(0, 0);
            this.chart2.Name = "chart2";
            this.chart2.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series1.BorderWidth = 5;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart2.Series.Add(series1);
            this.chart2.Size = new System.Drawing.Size(2148, 342);
            this.chart2.TabIndex = 67;
            this.chart2.Text = "chart2";
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.btnTare);
            this.pnlTop.Controls.Add(this.comboBoxCOMPortScale);
            this.pnlTop.Controls.Add(this.btnConnectScale);
            this.pnlTop.Controls.Add(this.lblForce);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(2148, 58);
            this.pnlTop.TabIndex = 72;
            // 
            // btnTare
            // 
            this.btnTare.Enabled = false;
            this.btnTare.Location = new System.Drawing.Point(701, 7);
            this.btnTare.Name = "btnTare";
            this.btnTare.Size = new System.Drawing.Size(160, 39);
            this.btnTare.TabIndex = 66;
            this.btnTare.Text = "Tare";
            this.btnTare.UseVisualStyleBackColor = true;
            this.btnTare.Click += new System.EventHandler(this.btnTare_Click);
            // 
            // VisibleColumn
            // 
            this.VisibleColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.VisibleColumn.DataPropertyName = "Visible";
            this.VisibleColumn.FalseValue = false;
            this.VisibleColumn.HeaderText = "";
            this.VisibleColumn.MinimumWidth = 6;
            this.VisibleColumn.Name = "VisibleColumn";
            this.VisibleColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.VisibleColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.VisibleColumn.TrueValue = true;
            this.VisibleColumn.Width = 125;
            // 
            // dgButtonDelete
            // 
            this.dgButtonDelete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dgButtonDelete.HeaderText = "Delete";
            this.dgButtonDelete.MinimumWidth = 6;
            this.dgButtonDelete.Name = "dgButtonDelete";
            this.dgButtonDelete.Width = 125;
            // 
            // pointsDataGridViewTextBoxColumn
            // 
            this.pointsDataGridViewTextBoxColumn.DataPropertyName = "Points";
            this.pointsDataGridViewTextBoxColumn.HeaderText = "Points";
            this.pointsDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.pointsDataGridViewTextBoxColumn.Name = "pointsDataGridViewTextBoxColumn";
            this.pointsDataGridViewTextBoxColumn.ReadOnly = true;
            this.pointsDataGridViewTextBoxColumn.Width = 1615;
            // 
            // visibleDataGridViewCheckBoxColumn
            // 
            this.visibleDataGridViewCheckBoxColumn.DataPropertyName = "Visible";
            this.visibleDataGridViewCheckBoxColumn.HeaderText = "Visible";
            this.visibleDataGridViewCheckBoxColumn.MinimumWidth = 6;
            this.visibleDataGridViewCheckBoxColumn.Name = "visibleDataGridViewCheckBoxColumn";
            this.visibleDataGridViewCheckBoxColumn.Width = 284;
            // 
            // bladeIDDataGridViewTextBoxColumn
            // 
            this.bladeIDDataGridViewTextBoxColumn.DataPropertyName = "Blade ID";
            this.bladeIDDataGridViewTextBoxColumn.HeaderText = "Blade ID";
            this.bladeIDDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.bladeIDDataGridViewTextBoxColumn.Name = "bladeIDDataGridViewTextBoxColumn";
            this.bladeIDDataGridViewTextBoxColumn.Width = 284;
            // 
            // sampleDataGridViewTextBoxColumn
            // 
            this.sampleDataGridViewTextBoxColumn.DataPropertyName = "Sample";
            this.sampleDataGridViewTextBoxColumn.HeaderText = "Sample";
            this.sampleDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.sampleDataGridViewTextBoxColumn.Name = "sampleDataGridViewTextBoxColumn";
            this.sampleDataGridViewTextBoxColumn.Width = 284;
            // 
            // bgwZHome
            // 
            this.bgwZHome.WorkerSupportsCancellation = true;
            this.bgwZHome.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwZHome_DoWork);
            // 
            // tmrGatherData
            // 
            this.tmrGatherData.Interval = 50;
            this.tmrGatherData.Tick += new System.EventHandler(this.tmrGatherData_Tick);
            // 
            // tmrAutoSave
            // 
            this.tmrAutoSave.Interval = 300000;
            this.tmrAutoSave.Tick += new System.EventHandler(this.tmrAutoSave_Tick);
            // 
            // QKPRobotGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2148, 1025);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Helvetica", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "QKPRobotGUI";
            this.Text = "Choppenheimer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgSummary)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnlSweepControls.ResumeLayout(false);
            this.tbPageZCalibrate.ResumeLayout(false);
            this.tabPageCutTest.ResumeLayout(false);
            this.tabPageCutTest.PerformLayout();
            this.tbPageZ.ResumeLayout(false);
            this.tbPageZ.PerformLayout();
            this.pnlChart.ResumeLayout(false);
            this.pnlChart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Timer tmrUpdate;
        private System.ComponentModel.BackgroundWorker bgwRunCutTest;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private DataGridView dgSummary;
        private Label label1;
        private Label label3;
        private TextBox tbSample;
        private TextBox tbTrialNum;
        private TextBox tbSetFrequency;
        private Label label6;
        private Button btnSetFrequency;
        private Button btnConnectScale;
        private ComboBox comboBoxCOMPortScale;
        private Label lblPowerLive;
        private Label lblForce;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private SplitContainer splitContainer1;
        private FlowLayoutPanel pnlSweepControls;
        private Panel pnlChart;
        private Panel pnlTop;
        private TabControl tbPageZCalibrate;
        private TabPage tabPageCutTest;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem dataToolStripMenuItem;
        private ToolStripMenuItem clearToolStripMenuItem;
        private DataGridViewCheckBoxColumn VisibleColumn;
        private DataGridViewButtonColumn dgButtonDelete;
        private DataGridViewTextBoxColumn pointsDataGridViewTextBoxColumn;
        private DataGridViewCheckBoxColumn visibleDataGridViewCheckBoxColumn;
        private DataGridViewTextBoxColumn bladeIDDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn sampleDataGridViewTextBoxColumn;
        private Button btnUp;
        private Button btnDown;
        private Button btnStop;
        private ToolStripMenuItem saveToolStripMenuItem;
        private TextBox tbBladeID;
        private Label lblBladeID;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private Button btnSlice;
        private System.ComponentModel.BackgroundWorker bgwZHome;
        private Button btnTare;
        private TabPage tbPageZ;
        private Button btnStop2;
        private Button btnDown2;
        private Button btnUp2;
        private Button btnZmin;
        private TextBox tbZEnd;
        private Label label4;
        private Button btnGoToStart;
        private Timer tmrGatherData;
        private Button btnPushCut;
        private Timer tmrAutoSave;
        private Button btnZPose;
        private Button btnPushCutGoToStart;
        private Button btnSetPushStart;
    }
}

