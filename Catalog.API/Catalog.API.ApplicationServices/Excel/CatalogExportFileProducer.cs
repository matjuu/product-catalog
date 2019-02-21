using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.API.Domain.Aggregates;
using OfficeOpenXml;

namespace Catalog.API.ApplicationServices.Excel
{
    public class CatalogExportFileProducer : ICatalogExportFileProducer
    {
        public async Task<byte[]> Produce(List<Product> products)
        {
            using (var excelPackage = new ExcelPackage())
            {
                var worksheet = excelPackage.Workbook.Worksheets.Add("Catalog");
                AddHeadings(worksheet);
                Configure(worksheet);
                WriteRows(products, worksheet);

                return excelPackage.GetAsByteArray();
            }
        }

        private void WriteRows(List<Product> products, ExcelWorksheet worksheet)
        {
            const int startingRowOffset = 2;
            for (var index = 0; index < products.Count; index++)
            {
                var row = index + startingRowOffset;
                WriteRow(worksheet, row, products[index]);
            }
        }

        private void WriteRow(ExcelWorksheet worksheet, int row, Product product)
        {
            worksheet.Cells[row, 1].Value = product.Id;
            worksheet.Cells[row, 2].Value = product.Name;
            worksheet.Cells[row, 3].Value = product.Code;
            worksheet.Cells[row, 4].Value = product.Price;
            worksheet.Cells[row, 5].Value = product.PriceApproved;
            worksheet.Cells[row, 6].Value = product.LastUpdated.ToLongDateString();
            worksheet.Cells[row, 7].Value = product.Image;
        }

        private void AddHeadings(ExcelWorksheet worksheet)
        {         
            worksheet.Cells[1, 1].Value = nameof(Product.Id);
            worksheet.Cells[1, 2].Value = nameof(Product.Name);
            worksheet.Cells[1, 3].Value = nameof(Product.Code);
            worksheet.Cells[1, 4].Value = nameof(Product.Price);
            worksheet.Cells[1, 5].Value = nameof(Product.PriceApproved);
            worksheet.Cells[1, 6].Value = nameof(Product.LastUpdated);
            worksheet.Cells[1, 7].Value = nameof(Product.Image);
        }

        private void Configure(ExcelWorksheet worksheet)
        {
            worksheet.Cells[1, 1, 1, 7].Style.Font.Bold = true;
            for (var i = 1; i <= 7; i++)
            {
                worksheet.Column(i).BestFit = true;
            }
        }
    }
}