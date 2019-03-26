using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace EhkBackend.ui.Common
{
    public static class imExpro
    {

        public static DataTable GetExcelDataTable(string filepath)
        {
            IWorkbook workbook;
            DataTable table = new DataTable();
            try
            {
                using (FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
                {
                    string fileExt = Path.GetExtension(filepath).ToLower();
                    if (fileExt == ".xls")
                    {

                        workbook = new HSSFWorkbook(fileStream);

                    }
                    else if (fileExt == ".xlsx")
                    {

                        workbook = new XSSFWorkbook(fileStream);
                    }
                    else
                    {
                        workbook = null;
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            //定位在第一个sheet
            ISheet sheet = workbook.GetSheetAt(0);
            //第一个为标题行
            IRow headerRow = sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;
            int rowCount = sheet.LastRowNum;

            //循环添加标题列
            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }


            //数据

            for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
            {

                IRow row = sheet.GetRow(i);
                DataRow dataRow = table.NewRow();
                if (row != null)
                {
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                        {
                            dataRow[j] = GetCellValue(row.GetCell(j));
                        }
                    }
                }
                table.Rows.Add(dataRow);
            }
            return table;
        }

        private static object GetCellValue(ICell cell)
        {
            if (cell == null)
            {
                return string.Empty;
            }
            switch (cell.CellType)
            {

                case CellType.Blank:
                    return string.Empty;
                case CellType.Boolean:
                    return cell.BooleanCellValue.ToString();
                case CellType.Error:
                    return cell.ErrorCellValue.ToString();
                case CellType.Numeric:
                case CellType.Unknown:
                default:
                    return cell.ToString();
                case CellType.String:
                    return cell.StringCellValue;
                //case cell.StringCellValue:
                case CellType.Formula:
                    try
                    {
                        HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(cell.Sheet.Workbook);
                        e.EvaluateInCell(cell);
                        return cell.ToString();

                    }
                    catch
                    {
                        return cell.NumericCellValue.ToString();
                    }

            }
            //throw new NotImplementedException();
        }
    }
}