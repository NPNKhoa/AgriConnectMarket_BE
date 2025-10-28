using System.Reflection;

namespace AgriConnectMarket.SharedKernel.Guards
{
    public static class Guard
    {
        public static void AgainstNull(object? input, string paramName)
        {
            if (input is null)
                throw new ArgumentNullException(paramName);
        }

        public static void AgainstNullOrEmpty(string? argument, string name)
        {
            if (string.IsNullOrWhiteSpace(argument)) throw new ArgumentException($"{name} cannot be null or empty.", name);
        }

        public static void AgainstNullOrWhiteSpace(string? input, string paramName)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException($"{paramName} cannot be null or whitespace.", paramName);
        }

        public static void AgainstNegative(int number, string paramName)
        {
            if (number < 0)
                throw new ArgumentOutOfRangeException(paramName, $"{paramName} cannot be negative.");
        }

        public static void AgainstOutOfRange<T>(T value, T min, T max, string paramName)
            where T : IComparable<T>
        {
            if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0)
                throw new ArgumentOutOfRangeException(paramName, $"{paramName} must be between {min} and {max}.");
        }

        public static void AgainstInvalidEnumValue(Type enumLikeType, string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{paramName} cannot be null or whitespace.", paramName);

            if (!enumLikeType.IsClass)
                throw new ArgumentException($"{enumLikeType.Name} must be a class type.", nameof(enumLikeType));

            var validValues = enumLikeType
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string))
                .Select(fi => fi.GetRawConstantValue()?.ToString())
                .ToList();

            if (!validValues.Contains(value))
            {
                var validList = string.Join(", ", validValues);
                throw new ArgumentException(
                    $"{paramName} has an invalid value '{value}'. Valid values are: {validList}.",
                    paramName
                );
            }
        }
    }
}
