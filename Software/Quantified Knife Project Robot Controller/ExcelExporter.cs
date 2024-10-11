using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QKPRobot
{
    public class ExcelExporter
    {
        public void ExportDataTablesToExcel(string filePath, params DataTable[] tables)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    sw.WriteLine("<?xml version=\"1.0\"?>");
                    sw.WriteLine("<?mso-application progid=\"Excel.Sheet\"?>");
                    sw.WriteLine("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"");
                    sw.WriteLine(" xmlns:o=\"urn:schemas-microsoft-com:office:office\"");
                    sw.WriteLine(" xmlns:x=\"urn:schemas-microsoft-com:office:excel\"");
                    sw.WriteLine(" xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\"");
                    sw.WriteLine(" xmlns:html=\"http://www.w3.org/TR/REC-html40\">");
                    sw.WriteLine(" <Styles>");
                    sw.WriteLine("  <Style ss:ID=\"1\">");
                    sw.WriteLine("   <Font ss:Bold=\"1\"/>");
                    sw.WriteLine("  </Style>");
                    sw.WriteLine(" </Styles>");

                    foreach (DataTable table in tables)
                    {
                        sw.WriteLine($" <Worksheet ss:Name=\"{SecurityElement.Escape(table.TableName)}\">");
                        WriteDataTableToExcelXml(sw, table);
                        sw.WriteLine(" </Worksheet>");
                    }

                    sw.WriteLine("</Workbook>");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving File. " + ex.Message);
            }
           
        }

        private void WriteDataTableToExcelXml(StreamWriter sw, DataTable dt)
        {
            sw.WriteLine("  <Table>");
            // Write header
            sw.WriteLine("   <Row>");
            foreach (DataColumn col in dt.Columns)
            {
                sw.WriteLine($"    <Cell ss:StyleID=\"1\"><Data ss:Type=\"String\">{SecurityElement.Escape(col.ColumnName)}</Data></Cell>");
            }
            sw.WriteLine("   </Row>");

            // Write data
            foreach (DataRow row in dt.Rows)
            {
                sw.WriteLine("   <Row>");
                foreach (DataColumn col in dt.Columns)
                {
                    string type = GetExcelDataType(col.DataType);
                    if (type != "Boolean")
                    {
                        string data = SecurityElement.Escape(row[col].ToString());                    
                        sw.WriteLine($"    <Cell><Data ss:Type=\"{type}\">{data}</Data></Cell>");
                    }
                    else
                    {
                        //string data = SecurityElement.Escape(row[col].ToString());
                        sw.WriteLine($"    <Cell><Data ss:Type=\"{type}\"></Data></Cell>");
                    }
                    
                }
                sw.WriteLine("   </Row>");
            }
            sw.WriteLine("  </Table>");
        }

        private string GetExcelDataType(Type dataType)
        {
            if (dataType == typeof(int) || dataType == typeof(decimal) || dataType == typeof(double) ||
                dataType == typeof(long) || dataType == typeof(short) || dataType == typeof(float))
                return "Number";
            else if (dataType == typeof(DateTime))
                return "DateTime";
            else if (dataType == typeof(bool))
                return "Boolean";
            else
                return "String";
        }
    }
}