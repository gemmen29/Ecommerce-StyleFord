using BL.AppServices;
using BL.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : Controller
    {
        ColorAppService _colorAppService;

        public ColorController(ColorAppService colorAppService)
        {
            this._colorAppService = colorAppService;
        }

        [HttpGet]
        public IActionResult GetAllColors()
        {
            return Ok(_colorAppService.GetAllColors());
        }
        [HttpGet("{id}")]
        public IActionResult GetColorById(int id)
        {
            return Ok(_colorAppService.GetColor(id));
        }

        [HttpPost]
        public IActionResult Create(ColorDTO colorDTO)
        {

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _colorAppService.SaveNewColor(colorDTO);
                
                //string urlDetails = Url.Link("DefaultApi", new { id = colorDTO.ID });
                //return Created(urlDetails, "Added Sucess");
                return Created("CreateColor" , colorDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, ColorDTO colorDTO)
        {

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _colorAppService.UpdateColor(colorDTO);
                return Ok(colorDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _colorAppService.DeleteColor(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("count")]
        public IActionResult ColorsCount()
        {
            return Ok(_colorAppService.CountEntity());
        }
        [HttpGet("{pageSize}/{pageNumber}")]
        public IActionResult GetColorsByPage(int pageSize, int pageNumber)
        {
            return Ok(_colorAppService.GetPageRecords(pageSize, pageNumber));
        }

    }
}
