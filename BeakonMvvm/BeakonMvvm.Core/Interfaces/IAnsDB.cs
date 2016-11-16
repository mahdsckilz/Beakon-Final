using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeakonMvvm.Core.Interfaces
{
    public interface IAnsDB
    {
        Task<List<Answ>> GetAns(string name);

        Task<int> DeleteAns(object id);
        Task<int> InsertAns(string from, string to, string cal, string loc, string extra);
       Task<bool> CheckIfExists(Answ p);


    }
}
