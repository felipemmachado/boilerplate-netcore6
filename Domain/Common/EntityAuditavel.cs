using System;

namespace Domain.Common
{
    public abstract class EntityAuditavel
    {
        protected EntityAuditavel()
        {
            CreateBy = null;
            CreateAt = null;
            UpdateBy = null;
            UpdateAt = null;
        }
        public string CreateBy { get; set; }
        public DateTime? CreateAt { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
