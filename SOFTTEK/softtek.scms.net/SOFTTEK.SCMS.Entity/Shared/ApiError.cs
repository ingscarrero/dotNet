using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SCMS.Entity.Shared
{
    public class ApiError
    {
        /// <summary>
        /// Message to the user.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Localized Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Localized reason for the failure.
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// Contact Url to get support for the error.
        /// </summary>
        public string SupportUrl { get; set; }
        /// <summary>
        /// Error code.
        /// </summary>
        public int Code { get; set; }
    }
}
