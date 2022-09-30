namespace BPR_API.APIModels
{
    [Serializable]
    public class ImageDTO
    {
        public string Username { get; set; }
        public byte[] ProfilePicture { get; set; }
    }
}
