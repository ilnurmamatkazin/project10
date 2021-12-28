using Microsoft.AspNetCore.Mvc;
using System;
using project10.Models;
using project10.Repositories;
// using Newtonsoft.Json;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace project10.Controllers
{
    [ApiController]
    [Route("api/favourites")]

    public class FavouritePlacecontroller : ApplicationController
    {
        private FavouritePlaceRepository _fpRepository;
        public FavouritePlacecontroller(IConfiguration configuration) : base(configuration)
        {
            _fpRepository =  new Repositories.FavouritePlaceRepository(this.connect);
        }

        [HttpPost]
        public IActionResult Create([FromBody] FavouritePlace fp)
        {
            Console.WriteLine("#####");

            var serializeOptions = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = true
};

            Console.WriteLine(JsonSerializer.Serialize(fp.Point, serializeOptions));


            try
            {
                //_fpRepository.Create(fp);

                return StatusCode(201, null);
            }
            catch (Exception err)
            {
                return StatusCode(500, err);
            }
        }

        [HttpGet]
        public IActionResult List()
        {
            try
            {
                JArray tr = _fpRepository.List();

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

                _fpRepository.Delete(id);

                return StatusCode(204, null);
            }
            catch (Exception err)
            {
                // swith по эксепшену
                return StatusCode(500, err);
            }
        }


        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] FavouritePlace fp)
        {
            try
            {
                _fpRepository.Update(id, fp); 
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
                JToken tr = _fpRepository.Show(id);
                return StatusCode(200, tr);
            }
            catch (Exception err)
            {
                return StatusCode(500, err);
            }
        }


    }
}
