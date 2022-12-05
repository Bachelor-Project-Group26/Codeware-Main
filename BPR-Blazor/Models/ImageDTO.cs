namespace BPR_Blazor.Models
{
    [Serializable]
    public class ImageDTO
    {
        public string Username { get; set; }
        public byte[] Image { get; set; }
    }
}
