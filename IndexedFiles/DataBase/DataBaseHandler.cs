using IndexedFiles.Core;
using System.Collections.Generic;

namespace IndexedFiles.DataBase
{
    internal class DataBaseHandler : IDataBaseHandler
    {
        public List<IBlock> Blocks { get; }
        private const int BlockCount = 10;

        public DataBaseHandler()
        {
            Blocks = new List<IBlock>();
        }

        public void Insert(IKey key)
        {

        }

        public void Remove(int id)
        {

        }

        public void Replace(int index, Block block)
        {

        }
    }
}
