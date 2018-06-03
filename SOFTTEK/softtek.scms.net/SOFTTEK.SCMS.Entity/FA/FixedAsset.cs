using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SF.DataModel;

namespace SOFTTEK.SCMS.Entity.FA
{
    public class FixedAsset : Shared.Asset
    {
        [InputField, OutputField]
        public string ImageUrl { get; set; }
        [InputField, OutputField, FilterField]
        public string SerialNumber { get; set; }
    }
}
