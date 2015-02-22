using System.Collections.Generic;

public class Analizator
{
    public Analizator()
    {
        nodes = new Dictionary<uint, Node>();
        edges = new Dictionary<Node, Dictionary<Node, Edge>>();
        nodeHistory = new List<Node>();
    }

    public Dictionary<uint, Node> nodes;
    public Dictionary<Node, Dictionary<Node, Edge>> edges;
    public List<Node> nodeHistory;

    private Node nodePrev = null;

    public Node GetNode(uint id)
    {
        Node node = null;
        if (nodes.ContainsKey(id))
        {
            node = nodes[id];
        }
        return node;
    }

    public void AddNode(uint id, float x, float y)
    {
        nodes.Add(id, new Node(id, x, y));
    }

    public void OnFixation(uint id, float time)
    {
        Node nodeCur = GetNode(id);

        if (nodePrev != null)
        {
            if (edges.ContainsKey(nodePrev))
            {
                if (edges[nodePrev].ContainsKey(nodeCur))
                {
                    edges[nodePrev][nodeCur].weight += 1;
                }
                else
                {
                    Edge edge = new Edge(nodePrev, nodeCur, 1);
                    edges[nodePrev].Add(nodeCur, edge);
                }
            }
            else
            {
                Edge edge = new Edge(nodePrev, nodeCur, 1);
                edges.Add(nodePrev, new Dictionary<Node, Edge>());
                edges[nodePrev].Add(nodeCur, edge);
            }
        }

        nodeHistory.Add(nodeCur);
        nodePrev = nodeCur;
    }

    public void DoPredict(List<Node> resultList)
    {
        if (edges.ContainsKey(nodePrev))
        {
            var nodesNext = edges[nodePrev];
            float weightBest = 0.0f;
            foreach (var nodeNext in nodesNext)
            {
                if (0.0f < weightBest)
                {
                    if (weightBest < nodeNext.Value.weight)
                    {
                        resultList.Clear();
                        resultList.Add(nodeNext.Value.next);
                        weightBest = nodeNext.Value.weight;
                    }
                    else if (weightBest == nodeNext.Value.weight)
                    {
                        resultList.Add(nodeNext.Value.next);
                    }
                }
                else
                {
                    resultList.Clear();
                    resultList.Add(nodeNext.Value.next);
                    weightBest = nodeNext.Value.weight;
                }
            }
        }
    }
}