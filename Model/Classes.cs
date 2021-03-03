using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace QBS.API.Example.Model
{
    public class Company
    {
        public Guid id { get; set; }
        public string systemVersion { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string businessProfileId { get; set; }
        public DateTime systemCreatedAt { get; set; }
        public string systemCreatedBy { get; set; }
        public DateTime systemModifiedAt { get; set; }
        public string systemModifiedBy { get; set; }
    }

    public class Customer
    {
        public Guid id { get; set; }
        public string number { get; set; }
        public string displayName { get; set; }
        public string type { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string postalCode { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string website { get; set; }
        public bool taxLiable { get; set; }
        public Guid taxAreaId { get; set; }
        public string taxRegistrationNumber { get; set; }
        public Guid currencyId { get; set; }
        public string currencyCode { get; set; }
        public Guid paymentTermsId { get; set; }
        public Guid shipmentMethodId { get; set; }
        public Guid paymentMethodId { get; set; }
        public string blocked { get; set; }

    }
    public class Credentials
    {
        public string UserId { get; set; } = "Your User Here";
        public string AccessKey { get; set; } = "Your Access Key from Business Central"
    }
    public class APIEndpoint
    {
        public string tenantId { get; set; } = "your tenant ID here";
        public string companyId { get; set; } = "your company ID here";
        public string apiPublisher { get; set; } = "api";
        public string apiGroup { get; set; } = "";
        public string apiVersion { get; set; } = "v2.0";
        public string apiEndpoint { get; set; } = "";
        public string sandBoxName { get; set; } = "sandbox";

        public String getURI()
        {
            String uri = "";
            if (apiEndpoint == "")
            {
                uri = (String.Format("https://api.businesscentral.dynamics.com/v2.0/{0}/{3}/{1}/{2}/companies",
                    tenantId, apiPublisher, apiVersion, sandBoxName));
            }
            else
            {
                if (apiGroup == "")
                    uri = (String.Format("https://api.businesscentral.dynamics.com/v2.0/{0}/{5}/{1}/{2}/companies({3})/{4}",
                        tenantId, apiPublisher, apiVersion, companyId, apiEndpoint, sandBoxName));
                else
                    uri = (String.Format("https://api.businesscentral.dynamics.com/v2.0/{0}/{6}/{1}/{2}/{3}/companies({4})/{5}",
                    tenantId, apiPublisher, apiGroup, apiVersion, companyId, apiEndpoint, sandBoxName));
            }
            return uri;
        }
    }
}
