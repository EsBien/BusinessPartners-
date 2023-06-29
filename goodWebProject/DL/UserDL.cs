using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DL
{
    
    public class UserDL :IuserDL
    {
        BusinessPartnersContext _context;
         public UserDL(BusinessPartnersContext context) {
            _context = context;
        }
        public async Task<UserTbl> getUser(string name, int id)
        {
            UserTbl newUser = await _context.UserTbls.FindAsync( id);
            if (newUser != null)
            {
                return newUser;
            }
            return null;
        }


    }
}
