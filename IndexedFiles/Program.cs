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
            /*for (int i = 0; i < 1200; i++)
            {
                dataBaseHandler.Insert(RandomString(5));
            }*/

            //Console.WriteLine(dataBaseHandler.Search(14));
            FileOperator.WriteObjectFile(dataBaseHandler.Blocks);
        }

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
