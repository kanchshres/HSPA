using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class PropertyController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public PropertyController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        
        // .../api/property/list/_
        [HttpGet("list/{sellOrRent}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyList(int sellOrRent)
        {
            var properties = await uow.PropertyRepository.GetPropertiesAsync(sellOrRent);
            var propertyListDTO = mapper.Map<IEnumerable<PropertyListDTO>>(properties);
            return Ok(propertyListDTO);
        }

        // .../api/property/detail/_
        [HttpGet("detail/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyDetail(int id)
        {
            var property = await uow.PropertyRepository.GetPropertyDetailAsync(id);
            var propertyDTO = mapper.Map<PropertyDetailDTO>(property);
            return Ok(propertyDTO);
        }

        // .../api/property/add
        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddProperty(PropertyDTO propertyDTO)
        {
            var property = mapper.Map<Property>(propertyDTO);
            var userID = GetUserID();
            property.PostedBy = userID;
            property.LastUpdatedBy = userID;
            uow.PropertyRepository.AddProperty(property);
            await uow.SaveAsync();
            return StatusCode(201);
        }
    }
}