using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BuildExeServices.Models;
using System.Transactions;
using BuildExeServices.Repository;
using System.ComponentModel.Design;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SyncTablesController : ControllerBase
    {
        private readonly ISyncTablesRepository _syncTablesRepository;
        public SyncTablesController(ISyncTablesRepository SyncTablesRepository)
        {
            _syncTablesRepository = SyncTablesRepository;
        }

        [HttpPut("{tableName}")]
        [Authorize]
        public async Task<IActionResult> Post(string tableName, [FromQuery] DateTime syncDate, [FromBody] JsonElement jsonData)
        {
            // Convert JsonElement to string
            string jsonString = jsonData.GetRawText();

            // Action value for insert
            const int action = 1;

            // Call the repository method
            await _syncTablesRepository.Insert(tableName, jsonString, syncDate, action);

            return Ok($"{tableName} inserted successfully with SyncDate: {syncDate}.");
        }





        [HttpPost("{tableName}")]
        [Authorize]
        public async Task<IActionResult> Put(string tableName, DateTime syncDate, [FromBody] JsonElement jsonData)
        {
            // Convert JsonElement to string
            string jsonString = jsonData.GetRawText();

            // Action value for insert
            const int action = 1;

            // Call the repository method
            await _syncTablesRepository.Update(tableName, jsonString, syncDate, action);

            return Ok($"{tableName} Updated successfully with SyncDate: {syncDate}.");
        }

    }
}
