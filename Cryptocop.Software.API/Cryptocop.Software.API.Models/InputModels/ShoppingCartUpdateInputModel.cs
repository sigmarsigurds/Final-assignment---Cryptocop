using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cryptocop.Software.API.Models.InputModels
{
    public class ShoppingCartUpdateInputModel
    {
        [Required]
        [Range(0.01, float.MaxValue)]
        public float? Quantity { get; set; }
    }
}