using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using AStarTools;


class AStar
{
    Dictionary<float, Node> nodes;


    public static List<Node> FindPath(IGraph<Node> graph,
            Node startNode, Node endNode, int maxIterations)
    {
        List<Node> path = new List<Node>();
        Dictionary<Node, float> nodeDict = new Dictionary<Node, float>();
        List<Node> visited = new List<Node>();

        UpdateNode(startNode, endNode);
        nodeDict.Add(startNode, startNode.weight);
        Node focusNode = startNode;

        for (int iter = 0; iter < maxIterations; iter++)
        {
            if (focusNode == endNode)
                break;

            foreach (Node iterNode in graph.Neighbors(focusNode))
            {
                UpdateNode(iterNode, focusNode, endNode);

                if(visited.Contains(iterNode))
                    continue;

                if (!nodeDict.ContainsKey(iterNode))
                    nodeDict.Add(iterNode, iterNode.weight);
            }

            visited.Add(focusNode);
            nodeDict.Remove(focusNode);
            nodeDict = nodeDict.OrderBy(e => e.Key.weight).ToDictionary(x => x.Key, x => x.Value);

            if(nodeDict.Count == 0)
                break;

            focusNode = nodeDict.Keys.First();
        }

        if (focusNode != endNode)
            return path;


        for (int iter = 0; iter < maxIterations; iter++)
        {
            path.Add(focusNode);

            if (focusNode == startNode)
                break;

            focusNode = focusNode.prev;

        }

        return path;
    }

    static void UpdateNode(Node inputNode, Node prev, Node endNode)
    {
        Vector3Int diff = endNode.coordinates - inputNode.coordinates;
        float distance = diff.magnitude;

        if (inputNode.prev == null || inputNode.prev.weight >= prev.weight)
            inputNode.prev = prev;

        inputNode.staticWeight = distance;
    }

    static void UpdateNode(Node inputNode, Node endNode)
    {
        Vector3Int diff = endNode.coordinates - inputNode.coordinates;
        float distance = diff.magnitude;

        inputNode.staticWeight = distance;
    }

    static public List<Node> GetPath(IGraph<Node> graph, Node startNode, Node endNode, int maxiterations = 1000)
    {
        return FindPath(graph, startNode, endNode, maxiterations);
    }


}