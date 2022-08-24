using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ShopOnline.Models
{
    public class FilePDF
    {
        public void PrintPDF(int id)
        {
            TTPHONEEntities db = new TTPHONEEntities();
            Order order = db.Orders.Find(id);
            var FontTitle = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 14, Font.BOLD);
            var FontContent = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12, Font.NORMAL);
            var FontHead = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12, Font.BOLD);
            var filename = HttpContext.Current.Server.MapPath(@"~/hoadon/Order_" + order.Account.AccountName + "_" + order.OrderID + ".pdf");
            using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
            using (Document doc = new Document(PageSize.A4))
            using (PdfWriter writer = PdfWriter.GetInstance(doc, fs))
            {
                doc.Open();

                PdfPTable table = new PdfPTable(7);

                PdfPCell cell1 = new PdfPCell(new Paragraph(new Chunk("HÓA DON BÁN HÀNG", FontTitle)));
                cell1.Colspan = 7;
                cell1.BorderColor = BaseColor.WHITE;
                cell1.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right

                PdfPCell cell2 = new PdfPCell(new Paragraph("Thông tin cửa hàng"));
                cell2.AddElement(new Paragraph("Don vi bán hàng: Cua hàng TTPHONE"));
                cell2.AddElement(new Paragraph("Website: TTPHONE.com.vn"));
                cell2.AddElement(new Paragraph("Email: TTPHONE@gmail.com.vn"));
                cell2.AddElement(new Paragraph("SDT: 0987654321"));
                cell2.BorderColor = BaseColor.WHITE;
                cell2.Colspan = 7;
                cell2.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right

                Customer customer = db.Customers.FirstOrDefault(c => c.AccountID == order.AccountID);
                PdfPCell cell3 = new PdfPCell(new Phrase(new Chunk("Thông tin khách hàng", FontHead)));
                cell3.AddElement(new Phrase("Ho tên khách hàng: " + customer.CustomerName));
                cell3.AddElement(new Phrase("Tai khoan: " + order.Account.AccountName));
                cell3.AddElement(new Phrase("Dia chi: " + customer.Address));
                cell3.AddElement(new Phrase("Email: " + customer.Email));
                cell3.AddElement(new Phrase("SDT: " + customer.PhoneNumber));
                cell3.AddElement(new Phrase("Ngày Dat: " + order.DateOrder));
                cell3.BorderColor = BaseColor.WHITE;
                cell3.Colspan = 7;
                cell3.HorizontalAlignment = Element.ALIGN_LEFT; //0=Left, 1=Centre, 2=Right


                table.AddCell(cell1);
                table.AddCell(cell2);
                table.AddCell(cell3);

                table.AddCell(new Phrase(new Chunk("STT", FontHead)));
                table.AddCell(new Phrase(new Chunk("Tên san phẩm", FontHead)));
                table.AddCell(new Phrase(new Chunk("Don giá", FontHead)));
                table.AddCell(new Phrase(new Chunk("So luong", FontHead)));
                table.AddCell(new Phrase(new Chunk("Khuyen mãi", FontHead)));
                table.AddCell(new Phrase(new Chunk("Tong tien", FontHead)));
                table.AddCell(new Phrase(new Chunk("Thành tien", FontHead)));

                int index = 1;
                foreach (var item in order.OrderDetails)
                {
                    table.AddCell(new Phrase(new Chunk(index.ToString(), FontContent)));
                    table.AddCell(new Phrase(new Chunk(item.Product.ProductName, FontContent)));
                    table.AddCell(new Phrase(new Chunk(item.Product.UnitPrice.ToString("0,000,000") + "VND", FontContent)));
                    table.AddCell(new Phrase(new Chunk(item.Quantity.ToString(), FontContent)));
                    table.AddCell(new Phrase(new Chunk(item.Product.Sale.Discount + "%", FontContent)));
                    table.AddCell(new Phrase(new Chunk((item.Product.UnitPrice * item.Quantity).ToString("0,000,000") + "VND", FontContent)));
                    table.AddCell(new Phrase(new Chunk((item.Product.UnitPrice * item.Quantity * item.Product.Sale.Discount/100).ToString("0,000,000") + "VND", FontContent)));
                    index++;
                }

                PdfPCell cell4 = new PdfPCell();
                cell4.AddElement(new Phrase(new Chunk("Tong Tien: " + order.OrderDetails.Sum(od => od.Product.UnitPrice * od.Quantity), FontHead)));
                cell4.AddElement(new Phrase(new Chunk("Thanh toan: " + order.OrderDetails.Sum(od => od.Product.UnitPrice * od.Quantity * od.Product.Sale.Discount), FontHead)));
                cell4.BorderColor = BaseColor.WHITE;
                cell4.Colspan = 7;
                cell4.HorizontalAlignment = Element.ALIGN_RIGHT;

                PdfPCell cell5 = new PdfPCell();
                cell5.AddElement(new Phrase(new Chunk("Nguoi mua hang", FontHead)));
                cell5.AddElement(new Phrase(new Chunk("(Ki, ghi ro ho ten) ", FontContent)));
                cell5.BorderColor = BaseColor.WHITE;
                cell5.Colspan = 3;
                cell5.HorizontalAlignment = Element.ALIGN_CENTER;



                PdfPCell cell6 = new PdfPCell();
                cell6.AddElement(new Phrase(new Chunk("Nguoi ban hang", FontHead)));
                cell6.AddElement(new Phrase(new Chunk("(Ki, ghi ro ho ten) ", FontContent)));
                cell6.BorderColor = BaseColor.WHITE;
                cell6.Colspan = 3;
                cell6.HorizontalAlignment = Element.ALIGN_CENTER; //0=Left, 1=Centre, 2=Right

                table.AddCell(cell4);
                table.AddCell(cell5);
                PdfPCell cellr = new PdfPCell();
                cellr.BorderColor = BaseColor.WHITE;
                table.AddCell(cellr);
                table.AddCell(cell6);

                doc.Add(table);
                doc.Close();
            }
        }
    }
}