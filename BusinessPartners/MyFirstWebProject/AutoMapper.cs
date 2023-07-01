using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entities;
using DTO;

namespace MyFirstWebProject
{
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
           // CreateMap<OrdersTbl, order_DTO>().ReverseMap();
           // CreateMap<CustomerTbl, customer_DTO>().ReverseMap();
          
        }
    }
}
