using System;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.models.Dto
{
	public class VillaNumberCreateDTO
    {
        [Required]
        public int VillaNo { get; set; }
        public string SpecialDetails { get; set; }

    }
}

