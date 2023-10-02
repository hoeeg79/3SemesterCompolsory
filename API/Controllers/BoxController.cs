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

    /**
     * A method used to get all boxes
     */
    [HttpGet]
    [Route("/api/catalogue")]
    public IEnumerable<Box> GetBoxes()
    {
        return _service.GetAllBoxes();
    }

    /**
     * A method used to create boxes
     */
    [HttpPost]
    [Route("/api/boxes")]
    public Box PostBox([FromBody] Box box)
    {
        return _service.PostBox(box);
    }

    /**
     * A method used to update boxes
     */
    [HttpPut]
    [Route("/api/boxes/{boxId}")]
    public Box UpdateBox([FromBody] Box box, [FromRoute] int boxId)
    {
        return _service.UpdateBox(box, boxId);
    }

    /**
     * A method used to delete boxes
     */
    [HttpDelete]
    [Route("/api/deletebox/{boxId}")]
    public void DeleteBox([FromRoute] int boxId)
    {
        _service.DeleteBox(boxId);
    }

    /**
     * A method used to search for boxes
     */
    [HttpGet]
    [Route("/api/boxes")]
    public IEnumerable<Box> searchBox([FromQuery] string searchTerm)
    {
        return _service.SearchBox(searchTerm);
    }

    /**
     * A method used to search for a specific box
     */
    [HttpGet]
    [Route("/api/boxes/{boxId}")]
    public Box GetBox([FromRoute] int boxId)
    {
        return _service.getBox(boxId);
    }
}