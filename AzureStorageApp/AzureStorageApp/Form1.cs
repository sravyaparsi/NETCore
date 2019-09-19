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
      
        private void button1_Click(object sender, EventArgs e)
        {
            StorageCredentials storageCred = new StorageCredentials("storageazureacc", "yuj+FvoDV8GQ7HANr9SfF+avWO+egRuS/p5+F+DUV0tdnSoxveRhHNEI+5LtwA391NMZQqj0fB59KslNng4sRQ==");

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
            StorageCredentials storageCred = new StorageCredentials("storageazureacc", "yuj+FvoDV8GQ7HANr9SfF+avWO+egRuS/p5+F+DUV0tdnSoxveRhHNEI+5LtwA391NMZQqj0fB59KslNng4sRQ==");


            CloudStorageAccount cloudstorageacc = new CloudStorageAccount(storageCred, true);

            var client = cloudstorageacc.CreateCloudQueueClient();

            var queue = client.GetQueueReference("storagequeue");

            queue.CreateIfNotExists();
            queue.AddMessage(new CloudQueueMessage(textBox2.Text));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StorageCredentials storageCred = new StorageCredentials("storageazureacc", "yuj+FvoDV8GQ7HANr9SfF+avWO+egRuS/p5+F+DUV0tdnSoxveRhHNEI+5LtwA391NMZQqj0fB59KslNng4sRQ==");

            CloudStorageAccount cloudStorageAccount = new CloudStorageAccount(storageCred, true);

            var client = cloudStorageAccount.CreateCloudTableClient();

            var table = client.GetTableReference("patientdetails");
            table.CreateIfNotExists();
            PatientDetails patient = new PatientDetails();
            patient.patientId = textBox3.Text;
            patient.area = textBox4.Text;
            patient.age = textBox5.Text;
            patient.healthCompliant = textBox6.Text;
            patient.PartitionKey = "patient";
            patient.RowKey = "j";
           
            TableOperation tableOPeration = TableOperation.InsertOrReplace(patient);
            table.Execute(tableOPeration);
            
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
