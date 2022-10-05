using System.ComponentModel;

namespace AchromaBot.Common;

public static class EnumExtensionMethods
    {
        public static string GetDescription(this Enum value)
        {
            var enumType = value.GetType();
            var memberInfo = enumType.GetMember(value.ToString());
            if ((memberInfo != null && memberInfo.Length > 0))
            {
                var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if ((attributes.Length > 0))
                {
                    return ((DescriptionAttribute)attributes[0]).Description;
                }
            }
            return value.ToString();
        }
    }