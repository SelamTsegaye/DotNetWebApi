using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotNetWebApi.Models;

namespace DotNetWebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class LocationController : ControllerBase
{
    private readonly TeddyBearsContext _context;

    public LocationController(TeddyBearsContext context)
    {
        _context = context;
    }

    [HttpGet("Locations")]
    public async Task<ActionResult<IEnumerable<PicnicLocationReturn>>> GetLocations()
    {
        var locations = from location in _context.PicnicLocations
                    select new PicnicLocationReturn () {
                            Id = location.Id,
                            LocationName = location.LocationName,
                            Capacity = location.Capacity,
                            Municipality = location.Municipality,
                            Picnics = location.Picnics.Select(p => p.PicnicName)
                        };
        return await locations.ToListAsync();
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

    [HttpPut("Locations2/{Id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PicnicLocation>> UpdatePicnicLocation2(int Id, PicnicLocation location)
    {
        if (Id != location.Id)
        {
            return BadRequest("URL Id must match the object Id");
        }

        var newLocationValue = new PicnicLocation
        {
            Id = Id,
            LocationName = location.LocationName,
            Capacity = location.Capacity,
            Municipality = location.Municipality
        };

        _context.PicnicLocations.Update(newLocationValue);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("Picnics/Locations/{locationName}")]
    public async Task<ActionResult<IEnumerable<PicnicReturn>>> GetPicnicsByLocation(string locationName)
    {
        var picnics = _context.Picnics
                                .Where(p => p.Location != null && p.Location.LocationName == locationName)
                                .Select(p => new PicnicReturn
                                {
                                    Id = p.Id,
                                    PicnicName = p.PicnicName,
                                    Location = p.Location,
                                    StartTime = p.StartTime,
                                    HasMusic = p.HasMusic == true,
                                    HasFood = p.HasFood == true,
                                    TeddyBears = p.TeddyBears.Select(tb => tb.Name)
                                });
        var picnicsFound = await picnics.ToListAsync();
        if (!picnicsFound.Any()){
            return NotFound();
        }
        //otherwise
        return picnicsFound;
    }
}