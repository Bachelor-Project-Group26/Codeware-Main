using System.ComponentModel.DataAnnotations;

namespace BPR_API.DBModels
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string SubjectCode { get; set; }
    }
}
