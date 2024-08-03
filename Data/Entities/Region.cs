using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerManagement.Data.Entities
{
    [Table("Regions")]
    public class Region
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string CodeName { get; set; }
        public string CodeNameEn { get; set; }
    }
}
