using SOFTTEK.SF.DataModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SCMS.Entity.PM
{
    public enum TechnicalObjectTypes
    {
        TechnicalObjectTypeTechnicalLocation = 0,
        TechnicalObjectTypeEquipment = 1
    }

    public class TechnicalObject : Shared.Asset
    {
        public const string kTypeTechnicalLocation = "TL_obj";
        public const string kTypeEquipment = "EQ_obj";

        private static readonly string[] kTOTStrings = { kTypeTechnicalLocation, kTypeEquipment };

        [InputField, OutputField, FilterField]
        public string Name { get; set; }

        [InputField, OutputField, FilterField]
        public string Type { get { return kTOTStrings[(int)TOType]; } }

        public TechnicalObjectTypes TOType { get; set; }

        public TechnicalObject TechnicalLocation { get; set; }

        public List<Measure> Measures { get; set; }

        public static TechnicalObjectTypes ParseTechnicalObjectTypeToEnum(string type)
        {
            return (TechnicalObjectTypes)Array.IndexOf(kTOTStrings, type);
        }
    }
}