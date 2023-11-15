using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class GraphController : ControllerBase
    {
        public record Graph(string Name);

        private static readonly Graph[] Graphs =
        {
            new("Freezing"),
            new("Bracing"),
            new("Chilly"),
            new("Cool"),
            new("Mild"),
            new("Warm"),
            new("Balmy"),
            new("Hot"),
            new("Sweltering"),
            new("Scorching")
        };

        private readonly ILogger<GraphController> _logger;

        public GraphController(ILogger<GraphController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType<IEnumerable<Graph>>(StatusCodes.Status200OK)]
        public IActionResult GetGraphs()
        {
            return Ok(Graphs);
        }

        [HttpGet, HttpHead]
        [Route("{name}")]
        [ProducesResponseType<Graph>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetGraphDetail([FromRoute, Required] string name)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            var graph = Graphs.FirstOrDefault(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            return graph is not null
                ? Ok(graph)
                : NotFound();
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType<Graph>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateGraph([FromBody, Required] Graph graph)
        {
            ArgumentNullException.ThrowIfNull(graph);

            return CreatedAtAction(nameof(GetGraphDetail), new { name = graph.Name }, graph);
        }

        [HttpPut]
        [Route("{name}")]
        [ProducesResponseType<Graph>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateGraph([FromRoute, Required] string name, [FromBody, Required] Graph graph)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentNullException.ThrowIfNull(graph);

            var result = Graphs.FirstOrDefault(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            return result is not null
                ? Ok(result)
                : NotFound();
        }

        [HttpDelete]
        [Route("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteGraph([FromRoute, Required] string name)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            return Ok();
        }
    }
}