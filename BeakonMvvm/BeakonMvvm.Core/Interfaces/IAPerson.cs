using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeakonMvvm.Core.Database
{
    public interface IAPerson
    {
        Task<List<Perso>> GetPersons();
        Task<List<Perso>> GetPersonsLoc();
        Task<int> InsertPerson(Perso p);

    }
}