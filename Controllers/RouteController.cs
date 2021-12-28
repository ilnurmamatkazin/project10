using Microsoft.AspNetCore.Mvc;
using System;
using project10.Models;
using project10.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Npgsql;


namespace project10.Controllers
{
    [ApiController]
    [Route("api/routes")]
    public class RouteController : ApplicationController
    {
        private RouteRepository _routeRepository;
        public RouteController(IConfiguration configuration) : base(configuration)
        {
            _routeRepository = new Repositories.RouteRepository(this.connect);;
        }



        [HttpPost]
        public IActionResult Create([FromBody] Route troutes)
        {
            try
            {
                _routeRepository.Create(troutes);

                return StatusCode(201, null);
            }
            catch (Exception err)
            {
                return StatusCode(500, err);
            }
        }


        [HttpGet]
        public IActionResult List([FromQuery] int typeroutes_id)
        {
            try
            {
                JArray tr = _routeRepository.List(typeroutes_id);
                return StatusCode(200, tr);
            }
            catch (Exception err)
            {
                return StatusCode(500, err);
            }
        }

       [HttpGet("{id}")]
        public IActionResult Show([FromRoute] int id)
        {
            try
            {
                JToken tr = _routeRepository.Show(id);
                return StatusCode(200, tr);
            }
            catch (Exception err)
            {
                return StatusCode(500, err);
            }
        }

   
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _routeRepository.Delete(id);

                return StatusCode(204, null);
            }
            catch (Exception err)
            {
                return StatusCode(500, err);
            }
        }

    }
}