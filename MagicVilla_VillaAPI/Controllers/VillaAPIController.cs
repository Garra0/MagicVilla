using System;
using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.models;
using MagicVilla_VillaAPI.models.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Controllers
{
    //important:
    [Route("api/VillaAPI")] // or use [Route("api/[controller]")]
    //[ApiController]
    public class VillaAPIController : ControllerBase
    {

        // connection the DB to the controller and make mapper
        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper; 
        public VillaAPIController(IVillaRepository dbVilla, IMapper mapper)
        {
            _dbVilla = dbVilla;
            _mapper = mapper;
        }


        //use the Villa class in model folder:
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            //IEnumerable<Villa> villaList = await _db.Villas.ToListAsync();
            // _db.Villas.ToListAsync() == _dbVilla.GetAll() because:
            // the GetAll functions do the other check ...
            IEnumerable<Villa> villaList = await _dbVilla.GetAllAsync();

            return Ok(_mapper.Map<List<VillaDTO>>(villaList));
        }

        [HttpGet("id", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VillaDTO>> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa =await _dbVilla.GetAsync(u => u.Id == id);
            if (villa != null)
            {
                return Ok(_mapper.Map<VillaDTO>(villa));
            }
            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody] VillaCreateDTO CreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // villa Exists
            if (await _dbVilla.GetAsync(u => u.Name.ToLower() == CreateDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Villa already Exists!");
                return BadRequest(ModelState);
            }

            if (CreateDTO == null)
            {
                return BadRequest(CreateDTO);
            }
            
            // villa is the destination and createDto source
            Villa model = _mapper.Map<Villa>(CreateDTO);

            await _dbVilla.CreateAsync(model);

            return CreatedAtRoute("GetVilla", new { id = model.Id }, model);
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa =await _dbVilla.GetAsync(u => u.Id == id);
            if (villa != null)
            {
                // remove fun here need to have 'await' because i made this function work like that , in it there are "Async"
                await _dbVilla.RemoveAsync(villa);
                return NoContent();
            }
            return NotFound();
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDTO UpdateDTO)
        {
            if (UpdateDTO == null || UpdateDTO.Id != id)
            {
                return BadRequest();
            }

            Villa model = _mapper.Map<Villa>(UpdateDTO);

            await _dbVilla.UpdateAsync(model);
            
            return NoContent();

        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla(int id ,JsonPatchDocument<VillaUpdateDTO> patchDTO)
        {
            var villa = await _dbVilla.GetAsync(u => u.Id == id , tracked:false);

            VillaUpdateDTO villaDTO = _mapper.Map<VillaUpdateDTO>(villa);

            if (patchDTO == null || 0 == id || villa == null)
            {
                return BadRequest();
            }

            patchDTO.ApplyTo(villaDTO, ModelState);

            Villa model = _mapper.Map<Villa>(villaDTO);

            await _dbVilla.UpdateAsync(model);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}

