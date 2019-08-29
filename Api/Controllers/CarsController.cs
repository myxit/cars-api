using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AntilopaApi.Data;
using AntilopaApi.Models;
using AntilopaApi.Services;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace AntilopaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly CarService _carService;
        private readonly IMapper _mapper;

        public CarsController(ApplicationDbContext context, CarService carService, IMapper mapper)
        {
            this._context = context;
            this._carService = carService;
            this._mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<CarViewModel[]>> Get()
        {
            var cars = await _context.Cars.ToArrayAsync();

            return this._mapper.Map<Car[], CarViewModel[]>(cars); 
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<CarViewModel>> Get(int id)
        {
            var data = await _context.Cars.FindAsync(id);
            if (data == null) {
                return NotFound();
            }

            return this._mapper.Map<Car, CarViewModel>(data);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Car>> Post([FromBody] CarInputModel inputModel)
        {
            var insResult = await this._carService.InsertAsync(inputModel);
            if (!insResult.isSuccess) {
                return BadRequest();
            }

            return CreatedAtAction(nameof(Get), this._mapper.Map<Car, CarViewModel>(insResult.Item2));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromRoute] int id, [FromBody] CarInputModel inputModel)
        {
            var updResult = await this._carService.UpdateAsync(id, inputModel);
            if (!updResult.isSuccess) {
                return BadRequest();
            }

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Car>> Delete(int id)
        {
            var findRes = await this._context.Cars.FindAsync(id);
            if (findRes == null) {
                return NotFound();
            }

            this._context.Cars.Remove(findRes);
            await this._context.SaveChangesAsync();
            return NoContent();            
        }
    }
}
