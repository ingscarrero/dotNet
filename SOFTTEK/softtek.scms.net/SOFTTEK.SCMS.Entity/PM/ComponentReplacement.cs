using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFTTEK.SF.DataModel;

namespace SOFTTEK.SCMS.Entity.PM
{
    public class ComponentReplacement
    {
        [PrimaryKeyField, OutputField, FilterField]
        public long Identifier { get; set; }

        [InputField, OutputField, FilterField]
        public string ExternalIdentifier { get; set; }

        [ForeignKeyField(typeof(Task), "Identifier"), InputField, OutputField, FilterField]
        public Task Task { get; set; }

        [ForeignKeyField(typeof(Material), "Identifier"), InputField, OutputField, FilterField]
        public Material Material { get; set; }
        
        [ForeignKeyField(typeof(Material), "Identifier"), InputField, OutputField, FilterField]
        public Material ReplacedMaterial { get; set; }

        [InputField, OutputField, FilterField]
        public string Comments { get; set; }
    }
}
