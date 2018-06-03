using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SF.DataModel;

namespace SOFTTEK.SCMS.Entity.FA
{
    public class PhysicalInventoryTakingItem
    {
        [PrimaryKeyField, OutputField, FilterField]
        public long Identifier { get; set; }

        [ForeignKeyField(typeof(FixedAsset), "Identifier"), InputField, OutputField, FilterField]
        public FixedAsset FixedAsset { get; set; }

        [CalculatedField, OutputField]
        public DateTime VerifiedAt { get; set; }

        [InputField, OutputField, FilterField]
        public string FixedAssetState { get; set; }

        [InputField, OutputField, FilterField]
        public string Comments { get; set; }

        [ForeignKeyField(typeof(Shared.Employee), "Identifier"), InputField, OutputField, FilterField]
        public Shared.Employee Responsible { get; set; }

        [ForeignKeyField(typeof(PhysicalInventoryTaking), "Identifier"), InputField, OutputField, FilterField]
        public PhysicalInventoryTaking PhysicalInventoryTaking { get; set; }
    }
}
