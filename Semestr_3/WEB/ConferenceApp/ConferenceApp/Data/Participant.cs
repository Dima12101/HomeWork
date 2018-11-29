using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Data
{
	public class Participant
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required(ErrorMessage = "Всё плохо:(")]
		[RegularExpression(@"^([a-zA-Z0-9_\-\.]+)"
		+ @"@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "И так тоже")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Укажите Вашу роль")]
		public bool? Speaker { get; set; }
	}
}
