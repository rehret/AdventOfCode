namespace CodeChallenge.Core.Helpers.Math;

public static class Dijkstra
{
    public record ShortestPathGraphResult<T>(
        IReadOnlyDictionary<T, int> Distances,
        IReadOnlyDictionary<T, T> PreviousNodes
    );

    /// <summary>
    ///
    /// </summary>
    /// <param name="graph"></param>
    /// <param name="source"></param>
    /// <typeparam name="T"></typeparam>
    /// <remarks>Implemented using pseudocode found at: https://en.wikipedia.org/wiki/Dijkstra's_algorithm#Using_a_priority_queue</remarks>
    /// <returns></returns>
    public static ShortestPathGraphResult<T> GetShortestPathGraph<T>(Graph<T> graph, T source)
        where T : IEquatable<T?>
    {
        var dist = graph.GetVertices().ToDictionary(vertex => vertex, _ => int.MaxValue);
        var prev = new Dictionary<T, T>();
        var queue = new PriorityQueue<T, int>();

        dist[source] = 0;
        queue.Enqueue(source, 0);

        while (queue.Count > 0)
        {
            var vertex = queue.Dequeue();
            foreach (var (_, neighbor, distance) in graph.GetEdges(vertex))
            {
                var alt = dist[vertex] + distance;
                if (alt < dist[neighbor])
                {
                    dist[neighbor] = alt;
                    if (!prev.TryAdd(neighbor, vertex))
                    {
                        prev[neighbor] = vertex;
                    }

                    if (!queue.UnorderedItems.Select(x => x.Element).Contains(neighbor))
                    {
                        queue.Enqueue(neighbor, alt);
                    }
                }
            }
        }

        return new ShortestPathGraphResult<T>(dist, prev);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="graph"></param>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IEnumerable<T> GetShortestPath<T>(Graph<T> graph, T source, T target)
        where T : IEquatable<T?>
    {
        var shortestPathGraph = GetShortestPathGraph(graph, source).PreviousNodes;

        var stack = new Stack<T>();
        var node = target;
        stack.Push(node);
        while (shortestPathGraph.ContainsKey(node) && !node.Equals(source))
        {
            stack.Push(shortestPathGraph[node]);
            node = shortestPathGraph[node];
        }

        return stack.ToList();
    }
}