﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SiccarCodeTest.Interfaces.Domain;
using SiccarCodeTest.Repositories;
using SiccarCodeTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiccarCodeTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaxController : ControllerBase
    {
        private readonly ILogger<TaxController> _logger;
        private readonly IRepository<Vehicle> _repository;

        public TaxController(ILogger<TaxController> logger, IRepository<Vehicle> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// Registers a vehicle to the repository
        /// </summary>
        /// <param name="vehicle">The vehicle that will be stored</param>
        /// <returns name="vehicle">Returns the stored vehicle with the vehicles total tax.</returns>
        [HttpPost("register-vehicle")]
        public async Task<Vehicle> RegisterVehicleAsync(Vehicle vehicle)
        {
            await _repository.InsertOrUpdate(vehicle.Registration, vehicle);
            return await _repository.GetById(vehicle.Registration));
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Registers multiple vehicles to the repository
        /// </summary>
        /// <param name="vehicles">The list of vehicles to store</param>
        /// <returns name="vehicle">Returns the stored vehicles with the vehicles total tax.</returns>
        [HttpPost("register-vehicles")]
        public async Task<List<Vehicle>> RegisterVehiclesAsync(List<Vehicle> vehicles)
        {
            //TODO
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns all registered vehicles
        /// </summary>
        [HttpGet]
        public async Task<List<Vehicle>> GetVehiclesAsync()
        {
            //TODO
            throw new NotImplementedException();
        }
    }
}
