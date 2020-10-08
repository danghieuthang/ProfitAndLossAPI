using AutoMapper;
using AutoMapper.Configuration;
using ProfitAndLoss.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfitAndLoss.WebApi.Helpers
{
	public class OrganizationProfile : Profile
	{
		public OrganizationProfile()
		{
			//CreateMap<User, UserViewModel>().ReverseMap();
		}
	}
}
