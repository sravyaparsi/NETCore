using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzureStorageApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

          private void label1_Click(object sender, EventArgs e)
        {

        }
        StorageCredentials storageCred = new StorageCredentials("parsistorage", "3BWzjimrDzUrCERaN0f7A/hl9cF0mlfEzRwkuQB1UiP0vhzPYKn0oJv42VLC8zoV+ihyHiYDkZJVzeBjy484Sw==");

        //Upload to blob
        private void button1_Click(object sender, EventArgs e)
        {
           
            FileStream fs = new FileStream(textBox1.Text, FileMode.Open);

            CloudStorageAccount cloudstorageacc = new CloudStorageAccount(storageCred, true);

            CloudBlobClient cloudBlobClient = cloudstorageacc.CreateCloudBlobClient();

            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("images");

            cloudBlobContainer.CreateIfNotExists();

            string filename = Path.GetFileName(textBox1.Text);

            CloudBlockBlob cloudBlockBlobs = cloudBlobContainer.GetBlockBlobReference(filename);

            cloudBlockBlobs.UploadFromStream(fs);
            fs.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
             

            CloudStorageAccount cloudstorageacc = new CloudStorageAccount(storageCred, true);

            var client = cloudstorageacc.CreateCloudQueueClient();

            var queue = client.GetQueueReference("storagequeue");

            queue.CreateIfNotExists();
            queue.AddMessage(new CloudQueueMessage(textBox2.Text));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=parsistorage;AccountKey=3BWzjimrDzUrCERaN0f7A/hl9cF0mlfEzRwkuQB1UiP0vhzPYKn0oJv42VLC8zoV+ihyHiYDkZJVzeBjy484Sw==;EndpointSuffix=core.windows.net";

            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(storageConnectionString);

            var client = cloudStorageAccount.CreateCloudTableClient();

            var table = client.GetTableReference("patientdetail");
            table.CreateIfNotExists();
            PatientDetails patient = new PatientDetails(textBox3.Text,textBox4.Text,textBox5.Text,textBox6.Text);
           
            patient.PartitionKey = "patient";
            patient.RowKey = patient.patientid;
           
            TableOperation tableOPeration = TableOperation.Insert(patient);
            table.Execute(tableOPeration);
            
        }
        private void button4_Click(object sender, EventArgs e)
        {
            CloudStorageAccount cloudstorageacc = new CloudStorageAccount(storageCred, true);

            var cloudtable = cloudstorageacc.CreateCloudTableClient();
            var table = cloudtable.GetTableReference("patientdetail");
            table.CreateIfNotExists();
            TableQuery<PatientDetails> tablequery = new TableQuery<PatientDetails>().Where(
                    TableQuery.CombineFilters(TableQuery.GenerateFilterCondition("patientid", QueryComparisons.Equal, textBox3.Text), TableOperators.And,
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "patient")
                ));

            var patientdetails = table.ExecuteQuery(tablequery);

            if (patientdetails.Any())
            {
                label7.Text = "patientId found";
                
            }
            else
            {
                label7.Text = "patientId not found";
            }

        }
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

      

        private void Form1_Load(object sender, EventArgs e)
        {

        }

       
    }
}
