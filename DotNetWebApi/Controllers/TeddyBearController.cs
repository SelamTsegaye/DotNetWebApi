using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotNetWebApi.Models;

namespace DotNetWebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class TeddyBearController : ControllerBase
{
    private readonly TeddyBearsContext _context;

    public TeddyBearController(TeddyBearsContext context)
    {
        _context = context;
    }

    [HttpGet()]
    public async Task<ActionResult<IEnumerable<TeddyBearReturn>>> GetTeddyBears()
    {
        var teddyBears = from teddyBear in _context.TeddyBears
                    select new TeddyBearReturn () {
                            Id = teddyBear.Id,
                            Name = teddyBear.Name,
                            PrimaryColor = teddyBear.PrimaryColor,
                            AccentColor = teddyBear.AccentColor,
                            IsDressed = teddyBear.IsDressed == true,
                            OwnerName = teddyBear.OwnerName,
                            Characteristic = teddyBear.Characteristic,
                            Picnics = teddyBear.Picnics.Select(p => p.PicnicName)
                        };
        return await teddyBears.ToListAsync();
    }

    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<TeddyBear>> CreateTeddyBear(TeddyBear teddyBear)
    {
        _context.TeddyBears.Add(teddyBear);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(CreateTeddyBear), new { id = teddyBear.Id }, teddyBear);
    }
}