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
    [Route("/api/catalogue")]
    public IEnumerable<Box> GetBoxes()
    {
        return _service.GetAllBoxes();
    }

    [HttpPost]
    [Route("/api/boxes")]
    public Box PostBox([FromBody] Box box)
    {
        return _service.PostBox(box);
    }

    [HttpPut]
    [Route("/api/boxes/{boxId}")]
    public Box UpdateBox([FromBody] Box box, [FromRoute] int boxId)
    {
        return _service.UpdateBox(box, boxId);
    }

    [HttpDelete]
    [Route("/api/deleteBox/{boxId}")]
    public void DeleteBox([FromRoute] int boxId)
    {
        _service.DeleteBox(boxId);
    }

    [HttpGet]
    [Route("/api/boxes")]
    public IEnumerable<Box> searchBox([FromQuery] string searchTerm)
    {
        return _service.SearchBox(searchTerm);
    }

    [HttpGet]
    [Route("/api/boxes/{boxId}")]
    public Box GetBox([FromRoute] int boxId)
    {
        return _service.getBox(boxId);
    }
}