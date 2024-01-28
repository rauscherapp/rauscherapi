using Application.Interfaces;
using AutoMapper;
using Domain.Core.Bus;
using Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/test")]
    [Produces("application/json", "application/xml")]
    [Consumes("application/json", "application/xml")]
    [Authorize]
    public class Controller : ApiController
    {
        private readonly IMapper _mapper;
        private readonly IPropertyCheckerService _propertyCheckerService;

        public Controller(INotificationHandler<DomainNotification> notifications,
            IMediatorHandler bus,
            IMapper mapper,
            IPropertyCheckerService propertyCheckerService)
            : base(notifications, bus)
        {
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            _propertyCheckerService = propertyCheckerService ?? throw new ArgumentException(nameof(propertyCheckerService));
        }

        /// <summary>
        /// Apenas teste do template
        /// </summary>
        /// <returns>Um ActionResult de teste</returns>
        [HttpGet(Name = "Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public IActionResult Get()
        {
            if (!ModelStateValid()) return ResponseAction();
            return ResponseAction("Tudo funcionando");

        }

        private bool ModelStateValid()
        {
            if (ModelState.IsValid) return true;
            NotifyModelStateErrors();
            return false;
        }
    }
}