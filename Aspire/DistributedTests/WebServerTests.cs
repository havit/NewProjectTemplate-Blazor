namespace Aspire.Tests;

[TestClass]
public class WebServerTests : IntegrationTestBase
{
	[TestMethod]
	public async Task GetWebResourceRootReturnsOkStatusCode()
	{
		var ct = context.CancellationToken;

		await _app.ResourceNotifications.WaitForResourceHealthyAsync("web-server", ct).WaitAsync(DefaultTimeout, ct);

		using var httpClient = _app.CreateHttpClient("web-server");
		using var response = await httpClient.GetAsync("/", ct);

		Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
	}
}

