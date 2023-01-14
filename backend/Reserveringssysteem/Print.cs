using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Authenticatie;
class PrintBestelling
{
    public string qrCode = "https://www.google.com";
    private readonly GebruikerContext _context;
    // public PrintBestelling()
    // {
    // }

    public string ticketPrinten(Bestelling bestellinginformatie)
    {
        findVoorstelling(bestellinginformatie);
        Guid guid = Guid.NewGuid();
        string path = "ticket"+guid+".pdf";
        using (var document = new Document())
        {
            // We create a writer that listens to the document
            // and directs a PDF-stream to a file
            PdfWriter.GetInstance(document, new FileStream(path, FileMode.Create));

            document.Open();

            var font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            var qr = new Paragraph(qrCode, font);
            var name = new Paragraph("John Doe", font);
            var eventName = new Paragraph("Theater Show", font);
            var date = new Paragraph("January 1, 2021", font);
            var seat = new Paragraph("Seat 1A", font);
            //var image = Image.GetInstance("Voorstelling image hier");
            var logo = Image.GetInstance("https://theater-laak.netlify.app/media/tl-logo.png");

            qr.Alignment = Element.ALIGN_RIGHT;
            //image.Alignment = Element.ALIGN_CENTER;

            document.Add(qr);
            document.Add(name);
            document.Add(eventName);
            document.Add(date);
            document.Add(seat);
            //document.Add(image);

            document.Close();
        }
        return path;
    }

    public Voorstelling findVoorstelling(Bestelling bestellinginformatie){

        return new Voorstelling();
    }
}