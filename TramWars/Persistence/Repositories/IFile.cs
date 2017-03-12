using System.Collections.Generic;

namespace TramWars.Persistence.Repositories
{
    public interface IFile
    {
         IEnumerable<string> GetLines();
    }
}