using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
  
    public class DocumentComponent: TrackableEntity
    {
        public int ID { get; set; }
        public string documentType { get; set; }
      
        public int UserCode { get; set; }
       


        public override DateTime? CreateDate { get; protected set; }

        public override void setCreateDate(DateTime? d)
        {
            if (!isCreateDateModified)
            {
                CreateDate = DateTime.Now;
                isCreateDateModified = true;
            }

        }

     

       
        public override int? CreatedBy { get; protected set; }

        public override void setCreatedBy(int ? id)
        {
            if (!isCreatedByModified)
            {
                CreatedBy = id;
                isCreatedByModified = true;
            }

        }



        
    }
}
