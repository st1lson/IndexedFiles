using System.Collections.Generic;

namespace IndexedFiles.Core
{
    internal sealed class Block : IBlock
    {
        public int BlockID { get; set; }

        public List<IKey> Keys { get; }

        public int Capacity { get; set; }

        public Block()
        {
            Capacity = 20;
            Keys = new List<IKey>();
        }

        public void Rebuild()
        {

        }
    }
}
