using ECode.Data;

namespace Sample1.Models
{
    [Table("Score")]
    public class ScoreModel
    {
        [Column(IsRequired = true)]
        public string SchoolId
        { get; set; }

        [Column(IsRequired = true)]
        public string StudentId
        { get; set; }

        [PrimaryKey(IsIdentity = true)]
        public int ID
        { get; set; }

        [Column(IsRequired = true)]
        public string Course
        { get; set; }

        [Column]
        public double Score
        { get; set; }
    }
}
