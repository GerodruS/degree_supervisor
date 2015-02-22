using System;

public class Node
{
    public Node(string line)
    {
        string[] elements = line.Split(',');
        this.id = Convert.ToUInt32(elements[1].Trim());
        this.x = Convert.ToSingle(elements[2].Replace('.', ','));
        this.y = Convert.ToSingle(elements[3].Replace('.', ','));
    }

    public Node(uint id, float x, float y)
    {
        this.id = id;
        this.x = x;
        this.y = y;
    }

    public uint id;
    public float x;
    public float y;
}