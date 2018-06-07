namespace Products.Domain
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        [Required(ErrorMessage = "The field {0} is Required.")]
        [MaxLength(50, ErrorMessage = "The faild {0} only can contain {1} character lenght.")]
        [Index("Product_description_Index", IsUnique = true)]
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "The field {0} is Required.")]
        public decimal Price { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        public string Image { get; set; }

        public double Stock { get; set; }

        [Display(Name = "Last Purchase")]
        public DateTime LastPurchase { get; set; }
            

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }


        //Relations
        [JsonIgnore]
        public virtual  Category Category { get; set; }
    }
}
