using System;
using Domain.Core.Commands;

namespace Domain.Commands
{
	public abstract class ApicredentialsCommand : Command
	{
		public string Apikey { get; set; }
		public string Apisecrethash { get; set; }
		public DateTime Createdat { get; set; }
		public DateTime? Lastusedat { get; set; }
		public Boolean Isactive { get; set; }
	}

  public abstract class GenerateApiCredentialsCommand : Command
  {
    public string Document { get; set; }
  }
}
