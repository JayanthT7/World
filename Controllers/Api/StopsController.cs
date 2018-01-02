using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Services;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers.Api
{
    [Authorize]
    [Route("api/trips/{tripName}/stops")]
    public class StopsController :Controller
    {
        private GeoCoordsService _coordsService;
        private ILogger<StopsController> _logger;
        private IWorldRepository _repository;

        public StopsController(IWorldRepository repository,ILogger<StopsController> logger,GeoCoordsService coordsService )
        {
            _repository = repository;
            _logger = logger;
            _coordsService = coordsService;
        }
        [HttpGet("")]
        public IActionResult Get(string tripName)
        {
            try
            {
                var trip = _repository.GetUserTripByName(tripName,User.Identity.Name);
                return Ok(Mapper.Map<IEnumerable<StopViewModel>>(trip.Stops.OrderBy(s =>s.Order).ToList()));
            }
            catch(Exception ex)
            {
                _logger.LogError("Failed to get Stops:{0}",ex);
            }
            return BadRequest("Failed to get Stops");
        }
        [HttpPost("")]
        public async Task<IActionResult> Post(string tripName,[FromBody] StopViewModel vm)
        {
            try
            {
                var newStop = Mapper.Map<Stop>(vm);
                //Look up geocodes

                var result = await _coordsService.GeoCoordsAsync(newStop.Name); 
                if(!result.Success)
                {
                    _logger.LogError(result.Message);
                }
                else
                {

                    newStop.Latitude = result.latitude;
                    newStop.Longitude = result.longitude;

                    //Save to the database
                    _repository.AddStop(tripName, newStop,User.Identity.Name);
                if(await _repository.SaveChangesAsync())
                {
                    return Created($"/api/trips/{tripName}/stops/{newStop.Name})",
                    Mapper.Map<StopViewModel>(newStop));
                }
                }

            }
            catch(Exception ex)
            {
                _logger.LogError("Failed to get Stops:{0}", ex);
            }
            return BadRequest("Failed to get Stops");
        }

    }
}
