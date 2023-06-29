using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL;
using Entities;

namespace BL_
{
    public class UserBL : IuserBL
    {
        IuserDL user;
        public UserBL(IuserDL user)
        {
            this.user = user;
        }

        public async Task<UserTbl> getUser(string name, int id)
        {
            UserTbl newUser = await user.getUser(name, id);
            return newUser;
        }

    }
}
