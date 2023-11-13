using f1_api.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace f1_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriverController : ControllerBase
    {
        private readonly ILogger<DriverController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly List<Driver> _drivers;

        public DriverController(ILogger<DriverController> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
            _drivers = new List<Driver>
            {
                new Driver { Id = 1, Name = "Driver1", Age = 25, Nationality = "Country1" },
                new Driver { Id = 2, Name = "Driver2", Age = 28, Nationality = "Country2" }
            };
        }

        [HttpGet(Name = "GetAllDrivers")]
        public IEnumerable<Driver> Get()
        {
            return _drivers;
        }

        [HttpGet("{id}", Name = "GetDriverById")]
        public IActionResult GetById(int id)
        {
            var driver = _drivers.FirstOrDefault(d => d.Id == id);

            if (driver == null)
                return NotFound();

            return Ok(driver);
        }

        [HttpGet("GetByName/{name}", Name = "GetDriverByName")]
        public IActionResult GetByName(string name)
        {
            var driver = _drivers.FirstOrDefault(d => d.Name == name);

            if (driver == null)
                return NotFound();

            return Ok(driver);
        }

        [NonAction]
        private string GetFilePath(string DriverName)
        {
            return _environment.WebRootPath + "\\Uploads\\Drivers\\" + DriverName;
        }

        [HttpPost("CreateAsync", Name = "CreateDriverAsync")]
        public async Task<IActionResult> CreateAsync(Driver driver)
        {
            bool flag = false;
            try
            {
                driver.Id = _drivers.Max(d => d.Id) + 1;

                var _uploadedFiles = Request.Form.Files;
                foreach (var source in _uploadedFiles)
                {
                    string Filename = source.FileName;
                    string Filepath = GetFilePath(Filename);
                    if (!Directory.Exists(Filepath))
                    {
                        Directory.CreateDirectory(Filepath);
                    }
                    string imagePath = Filepath + "\\image.png";
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                    using (FileStream stream = System.IO.File.Create(imagePath))
                    {
                        await source.CopyToAsync(stream);
                        flag = true;
                    }
                }

                _drivers.Add(driver);
            }
            catch (Exception e)
            {

            }
            return CreatedAtRoute("GetDriverById", new { id = driver.Id }, driver);
        }

        [HttpPost(Name = "CreateDriver")]
        public IActionResult Create(Driver driver)
        {
            driver.Id = _drivers.Max(d => d.Id) + 1;

            _drivers.Add(driver);

            return CreatedAtRoute("GetDriverById", new { id = driver.Id }, driver);
        }

        [HttpPut("{id}", Name = "UpdateDriver")]
        public IActionResult Update(int id, Driver updatedDriver)
        {
            var existingDriver = _drivers.FirstOrDefault(d => d.Id == id);

            if (existingDriver == null)
                return NotFound();

            existingDriver.Name = updatedDriver.Name;
            existingDriver.Age = updatedDriver.Age;
            existingDriver.Nationality = updatedDriver.Nationality;

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteDriver")]
        public IActionResult Delete(int id)
        {
            var driver = _drivers.FirstOrDefault(d => d.Id == id);

            if (driver == null)
                return NotFound();

            _drivers.Remove(driver);

            return NoContent();
        }
    }
}
