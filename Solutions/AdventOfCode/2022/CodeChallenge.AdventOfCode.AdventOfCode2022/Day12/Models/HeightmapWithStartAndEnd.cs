namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day12.Models;

using CodeChallenge.Core.Helpers.Math;

internal record HeightmapWithStartAndEnd(Graph<Coordinate> Heightmap, Coordinate Start, Coordinate End);