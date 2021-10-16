using IndexedFiles.Core;
using IndexedFiles.DataBase;
using System;
using System.IO;

namespace IndexedFiles.FileManager
{
    internal static class FileOperator
    {
        private const string _path = "../../../DataFiles/default.txt";

        public static IDataBaseHandler ReadFile()
        {
            IDataBaseHandler dataBase = new DataBaseHandler();
            using (StreamReader reader = new (_path))
            {
                Block block = new ();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line))
                    {
                        if (block.Keys.Count != 0)
                        {
                            dataBase.Blocks.Add(block);
                            block = new Block();
                        }

                        line = reader.ReadLine();
                        block.BlockID = Int32.Parse(line);
                        line = reader.ReadLine();
                        if (string.IsNullOrEmpty(line))
                        {
                            break;
                        }
                    }

                    string[] value = line.Split(",");
                    (int id, string data) = (Int32.Parse(value[0]), value[1]);
                    block.Keys.Add(new Key()
                    {
                        Id = id,
                        Data = data
                    });
                }

                dataBase.Blocks.Add(block);
            }

            return dataBase;
        }

        public static void WriteFile(IDataBaseHandler dataBase)
        {
            using (StreamWriter writer = new(_path, false))
            {
                foreach (Block block in dataBase.Blocks)
                {
                    writer.WriteLine();
                    writer.WriteLine($"{block.BlockID}");
                    foreach (Key key in block.Keys)
                    {
                        writer.WriteLine(key);
                    }
                }
            }
        }
    }
}
