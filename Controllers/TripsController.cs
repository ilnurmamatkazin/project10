using Microsoft.AspNetCore.Mvc;
using System;
using project10.Models;
using project10.Repositories;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;



namespace project10.Controllers
{
  [Route("api/trips")]
  public class TripsController : ApplicationController
  {
    private TripsRepository _tripsRepository;

    public TripsController(IConfiguration configuration) : base(configuration)
    {
      _tripsRepository = new Repositories.TripsRepository(this.connect);
    }

    [HttpGet()]
    [SwaggerOperation(
    Summary = "Получить список всех трекеров",
    Description = "Данный метод формирует список из json объектов с реквизитами всех трекеров.",
    Tags = new[] { "trips - трекер пользователя" }
    )]
    [Consumes(MediaTypeNames.Application.Json)]
    // [ConsumesAttribute( "application/json", new string[] {"application/json"})]
    // [SwaggerResponse(StatusCodes.Status201Created, typeof(api.Anomaly), Description = "Successfull operation", new string[] {"application/json"})]
    [SwaggerResponseAttribute(StatusCodes.Status200OK, "Реквизиты списка", typeof(TypeRoute), new string[] { "application/json" })]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в строке запроса")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка выполнения метода")]
    public IActionResult List([FromQuery] int user_id, [FromQuery] DateTime dates)
    {
      try
      {
        DateTime date = dates.Date;
        JArray tr = _tripsRepository.List(user_id, date);

        return StatusCode(200, tr);
      }
      catch (Exception err)
      {
        return StatusCode(500, err);
      }
    }


    [HttpPost]
    [SwaggerOperation(
    Summary = "Создать трекер",
    Description = "Данный метод реализует создание трекера. Данные приходят в теле запроса.",
    Tags = new[] { "trips - трекер пользователя" }
    )]
    [Consumes(MediaTypeNames.Application.Json)]
    // [ConsumesAttribute( "application/json", new string[] {"application/json"})]
    // [SwaggerResponse(StatusCodes.Status201Created, typeof(api.Anomaly), Description = "Successfull operation", new string[] {"application/json"})]
    [SwaggerResponseAttribute(StatusCodes.Status201Created, "Новый маршрут сохранен в системе")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в теле запроса")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка выполнения метода")]
    public IActionResult Create([FromBody] Trips tr)
    {
      try
      {
        _tripsRepository.Create(tr);

        return StatusCode(201, null);
      }
      catch (Exception err)
      {
        return StatusCode(500, err);
      }
    }



    // [HttpPut("{id}")]
    // [SwaggerOperation(
    // Summary = "Изменить выбранный трекер",
    // Description = "Данный метод реализует изменение трекера. Данные приходят в теле запроса.",
    // Tags = new[] { "trips - трекер пользователя" }
    // )]
    // [Consumes(MediaTypeNames.Application.Json)]
    // // [ConsumesAttribute( "application/json", new string[] {"application/json"})]
    // // [SwaggerResponse(StatusCodes.Status201Created, typeof(api.Anomaly), Description = "Successfull operation", new string[] {"application/json"})]
    // [SwaggerResponseAttribute(StatusCodes.Status204NoContent, "Тип маршрута обновлен в системе")]
    // [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в теле запроса")]
    // [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка выполнения метода")]
    // public IActionResult Update([FromRoute] int id, [FromBody] Trips tr)
    // {

    //   Console.WriteLine(id);
    //   try
    //   {
    //     _tripsRepository.Update(id, tr);
    //     return StatusCode(204, null);
    //   }
    //   catch (Exception err)
    //   {
    //     return StatusCode(500, err);
    //   }
    // }

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Получить реквизиты выбранного трекера",
        Description = "Данный метод формирует json объект с реквизитами выбранного трекера.",
        Tags = new[] { "trips - трекер пользователя" }
    )]
    [Consumes(MediaTypeNames.Application.Json)]
    // [ConsumesAttribute( "application/json", new string[] {"application/json"})]
    // [SwaggerResponse(StatusCodes.Status201Created, typeof(api.Anomaly), Description = "Successfull operation", new string[] {"application/json"})]
    [SwaggerResponseAttribute(StatusCodes.Status200OK, "Реквизиты выбранного типа маршрута", typeof(TypeRoute), new string[] { "application/json" })]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в строке запроса")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка выполнения метода")]
    public IActionResult Show([FromRoute] int id)
    {
      try
      {
        JToken tr = _tripsRepository.Show(id);
        return StatusCode(200, tr);
      }
      catch (Exception err)
      {
        return StatusCode(500, err);
      }
    }


    [HttpDelete("{id}")]
    [SwaggerOperation(
    Summary = "Удалить выбранный трекер",
    Description = "Данный метод удаляет json объект с реквизитами выбранного трекера.",
    Tags = new[] { "trips - трекер пользователя" }
    )]
    [Consumes(MediaTypeNames.Application.Json)]
    // [ConsumesAttribute( "application/json", new string[] {"application/json"})]
    // [SwaggerResponse(StatusCodes.Status201Created, typeof(api.Anomaly), Description = "Successfull operation", new string[] {"application/json"})]
    [SwaggerResponseAttribute(StatusCodes.Status200OK, "Реквизиты выбранного типа маршрута", typeof(TypeRoute), new string[] { "application/json" })]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в строке запроса")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка выполнения метода")]

    public IActionResult Delete([FromRoute] int id)
    {
      try
      {

        _tripsRepository.Delete(id);

        return StatusCode(204, null);
      }
      catch (Exception err)
      {
        return StatusCode(500, err);
      }
    }

  }
}