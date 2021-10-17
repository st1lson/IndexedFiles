using IndexedFiles.Core.ObjectArea;
using IndexedFiles.DataBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IndexedFiles.FileManager
{
    internal static class FileOperator
    {
        private const string _indexFile = "../../../DataFiles/index.txt";
        private const string _objectFile = "../../../DataFiles/default.txt";

        public static IDataBaseHandler DeserializeDataBase()
        {
            IDataBaseHandler dataBase = new DataBaseHandler();

            using StreamReader reader = new(_objectFile);
            IBlock block = null;
            int blockId = 0;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrEmpty(line))
                {
                    if (block is not null)
                    {
                        dataBase.Blocks.Add(block);
                    }

                    block = new Block()
                    {
                        BlockID = blockId++
                    };
                }

                string[] splitedLine = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
                if (!Int32.TryParse(splitedLine[0], out int id))
                {
                    throw new FormatException();
                }

                IKey key = new Key()
                {
                    Id = id,
                    Data = splitedLine[1]
                };

                block.Keys.Add(key);
            }

            return dataBase;
        }

        public static List<string> ReadIndexFile()
        {
            List<string> data = new();
            using StreamReader reader = new(_indexFile);
            while(!reader.EndOfStream)
            {
                data.Add(reader.ReadLine());
            }

            return data;
        }

        public static void WriteIndexFile(List<IBlock> blocks)
        {
            using StreamWriter writer = new(_indexFile, false, Encoding.Default);
            foreach (IBlock block in blocks)
            {
                writer.WriteLine($"{Block.Capacity * block.BlockID + Block.Capacity}, {block.BlockID}");
            }
        }

        public static void WriteObjectFile(List<IBlock> blocks)
        {
            using StreamWriter writer = new(_objectFile, false, Encoding.Default);
            foreach (IBlock block in blocks)
            {
                writer.WriteLine();
                foreach (IKey key in block.Keys)
                {
                    writer.WriteLine(key);
                }
            }
        }
    }
}
