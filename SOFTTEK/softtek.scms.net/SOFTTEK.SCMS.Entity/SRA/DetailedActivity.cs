using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SCMS.Entity.SRA
{
    public enum DETAILED_ACTIVITY_FIELDS
	{
	    STAGE = 1,
        DEV_ACT = 2,
        REQ = 3
	}

    public abstract class DetailedActivity : Activity
    {
        const string stageFieldName = "Stage";
        const string developmentActivityFieldName = "DevelopmentActivity";
        const string requirementFieldName = "Requirement";

        private string stage;
        private string developmentActivity;
        private string requirement;
        protected string details;

        public string Stage { 
            get { return String.IsNullOrEmpty(stage) ? (!String.IsNullOrEmpty(details) ? GetDetailField(DETAILED_ACTIVITY_FIELDS.STAGE) : string.Empty) : stage; } 
            set { stage = value; } 
        }
        public string DevelopmentActivity { 
            get { return String.IsNullOrEmpty(developmentActivity) ? (!String.IsNullOrEmpty(details) ? GetDetailField(DETAILED_ACTIVITY_FIELDS.DEV_ACT) : string.Empty) : developmentActivity; } 
            set { developmentActivity = value; } 
        }
        public string Requirement { 
            get { return String.IsNullOrEmpty(requirement) ? (!String.IsNullOrEmpty(details) ? GetDetailField(DETAILED_ACTIVITY_FIELDS.REQ) : string.Empty) : requirement; } 
            set { requirement = value; } 
        }
        public override string Details
        {
            get
            {
                return JoinDetails();
            }
            set
            {
                details = value;
            }
        }

        protected virtual string JoinDetails()
        {

            string result = details;
            
            if (!string.IsNullOrEmpty(stage))
            {
                string _result = String.IsNullOrEmpty(result) ? 
                    string.Empty : 
                    (!String.IsNullOrEmpty(GetDetailField(DETAILED_ACTIVITY_FIELDS.STAGE)) ? 
                        result.Replace(GetDetailField(DETAILED_ACTIVITY_FIELDS.STAGE), string.Empty) : 
                        result
                    ); 
                result = string.Format("{0}:{1}|{2}", stageFieldName, stage, _result);
            }

            if (!string.IsNullOrEmpty(developmentActivity))
            {
                string _result = String.IsNullOrEmpty(result) ?
                    string.Empty :
                    (!String.IsNullOrEmpty(GetDetailField(DETAILED_ACTIVITY_FIELDS.DEV_ACT)) ?
                        result.Replace(GetDetailField(DETAILED_ACTIVITY_FIELDS.DEV_ACT), string.Empty) :
                        result
                    );
                result = string.Format("{0}:{1}|{2}", developmentActivityFieldName, developmentActivity,_result);
            }

            if (!string.IsNullOrEmpty(requirement))
            {
                string _result = String.IsNullOrEmpty(result) ?
                    string.Empty :
                    (!String.IsNullOrEmpty(GetDetailField(DETAILED_ACTIVITY_FIELDS.REQ)) ?
                        result.Replace(GetDetailField(DETAILED_ACTIVITY_FIELDS.REQ), string.Empty) :
                        result
                    );
                result = string.Format("{0}:{1}|{2}", requirementFieldName, requirement, _result);
            }

            return result;
        }



        protected string GetDetailField(DETAILED_ACTIVITY_FIELDS field)
        {
            string result = string.Empty;
            switch (field)
            {
                case DETAILED_ACTIVITY_FIELDS.STAGE:
                    result = ExtractFieldFromDetails(stageFieldName);
                    break;
                case DETAILED_ACTIVITY_FIELDS.DEV_ACT:
                    result = ExtractFieldFromDetails(developmentActivityFieldName);
                    break;
                case DETAILED_ACTIVITY_FIELDS.REQ:
                    result = ExtractFieldFromDetails(requirementFieldName);
                    break;
                default:
                    throw new NotSupportedException("The provided [DETAILED_ACTIVITY_FIELDS] is not supported.");
            }

            return result;
        }

        protected virtual string ExtractFieldFromDetails(string fieldName) {
            throw new NotImplementedException();
        }

    }
}
