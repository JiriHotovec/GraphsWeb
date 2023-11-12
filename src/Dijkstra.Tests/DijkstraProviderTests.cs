using Core;

namespace Dijkstra.Tests;

public class DijkstraProviderTests
{
    [Fact]
    public async void GetShortestPathAsync_ValidInputs_ReturnResult()
    {
        var edgeAB = new WeightedEdge(new Vertex("A"), new Vertex("B"), new Weight(1));
        var edgeBC = new WeightedEdge(new Vertex("B"), new Vertex("C"), new Weight(1));
        var edgeAC = new WeightedEdge(new Vertex("A"), new Vertex("C"), new Weight(3));
        var edges = new[] { edgeAB, edgeBC, edgeAC };
        var dijkstra = new DijkstraProvider(edges);
        var expected = new[] { edgeAB, edgeBC };

        var actual = await dijkstra.GetShortestPathAsync(new Vertex("A"), new Vertex("C"));

        actual.Paths.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async void GetShortestPathAsync_ManyInputs_ReturnResult()
    {
        var edgeSA = new WeightedEdge(new Vertex("S"), new Vertex("A"), new Weight(6));
        var edgeSB = new WeightedEdge(new Vertex("S"), new Vertex("B"), new Weight(2));
        var edgeSC = new WeightedEdge(new Vertex("S"), new Vertex("C"), new Weight(3));
        var edgeSD = new WeightedEdge(new Vertex("S"), new Vertex("D"), new Weight(9));

        var edgeAB = new WeightedEdge(new Vertex("A"), new Vertex("B"), new Weight(3));
        var edgeBC = new WeightedEdge(new Vertex("B"), new Vertex("C"), new Weight(2));
        var edgeCD = new WeightedEdge(new Vertex("C"), new Vertex("D"), new Weight(5));

        var edgeAE = new WeightedEdge(new Vertex("A"), new Vertex("E"), new Weight(4));
        var edgeBE = new WeightedEdge(new Vertex("B"), new Vertex("E"), new Weight(7));
        var edgeBF = new WeightedEdge(new Vertex("B"), new Vertex("F"), new Weight(3));
        var edgeEF = new WeightedEdge(new Vertex("E"), new Vertex("F"), new Weight(3));
        var edgeCF = new WeightedEdge(new Vertex("C"), new Vertex("F"), new Weight(1));
        var edgeDG = new WeightedEdge(new Vertex("D"), new Vertex("G"), new Weight(1));
        var edgeFG = new WeightedEdge(new Vertex("F"), new Vertex("G"), new Weight(2));


        var edges = new[]
        {
            edgeSA,
            edgeSB,
            edgeSC,
            edgeSD,
            edgeAB,
            edgeBC,
            edgeCD,
            edgeAE,
            edgeBE,
            edgeBF,
            edgeEF,
            edgeCF,
            edgeDG,
            edgeFG
        };
        var dijkstra = new DijkstraProvider(edges);
        var expected = new[]
        {
            edgeSC,
            edgeCF,
            edgeFG,
            edgeDG
        };

        var actual = await dijkstra.GetShortestPathAsync(new Vertex("S"), new Vertex("D"));

        actual.Paths.Should().BeEquivalentTo(expected);
    }
}