using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConferenceApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConferenceApp.Pages
{
    public class RegisterModel : PageModel
    {
		private readonly AppDbContext db;
		public RegisterModel(AppDbContext db)
		{
			this.db = db;
		}
		[BindProperty]
		public Data.Participant Participant { get; set; }

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			db.Participants.Add(Participant);
			await db.SaveChangesAsync();
			return RedirectToPage($"/Thanks", new { name = Participant.Name });
		}
	}
}