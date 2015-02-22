public class Edge
{
    public Edge(Node prev, Node next, float weight)
    {
        this.prev = prev;
        this.next = next;
        this.weight = weight;
    }

    public Node prev;
    public Node next;
    public float weight;
}