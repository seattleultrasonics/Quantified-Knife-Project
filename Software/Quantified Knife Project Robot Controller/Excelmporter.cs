using System;
using System.Data;
using System.Xml;

namespace QKPRobot
{
    public class ExcelImporter
    {
        public void ImportDataTablesFromExcel(string filePath, DataTable dtCutTestRawData, DataTable dtCutTestSummaryData)
        {
            try
            {
                using (XmlReader reader = XmlReader.Create(filePath))
                {
                    DataTable currentTable = null;
                    bool isHeaderRow = true;
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            switch (reader.Name)
                            {
                                case "Worksheet":
                                    string tableName = reader.GetAttribute("ss:Name");
                                    if (tableName == "CutTestRawData")
                                    {
                                        currentTable = dtCutTestRawData;
                                    }
                                    else if (tableName == "CutTestSummaryData")
                                    {
                                        currentTable = dtCutTestSummaryData;
                                    }
                                    isHeaderRow = true; // Reset for each worksheet
                                    break;
                                case "Row":
                                    if (currentTable != null)
                                    {
                                        if (isHeaderRow)
                                        {
                                            isHeaderRow = false;
                                        }
                                        else
                                        {
                                            DataRow row = currentTable.NewRow();
                                            for (int i = 0; i < currentTable.Columns.Count; i++)
                                            {
                                                if (reader.ReadToFollowing("Cell"))
                                                {
                                                    if (reader.ReadToDescendant("Data"))
                                                    {
                                                        string cellValue = reader.ReadElementContentAsString();
                                                        row[i] = ConvertData(cellValue, currentTable.Columns[i].DataType, currentTable.Columns[i].AllowDBNull);
                                                    }
                                                }
                                            }
                                            currentTable.Rows.Add(row);
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Importing File. " + ex.Message);
            }
        }


        private object ConvertData(string value, Type dataType, bool allowDBNull)
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                {
                    if (allowDBNull)
                        return DBNull.Value;

                    // Provide default values for non-nullable types
                    if (dataType == typeof(int))
                        return 0;
                    else if (dataType == typeof(double))
                        return 0.0;
                    else if (dataType == typeof(decimal))
                        return 0.0m;
                    else if (dataType == typeof(DateTime))
                        return DateTime.MinValue;
                    else if (dataType == typeof(bool))
                        return false;
                    else
                        return string.Empty; // Default for string type
                }

                if (dataType == typeof(int))
                    return int.Parse(value);
                else if (dataType == typeof(double))
                    return double.Parse(value);
                else if (dataType == typeof(decimal))
                    return decimal.Parse(value);
                else if (dataType == typeof(DateTime))
                    return DateTime.Parse(value);
                else if (dataType == typeof(bool))
                    return bool.Parse(value);
                else
                    return value; // Default to string
            }
            catch
            {
                if (allowDBNull)
                    return DBNull.Value;

                // Provide default values for non-nullable types in case of conversion error
                if (dataType == typeof(int))
                    return 0;
                else if (dataType == typeof(double))
                    return 0.0;
                else if (dataType == typeof(decimal))
                    return 0.0m;
                else if (dataType == typeof(DateTime))
                    return DateTime.MinValue;
                else if (dataType == typeof(bool))
                    return false;
                else
                    return string.Empty; // Default for string type
            }
        }

    }
}
