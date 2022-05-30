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
  [Route("api/favourites")]
  [ApiController]
  public class FavouritePlacecontroller : ApplicationController
  {
    private FavouritePlaceRepository _fpRepository;
    public FavouritePlacecontroller(IConfiguration configuration) : base(configuration)
    {
      _fpRepository = new Repositories.FavouritePlaceRepository(this.strConnect);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Создать избранное место",
        Description = "Данный метод реализует создание избранного места. Данные приходят в теле запроса.",
        Tags = new[] { "favourites - избранные места пользователя" }
    )]
    [Consumes( MediaTypeNames.Application.Json )]
    [SwaggerResponseAttribute(StatusCodes.Status201Created, "Новое избранное место сохранено в системе")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в теле запроса")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка выполнения метода")]
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

    [HttpGet()]
    [SwaggerOperation(
        Summary = "Получить список избранных мест",
        Description = "Данный метод формирует список из json объектов с реквизитами избранных мест.",
        Tags = new[] { "favourites - избранные места пользователя" }
    )]
    [Consumes( MediaTypeNames.Application.Json )]
    // [ConsumesAttribute( "application/json", new string[] {"application/json"})]
    // [SwaggerResponse(StatusCodes.Status201Created, typeof(api.Anomaly), Description = "Successfull operation", new string[] {"application/json"})]
    [SwaggerResponseAttribute(StatusCodes.Status200OK, "Реквизиты списка", typeof(FavouritePlace), new string[] {"application/json"})]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в строке запроса")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка выполнения метода")]
    public IActionResult List([FromQuery] int user_id)
    {
      try
      {
        JArray tr = _fpRepository.List(user_id);

        return StatusCode(200, tr);
      }
      catch (Exception err)
      {
        return StatusCode(500, err);
      }
    }


    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Удалить избранное место",
        Description = "Данный метод удаляет json объект с реквизитами избранного места.",
        Tags = new[] { "favourites - избранные места пользователя" }
    )]
    [Consumes( MediaTypeNames.Application.Json )]
    // [ConsumesAttribute( "application/json", new string[] {"application/json"})]
    // [SwaggerResponse(StatusCodes.Status201Created, typeof(api.Anomaly), Description = "Successfull operation", new string[] {"application/json"})]
    [SwaggerResponseAttribute(StatusCodes.Status200OK, "Реквизиты избранного места", typeof(FavouritePlace), new string[] {"application/json"})]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в строке запроса")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка выполнения метода")]
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
    [SwaggerOperation(
        Summary = "Изменить избранное место",
        Description = "Данный метод реализует Изменение избранного места. Данные приходят в теле запроса.",
        Tags = new[] { "favourites - избранные места пользователя" }
    )]
    [Consumes( MediaTypeNames.Application.Json )]
    // [ConsumesAttribute( "application/json", new string[] {"application/json"})]
    // [SwaggerResponse(StatusCodes.Status201Created, typeof(api.Anomaly), Description = "Successfull operation", new string[] {"application/json"})]
    [SwaggerResponseAttribute(StatusCodes.Status204NoContent, "Избранное место обновленно в системе")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в теле запроса")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка выполнения метода")]
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
    [SwaggerOperation(
        Summary = "Получить реквизиты избранного места",
        Description = "Данный метод формирует json объект с реквизитами избранного места.",
        Tags = new[] { "favourites - избранные места пользователя" }
    )]
    [Consumes( MediaTypeNames.Application.Json )]
    // [ConsumesAttribute( "application/json", new string[] {"application/json"})]
    // [SwaggerResponse(StatusCodes.Status201Created, typeof(api.Anomaly), Description = "Successfull operation", new string[] {"application/json"})]
    [SwaggerResponseAttribute(StatusCodes.Status200OK, "Реквизиты избранного места", typeof(FavouritePlace), new string[] {"application/json"})]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в строке запроса")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка выполнения метода")]
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
