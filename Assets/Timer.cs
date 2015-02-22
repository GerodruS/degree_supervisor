using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text label;

    // Use this for initialization
    private void Start()
    {
        label = GetComponent<Text>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        label.text = UnityEngine.Time.time.ToString();
    }
}