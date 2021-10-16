using System.Collections.Generic;

namespace IndexedFiles.Core
{
    internal interface IBlock
    {
        public int BlockID { get; }
        public List<IKey> Keys { get; }
        public int Capacity { get; set; }

        public void Rebuild();
    }
}
