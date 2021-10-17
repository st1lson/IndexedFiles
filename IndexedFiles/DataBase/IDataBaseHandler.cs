﻿using IndexedFiles.Core.ObjectArea;
using System.Collections.Generic;

namespace IndexedFiles.DataBase
{
    internal interface IDataBaseHandler
    {
        public List<IBlock> Blocks { get; }

        public void Insert(string item);
        public void Remove(string item);
        public void Replace(string item, int id, IBlock block);
    }
}