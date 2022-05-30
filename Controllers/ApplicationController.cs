using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;

namespace project10.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class ApplicationController : ControllerBase
    {
        // protected readonly NpgsqlConnection connect;
        protected readonly string strConnect;

        public ApplicationController(IConfiguration configuration)
        {
            // this.connect = new NpgsqlConnection(configuration.GetConnectionString("PGConnection"));
            // this.connect.Open();
           
            this.strConnect = configuration.GetConnectionString("PGConnection");
        }

    }
}