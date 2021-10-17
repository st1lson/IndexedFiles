using System.Collections.Generic;

namespace IndexedFiles.Core.IndexArea
{
    internal interface IIndexData
    {
        public List<(int, int)> Indexes { get; }

        public void Rebuild();
    }
}
