
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
    [Route("api/users")]
    public class UserController : ApplicationController
    {
        private UserRepository _userRepository;
        public UserController(IConfiguration configuration) : base(configuration)
        {
            _userRepository = new Repositories.UserRepository(this.strConnect);;
        }


//registration
        [HttpPost]
        [SwaggerOperation(
        Summary = "Создать учетную запись",
        Description = "Данный метод реализует создание учетной записи. Данные приходят в теле запроса.",
        Tags = new[] { "user - регисрация и вход пользователя" }
        )]
        [Consumes( MediaTypeNames.Application.Json )]
        // [ConsumesAttribute( "application/json", new string[] {"application/json"})]
        // [SwaggerResponse(StatusCodes.Status201Created, typeof(api.Anomaly), Description = "Successfull operation", new string[] {"application/json"})]
        [SwaggerResponseAttribute(StatusCodes.Status201Created, "Новый пользователь сохранен в системе")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в теле запроса")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка выполнения метода")]
        public IActionResult Registration([FromBody] Users user)
        {
            try
            {
                Console.WriteLine(user);
                int userId = _userRepository.Registration(user);

                return StatusCode(201, userId);
            }
            catch (Exception err)
            {
                return StatusCode(500, err);
            }
        }


//login
        [HttpPut]
        [SwaggerOperation(
        Summary = "Войти в учетную запись",
        Description = "Данный метод реализует вход в учетную запись. Данные приходят в теле запроса.",
        Tags = new[] { "user - регисрация и вход пользователя" }
        )]
        [Consumes( MediaTypeNames.Application.Json )]
        // [ConsumesAttribute( "application/json", new string[] {"application/json"})]
        // [SwaggerResponse(StatusCodes.Status201Created, typeof(api.Anomaly), Description = "Successfull operation", new string[] {"application/json"})]
        [SwaggerResponseAttribute(StatusCodes.Status201Created, "Пользователь вошел в систему")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в теле запроса")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка выполнения метода")]
        public IActionResult Login([FromBody] UserLogin user)
        {
            try
            {

                int userId = _userRepository.Login(user.login, user.password);
                if(userId == 0){
                    return StatusCode(401, "Пользователь не прошел авторизацию");
                }

                Console.WriteLine(userId);


                return StatusCode(200, userId);
            }
            catch (Exception err)
            {
                return StatusCode(500, err);
            }
        }
    }
}