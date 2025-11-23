using AStarPathfinding;

internal class PathNode(VecInt2 position, PathNode? parent, int gCost, int hCost)
{
    public VecInt2 position = position;
    public PathNode? parent = parent;
    public int gCost = gCost;
    public int hCost = hCost;
    public int FCost => gCost + hCost ;
}