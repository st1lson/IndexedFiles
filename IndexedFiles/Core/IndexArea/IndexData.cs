using System.Collections.Generic;

namespace IndexedFiles.Core.IndexArea
{
    internal class IndexData : IIndexData
    {
        public List<(int, int)> Indexes { get; }
        public IndexData() => Indexes = new();

        public void Rebuild()
        {

        }
    }
}
