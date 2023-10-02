using Microsoft.AspNetCore.Mvc;
using service;
using Infrastructure.Models;

namespace API.Controllers;

[ApiController]
public class BoxController : ControllerBase
{
    private readonly Service _service;
    
    public BoxController(Service service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("api/catalogue")]
    public IEnumerable<Box> GetBoxes()
    {
        return _service.GetAllBoxes();
    }

    [HttpPost]
    [Route("api/boxes")]
    public Box PostBox([FromBody] Box box)
    {
        return _service.PostBox(box);
    }
}