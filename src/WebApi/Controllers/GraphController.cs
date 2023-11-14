using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GraphController : ControllerBase
    {
        public record Graph(string Name);

        private static readonly Graph[] Graphs = new[]
        {
            new Graph("Freezing"),
            new Graph("Bracing"),
            new Graph("Chilly"),
            new Graph("Cool"),
            new Graph("Mild"),
            new Graph("Warm"),
            new Graph("Balmy"),
            new Graph("Hot"),
            new Graph("Sweltering"),
            new Graph("Scorching")
        };

        private readonly ILogger<GraphController> _logger;

        public GraphController(ILogger<GraphController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetAll")]
        public IEnumerable<Graph> GetAll()
        {
            return Graphs;
        }
    }
}