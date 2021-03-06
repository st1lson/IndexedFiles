using System;
using System.Collections.Generic;

namespace IndexedFiles.Core.ObjectArea
{
    public sealed class Block : IBlock
    {
        public static int BlocksCount = 10;
        public static int Capacity = 10;
        public int KeysCount { get; set; }
        public int BlockID { get; set; }

        public List<IKey> Keys { get; set; }

        public Block()
        {
            Keys = new List<IKey>();
        }

        public void RemoveEmptyKey()
        {
            foreach (IKey key in Keys)
            {
                if (key.GetType() == typeof(EmptyKey))
                {
                    Keys.Remove(key);
                    return;
                }
            }
        }

        public void Rebuild(ref List<IKey> keys)
        {
            Keys.Clear();
            KeysCount = 0;

            while (Keys.Count < Capacity)
            {
                Keys.Add(new EmptyKey());
            }
        }
    }
}
