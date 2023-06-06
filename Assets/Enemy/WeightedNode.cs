public class WeightedNode
{
    Node node;
    public Node Node { get { return node; }}
    int pathWeight;
    public int PathWeight { get { return pathWeight; }}
    int heuristicWeight;
    public int HeuristicWeight { get { return heuristicWeight; }}

    public WeightedNode(Node node, int pathWeight, int heuristicWeight) {
        this.node = node;
        this.pathWeight = pathWeight;
        this.heuristicWeight = heuristicWeight;
    }
}
