using Xunit;
using DotNetWebApi.Services;

namespace DotNetWebApi.Tests;

public class ExternalApiServiceTest
{
    [Fact]
    public void TestTwoIsEven()
    {
        // instantiating a ExternalApiService requires an injected HttpClient, 
        // so, just provide one
        var externalApiService = new ExternalApiService(new HttpClient());
        Assert.True(externalApiService.IsEven(2));
    }
}