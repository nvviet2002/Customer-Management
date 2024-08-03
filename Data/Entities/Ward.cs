using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerManagement.Data.Entities
{
    [Table("Wards")]
    public class Ward
    {
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string FullName { get; set; }
        public string FullNameEn { get; set; }
        public string CodeName { get; set; }
        public string? DistrictCode { get; set; }
        public int? UnitId { get; set; }

        //forein key
        [ForeignKey("UnitId")]
        public Unit? Unit { get; set; }
        [ForeignKey("DistrictCode")]
        public District? District { get; set; }


    }
}
