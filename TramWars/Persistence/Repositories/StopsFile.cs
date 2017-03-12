using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TramWars.Persistence.Repositories
{
    public class StopsFile : IFile
    {
        private const string SourcePath = "Scripts/Output";

        public IEnumerable<string> GetLines()
        {
            return Directory.GetFiles(SourcePath).SelectMany(File.ReadLines);
        }
    }
}