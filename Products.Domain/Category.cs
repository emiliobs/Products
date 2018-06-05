using System;
using System.Collections.Generic;
namespace Products.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage ="The field {0} is Required.")]
        [MaxLength(50, ErrorMessage ="The faild {0} only can contain {1} character lenght.")]
        [Index("Category_description_Index", IsUnique = true)]
        public string Description { get; set; }
    }
}
