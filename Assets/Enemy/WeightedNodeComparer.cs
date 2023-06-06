using System.Collections;
using System.Collections.Generic;

public class WeightedNodeComparer : Comparer<WeightedNode>
{
    public override int Compare(WeightedNode x, WeightedNode y)
    {
        return x.HeuristicWeight - y.HeuristicWeight;
    }
}
