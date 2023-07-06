using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public abstract class TrackableEntity
    {
        public virtual DateTime? CreateDate { get; protected set; }

        protected bool isCreateDateModified { get; set; } = false;
        public DateTime? LastUpdateDate { get; set; }

        public abstract void setCreateDate(DateTime? d);
     
        public virtual int? CreatedBy { get; protected set; }

        protected bool isCreatedByModified = false;
        public abstract void setCreatedBy(int? id);
      
        public int? LastUpdatedBy { get; set; }
    }
}
