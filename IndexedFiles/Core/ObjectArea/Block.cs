using System.Collections.Generic;

namespace IndexedFiles.Core.ObjectArea
{
    internal sealed class Block : IBlock
    {
        public static int BlocksCount = 10;
        public static int Capacity = 10;
        public int BlockID { get; set; }

        public List<IKey> Keys { get; }

        public Block()
        {
            Keys = new List<IKey>();
        }

        public void Rebuild()
        {

        }
    }
}
