using IndexedFiles.Core.ObjectArea;
using System.Collections.Generic;

namespace IndexedFiles.DataBase
{
    internal interface IDataBaseHandler
    {
        public List<IBlock> Blocks { get; }

        public void Insert(string item, int id = 0);
        public void Remove(int id);
        public void Replace(string item, int id, IBlock block);
        public void SetIndexes(List<int> indexes);
    }
}
