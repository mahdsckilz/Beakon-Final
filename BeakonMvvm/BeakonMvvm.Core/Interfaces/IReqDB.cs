using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeakonMvvm.Core.Interfaces
{
    public interface IReqDB
    {
        Task<List<Req>> GetReq(string t);
        Task<int> DeleteReq(object id);

        Task<int> InsertReq(Req p);
        Task<bool> CheckIfExists(Req p);



    }
}
