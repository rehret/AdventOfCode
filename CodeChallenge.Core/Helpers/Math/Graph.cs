namespace CodeChallenge.Core.Helpers.Math;

public class Graph<T>
    where T : IEquatable<T?>
{
    public record Edge(T First, T Second, int Distance);

    private readonly List<T> _vertices;
    private readonly List<Edge> _edges;

    public Graph()
    {
        _vertices = new List<T>();
        _edges = new List<Edge>();
    }

    public IReadOnlyList<T> GetVertices() => _vertices;

    public IReadOnlyList<T> GetNeighbors(T vertex)
    {
        return _edges
            .Where(edge => edge.First.Equals(vertex))
            .Select(edge => edge.Second)
            .ToList();
    }

    public IReadOnlyList<Edge> GetEdges(T vertex)
    {
        return _edges
            .Where(edge => edge.First.Equals(vertex))
            .ToList();
    }

    public void AddVertex(T vertex)
    {
        if (!_vertices.Contains(vertex))
        {
            _vertices.Add(vertex);
        }
    }

    public void AddEdge(T vertex1, T vertex2, int distance = 1)
    {
        var edge = new Edge(vertex1, vertex2, distance);
        if (!_edges.Contains(edge))
        {
            _edges.Add(edge);
        }
    }

    public void RemoveEdge(T vertex1, T vertex2)
    {
        _edges.RemoveAll(edge => edge.First.Equals(vertex1) && edge.Second.Equals(vertex2));
    }
}