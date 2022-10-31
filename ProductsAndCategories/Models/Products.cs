using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAndCategories.Models
{
    public class Products
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string ImgURL { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("CategoryID")]
        public Category Category { get; set; }
        public int CategoryID { get; set; }


    }
}
