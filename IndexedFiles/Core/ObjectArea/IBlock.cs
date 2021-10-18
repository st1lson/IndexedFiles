using System.Collections.Generic;

namespace IndexedFiles.Core.ObjectArea
{
    public interface IBlock
    {
        public int BlockID { get; }
        public int KeysCount { get; set; }
        public List<IKey> Keys { get; set; }

        public void RemoveEmptyKey();
        public void Rebuild(ref List<IKey> keys);
    }
}
