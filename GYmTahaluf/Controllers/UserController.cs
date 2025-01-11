using GYmTahaluf.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

public class UsersController : Controller
{
    private readonly ModelContext _context;
    private readonly IWebHostEnvironment _webHostEnviroment;

    public UsersController(ModelContext context, IWebHostEnvironment webHostEnviroment)
    {
        _context = context;
        _webHostEnviroment = webHostEnviroment;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Users.ToListAsync());
    }

    public async Task<IActionResult> Details(decimal? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(m => m.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }

    public IActionResult Create()
    {

        // Retrieve all roles from the database
        var roles = _context.Roles.Select(r => new { r.Id, r.RoleName }).ToList();

        // Assign the roles to the ViewBag for use in the view
        ViewBag.Roles = new SelectList(roles, "Id", "RoleName"); return View();
    }
    public IActionResult LoginPage()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register([Bind("UserName,Email,ImagePath,imageFile,Password")] User user)
    {
        


            if (user.imageFile != null)
            {
                string wwwRootPath = _webHostEnviroment.WebRootPath; //C:\Users\d.kanaan.ext\Desktop\ResturantMVC\ResturantMVC\wwwroot\
                string fileName = Guid.NewGuid().ToString() + "_" + user.imageFile.FileName;
                string path = Path.Combine(wwwRootPath + "/Images/", fileName);



                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await user.imageFile.CopyToAsync(fileStream);



                }
                user.UserImage = fileName;





            }



        Console.WriteLine(user.Email);
            User login = new User();
            login.UserName = user.UserName;
            login.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        login.Email = user.Email;
            login.RoleId = 3;
            login.CreatedAt = DateTime.Now;
            login.UpdateAt = DateTime.Now;
            login.LastLogin= DateTime.Now;
            _context.Add(login);
            await _context.SaveChangesAsync();

            return RedirectToAction("LoginPage");
    
    }
    [HttpPost]
	public IActionResult Login(string Email,string Password)

	{

        var auth = _context.Users.Single(x => x.Email ==Email);

        if ( BCrypt.Net.BCrypt.Verify(Password, auth.Password))

		{ // 1 > customer

			// 2 > admin

			// 3 > employee

			switch (auth.RoleId)

			{

				case 1:

					HttpContext.Session.SetString("AdminName",
					auth.UserName);

					return RedirectToAction("Index", "Admin");

				case 3:
					HttpContext.Session.SetInt32("MemberId", (int)auth.Id);

                    return RedirectToAction("MemberHome", "Home", new { userId = auth.Id });

            }

        }

        return RedirectToAction("LoginPage");

    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Username,Email,Password,RoleId")] User user)
    {
        if (ModelState.IsValid)
        {
            user.Password= BCrypt.Net.BCrypt.HashPassword(user.Password);
            _context.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }

    public async Task<IActionResult> Edit(decimal? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(decimal id, [Bind("Id,Username,Email,Password,RoleId")] User user)
    {
        if (id != user.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                if (existingUser.Password != user.Password) {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                }
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.Id))
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
        return View(user);
    }

    public async Task<IActionResult> Delete(decimal? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(m => m.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(decimal id)
    {
        var user = await _context.Users.FindAsync(id);
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool UserExists(decimal id)
    {
        return _context.Users.Any(e => e.Id == id);
    }
}
