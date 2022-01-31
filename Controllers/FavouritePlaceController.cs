using Microsoft.AspNetCore.Mvc;
using System;
using project10.Models;
using project10.Repositories;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;

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

        /// <summary>
        /// Избранные места.
        /// </summary>
        /// <remarks>
        /// Данный метод реализует создание списка избранных мест
        /// </remarks>
        /// <param name="body"></param>
        /// <returns>New Created Todo Item</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="500">If the item is incorrect</response>
        /// [HttpPost]
        [ProducesResponseType(typeof(FavouritePlace), 201)]
        

        [HttpPost]
        public IActionResult Create([FromBody] FavouritePlace fp)
        {
             try
            {
                _fpRepository.Create(fp);

                return StatusCode(201, fp);
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
        public IActionResult Update([FromBody] FavouritePlace fp)
        {
            try
            {
                _fpRepository.Update(fp); 
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
