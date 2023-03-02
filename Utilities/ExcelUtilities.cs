using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Utilities
{
    public class ExcelUtilities
    {
        #region Constructors

        public ExcelUtilities()
        {
            ParameterData = new Dictionary<string, object>();
        }

        #endregion

        #region Properties

        public byte[] TemplateFileData { get; set; }
        public ConfigInfo ConfigInfo { get; set; }

        public IDictionary<string, object> ParameterData { get; set; }
        public byte[] OutputData { get; set; }

        #endregion

        public byte[] Export<T>(IList<T> entities) where T : class
        {
            return Export(entities, null);
        }

        public byte[] Export<T>(IList<T> entities, SheetInfo sheetInfo, ExcelRange excelRange) where T : class
        {
            //1. Read Config
            ReadConfig(sheetInfo);

            //2. Fill Paremter
            FillParameter(sheetInfo);

            //3. Export
            FillData(entities, sheetInfo);

            using(ExcelPackage excelPackage = new ExcelPackage())
            {
                //Open Excel + Get WorkSheet
                using (var memoryStream = new MemoryStream(OutputData))
                {
                    excelPackage.Load(memoryStream);
                }
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.First();


                //create a new piechart of type Line
                ExcelLineChart lineChart = worksheet.Drawings.AddChart("lineChart", eChartType.Line) as ExcelLineChart;

                //set the title
                lineChart.Title.Text = "LineChart Example";

                //create the ranges for the chart
                var rangeLabel = worksheet.Cells["A5:B5"];
                var range1 = worksheet.Cells["A6:B6"];
                //var range2 = worksheet.Cells["B3:K3"];

                //add the ranges to the chart
                lineChart.Series.Add(range1, rangeLabel);
                //lineChart.Series.Add(range2, rangeLabel);

                //set the names of the legend
                lineChart.Series[0].Header = worksheet.Cells["A6"].Value.ToString();
                //lineChart.Series[1].Header = worksheet.Cells["B5"].Value.ToString();

                //position of the legend
                lineChart.Legend.Position = eLegendPosition.Right;

                //size of the chart
                lineChart.SetSize(600, 300);

                //add the chart at cell B6
                lineChart.SetPosition(8, 0, 0, 0);



                OutputData = excelPackage.GetAsByteArray();
            }

            return OutputData;
        }

        public byte[] Export<T>(IList<T> entities, SheetInfo sheetInfo) where T : class
        {
            //1. Read Config
            ReadConfig(sheetInfo);

            //2. Fill Paremter
            FillParameter(sheetInfo);

            //3. Export
            FillData(entities, sheetInfo);
            
            return OutputData;
        }

        private void ReadConfig(SheetInfo sheetInfo)
        {
            ConfigInfo = new ConfigInfo();

            //1. Read data file
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                //Open Excel + Get WorkSheet
                using (var memoryStream = new MemoryStream(TemplateFileData))
                {
                    excelPackage.Load(memoryStream);
                }

                //Get Worksheet
                ExcelWorksheet excelWorksheet = GetWorkSheet(excelPackage, sheetInfo);
                if (excelWorksheet == null)
                    excelWorksheet = excelPackage.Workbook.Worksheets.First();

                //Get Config
                var dimension = excelWorksheet.Dimension;
                var cells = excelWorksheet.Cells;
                for (int rowIndex = 1; rowIndex <= dimension.Rows; rowIndex++)
                {
                    for (int columnIndex = 1; columnIndex <= dimension.Columns; columnIndex++)
                    {
                        var cell = cells[rowIndex, columnIndex];
                        string text = cell.Text;

                        var fieldInfo = ParseConfig(text);
                        if (fieldInfo != null)
                        {
                            fieldInfo.ExcelAddress = cell.Address;
                            fieldInfo.ExcelRow = rowIndex;
                            fieldInfo.ExcelColumn = columnIndex;
                            ConfigInfo.Fields.Add(fieldInfo);
                        }
                    }
                }

                TemplateFileData = excelPackage.GetAsByteArray();
            }
        }

        private void FillParameter(SheetInfo sheetInfo)
        {
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                using (var memoryStream = new MemoryStream(TemplateFileData))
                {
                    excelPackage.Load(memoryStream);
                }

                //Get Worksheet
                ExcelWorksheet excelWorksheet = GetWorkSheet(excelPackage, sheetInfo);
                if (excelWorksheet == null)
                    excelWorksheet = excelPackage.Workbook.Worksheets.First();

                using (var cells = excelWorksheet.Cells)
                {
                    FieldInfo[] fieldInfos = ConfigInfo.Fields.Where(f => f.Type == KeyType_Parameter).ToArray();
                    foreach (var fieldInfo in fieldInfos)
                    {
                        object value = string.Empty;
                        if (ParameterData.TryGetValue(fieldInfo.Name, out value))
                        {
                            using (var cell = cells[fieldInfo.ExcelAddress])
                            {
                                cell.Value = value;
                            }
                        }
                    }
                }

                OutputData = excelPackage.GetAsByteArray();
            };
        }

        private void FillData<T>(IList<T> entities) where T : class
        {
            FillData(entities, null);
        }

        private void FillData<T>(IList<T> entities, SheetInfo sheetInfo) where T : class
        {
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                using (var memoryStream = new MemoryStream(OutputData))
                {
                    excelPackage.Load(memoryStream);
                }

                //Get Worksheet
                ExcelWorksheet excelWorksheet = GetWorkSheet(excelPackage, sheetInfo);
                if (excelWorksheet == null)
                    excelWorksheet = excelPackage.Workbook.Worksheets.First();

                using (var cells = excelWorksheet.Cells)
                {
                    FieldInfo[] fieldInfos = ConfigInfo.Fields.Where(f => f.Type == KeyType_Field).ToArray();
                    if (fieldInfos.Length > 0)
                    {
                        int rowBeginIndex = fieldInfos.FirstOrDefault().ExcelRow;
                        //Insert Zone
                        excelWorksheet.InsertRow(rowBeginIndex + 1, entities.Count - 1, rowBeginIndex);

                        //Fill
                        int rowIndex = rowBeginIndex;
                        foreach (var entity in entities)
                        {
                            foreach (var fieldInfo in fieldInfos)
                            {
                                var value = ReflectionUtilities.FollowPropertyPath(entity, fieldInfo.Name);
                                cells[rowIndex, fieldInfo.ExcelColumn].Value = value;
                            }
                            rowIndex++;
                        }
                    }
                }

                OutputData = excelPackage.GetAsByteArray();
            };
        }

        private ExcelWorksheet GetWorkSheet(ExcelPackage excelPackage, SheetInfo sheetInfo)
        {
            ExcelWorksheet excelWorksheet = null;
            if (sheetInfo != null)
                excelWorksheet = sheetInfo.SheetIndex > 0 ? excelPackage.Workbook.Worksheets[sheetInfo.SheetIndex] : excelPackage.Workbook.Worksheets[sheetInfo.SheetName];
            else
                excelWorksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
            return excelWorksheet;
        }

        protected FieldInfo ParseConfig(string text)
        {
            FieldInfo fieldInfo = null;

            if (text.Contains(Key_Start) && text.Contains(Key_End))
            {
                string textNoKey = text.Replace(Key_Start, string.Empty).Replace(Key_End, string.Empty);
                string[] textNoKeyParts = textNoKey.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                if (textNoKeyParts.Length == 2)
                {
                    fieldInfo = new FieldInfo()
                    {
                        Type = textNoKeyParts[0],
                        Name = textNoKeyParts[1]
                    };
                }
            }
            else
                fieldInfo = null;

            return fieldInfo;
        }

        #region Constants
        public const string Key_Start = "[[%";
        public const string Key_End = "%]]";
        public const string Key_Seperator = ":";
        public const string KeyType_Parameter = "Parameter";
        public const string KeyType_Field = "Field";
        #endregion
    }

    public class FieldInfo
    {
        public string Name { get; set; }
        public string ExcelAddress { get; set; }
        public int ExcelRow { get; set; }
        public int ExcelColumn { get; set; }
        public string Type { get; set; }
    }

    public class ConfigInfo
    {
        public ConfigInfo()
        {
            Fields = new List<FieldInfo>();
        }
        public IList<FieldInfo> Fields { get; set; }
    }

    public class SheetInfo
    {
        public string SheetName { get; set; }
        public int SheetIndex { get; set; }
    }
}
