using System;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.models.Dto
{
	public class VillaNumberUpdateDTO
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int VillaId { get; set; }
        public string SpecialDetails { get; set; }

    }
}

