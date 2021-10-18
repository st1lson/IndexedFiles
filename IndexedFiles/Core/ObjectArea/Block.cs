using System;
using System.Collections.Generic;

namespace IndexedFiles.Core.ObjectArea
{
    internal sealed class Block : IBlock
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
            foreach (IKey key in keys)
            {
                if (key.Id >= BlockID * Block.Capacity && key.Id < BlockID * Block.Capacity + Capacity)
                {
                    Keys.Add(key);
                    KeysCount++;
                }
            }

            for (int i = 0; i < Keys.Count; i++)
            {
                keys.Remove(Keys[i]);
            }

            while (Keys.Count < Capacity)
            {
                Keys.Add(new EmptyKey());
            }
        }
    }
}
