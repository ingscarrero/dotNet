﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SF.DataModel;

namespace SOFTTEK.SCMS.Entity.FA
{
    public class PurchaseRequest : Request
    {

        [InputField, OutputField, FilterField]
        public string ExternalIdentifier { get; set; }

        [ForeignKeyField(typeof(FixedAsset), "Identifier"), InputField, OutputField, FilterField]
        public FixedAsset FixedAsset { get; set; }
        
        [ForeignKeyField(typeof(NoveltyRequest), "Identifier"), InputField, OutputField, FilterField]
        public NoveltyRequest Novelty { get; set; }
    }
}
