using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_VillaAPI.models
{
	public class VillaNumber
	{
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int VillaNo { get; set; }
        // make the relationship between villa and villaNumber using FK
        // VillaId is FK for Villa class
        [ForeignKey("Villa")] //[ForeignKey(nameof(Villa))]
        public int VillaId { get; set; }
        //  to keep the villa obj in the villaNo
        public Villa Villa { get; set; }
        public string SpecialDetails { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}

