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
    [Route("api/typeRoutes")]
    public class TypeRouteController : ApplicationController
    {
        private TypeRouteRepository _typeRouteRepository;

        public TypeRouteController(IConfiguration configuration) : base(configuration)
        {
            _typeRouteRepository = new Repositories.TypeRouteRepository(this.connect);
        }

        [HttpGet]
        public IActionResult List()
        {
            try
            {
                JArray tr = _typeRouteRepository.List();

                return StatusCode(200, tr);
            }
            catch (Exception err)
            {
                return StatusCode(500, err);
            }
        }


        [HttpPost]
        public IActionResult Create([FromBody] TypeRoute tr)
        {
            try
            {
                _typeRouteRepository.Create(tr);

                return StatusCode(201, null);
            }
            catch (Exception err)
            {
                return StatusCode(500, err);
            }
        }



        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] TypeRoute tr)
        {
            try
            {
                _typeRouteRepository.Update(id, tr); 
                return StatusCode(204, null);
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
                JToken tr = _typeRouteRepository.Show(id);
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

                _typeRouteRepository.Delete(id);

                return StatusCode(204, null);
            }
            catch (Exception err)
            {
                // swith по эксепшену
                return StatusCode(500, err);
            }
        }

    }
}