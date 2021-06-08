using System.ComponentModel.DataAnnotations;

namespace EComm.Model
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        [Range(1.00, 500.00)]
        public decimal? UnitPrice { get; set; }

        public string Package { get; set; }

        public bool IsDiscontinued { get; set; }

        public int SupplierId { get; set; }

        public Supplier Supplier { get; set; }
    }
}
