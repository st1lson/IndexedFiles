using IndexedFiles.Core.ObjectArea;
using IndexedFiles.FileManager;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public void Insert(string data, int id = 0)
        {
            if (id == 0)
            {
                while (_indexes.Contains(id))
                {
                    id++;
                }
            }

            _indexes.Add(id);
            int blockId = id / Block.Capacity;

            IKey key = new Key()
            {
                Id = id,
                Data = data
            };

            Blocks[blockId].Keys.Add(key);
            Blocks[blockId].RemoveEmptyKey();
            Blocks[blockId].Keys = Blocks[blockId].Keys.OrderBy(key => key.Id).ToList();
            Blocks[blockId].KeysCount++;

            if ((int)Math.Ceiling((double)Blocks[blockId].KeysCount / Block.Capacity * 100) >= 90)
            {
                List<IKey> keys = new();
                foreach (IBlock block in Blocks)
                {
                    foreach (IKey k in block.Keys)
                    {
                        if (k.Data is not null)
                        {
                            keys.Add(k);
                        }
                    }
                }

                Block.Capacity = (int)Math.Ceiling((double)Block.Capacity / 100 * 120);
                foreach (IBlock block in Blocks)
                {
                    block.Rebuild(ref keys);
                    block.Keys = block.Keys.OrderBy(key => key.Id).ToList();
                }

                FileOperator.WriteIndexFile(Blocks);
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
            IKey currentKey = Blocks[block.BlockID].Keys[key.Id];
            Blocks[block.BlockID].Keys.Remove(currentKey);
            Blocks[block.BlockID].Keys.Add(key);
        }
    }
}
