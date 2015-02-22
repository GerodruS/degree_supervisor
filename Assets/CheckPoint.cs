using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Supervisor supervisor;

    private void Start()
    {
        supervisor = GameObject.Find("Supervisor").GetComponent<Supervisor>();
        supervisor.AddNode(this);
    }

    private float timeStart = 0.0f;
    private bool onIn = false;

    private void OnMouseEnter()
    {
        timeStart = Time.time;
        onIn = true;
    }

    private void OnMouseExit()
    {
        if (onIn)
        {
            float t = Random.Range(timeStart, Time.time);
            supervisor.onCheckpoint(this, t);
            onIn = false;
        }
    }
}