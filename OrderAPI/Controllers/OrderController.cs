﻿using KafkaPublisher.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMessagePubisher _messagePubisher;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger, IMessagePubisher messagePubisher)
        {
            _logger = logger;
            _messagePubisher = messagePubisher;
        }

        [HttpPost("createorder")]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            //some db operation to create an order
            var logMessage = "Created {order}" + JsonConvert.SerializeObject(order);

            //publish the item in the confluent topic
            await _messagePubisher.PublishAsync("application-logs", logMessage);

            //doing the payment operation
            logMessage = "Payment succeded {order}" + JsonConvert.SerializeObject(order);

            await _messagePubisher.PublishAsync("application-logs", logMessage);

            return Ok();
        }
    }
}
