using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAP.Middleware.Connector;
using System.Configuration;
using Microsoft.Win32;

namespace SOFTTEK.SAP.Integration
{
    public class SAPConnection
    {
        #region Constants

        private const string kSAPConnectionRegistryKey = @"SOFTWARE\Wow6432Node\{0}\{1}\{2}";
        private const string kSAPConnectionStringSeparator = "|";
        private const string kSAPConnectionStringErrorUnableToExtract = "|00|||102|ES|10|0|EDA|";
        private const string kSAPServiceProvider = "SOFTTEK";
        private const string kSAPDestinationKeyFormatting = "SAP_CONNECTION_{0}_{1}";


        //Registry Settings Keys
        private const string kRegSettingKeyApplicationServerHost = "ashost";
        private const string kRegSettingKeySystemNumber = "sysnr";
        private const string kRegSettingKeyUser = "User";
        private const string kRegSettingKeyClient = "Client";
        private const string kRegSettingKeyPassword = "Password";
        private const string kRegSettingKeySystemID = "Destination";
        private const string kRegSettingKeyRouter = "Router";

        //AppSettings Keys
        private const string kAppSettingsKeyEnv = "Environment";
        private const string kAppSettingsKeySAPDefaultDestination = "Default_SAP_Destination";
        private const string kAppSettingsKeySAPVersion = "SAP_Version";
        private const string kAppSettingsKeySAPCustomerVersionID = "Customer_SAP_Version_id";
        private const string kAppSettingsKeySAPCustomerInstanceID = "Customer_SAP_Instance_id";
        private const string kAppSettingsKeySAPLanguage = "Customer_SAP_Language";
        private const string kAppSettingsKeySAPMaxPoolSize = "Customer_SAP_Max_Pool_Size";
        private const string kAppSettingsKeySAPIdleTimeout = "Customer_SAP_idle_Timeout";

        // SAP Versions
        private const string kSAPVersion6 = "6.0";
        private const string kSAPVersion6Alias = "60";
        private const string kSAPVersion4_9 = "4.9";
        private const string kSAPVersion4_9Alias = "49";

        // SAP Environment Key Identifiers
        private const string kEnvironmentQA = "QA";
        private const string kEnvironmentDEV = "DEV";
        private const string kEnvironmentPROD = "PROD";
        
        #endregion

        #region Helpers
        /// <summary>
        /// Returns the resulting encoded value of a provided string by a applying an encoding algorithm. 
        /// </summary>
        /// <param name="value">Value to encode.</param>
        /// <returns>Encoded Value</returns>
        private string Encode(string value)
        {

            char[] chars;
            int intCaracter;
            string encodedValue;

            chars = value.ToCharArray();
            encodedValue = "";

            for (int i = 0; i < chars.Length; i++)
            {
                intCaracter = ((int)chars[i]) + (i + 1);
                encodedValue = (char)intCaracter + encodedValue;
            }
            return value;

        }


        /// <summary>
        /// Returns the resulting decoded value of a provided string by a applying an decoding algorithm.
        /// </summary>
        /// <param name="value">Value to decode.</param>
        /// <returns>Decoded Value</returns>
        private string Decode(string value)
        {

            char[] chars;
            int intCaracter;

            string decodedValue;

            chars = value.ToCharArray();
            decodedValue = "";

            for (int i = (chars.Length - 1); i >= 0; i--)
            {
                intCaracter = ((int)chars[i]) - (chars.Length - i);
                decodedValue = decodedValue + (char)intCaracter;
            }

            return decodedValue;
        } 
        #endregion



