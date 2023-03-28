using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using DotNetWebApi.Models;
using DotNetWebApi.Controllers;

namespace DotNetWebApi.Tests;

public class LocationControllerTest
{

    [Fact]
    public async void PutLocationShouldUpdateDBContextCorrectly()
    {
        var locationName = "Piedmont park";
        var teddyBearContext = new Mock<TeddyBearsContext>(new DbContextOptionsBuilder<TeddyBearsContext>().Options);

        var mockLocationDbSet = new Mock<DbSet<PicnicLocation>>();
        teddyBearContext.Setup(m => m.PicnicLocations).Returns(mockLocationDbSet.Object);

        var controller = new LocationController(teddyBearContext.Object);

        var newLocation = new PicnicLocation();
        newLocation.LocationName = locationName;
        await controller.UpdatePicnicLocation(1, newLocation);

        teddyBearContext.Verify(m => m.PicnicLocations, Times.Once());
        teddyBearContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }
}