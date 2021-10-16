using IndexedFiles.Core;
using System.Collections.Generic;

namespace IndexedFiles.DataBase
{
    internal interface IDataBaseHandler
    {
        public List<IBlock> Blocks { get; }

        public void Insert(IKey key);
        public void Remove(int id);
        public void Replace(int index, Block block);
    }
}
