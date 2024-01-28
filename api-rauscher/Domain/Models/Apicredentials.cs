using System;

namespace Domain.Models
{
	public class ApiCredentials
	{
		public ApiCredentials(
		string apikey,
		string apisecrethash,
		DateTime createdat,
		DateTime? lastusedat,
		Boolean isactive
		)
		{
			Apikey = apikey;
			Apisecrethash = apisecrethash;
			Createdat = createdat;
			Lastusedat = lastusedat;
			Isactive = isactive;
		}
		public string Apikey { get; set; }
		public string Apisecrethash { get; set; }
		public DateTime Createdat { get; set; }
		public DateTime? Lastusedat { get; set; }
		public Boolean Isactive { get; set; }
	}
}