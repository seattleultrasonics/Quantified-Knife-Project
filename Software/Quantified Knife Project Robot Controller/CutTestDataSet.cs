using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace QKPRobot
{
    internal class CutTestDataSet
    {
        public DataTable CutTestRawData;
        public DataTable CutTestSummaryData;
        public DataSet CutTestDS;
        public string _filePath;
        public CutTestDataSet()
        {

            InitDataTables();
        }

        private void InitDataTables()
        {
            CutTestRawData = new DataTable("CutTestRawData");

            // Define columns
            DataColumn rowIDColumn = new DataColumn("RowID", typeof(int));
            rowIDColumn.AutoIncrement = true;
            rowIDColumn.AutoIncrementSeed = 1;
            rowIDColumn.AutoIncrementStep = 1;

            CutTestRawData.Columns.Add(rowIDColumn);
            CutTestRawData.Columns.Add("BladeID", typeof(string));
            CutTestRawData.Columns.Add("Sample", typeof(string));
            CutTestRawData.Columns.Add("Trial", typeof(int));
            CutTestRawData.Columns.Add("PosX", typeof(double));
            CutTestRawData.Columns.Add("PosY", typeof(double));
            CutTestRawData.Columns.Add("PosZ", typeof(double));
            CutTestRawData.Columns.Add("Pitch", typeof(double));
            CutTestRawData.Columns.Add("Roll", typeof(double));
            CutTestRawData.Columns.Add("Yaw", typeof(double));
            CutTestRawData.Columns.Add("TimeElapsedMS", typeof(int));
            CutTestRawData.Columns.Add("Force", typeof(double));
            CutTestRawData.Columns.Add("SeriesName", typeof(string));

            // Set the primary key
            CutTestRawData.PrimaryKey = new DataColumn[] { rowIDColumn };

            CutTestRawData.RowChanged += new DataRowChangeEventHandler(OnRowChanged);
            CutTestRawData.RowDeleted += new DataRowChangeEventHandler(OnRowChanged);

            //Summary data table

            CutTestDS = new DataSet();
            CutTestDS.Tables.Add(CutTestRawData);
            CreateSummaryTable();

        }

        private void CreateSummaryTable()
        {
            CutTestSummaryData = new DataTable("CutTestSummaryData");
            CutTestSummaryData.Columns.Add("BladeID", typeof(string));
            CutTestSummaryData.Columns.Add("Sample", typeof(string));
            CutTestSummaryData.Columns.Add("Trial", typeof(int));
            CutTestSummaryData.Columns.Add("Max Force", typeof(double));
            CutTestSummaryData.Columns.Add("Average Force", typeof(double));
            CutTestSummaryData.Columns.Add("Total Force", typeof(double));
            CutTestDS.Tables.Add(CutTestSummaryData);

        }

        public void SaveAs(string filePath)
        {
            if (filePath != null)
            {
                
                ExcelExporter excelExporter = new ExcelExporter();
                excelExporter.ExportDataTablesToExcel(filePath, CutTestSummaryData, CutTestRawData);
                Console.WriteLine("File saved to " + filePath);
                _filePath = filePath;
            }
            else
            {
                Console.WriteLine("Save operation was canceled.");
            }
        }

        public void Save()
        {
            if (_filePath != null)
            {
                SaveAs(_filePath);
            }
        }

        public void SaveDataSetToMemory()
        {

            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new XmlTextWriter(memoryStream, Encoding.UTF8))
                {
                    CutTestDS.WriteXml(writer);
                    string serializedDataSet = Encoding.UTF8.GetString(memoryStream.ToArray());
                    Properties.Settings.Default.DataSetStorage = serializedDataSet;
                    Properties.Settings.Default.Save();
                }
            }
        }

        public void LoadDataSetFromMemory()
        {
            DataSet dataSet = new DataSet();
            string serializedDataSet = Properties.Settings.Default.DataSetStorage;
            if (!string.IsNullOrEmpty(serializedDataSet))
            {
                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(serializedDataSet)))
                {
                    dataSet.ReadXml(stream);
                }
            }
            if (dataSet.Tables.Count > 0)
            {
                DataColumn rowIDColumn = CutTestRawData.Columns["RowID"];

                int maxRowID = 0;
                foreach (DataRow row in CutTestRawData.Rows)
                {
                    int rowID = Convert.ToInt32(row["RowID"]);
                    if (rowID > maxRowID)
                    {
                        maxRowID = rowID;
                    }
                }
                rowIDColumn.AutoIncrement = true;
                rowIDColumn.AutoIncrementSeed = maxRowID + 1;
                rowIDColumn.AutoIncrementStep = 1;
                CutTestRawData.PrimaryKey = new DataColumn[] { rowIDColumn };

                CutTestDS.Tables["CutTestRawData"].Columns["RowID"].DataType = typeof(int);
                /*
                if (!dataSet.Tables["CutTestRawData"].Columns.Contains("Trial"))
                {
                    dataSet.Tables["CutTestRawData"].Columns.Add("Trial", typeof(int));
                }

                if (!dataSet.Tables["CutTestRawData"].Columns.Contains("Force"))
                {
                    dataSet.Tables["CutTestRawData"].Columns.Add("Force", typeof(double));
                }
                */
                this.CutTestDS = dataSet;
                this.CutTestRawData = CutTestDS.Tables["CutTestRawData"];
                this.CutTestSummaryData = CutTestDS.Tables["CutTestSummaryData"];

                /*
                this.CutTestRawData.Columns.Add("SeriesName", typeof(string));
                foreach (DataRow row in this.CutTestRawData.Rows)
                {

                    row["SeriesName"] = $"{row["BladeID"]}-{row["Sample"]}-{row["Trial"]}";
                }
                */

            }
            if (CutTestSummaryData == null)
            {
                CreateSummaryTable();
            }

            this.UpdateSummaryTable();

        }

        public void ClearData()
        {
            CutTestRawData.Rows.Clear();
            CutTestSummaryData.Rows.Clear();

        }

        private void OnRowChanged(object sender, DataRowChangeEventArgs e)
        {
            // Raise an event or call a method to update the chart
            RowChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler RowChanged;

        public void UpdateSummaryTable()
        {
            if (CutTestSummaryData != null)
            {
                // Clear the existing summary data
                CutTestSummaryData.Rows.Clear();
            }

            
                // Group the data and calculate summary statistics
                var summaryData = from row in CutTestRawData.AsEnumerable()
                                  group row by new
                                  {
                                      BladeID = row["BladeID"] as string,
                                      Sample = row["Sample"] as string,
                                      Trial = Convert.ToInt32(row["Trial"])
                                  } into g
                                  select new
                                  {
                                      BladeID = g.Key.BladeID,
                                      Sample = g.Key.Sample,
                                      Trial = g.Key.Trial,
                                      MaxForce = g.Max(r => Convert.ToDouble(r["Force"])),
                                      AverageForce = g.Average(r => Convert.ToDouble(r["Force"])),
                                      TotalForce = g.Sum(r => Convert.ToDouble(r["Force"])),
                                  };
           

            // Add the summary data to the summary table
            foreach (var item in summaryData)
            {
                DataRow summaryRow = CutTestSummaryData.NewRow();
                summaryRow["BladeID"] = item.BladeID;
                summaryRow["Sample"] = item.Sample;
                summaryRow["Trial"] = item.Trial;
                summaryRow["Max Force"] = item.MaxForce;
                summaryRow["Average Force"] = item.AverageForce;
                summaryRow["Total Force"] = item.TotalForce;
                CutTestSummaryData.Rows.Add(summaryRow);
            }
            
        }

        public void DeleteSeries(string BladeID, string Sample, string Trial)
        {

            // Find and delete corresponding rows in CutTestRawData
            var rowsToDelete = CutTestRawData.AsEnumerable()
                .Where(row => row.Field<string>("BladeID") == BladeID &&
                              row.Field<string>("Sample") == Sample &&
                              row.Field<string>("Trial") == Trial)
                .ToList();
            
            foreach (var row in rowsToDelete)
            {
                CutTestRawData.Rows.Remove(row);
            }
            CutTestRawData.AcceptChanges();

            //UpdateSummaryTable();

        }
    }
}
