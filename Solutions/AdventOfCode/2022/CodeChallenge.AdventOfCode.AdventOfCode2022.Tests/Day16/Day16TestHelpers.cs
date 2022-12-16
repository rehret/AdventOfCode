namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Tests.Day16;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day16.Models;
using CodeChallenge.Core.Helpers.Math;

internal static class Day16TestHelpers
{
    public static Graph<Valve> GetSampleInput()
    {
        var aa = new Valve("AA", 0);
        var bb = new Valve("BB", 13);
        var cc = new Valve("CC", 2);
        var dd = new Valve("DD", 20);
        var ee = new Valve("EE", 3);
        var ff = new Valve("FF", 0);
        var gg = new Valve("GG", 0);
        var hh = new Valve("HH", 22);
        var ii = new Valve("II", 0);
        var jj = new Valve("JJ", 21);

        var graph = new Graph<Valve>();
        graph.AddVertex(aa);
        graph.AddVertex(bb);
        graph.AddVertex(cc);
        graph.AddVertex(dd);
        graph.AddVertex(ee);
        graph.AddVertex(ff);
        graph.AddVertex(gg);
        graph.AddVertex(hh);
        graph.AddVertex(ii);
        graph.AddVertex(jj);

        graph.AddEdge(aa, dd);
        graph.AddEdge(aa, ii);
        graph.AddEdge(aa, bb);

        graph.AddEdge(bb, cc);
        graph.AddEdge(bb, aa);

        graph.AddEdge(cc, dd);
        graph.AddEdge(cc, bb);

        graph.AddEdge(dd, cc);
        graph.AddEdge(dd, aa);
        graph.AddEdge(dd, ee);

        graph.AddEdge(ee, ff);
        graph.AddEdge(ee, dd);

        graph.AddEdge(ff, ee);
        graph.AddEdge(ff, gg);

        graph.AddEdge(gg, ff);
        graph.AddEdge(gg, hh);

        graph.AddEdge(hh, gg);

        graph.AddEdge(ii, aa);
        graph.AddEdge(ii, jj);

        graph.AddEdge(jj, ii);

        return graph;
    }
}