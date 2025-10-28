﻿using AgriConnectMarket.SharedKernel.Entities;
using AgriConnectMarket.SharedKernel.Guards;
using AgriConnectMarket.SharedKernel.Interfaces;

namespace AgriConnectMarket.Domain.Entities
{
    public class Address : BaseEntity<Guid>, IAuditableEntity
    {
        public string Province { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string? Detail { get; set; }
        public bool IsDefault { get; set; }

        // Aggregatred properties
        public Guid ProfileId { get; set; }
        public Profile Profile { get; set; }
        public Farm Farm { get; set; }

        // Auditable properties
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        private Address() { }
        // Constructor
        public Address(string province, string district, string ward, string? detail, Guid profileId, bool isDefault = true)
        {
            Guard.AgainstNull(profileId, nameof(profileId));
            Guard.AgainstNullOrEmpty(province, nameof(province));
            Guard.AgainstNullOrEmpty(district, nameof(district));
            Guard.AgainstNullOrEmpty(ward, nameof(ward));
            Guard.AgainstNullOrEmpty(detail, nameof(detail));

            this.Province = province;
            this.District = district;
            this.Ward = ward;
            this.Detail = detail;
            this.ProfileId = profileId;
            this.IsDefault = isDefault;
        }
    }
}
