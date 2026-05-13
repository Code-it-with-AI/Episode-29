namespace Gemma4Test.Models
{
    public class FileChunk
    {
        public string FileName { get; set; } = string.Empty;
        public int TotalChunks { get; set; }
        public int ChunkNumber { get; set; }
        public long Offset { get; set; }
        public byte[] Data => Array.Empty<byte>();
    }

    public class UploadedFile
    {
        public string Name { get; set; } = string.Empty;
        public long Size { get; set; }
        public DateTime Modified { get; set; }
    }
}
