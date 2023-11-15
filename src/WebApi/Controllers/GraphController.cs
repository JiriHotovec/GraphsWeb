using System.ComponentModel.DataAnnotations;
using Core;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services.Abstractions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class GraphController : ControllerBase
    {
        private readonly ILogger<GraphController> _logger;
        private readonly IGraphService _graphService;

        public GraphController(ILogger<GraphController> logger, IGraphService graphService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _graphService = graphService ?? throw new ArgumentNullException(nameof(graphService));
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType<IEnumerable<GraphDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGraphs(CancellationToken cancellationToken = default)
        {
            var graphs = await _graphService.GetGraphs(cancellationToken);

            return Ok(graphs);
        }

        [HttpGet, HttpHead]
        [Route("{name}")]
        [ProducesResponseType<GraphDetailDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetGraphDetail([FromRoute, Required] string name, CancellationToken cancellationToken = default)
        {
            var graph = await _graphService.GetGraph(new GraphName(name), cancellationToken);

            return graph is not null
                ? Ok(graph)
                : NotFound();
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType<GraphDetailDto>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateGraph([FromBody, Required] GraphDetailDto graph, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(graph);

            var result = await _graphService.UpsertGraph(new GraphName(graph.Name), graph, cancellationToken);

            return CreatedAtAction(nameof(GetGraphDetail), new { name = result.Name }, result);
        }

        [HttpPut]
        [Route("{name}")]
        [ProducesResponseType<GraphDetailDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateGraph(
            [FromRoute, Required] string name,
            [FromBody, Required] GraphDetailDto graph,
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(graph);

            var graphName = new GraphName(name);
            if (!await _graphService.ExistsGraph(graphName, cancellationToken))
            {
                return NotFound();
            }

            var result = await _graphService.UpsertGraph(graphName, graph, cancellationToken);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteGraph([FromRoute, Required] string name, CancellationToken cancellationToken = default)
        {
            await _graphService.DeleteGraph(new GraphName(name), cancellationToken);

            return Ok();
        }
    }
}