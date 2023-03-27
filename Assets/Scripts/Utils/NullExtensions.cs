using System;

namespace Utils
{
    public static class NullExtensions
    {
        public static T EnsureNotNull<T>(this T? value, string? message = null)
        {
            if (value == null)
            {
                throw new Exception(message ?? typeof(T).FullName);
            }

            return value;
        }
    }
}