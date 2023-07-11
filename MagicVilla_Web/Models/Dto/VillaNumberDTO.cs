using System;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.models.Dto
{
    // why i have VillaNumberCreateDTO and VillaNumberUpateDTO
    // why i just have 'VillaNumberDTO'
    // maybe in the future i will have some thing dont let
    // me use just VillaNumberDTO and i need 2 other classes
    // then i write the other now thatis beter...
    public class VillaNumberDTO
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int VillaId { get; set; }
        public string SpecialDetails { get; set; }
        public VillaDTO Villa { get; set; }

    }
}

