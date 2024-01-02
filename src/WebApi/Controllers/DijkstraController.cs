using System.ComponentModel.DataAnnotations;
using Core;
using Microsoft.AspNetCore.Mvc;
using WebApi.Middlewares;
using WebApi.Services.Abstractions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class DijkstraController : ControllerBase
    {
        private readonly ILogger<DijkstraController> _logger;
        private readonly IDijkstraService _dijkstraService;

        public DijkstraController(ILogger<DijkstraController> logger, IDijkstraService dijkstraService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dijkstraService = dijkstraService ?? throw new ArgumentNullException(nameof(dijkstraService));
        }

        [HttpGet]
        [Route("{graphName}/{sourceName}/{destinationName}")]
        [ProducesResponseType<IEnumerable<WeightedEdge>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ExceptionResponse>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetShortestPath(
            [FromRoute, Required] string graphName,
            [FromRoute, Required] string sourceName,
            [FromRoute, Required] string destinationName,
            CancellationToken cancellationToken = default)
        {
            var path = await _dijkstraService.GetShortestPath(
                new GraphName(graphName),
                new Vertex(sourceName),
                new Vertex(destinationName),
                cancellationToken);

            return Ok(path);
        }
    }
}