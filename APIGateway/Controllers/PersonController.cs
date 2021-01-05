using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIGateway.Models;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIGateway.Controllers
{
    [Route("person-api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        [Authorize(Policy = "person.read")]
        // GET: api/<PersonController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PersonController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [Authorize(Policy = "person.write")]
        // POST api/<PersonController>
        [HttpPost]
        public IActionResult Post([FromBody] Person model)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "person.queue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                PersonMessage personMessage = new PersonMessage(model, "post");

                string message = JsonConvert.SerializeObject(personMessage);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "person.queue",
                                     basicProperties: null,
                                     body: body);
            }
            return Ok();
        }

        // PUT api/<PersonController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PersonController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
