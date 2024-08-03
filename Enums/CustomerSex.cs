using System.ComponentModel;

namespace CustomerManagement.Enums
{
    public enum CustomerSex
    {
        [Description("Nam")]
        Male,

        [Description("Nữ")]
        Female,

        [Description("Khác")]
        Other
    }
}
