using GYmTahaluf.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ContactController : Controller
{
    private readonly ModelContext _context;

    public ContactController(ModelContext context)
    {
        _context = context;
    }

    // GET: Contact/Show/5
    public IActionResult Index()
    {
        var contact = _context.Contacts.FirstOrDefault();
        if (contact == null)
        {
            return NotFound();
        }
        return View(contact);
    }


    public async Task<IActionResult> Details(decimal? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var contact = await _context.Contacts
            .FirstOrDefaultAsync(m => m.Id == id);
        if (contact == null)
        {
            return NotFound();
        }

        return View(contact);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Email,Message,PhoneNumber")] Contact contact)
    {
        if (ModelState.IsValid)
        {
            _context.Add(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(contact);
    }

    // GET: Contact/Edit/5
    public IActionResult Edit(decimal id)
    {
        var contact = _context.Contacts.FirstOrDefault(c => c.Id == id);
        if (contact == null)
        {
            return NotFound();
        }
        return View(contact);
    }

    // POST: Contact/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(decimal Id, [Bind("Email,Insta,Facebook,Phone")] Contact contact)
    {
   

        if (ModelState.IsValid)
        {
            try
            {
                // Update the contact details
                var existingContact = _context.Contacts.FirstOrDefault(c => c.Id == Id);
                if (existingContact != null)
                {
                    existingContact.Email = contact.Email;
                    existingContact.Insta = contact.Insta;
                    existingContact.Facebook = contact.Facebook;
                    existingContact.Phone = contact.Phone;
                }
                _context.Update(existingContact);
                _context.SaveChanges(); 
            }
            

            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Contacts.Any(c => c.Id == Id))
                {
                    return NotFound();
                }
                throw;
            }
            return RedirectToAction(nameof(Index), new { id = contact.Id });
        }
        return View(contact);
    }

    public async Task<IActionResult> Delete(decimal? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var contact = await _context.Contacts
            .FirstOrDefaultAsync(m => m.Id == id);
        if (contact == null)
        {
            return NotFound();
        }

        return View(contact);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(decimal id)
    {
        var contact = await _context.Contacts.FindAsync(id);
        _context.Contacts.Remove(contact);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ContactExists(decimal id)
    {
        return _context.Contacts.Any(e => e.Id == id);
    }
}
