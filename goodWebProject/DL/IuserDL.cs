﻿using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public interface IuserDL
    {
        Task<UserTbl> getUser(string name, int id);
    }
}
