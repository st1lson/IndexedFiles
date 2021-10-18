using IndexedFiles.DataBase;
using IndexedFiles.FileManager;
using System;
using System.Linq;

namespace IndexedFiles
{
    internal class Program
    {
        private static Random random = new Random();
        static void Main(string[] args)
        {
            IDataBaseHandler dataBaseHandler = FileOperator.DeserializeDataBase();
            /*for (int i = 0; i < 150; i++)
            {
                int value;
                if (i < 100)
                {
                    value = random.Next(100);
                }
                else
                {
                    value = random.Next(1000);
                }

                dataBaseHandler.Insert(RandomString(5), value);
            }*/
            dataBaseHandler.Remove(99);
            FileOperator.WriteObjectFile(dataBaseHandler.Blocks);
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
