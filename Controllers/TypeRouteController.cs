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
    [Route("api/typeRoutes")]
    public class TypeRouteController : ApplicationController
    {
        private TypeRouteRepository _typeRouteRepository;

        public TypeRouteController(IConfiguration configuration) : base(configuration)
        {
            _typeRouteRepository = new Repositories.TypeRouteRepository(this.strConnect);
        }

        [HttpGet]
         [SwaggerOperation(
        Summary = "Получить список всех типов маршрутов",
        Description = "Данный метод формирует список из json объектов с реквизитами всех типов маршрутов.",
        Tags = new[] { "typeRoutes - типы маршрутов пользователя" }
        )]
        [Consumes( MediaTypeNames.Application.Json )]
        // [ConsumesAttribute( "application/json", new string[] {"application/json"})]
        // [SwaggerResponse(StatusCodes.Status201Created, typeof(api.Anomaly), Description = "Successfull operation", new string[] {"application/json"})]
        [SwaggerResponseAttribute(StatusCodes.Status200OK, "Реквизиты списка", typeof(TypeRoute), new string[] {"application/json"})]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в строке запроса")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка выполнения метода")]
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
        [SwaggerOperation(
        Summary = "Создать тип маршрута",
        Description = "Данный метод реализует создание типа маршрута. Данные приходят в теле запроса.",
        Tags = new[] { "typeRoutes - типы маршрутов пользователя" }
        )]
        [Consumes( MediaTypeNames.Application.Json )]
        // [ConsumesAttribute( "application/json", new string[] {"application/json"})]
        // [SwaggerResponse(StatusCodes.Status201Created, typeof(api.Anomaly), Description = "Successfull operation", new string[] {"application/json"})]
        [SwaggerResponseAttribute(StatusCodes.Status201Created, "Новый маршрут сохранен в системе")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в теле запроса")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка выполнения метода")]
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
         [SwaggerOperation(
        Summary = "Изменить выбранный тип маршрута",
        Description = "Данный метод реализует изменение типа маршрута. Данные приходят в теле запроса.",
        Tags = new[] { "typeRoutes - типы маршрутов пользователя" }
        )]
        [Consumes( MediaTypeNames.Application.Json )]
        // [ConsumesAttribute( "application/json", new string[] {"application/json"})]
        // [SwaggerResponse(StatusCodes.Status201Created, typeof(api.Anomaly), Description = "Successfull operation", new string[] {"application/json"})]
        [SwaggerResponseAttribute(StatusCodes.Status204NoContent, "Тип маршрута обновлен в системе")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в теле запроса")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка выполнения метода")]
        public IActionResult Update([FromRoute] int id, [FromBody] TypeRoute tr)
        {

            Console.WriteLine(id);
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
         [SwaggerOperation(
            Summary = "Получить реквизиты выбранного типа маршрута",
            Description = "Данный метод формирует json объект с реквизитами выбранного типа маршрута.",
            Tags = new[] { "typeRoutes - типы маршрутов пользователя" }
        )]
        [Consumes( MediaTypeNames.Application.Json )]
        // [ConsumesAttribute( "application/json", new string[] {"application/json"})]
        // [SwaggerResponse(StatusCodes.Status201Created, typeof(api.Anomaly), Description = "Successfull operation", new string[] {"application/json"})]
        [SwaggerResponseAttribute(StatusCodes.Status200OK, "Реквизиты выбранного типа маршрута", typeof(TypeRoute), new string[] {"application/json"})]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в строке запроса")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка выполнения метода")]
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
        [SwaggerOperation(
        Summary = "Удалить выбранный тип маршрута",
        Description = "Данный метод удаляет json объект с реквизитами выбранного типа маршрута.",
        Tags = new[] { "typeRoutes - типы маршрутов пользователя" }
        )]
        [Consumes( MediaTypeNames.Application.Json )]
        // [ConsumesAttribute( "application/json", new string[] {"application/json"})]
        // [SwaggerResponse(StatusCodes.Status201Created, typeof(api.Anomaly), Description = "Successfull operation", new string[] {"application/json"})]
        [SwaggerResponseAttribute(StatusCodes.Status200OK, "Реквизиты выбранного типа маршрута", typeof(TypeRoute), new string[] {"application/json"})]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в строке запроса")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка выполнения метода")]

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