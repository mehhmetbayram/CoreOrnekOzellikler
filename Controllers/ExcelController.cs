using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using OfficeOpenXml;
using ProjeCoreOrnekOzellikler.Entities;
using ProjeCoreOrnekOzellikler.Models;

namespace ProjeCoreOrnekOzellikler.Controllers
{
    [Authorize]
    public class ExcelController : Controller
    {
        private IHostEnvironment _hostingEnvironment;
        private DataContext _context;

        public ExcelController(IHostEnvironment hostingEnvironment, DataContext dataContext)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = dataContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ImportExcel(IFormFile formFile)
        {
            Random random = new Random();
            var currentItems = _context.CdItems.ToList();
            List<cdItem> cdItems = new List<cdItem>();
            using (var importStream = new MemoryStream())
            {
                formFile.CopyToAsync(importStream, CancellationToken.None);
                using (var package = new ExcelPackage(importStream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;
                    int stok;
                    string fiyat;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        cdItem cdItem = new cdItem();
                        cdItem.ItemCode = worksheet.Cells[row, 1].Value != null ? worksheet.Cells[row, 1].Value.ToString().Trim() : "";
                        cdItem.ItemName = worksheet.Cells[row, 2].Value != null ? worksheet.Cells[row, 2].Value.ToString().Trim() : "";

                        fiyat = worksheet.Cells[row, 3].Value != null ? worksheet.Cells[row, 3].Value.ToString().Trim() : "0";
                        if (string.IsNullOrEmpty(fiyat) || fiyat.ToLower() == "nan")
                        {
                            cdItem.Price = 0;
                        }
                        else
                        {
                            cdItem.Price = Convert.ToDecimal(fiyat);
                        }

                        cdItem.Description = worksheet.Cells[row, 4].Value != null ? worksheet.Cells[row, 4].Value.ToString().Trim() : "";
                        stok = worksheet.Cells[row, 5].Value != null ? Convert.ToInt32(worksheet.Cells[row, 5].Value) : 0;
                        cdItem.Qty = (short)stok;
                        cdItem.IsActive = true;
                        cdItem.CreateDate = DateTime.Now;
                        cdItem.CreateTime = TimeSpan.Parse(DateTime.Now.ToString("hh:mm:ss"));
                        cdItem.CategoryId = random.Next(26, 58);

                        if (!currentItems.Any(x => x.ItemCode == cdItem.ItemCode))
                        {
                            cdItems.Add(cdItem);
                        }


                    }
                    _context.CdItems.AddRange(cdItems);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Index");



        }


        public IActionResult ExportExcel()
        {
            List<Product> productList = new List<Product>()
            {
                new Product()
                {
                    UrunKodu="P-0000000006",
                    UrunAdi="Aleta",
                    AltGrup="Topuklu",
                    Fiyat=250,
                    Sezon="2019YAZ",
                    Tedarikci="Vena",
                    Topuk="15CM",
                    Stok=20
                },new Product()
                {
                    UrunKodu="P-0000000007",
                    UrunAdi="Eva",
                    AltGrup="Dolgu Topuk",
                    Fiyat=189,
                    Sezon="2018YAZ",
                    Tedarikci="Madam",
                    Topuk="10CM",
                    Stok=15
                },new Product()
                {
                    UrunKodu="P-0000000008",
                    UrunAdi="Demo",
                    AltGrup="Spor",
                    Fiyat=40,
                    Sezon="2016YAZ",
                    Tedarikci="METO",
                    Topuk="6CM",
                    Stok=22
                },new Product()
                {
                    UrunKodu="P-0000000009",
                    UrunAdi="Sena",
                    AltGrup="Sandalet",
                    Fiyat=75,
                    Sezon="2011YAZ",
                    Tedarikci="bedo",
                    Topuk="6CM",
                    Stok=15
                },new Product()
                {
                    UrunKodu="P-0000000010",
                    UrunAdi="York",
                    AltGrup="Topuklu",
                    Fiyat=98,
                    Sezon="2020YAZ",
                    Tedarikci="LEVO",
                    Topuk="7CM",
                    Stok=22
                },new Product()
                {
                    UrunKodu="P-0000000011",
                    UrunAdi="Leydi",
                    AltGrup="Topuklu",
                    Fiyat=99,
                    Sezon="2020YAZ",
                    Tedarikci="ALMERA",
                    Topuk="14CM",
                    Stok=39
                }
            };
            var exportStream = new MemoryStream();
            using (var package = new ExcelPackage(exportStream))
            {
                string toplamFormat = $"SUM(H2: H{productList.Count + 1})";
                string yazilacakYer = $"H{productList.Count + 2}";

                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(productList, true);

                #region Font Ayarlari
                workSheet.Cells["A1:H1"].Style.Font.Bold = true;//Font kalin olacak
                workSheet.Cells.Style.Font.Color.SetColor(Color.Black);//Yazı renk
                workSheet.Cells.Style.Font.Size = 10;//Yazı fontu
                #endregion

                #region Text Ortalama
                workSheet.Cells[$"A1:H{productList.Count + 1}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;//Text ortalama
                workSheet.Cells[$"A1:H{productList.Count + 1}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                #endregion

                #region Tablo Icine Alma
                workSheet.Cells[$"A1:H{productList.Count + 1}"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;//Tablo icine alıyoruz
                workSheet.Cells[$"A1:H{productList.Count + 1}"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                workSheet.Cells[$"A1:H{productList.Count + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                workSheet.Cells[$"A1:H{productList.Count + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                #endregion

                #region Kolon genisliklerini otomatik ayarliyoruz
                workSheet.Cells[$"A1:H{productList.Count + 1}"].AutoFitColumns();
                #endregion

                workSheet.Cells[yazilacakYer].Formula = toplamFormat;

                workSheet.Cells[yazilacakYer].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                workSheet.Cells[$"G{productList.Count + 2}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                workSheet.Cells[$"G{productList.Count + 2}"].Value = "Toplam Stok :";
                workSheet.Cells[$"G{productList.Count + 2}"].Style.Font.Bold = true;

                package.Workbook.Calculate();//Matematiksel islemleri yapıyoruz
                workSheet.Cells[yazilacakYer].Style.Font.Bold = true;
                //var result = workSheet.Cells[yazilacakYer].Value;
                package.Save();
                //arrayData = package.GetAsByteArray();
            }
            exportStream.Position = 0;
            string fileName = $"ProductNewList.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            //Excel dosyasını streamde tutarak dosya adini ve uzantısını parametre olarak metod ile yollayıp mail attiriyoruz
            //MailHelper.SendMail(exportStream, fileName, contentType);

            return File(exportStream, contentType, fileName);







        }
    }
}