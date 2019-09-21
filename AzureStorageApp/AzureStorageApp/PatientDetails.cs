using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorageApp
{
    public class PatientDetails: TableEntity
    {
        public PatientDetails(string patientid, string area, string age, string healthcompliment)
        {
            this.age = age;
            this.area = area;
            this.healthcompliant = healthcompliant;
            this.patientid = patientid;
        }
        public PatientDetails()
        {

        }
        public  string patientid { get; set; }

        public  string area { get; set; }

        public string age { get; set; }

        public string healthcompliant { get; set; }
    }
}
