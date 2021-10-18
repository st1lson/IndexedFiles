namespace IndexedFiles.Core.ObjectArea
{
    public class Key : IKey
    {
        public int Id {  get; set; }
        public string Data { get; set; }

        public override string ToString()
        {
            return $"{Id},{Data}";
        }
    }
}
