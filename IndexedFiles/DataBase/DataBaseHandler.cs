using IndexedFiles.Core.ObjectArea;
using IndexedFiles.FileManager;
using System.Collections.Generic;

namespace IndexedFiles.DataBase
{
    internal class DataBaseHandler : IDataBaseHandler
    {
        public List<IBlock> Blocks { get; }
        private readonly List<int> _indexes;

        public DataBaseHandler()
        {
            Blocks = new List<IBlock>();
            _indexes = new List<int>();
        }

        public void Insert(string item)
        {

        }

        public void Remove(string item)
        {

        }

        public void Replace(string item, int id, IBlock block)
        {
            IKey key = new Key()
            {
                Id = id,
                Data = item
            };

            Replace(key, block);
        }

        private void Replace(IKey key, IBlock block)
        {

        }

        private int CreateIndex()
        {
            return -1;
        }
    }
}
