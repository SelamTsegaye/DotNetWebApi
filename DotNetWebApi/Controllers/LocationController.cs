using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotNetWebApi.Models;

namespace DotNetWebApi.Controllers;

[Route("api/")]
[ApiController]
public class LocationController : ControllerBase
{
    private readonly TeddyBearsContext _context;

    public LocationController(TeddyBearsContext context)
    {
        _context = context;
    }

    [HttpPut("Locations/{Id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PicnicLocation>> UpdatePicnicLocation(int Id, PicnicLocation location)
    {
        if (Id != location.Id)
        {
            return BadRequest("URL Id must match the object Id");
        }

        var locationToUpdate = await _context.PicnicLocations.FindAsync(Id);
        if (locationToUpdate == null)
        {
            return NotFound();
        }

        locationToUpdate.LocationName = location.LocationName;
        locationToUpdate.Capacity = location.Capacity;
        locationToUpdate.Municipality = location.Municipality;

        await _context.SaveChangesAsync();

        return NoContent();
    }

}