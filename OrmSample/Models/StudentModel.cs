using ECode.Data;

namespace Sample1.Models
{
    [Table("Student")]
    public class StudentModel
    {
        [PrimaryKey]
        public string SchoolId
        { get; set; }

        [PrimaryKey]
        public string ID
        { get; set; }

        [Column(IsRequired = true)]
        public string Name
        { get; set; }

        [Column]
        public string Remark
        { get; set; }
    }
}