        /// <summary>
        /// Retrieves the System Configuration Connection String to stablish the a SAP Connection
        /// </summary>
        /// <returns>SAP Connection String</returns>
        public string SAPConnectionString() { 

            string serviceProvider = ConfigurationManager.AppSettings.Get(kSAPServiceProvider);
            string customerSAPVersionID = ConfigurationManager.AppSettings.Get(kAppSettingsKeySAPCustomerVersionID);
            string customerSAPInstanceID = ConfigurationManager.AppSettings.Get(kAppSettingsKeySAPCustomerInstanceID);
            string customerSAPLanguage = ConfigurationManager.AppSettings.Get(kAppSettingsKeySAPLanguage);
            string customerSAPMaxPoolSize = ConfigurationManager.AppSettings.Get(kAppSettingsKeySAPMaxPoolSize);
            string customerSAPIdleTimeout = ConfigurationManager.AppSettings.Get(kAppSettingsKeySAPIdleTimeout);

            if (string.IsNullOrEmpty(serviceProvider))
            {
                throw new Exception("Please setup the [SAP Service Provider] at the application configuration.");
            }

            if (string.IsNullOrEmpty(customerSAPVersionID))
            {
                throw new Exception("Please setup the [SAP Customer Version ID] at the application configuration.");
            }

            if (string.IsNullOrEmpty(customerSAPInstanceID))
            {
                throw new Exception("Please setup the [SAP Customer Version ID] at the application configuration.");
            }

            if (string.IsNullOrEmpty(customerSAPLanguage))
            {
                throw new Exception("Please setup the [SAP Connection Language] at the application configuration.");
            }

            if (string.IsNullOrEmpty(customerSAPMaxPoolSize))
            {
                throw new Exception("Please setup the [SAP Connection Max Pool Size] at the application configuration.");
            }

            if (string.IsNullOrEmpty(customerSAPIdleTimeout))
            {
                throw new Exception("Please setup the [SAP Connection Idle Timeout] at the application configuration.");
            }


            RegistryKey regKey = Registry.LocalMachine;
            regKey = regKey.OpenSubKey(string.Format(kSAPConnectionRegistryKey, serviceProvider, customerSAPVersionID, customerSAPInstanceID), false);

            if (regKey != null)
            {
                String[] subKeys = regKey.GetValueNames();
                //ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString
                var AppServerHost = regKey.GetValue(kRegSettingKeyApplicationServerHost).ToString();
                var SystemNumber = regKey.GetValue(kRegSettingKeySystemNumber).ToString();
                var User = regKey.GetValue(kRegSettingKeyUser).ToString();
                var Password = Decode(regKey.GetValue(kRegSettingKeyPassword).ToString());
                var Client = regKey.GetValue(kRegSettingKeyClient).ToString();
                //var Language = "ES";
                //var MaxPoolSize = "10";
                //var IdleTimeout = "600";
                var SystemID = regKey.GetValue(kRegSettingKeySystemID).ToString();
                var Router = string.IsNullOrWhiteSpace(regKey.GetValue(kRegSettingKeyRouter).ToString()) ? string.Empty : regKey.GetValue("Router").ToString();

                //"172.19.141.34|00|CO_WSOFTTEK|99rkyxiqT|102|ES|10|0|EDA|/H/181.48.221.21/S/3299/H/"

                string result = String.Join(kSAPConnectionStringSeparator, 
                    AppServerHost, 
                    SystemNumber, 
                    User, 
                    Password, 
                    Client, 
                    customerSAPLanguage, 
                    customerSAPMaxPoolSize, 
                    customerSAPIdleTimeout, 
                    SystemID, 
                    Router
                );

                return result;
            }
            else
            {
                return (kSAPConnectionStringErrorUnableToExtract);
            }
        }

        /// <summary>
        /// Create a RFC Destination Instance with the cofigurated settings.
        /// </summary>
        /// <returns>RfcDestination instance.</returns>
        public RfcDestination LoadDestination(){

            try
            {
                RfcDestination rfcDestination;

                string environment = ConfigurationManager.AppSettings.Get(kAppSettingsKeyEnv);
                string sapVersion = ConfigurationManager.AppSettings.Get(kAppSettingsKeySAPVersion);
                string sapDefaultDestination = ConfigurationManager.AppSettings.Get(kAppSettingsKeySAPDefaultDestination);

                if (string.IsNullOrEmpty(environment))
                {
                    environment = kEnvironmentQA;
                }

                if (string.IsNullOrEmpty(sapVersion))
                {
                    throw new Exception("Please setup the target SAP Version at the application configuration.");
                }

                if (string.IsNullOrEmpty(sapDefaultDestination))
                {
                    throw new Exception("Please setup the information of the default SAP Destination at the application configuration.");
                }

                switch (sapVersion)
                {
                    case kSAPVersion6:
                        string destinationName = string.Format(kSAPDestinationKeyFormatting, kSAPVersion6Alias, environment);
                        rfcDestination = RfcDestinationManager.GetDestination(destinationName);
                        break;
                    case kSAPVersion4_9:
                        throw new Exception("Unsupported SAP Version.");
                    default:
                        rfcDestination = RfcDestinationManager.GetDestination(sapDefaultDestination);
                        break;
                }

                return rfcDestination;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                System.Diagnostics.Debug.Print(ex.InnerException.Message);

                return null;
            }
        }
    }
}
