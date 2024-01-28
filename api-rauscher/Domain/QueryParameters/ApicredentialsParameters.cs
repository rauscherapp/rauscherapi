using System;
namespace Domain.QueryParameters
{
	public class ApiCredentialsParameters : QueryParameters
	{
		public string Apikey { get; set; }
		public string Apisecrethash { get; set; }
		public DateTime Createdat { get; set; }
		public DateTime? Lastusedat { get; set; }
		public Boolean Isactive { get; set; }
	}
}
