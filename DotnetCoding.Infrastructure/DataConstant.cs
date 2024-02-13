using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoding.Infrastructure
{
    /// <summary>
    /// Adding constant data for avoiding hard code, and also create for the look up tables.
    /// </summary>
    public class DataConstant
    {
        public class RequestReasonCons
        {
            public int FiveThsoundsMore = 0;
            public int FiftyPercentMore = 1;
            public int Delete = 2;
        }

        public class ProductStatusCons
        {
            public int PendingApproval = 0;
            public int Active = 1;
            public int Deactive = 2;
        }

    }
}
