using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Entities;
using AgriConnectMarket.SharedKernel.Interfaces;

namespace AgriConnectMarket.Domain.Entities
{
    public class Transaction : BaseEntity<Guid>, IAuditableEntity
    {
        public Guid OrderId { get; set; }
        public string TransactionRef { get; set; }
        public string TransactionNo { get; set; }
        public string BankCode { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public virtual Order Order { get; set; }

        public Transaction()
        {

        }

        private Transaction(Guid orderId, string txRef, string txNo, decimal amount, DateTime createdAt)
        {
            this.OrderId = orderId;
            this.TransactionRef = txRef;
            this.TransactionNo = txNo;
            this.Status = TransactionStatusConst.PENDING;
            this.CreatedAt = createdAt;
            this.Amount = amount;
        }

        public static Transaction Create(Guid orderId, string txRef, string txNo, decimal amount, DateTime createdAt)
            => new Transaction(orderId, txRef, txNo, amount, createdAt);

        public void UpdateTranasctionStatus(string vnpResponseCode, string bankCode)
        {
            this.Status = vnpResponseCode.Equals("00") ? TransactionStatusConst.SUCCESS : TransactionStatusConst.FAIL;
            this.BankCode = bankCode ?? "";
            this.UpdatedAt = DateTime.UtcNow;
        }
    }
}
