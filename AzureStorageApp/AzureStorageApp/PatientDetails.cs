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
        public  string patientId { get; set; }

        public  string area { get; set; }

        public string age { get; set; }

        public string healthCompliant { get; set; }
    }
}
