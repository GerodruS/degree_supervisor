using System;
using System.Collections.Generic;
using UnityEngine;

public class Supervisor : MonoBehaviour
{
    public Supervisor supervisor;
    public bool isShowEdge = false;
    public bool isShowPredict = false;
    public float delta = 0.1f;

    private List<Node> nodePredicted = new List<Node>();
    private float weightPredicted = 0.0f;
    private Analizator analizator = new Analizator();
    private Dictionary<uint, CheckPoint> checkPoints = new Dictionary<uint, CheckPoint>();

    private void Start()
    {
    }

    private Color[] colors = new Color[]
    {
        Color.red,
        Color.yellow,
        Color.green,
        Color.cyan,
        Color.blue,
        Color.magenta
    };

    private void FixedUpdate()
    {
        if (isShowEdge)
        {
            foreach (var nodeA in analizator.nodes)
            {
                uint idA = nodeA.Key;
                foreach (var nodeB in analizator.nodes)
                {
                    uint idB = nodeB.Key;
                    if (idA != idB &&
                        analizator.edges.ContainsKey(nodeA.Value) &&
                        analizator.edges[nodeA.Value].ContainsKey(nodeB.Value))
                    {
                        int w = Convert.ToInt32(analizator.edges[nodeA.Value][nodeB.Value].weight);
                        if (6 < w)
                        {
                            w = 6;
                        }
                        if (0 < w)
                        {
                            Debug.DrawLine(checkPoints[idA].transform.position + delta * (idA < idB ? Vector3.right : Vector3.left),
                                           checkPoints[idB].transform.position + delta * (idA < idB ? Vector3.right : Vector3.left),
                                           colors[w - 1]);
                        }
                    }
                }
            }
        }
        if (isShowPredict && 0 < nodePredicted.Count)
        {
            foreach (var cp in checkPoints)
            {
                if (nodePredicted.Exists(p => p.id == cp.Key))
                {
                    int w = Convert.ToInt32(weightPredicted);
                    cp.Value.renderer.material.color = colors[w];
                }
                else
                {
                    cp.Value.renderer.material.color = Color.white;
                }
            }
        }
    }

    public void AddNode(CheckPoint checkPoint)
    {
        uint id = (uint)checkPoint.GetInstanceID();
        checkPoints.Add(id, checkPoint);
        analizator.AddNode(id,
                       checkPoint.transform.position.x,
                       checkPoint.transform.position.y);
    }

    public void onCheckpoint(CheckPoint checkPoint, float time)
    {
        analizator.OnFixation((uint)checkPoint.GetInstanceID(), time);
        analizator.DoPredict(nodePredicted);

        string message = string.Format("{0, 10}, {1, 5}, {2, 5}, {3, 5}\n",
            (uint)checkPoint.GetInstanceID(),
            checkPoint.transform.position.x.ToString(),
            checkPoint.transform.position.y.ToString(),
            time.ToString());
        Debug.Log(message);
    }
}