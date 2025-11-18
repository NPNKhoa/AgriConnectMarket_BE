using AgriConnectMarket.SharedKernel.Guards;

namespace AgriConnectMarket.Domain.ValueObjects
{
    public sealed class BatchCode
    {
        public string Value { get; set; }

        private BatchCode(string value)
        {
            Value = value;
        }

        public static BatchCode FromParts(string prefix, int number)
        {
            Guard.AgainstNullOrWhiteSpace(prefix, nameof(prefix));

            var formatted = $"{prefix}-{number:000}";

            return new BatchCode(formatted);
        }

        public override string ToString() => Value;
    }
}
