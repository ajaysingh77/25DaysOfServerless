using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Day3.Models
{
    public class ImageEntity : TableEntity
    {
        public ImageEntity(string imageName)
        {
            PartitionKey = imageName;
            RowKey = imageName;
        }
        public string ImageUrl { get; set; }
    }
}
