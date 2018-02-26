using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities.Products
{
    public class Image
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public byte[] Data { set; get; }
        public int Length { set; get; }
        public int Width { set; get; }
        public int Height { set; get; }
        public string ContentType { set; get; }

        public Product Product { set; get; }
        public int ProductId { set; get; }
    }
}
