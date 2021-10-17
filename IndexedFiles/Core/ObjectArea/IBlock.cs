using System.Collections.Generic;

namespace IndexedFiles.Core.ObjectArea
{
    internal interface IBlock
    {
        public int BlockID { get; }
        public List<IKey> Keys { get; }

        public void Rebuild();
    }
}
