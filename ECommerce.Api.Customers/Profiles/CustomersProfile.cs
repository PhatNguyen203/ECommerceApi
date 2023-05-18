using AutoMapper;
using ECommerce.Api.Customers.Dtos;
using ECommerce.Api.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Profiles
{
    public class CustomersProfile : Profile
    {
        public CustomersProfile()
        {
            CreateMap<Customer, CustomerDto>();
        }
    }
}
