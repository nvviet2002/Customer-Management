using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerManagement.Data.Entities
{
    [Table("Districts")]
    public class District
    {
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string FullName { get; set; }
        public string FullNameEn { get; set; }
        public string CodeName { get; set; }
        public string? ProvinceCode { get; set; }
        public int? UnitId { get; set; }

        [ForeignKey("UnitId")]
        public Unit? Unit { get; set; }

        [ForeignKey("ProvinceCode")]
        public Province? Province { get; set; }


    }
}
