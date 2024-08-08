using DocumentFormat.OpenXml;
using System.ComponentModel;

namespace CustomerManagement.Extensions
{
    public static class EnumExtension
    {
        public static string GetEnumDescription(this Enum enumValue)
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());
            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                return attribute.Description;
            }
            throw new ArgumentException("Item not found.", nameof(enumValue));
        }

        //public static T? GetEnumByDescription<T>(Type enumType, string description)
        //{
        //    var fields = enumType.GetFields();
        //    foreach (var field in fields)
        //    {
        //        if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
        //        {
        //            if (string.Equals(attribute.Description, description))
        //            {
        //               return (T) field.GetValue(field);
        //            }
        //        }
        //    }

        //    return null;
        //}
    }
}
