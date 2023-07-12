using System;
using MagicVilla_Web.models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVilla_Web.models.VM
{
	public class VillaNumberCreateVM
	{
		public VillaNumberCreateVM()
		{
            // we will use VillaNumber in CreateVillaNumber
			// why?
			// because we give it the access to villa list
            VillaNumber = new VillaNumberCreateDTO();
		}
		public VillaNumberCreateDTO VillaNumber { get; set; }
		[ValidateNever]
		// we will keep in VillaList -> Id and name of villas
		public IEnumerable<SelectListItem> VillaList { get; set; }
    }
}

