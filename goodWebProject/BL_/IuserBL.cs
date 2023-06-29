using Entities;
using System.Threading.Tasks;

namespace BL_
{
    public interface IuserBL
    {
        Task<UserTbl> getUser(string name, int id);
    }

}
