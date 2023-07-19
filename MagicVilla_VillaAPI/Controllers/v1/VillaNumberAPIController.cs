using System;
using System.Data;
using System.Net;
using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.models;
using MagicVilla_VillaAPI.models.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.v1.Controllers
{
    //important:
    [Route("api/v{version:apiVersion}/VillaNumberAPI")] // or use [Route("api/[controller]")]
    [ApiController]
    // in program.cs file i have as defaulte version is (1.0) ,
    // but i can make other versions for me (example: in mutliple versions)
    // then i will add here new line for the new version '[ApiVersion("2.0")]' ,'[ApiVersion("1.0")]':
    [ApiVersion("1.0")]
    //[ApiVersion("1.0",Deprecated=true)] // Deprecated=true to show the depracated version
    public class VillaNumberAPIController : ControllerBase
    {

        // connection the DB to the controller and make mapper
        protected APIResponse _response;
        private readonly IVillaNumberRepository _dbVillaNumber;
        // to check the villa ID exists or not
        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper;
        public VillaNumberAPIController(IVillaNumberRepository dbVillaNumber, IMapper mapper , IVillaRepository dbVilla)
        {
            _dbVillaNumber = dbVillaNumber;
            _mapper = mapper;
            _dbVilla = dbVilla;
            this._response = new();
        }

        // if i didnot write 'GetString' i will get error
        // because i will be have 2 (HttpGet) functions
        [HttpGet("GetString")]
        public IEnumerable<string> Get()
        {
            return new string[] { "olala1", "olala2" };
        }

        [HttpGet]
        //[MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVillaNumbers()
        {
            try
            {
                IEnumerable<VillaNumber> villaNumberList = await _dbVillaNumber.GetAllAsync(includeProperties:"Villa");
                _response.Result = _mapper.Map<List<VillaNumberDTO>>(villaNumberList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [HttpGet("{id:int}", Name = "GetVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // i cant say (int villaNo) , i ((must)) write (int id):
        public async Task<ActionResult<APIResponse>> GetVillaNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                // when i was use villaNo instead of 'id' that make error in actions!>.....
                var villaNumber = await _dbVillaNumber.GetAsync(u => u.VillaNo == id);
                if (villaNumber != null)
                {
                    _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
                    _response.StatusCode = HttpStatusCode.OK;
                    return Ok(_response);
                }
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody] VillaNumberCreateDTO CreateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (await _dbVilla.GetAsync(u => u.Id == CreateDTO.VillaId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Villa ID is Invalid!");
                    return BadRequest(ModelState);
                }
                // villa Exists
                if (await _dbVillaNumber.GetAsync(u => u.VillaNo == CreateDTO.VillaNo) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Villa Number already Exists!");
                    return BadRequest(ModelState);
                }

                if (CreateDTO == null)
                {
                    return BadRequest(CreateDTO);
                }

                // villa is the destination and createDto source
                VillaNumber villaNumber = _mapper.Map<VillaNumber>(CreateDTO);

                await _dbVillaNumber.CreateAsync(villaNumber);
                _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVillaNumber", new { id = villaNumber.VillaNo }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [HttpDelete("{id:int}", Name = "DeleteVillaNumber")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // i deleted the 'I' from ActionResult
        public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var villaNumber = await _dbVillaNumber.GetAsync(u => u.VillaNo == id);
                if (villaNumber != null)
                {
                    await _dbVillaNumber.RemoveAsync(villaNumber);
                    _response.StatusCode = HttpStatusCode.NoContent;
                    _response.IsSuccess = true;
                    return Ok(_response);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut("{id:int}",Name="UpdateVillaNumber")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateVillaNumber(int id, [FromBody] VillaNumberUpdateDTO UpdateDTO)
        {
            try
            {
                if (UpdateDTO == null || UpdateDTO.VillaNo != id)
                {
                    return BadRequest();
                }
                if (await _dbVilla.GetAsync(u => u.Id == UpdateDTO.VillaId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Villa ID is Invalid!");
                    return BadRequest(ModelState);
                }
                VillaNumber model = _mapper.Map<VillaNumber>(UpdateDTO);

                await _dbVillaNumber.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string>() { ex.ToString() };
            }
            return _response;

        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVillaNumber")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVillaNumber(int id, JsonPatchDocument<VillaNumberUpdateDTO> patchDTO)
        {
            var villaNumber = await _dbVillaNumber.GetAsync(u => u.VillaNo == id, tracked: false);

            VillaNumberUpdateDTO villaNumberDTO = _mapper.Map<VillaNumberUpdateDTO>(villaNumber);

            if (patchDTO == null || 0 == id || villaNumber == null)
            {
                return BadRequest();
            }

            patchDTO.ApplyTo(villaNumberDTO, ModelState);

            VillaNumber model = _mapper.Map<VillaNumber>(villaNumberDTO);

            await _dbVillaNumber.UpdateAsync(model);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}

