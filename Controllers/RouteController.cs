
using Microsoft.AspNetCore.Mvc;
using System;
using project10.Models;
using project10.Repositories;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;
using Npgsql;
using Microsoft.AspNetCore.Http;
using NpgsqlTypes;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Net.Mime;

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
        [SwaggerOperation(
        Summary = "Создать маршрут",
        Description = "Данный метод реализует создание маршрута. Данные приходят в теле запроса.",
        Tags = new[] { "routes - все маршруты пользователя" }
        )]
        [Consumes( MediaTypeNames.Application.Json )]
        // [ConsumesAttribute( "application/json", new string[] {"application/json"})]
        // [SwaggerResponse(StatusCodes.Status201Created, typeof(api.Anomaly), Description = "Successfull operation", new string[] {"application/json"})]
        [SwaggerResponseAttribute(StatusCodes.Status201Created, "Новый маршрут сохранен в системе")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в теле запроса")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка выполнения метода")]
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
         [SwaggerOperation(
        Summary = "Получить список всех маршрутов",
        Description = "Данный метод формирует список из json объектов с реквизитами всех маршрутов.",
        Tags = new[] { "routes - все маршруты пользователя" }
        )]
        [Consumes( MediaTypeNames.Application.Json )]
        // [ConsumesAttribute( "application/json", new string[] {"application/json"})]
        // [SwaggerResponse(StatusCodes.Status201Created, typeof(api.Anomaly), Description = "Successfull operation", new string[] {"application/json"})]
        [SwaggerResponseAttribute(StatusCodes.Status200OK, "Реквизиты списка", typeof(Route), new string[] {"application/json"})]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в строке запроса")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка выполнения метода")]
        public IActionResult List()
        {
            try
            {
                JArray tr = _routeRepository.List();
                return StatusCode(200, tr);
            }
            catch (Exception err)
            {
                return StatusCode(500, err);
            }
        }

       [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Получить реквизиты выбранного маршрута",
            Description = "Данный метод формирует json объект с реквизитами выбранного машрута.",
            Tags = new[] { "routes - все маршруты пользователя" }
        )]
        [Consumes( MediaTypeNames.Application.Json )]
        // [ConsumesAttribute( "application/json", new string[] {"application/json"})]
        // [SwaggerResponse(StatusCodes.Status201Created, typeof(api.Anomaly), Description = "Successfull operation", new string[] {"application/json"})]
        [SwaggerResponseAttribute(StatusCodes.Status200OK, "Реквизиты выбранного маршрута", typeof(Route), new string[] {"application/json"})]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в строке запроса")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка выполнения метода")]
        public IActionResult Show([FromRoute] int id, [FromQuery] int distance, [FromQuery] int time, [FromQuery] int level)
        {
            Console.WriteLine(id);
            try
            {
                JToken tr = _routeRepository.Show(id, distance, time,  level);
                return StatusCode(200, tr);
            }
            catch (Exception err)
            {
                return StatusCode(500, err);
            }
        }


        [HttpPut("{id}")]
         [SwaggerOperation(
        Summary = "Изменить выбранный маршрут",
        Description = "Данный метод реализует изменение маршрута. Данные приходят в теле запроса.",
        Tags = new[] { "routes - все маршруты пользователя" }
        )]
        [Consumes( MediaTypeNames.Application.Json )]
        // [ConsumesAttribute( "application/json", new string[] {"application/json"})]
        // [SwaggerResponse(StatusCodes.Status201Created, typeof(api.Anomaly), Description = "Successfull operation", new string[] {"application/json"})]
        [SwaggerResponseAttribute(StatusCodes.Status204NoContent, "Маршрут обновлен в системе")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в теле запроса")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка выполнения метода")]
        public IActionResult Update([FromRoute] int id, [FromBody] Route r)
        {
            try
            {
                _routeRepository.Update(id, r); 
                return StatusCode(204, null);
            }
            catch (Exception err)
            {
                return StatusCode(500, err);
            }
        }

   
        [HttpDelete("{id}")]
        [SwaggerOperation(
        Summary = "Удалить выбранный маршрут",
        Description = "Данный метод удаляет json объект с реквизитами выбранного маршрута.",
        Tags = new[] { "routes - все маршруты пользователя" }
        )]
        [Consumes( MediaTypeNames.Application.Json )]
        // [ConsumesAttribute( "application/json", new string[] {"application/json"})]
        // [SwaggerResponse(StatusCodes.Status201Created, typeof(api.Anomaly), Description = "Successfull operation", new string[] {"application/json"})]
        [SwaggerResponseAttribute(StatusCodes.Status200OK, "Реквизиты выбранного маршрута", typeof(Route), new string[] {"application/json"})]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в строке запроса")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка выполнения метода")]
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