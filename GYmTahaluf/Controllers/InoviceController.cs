using GYmTahaluf.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;


using System.IO;

public class InvoicesController : Controller
{
    private readonly ModelContext _context;

    public InvoicesController(ModelContext context)
    {
        _context = context;
    }

    // GET: Invoices
    public async Task<IActionResult> Index()
    {
        return View(await _context.Invoices.ToListAsync());
    }

    public byte[] GenerateInvoicePdf( int paymentId, decimal amount, string userName, DateTime transactionDate)
    {
        using (var ms = new MemoryStream())
        {
            var document = new iTextSharp.text.Document();
            var writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, ms);
            document.Open();

            document.Add(new iTextSharp.text.Paragraph($"Amount Paid: {amount:C}"));
            document.Add(new iTextSharp.text.Paragraph($"User: {userName}"));
            document.Add(new iTextSharp.text.Paragraph($"Transaction Date: {transactionDate:dd-MM-yyyy}"));

            document.Close();
            writer.Close();

            var directoryPath = Path.Combine("wwwroot", "invoices");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);  // Ensure directory exists
            }

            var filePath = Path.Combine(directoryPath, $"Invoice_{transactionDate}.pdf");
            try
            {
                System.IO.File.WriteAllBytes(filePath, ms.ToArray());  // Write PDF to file
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing file: {ex.Message}");
            }

            var invoice = new Invoice
            {
                PaymentId = paymentId,
                Url = filePath,
                GeneratedAt = DateTime.Now
            };
            _context.Invoices.Add(invoice);
            _context.SaveChanges();

            return ms.ToArray();
        }
    }

    //public void SendInvoiceByEmail(string toEmail, string userName, byte[] pdfInvoice)
    //{
    //    try
    //    {
    //        using (var message = new MailMessage(_emailSettings.Email, toEmail))
    //        {
    //            message.Subject = "Your Payment Invoice";
    //            message.Body = $"Dear {userName},\n\nPlease find attached your invoice for the recent payment.\n\nThank you for your subscription.";

    //            // Attach PDF
    //            var attachment = new Attachment(new MemoryStream(pdfInvoice), "Invoice.pdf", "application/pdf");
    //            message.Attachments.Add(attachment);

    //            using (var smtp = new SmtpClient(_emailSettings.Host, _emailSettings.Port))
    //            {
    //                smtp.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password);
    //                smtp.EnableSsl = true;
    //                smtp.Send(message);
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine($"Error sending email: {ex.Message}");
    //    }
    //}

    public IActionResult DownloadInvoice(decimal invoiceId)
    {
        var invoice = _context.Invoices.FirstOrDefault(i => i.Id == invoiceId);
        if (invoice == null) return NotFound();

        return File(invoice.Url, "application/pdf", $"Invoice_{invoiceId}.pdf");
    }

    public IActionResult CompletePayment( int paymentId,decimal amount, string userName, string email)
    {

        // Generate the PDF
        var pdfInvoice = GenerateInvoicePdf(paymentId, amount, userName, DateTime.Now);
        var user=_context.Users.Where(u => u.Email == email). First();   
        // Send the email
        SendEmailWithAttachment(email, pdfInvoice);

        return RedirectToAction("MemberHome", "Home", new { userId = user.Id });
    }

    // GET: Invoices/Details/5
    public async Task<IActionResult> Details(decimal? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var invoice = await _context.Invoices
            .FirstOrDefaultAsync(m => m.Id == id);
        if (invoice == null)
        {
            return NotFound();
        }

        return View(invoice);
    }

    // GET: Invoices/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Invoices/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Amount,Date")] Invoice invoice)
    {
        if (ModelState.IsValid)
        {
            _context.Add(invoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(invoice);
    }

    // GET: Invoices/Edit/5
    public async Task<IActionResult> Edit(decimal? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var invoice = await _context.Invoices.FindAsync(id);
        if (invoice == null)
        {
            return NotFound();
        }
        return View(invoice);
    }

    // POST: Invoices/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(decimal id, [Bind("Id,Amount,Date")] Invoice invoice)
    {
        if (id != invoice.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(invoice);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(invoice.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(invoice);
    }

    // GET: Invoices/Delete/5
    public async Task<IActionResult> Delete(decimal? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var invoice = await _context.Invoices
            .FirstOrDefaultAsync(m => m.Id == id);
        if (invoice == null)
        {
            return NotFound();
        }

        return View(invoice);
    }

    // POST: Invoices/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(decimal id)
    {
        var invoice = await _context.Invoices.FindAsync(id);
        _context.Invoices.Remove(invoice);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool InvoiceExists(decimal id)
    {
        return _context.Invoices.Any(e => e.Id == id);
    }
    public async Task SendSimpleEmail(string toEmail)
    {
        try
        {
            var senderEmail = "your-email@gmail.com";
            var appPassword = "your-app-password";

            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(senderEmail, appPassword);

                var mail = new MailMessage
                {
                    From = new MailAddress(senderEmail),
                    Subject = "Test Email",
                    Body = "This is a test email to check SMTP configuration.",
                    IsBodyHtml = true
                };
                mail.To.Add(toEmail);

                await client.SendMailAsync(mail);
                Console.WriteLine("Simple email sent successfully!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error sending simple email: " + ex.Message);
        }
    }
    public async Task SendEmailWithAttachment(string toEmail, byte[] pdfBytes)
    {
        try
        {
            // Gmail credentials
            var senderEmail = "aymanhaniadas28@gmail.com";
            var password = "tdbn owup bhxv bmtj"; // Use the app password if 2-step verification is enabled

            // Configure the SMTP client
            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(senderEmail, password);

                // Create the email
                var mail = new MailMessage
                {
                    From = new MailAddress(senderEmail),
                    Subject = "Demo Invoice",
                    Body = "Please find your invoice attached.",
                    IsBodyHtml = true
                };
                mail.To.Add(toEmail);

                // Attach the PDF
                mail.Attachments.Add(new Attachment(new MemoryStream(pdfBytes), "invoice.pdf"));

                // Send the email
                await client.SendMailAsync(mail);

                Console.WriteLine("Email sent successfully!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send email: {ex.Message}");
        }
    }
}
