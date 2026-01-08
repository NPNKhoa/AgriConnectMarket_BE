using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Entities;
using AgriConnectMarket.SharedKernel.Guards;
using AgriConnectMarket.SharedKernel.Interfaces;

namespace AgriConnectMarket.Domain.Entities
{
    public class Transaction : BaseEntity<Guid>, IAuditableEntity
    {
        public string TransactionRef { get; set; }
        public string BankCode { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        private readonly List<Order> _orders = new();
        public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();

        public Transaction()
        {

        }

        private Transaction(string txRef, decimal amount, DateTime createdAt)
        {
            this.TransactionRef = txRef;
            this.Status = TransactionStatusConst.PENDING;
            this.CreatedAt = createdAt;
            this.Amount = amount;
            this.BankCode = "";
        }

        public static Transaction Create(string txRef, decimal amount, DateTime createdAt)
            => new Transaction(txRef, amount, createdAt);

        public void UpdateTranasctionStatus(string vnpResponseCode, string bankCode)
        {
            this.Status = vnpResponseCode.Equals("00") ? TransactionStatusConst.SUCCESS : TransactionStatusConst.FAIL;
            this.BankCode = bankCode ?? "";
            this.UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateBankCode(string bankCode)
        {
            Guard.AgainstNullOrEmpty(bankCode, nameof(bankCode));

            BankCode = bankCode;
        }
    }
}
