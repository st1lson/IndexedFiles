using System;

namespace IndexedFiles.Core.ObjectArea
{
    public class EmptyKey : IKey
    {
        public int Id { get; set; }
        public string Data { get; set; }

        public EmptyKey()
        {
            Id = Int32.MaxValue;
            Data = default;
        }

        public override string ToString()
        {
            return "Empty key";
        }
    }
}
