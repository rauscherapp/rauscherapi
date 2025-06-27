using Domain.Models;
using Domain.QueryParameters;
using MediatR;
using System;
using System.Linq;

namespace Domain.Queries
{
	public class ObterAppParametersQuery : IRequest<AppParameters>
	{		
		public ObterAppParametersQuery()
		{			
		}
	}
}
