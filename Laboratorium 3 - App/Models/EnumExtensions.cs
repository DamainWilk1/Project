using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Laboratorium_3___App.Models
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum e)
        {
            return e.GetType()
                .GetMember(e.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()
                .GetName();
        }
    }
}
