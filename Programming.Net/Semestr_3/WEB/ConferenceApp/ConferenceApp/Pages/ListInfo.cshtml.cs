using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConferenceApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConferenceApp.Pages
{
    public class ListInfoModel : PageModel
    {
		private readonly AppDbContext db;
		public ListInfoModel(AppDbContext db)
		{
			this.db = db;
		}
		public IList<Participant> Participants { get; private set; }
		public async Task OnGetAsync()
		{
			Participants = await db.Participants.AsNoTracking().ToListAsync();
		}
	}
}