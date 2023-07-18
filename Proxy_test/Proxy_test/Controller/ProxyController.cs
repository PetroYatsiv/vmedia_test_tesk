using Microsoft.AspNetCore.Mvc;
using Proxy_test.Services;
using System.Net;

namespace Proxy_test.Controller;

[ApiController]
[Route("api/[controller]")]
public class ProxyController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly IUrlChanger _urlChanger;
    private readonly IContentChanger _contentChanger;
    public ProxyController(HttpClient httpClient, IUrlChanger urlChanger, IContentChanger contentChanger)
    {
        _httpClient = httpClient;
        _urlChanger = urlChanger;
        _contentChanger = contentChanger;

    }

    [HttpGet]
    public async Task<IActionResult> GetPage([FromQuery(Name = "url")] string url)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                content = _urlChanger.ChangeUrl(content);
                content = _contentChanger.ChangeContent(content);

                return Content(content, "text/html");
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
