using IndexedFiles.Core.ObjectArea;
using IndexedFiles.Enums;
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
            Blocks[blockId].RemoveEmptyKey();
            Blocks[blockId].Keys = Blocks[blockId].Keys.OrderBy(key => key.Id).ToList();
            Blocks[blockId].KeysCount++;

            if (IsNeedToRebuild())
            {
                List<IKey> keys = new();
                _indexes.Clear();
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

                foreach (IKey k in keys)
                {
                    Insert(k.Data, k.Id);
                }

                FileOperator.WriteIndexFile(Blocks);
            }
        }

        public void Remove(int id)
        {
            int blockId = id / Block.Capacity;
            IKey key = Search(id);

            if (key is null)
            {
                throw new IndexOutOfRangeException();
            }

            Blocks[blockId].Keys.Remove(key);
            Blocks[blockId].Keys.Add(new EmptyKey());
            Blocks[blockId].Keys = Blocks[blockId].Keys.OrderBy(key => key.Id).ToList();
            Blocks[blockId].KeysCount--;
        }

        public void Replace(int id, string item)
        {
            IKey key = new Key()
            {
                Id = id,
                Data = item
            };

            Replace(key);
        }

        public IKey Search(int id)
        {
            try
            {
                int blockId = id / Block.Capacity;
                int k = (int)Math.Floor(Math.Log2(Blocks[blockId].KeysCount));
                int i = (int)Math.Pow(2, k) - 1;

                if (id < Blocks[blockId].Keys[i].Id)
                {
                    return SharrSearch(id, blockId, k, i, SharrMethod.FirstMethod);
                }
                else if (id > Blocks[blockId].Keys[i].Id)
                {
                    return SharrSearch(id, blockId, k, i, SharrMethod.SecondMethod);
                }
                else
                {
                    return Blocks[blockId].Keys[i];
                }
            }
            catch (Exception)
            {
                throw new NullReferenceException();
            }
        }

        private IKey SharrSearch(int id, int blockId, int k, int i, SharrMethod method)
        {
            if (method is SharrMethod.FirstMethod)
            {
                int sequence = (int)Math.Pow(2, k);

                while (true)
                {
                    if (i > Blocks[blockId].KeysCount)
                    {
                        i -= (sequence / 2 + 1);
                        sequence /= 2;
                        continue;
                    }
                    else if (i < 0)
                    {
                        i += (sequence / 2 + 1);
                        sequence /= 2;
                        continue;
                    }

                    if (id > Blocks[blockId].Keys[i].Id)
                    {
                        i += (sequence / 2 + 1);
                        sequence /= 2;
                    }
                    else if (id < Blocks[blockId].Keys[i].Id)
                    {
                        i -= (sequence / 2 + 1);
                        sequence /= 2;
                    }
                    else
                    {
                        if (i > Blocks[blockId].KeysCount || i < 0)
                        {
                            throw new IndexOutOfRangeException();
                        }

                        return Blocks[blockId].Keys[i];
                    }
                }
            }
            else
            {
                int I = (int)Math.Floor(Math.Log2(blockId - (int)Math.Pow(2, k) + 1));
                i = Blocks[blockId].KeysCount + 1 - (int)Math.Pow(2, I);
                int sequence = (int)Math.Pow(2, I);

                while (true)
                {
                    if (i > Blocks[blockId].KeysCount)
                    {
                        i -= (sequence / 2 + 1);
                        sequence /= 2;
                        continue;
                    }
                    else if (i < 0)
                    {
                        i += (sequence / 2 + 1);
                        sequence /= 2;
                        continue;
                    }

                    if (id > Blocks[blockId].Keys[i].Id)
                    {
                        i += (sequence / 2 + 1);
                        sequence /= 2;
                    }
                    else if (id < Blocks[blockId].Keys[i].Id)
                    {
                        i -= (sequence / 2 + 1);
                        sequence /= 2;
                    }
                    else
                    {
                        if (i > Blocks[blockId].KeysCount || i < 0)
                        {
                            throw new IndexOutOfRangeException();
                        }

                        return Blocks[blockId].Keys[i];
                    }
                }
            }
        }

        public void SetIndexes(List<int> indexes) => _indexes = indexes;

        private void Replace(IKey key)
        {
            IKey currentKey = Search(key.Id);
            Remove(currentKey.Id);
            _indexes.Remove(currentKey.Id);
            Insert(key.Data, key.Id);
        }

        private bool IsNeedToRebuild()
        {
            foreach (IBlock block in Blocks)
            {
                if ((int)Math.Ceiling((double)block.KeysCount / Block.Capacity * 100) < 80)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
