using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoding.Core.Models
{
    //create new table for approval queue table
    public class ApprovalQueue
    {
        public int id { get; set; }
        public int RequestReasonTypeId { get; set; }
        public DateTime RequestDate { get; set; }
        //Create look up table for the different reason type to avoid hard code
        public RequestReason RequestReasonType { get; set; }

    }
}
