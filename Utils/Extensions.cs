using System;
using SweetHome.Models;
using System.Collections.Generic;
using System.Linq;

namespace SweetHome.Utils
{
	public static class Extensions
	{
		public static void IfNotNull<T>(this Nullable<T> nullable, Action<T> action) where T : struct
        {
            if (nullable.HasValue)
            {
                action(nullable.Value);
            }
        }
        public static bool Check<T>(this Nullable<T> nullable, Func<T, bool> predicate) where T : struct
        {
            return nullable.HasValue ? predicate(nullable.Value) : false;
        }
        public static string PhonesList(this IEnumerable<Phone> phones)
        {
            return (phones != null && phones.Any()) ? phones.Select(phone => phone.FormattedPhone).Aggregate((p1, p2) => p1 + ", " + p2) : "";
        }
	}
}