using System.Collections.Generic;

namespace TramWars.Tests.Persistence.Repositories
{
    public interface IFile
    {
         IEnumerable<string> GetLines();
    }
}