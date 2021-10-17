using IndexedFiles.Core.ObjectArea;
using IndexedFiles.FileManager;
using System;
using System.Collections.Generic;

namespace IndexedFiles.DataBase
{
    internal class DataBaseHandler : IDataBaseHandler
    {
        public List<IBlock> Blocks { get; }
        private List<int> _indexes;

        public DataBaseHandler()
        {
            Blocks = new List<IBlock>();
            _indexes = new List<int>();
        }

        public void Insert(string data)
        {
            int id = 0;
            while (_indexes.Contains(id))
            {
                id++;
            }

            _indexes.Add(id);
            int blockId = id / Block.Capacity;

            IKey key = new Key()
            {
                Id = id,
                Data = data
            };

            Blocks[blockId].Keys.Add(key);

            if ((int)Math.Ceiling((double)Blocks[blockId].Keys.Count / Block.Capacity * 100) >= 90)
            {
                // rebuild
            }
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

        public void SetIndexes(List<int> indexes) => _indexes = indexes;

        private void Replace(IKey key, IBlock block)
        {

        }
    }
}
