using IndexedFiles.Core.ObjectArea;
using System.Collections.Generic;

namespace IndexedFiles.DataBase
{
    public interface IDataBaseHandler
    {
        public List<IBlock> Blocks { get; }

        public void Insert(string item, int id = 0);
        public void Remove(int id);
        public void Replace(int id, string item);
        public IKey Search(int index);
        public void SetIndexes(List<int> indexes);
        public List<string> GetIndexArea();
        public List<string> GetObjectArea();
    }
}
