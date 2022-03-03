using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Abgeleitet von ControllerBase und enthält
    /// grundlegende Funktionen von ApiController
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {

    }
}
