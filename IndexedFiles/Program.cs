using IndexedFiles.DataBase;
using IndexedFiles.FileManager;
using System;

namespace IndexedFiles
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IDataBaseHandler dataBaseHandler = FileOperator.DeserializeDataBase();
            dataBaseHandler.Insert("LOL");
            FileOperator.WriteObjectFile(dataBaseHandler.Blocks);
        }
    }
}
