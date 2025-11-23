using AStarPathfinding;

internal class Pathfinder
{
    private readonly PriorityQueue<PathNode, int> openSet = new();
    private readonly HashSet<VecInt2> closedSet = [];
    private readonly Dictionary<VecInt2, PathNode> openSetReferences = [];
    private VecInt2 targetPosition;

    private TraversalRule[] traversalRules = TraversalRules.cardinal;

    public Dictionary<VecInt2, PathNode>.ValueCollection GetOpenSet()
    {
        return openSetReferences.Values;
    }

    public HashSet<VecInt2> GetClosedSet()
    {
        return closedSet;
    }

    public void Start(VecInt2 start, VecInt2 end)
    {
        openSet.Clear();
        openSetReferences.Clear();
        closedSet.Clear();
        PathNode startNode = new(start, null, 0, 0);
        openSet.Enqueue(startNode, 0);
        openSetReferences.Add(startNode.position, startNode);
        targetPosition = end;
        traversalRules = TraversalRules.cardinal;
    }

    public void Step(Board board)
    {
        PathNode current;
        do {
            if (openSet.Count == 0) return;
            current = openSet.Dequeue();
        } while (closedSet.Contains(current.position));

        openSetReferences.Remove(current.position);

        if (current.position == targetPosition)
        {
            // reached goal
            return;
        }

        // check possible new node positions
        foreach (var rule in traversalRules)
        {
            VecInt2 newNodePos = current.position + rule.dv;
            if (closedSet.Contains(newNodePos)) continue;

            // check if newNodePos can be traversed to
            bool canTraverse = true;
            int ruleIdx = 0;
            while (canTraverse && ruleIdx < rule.requirements.Count)
            {
                TraversalRequirement req = rule.requirements[ruleIdx];
                VecInt2 checkPos = current.position + req.relativePosition;

                if (board.IsInBounds(checkPos.x, checkPos.y))
                {
                    // if any tiles break the requirement, can't traverse
                    bool isEmpty = board.GetTile(checkPos.x, checkPos.y).IsEmpty;
                    canTraverse &= req.mustBeEmpty == isEmpty;
                }

                ruleIdx++;
            }

            // skip this traversal rule if cannot traverse
            if (!canTraverse) continue;

            // calculate costs of new node
            int gCost = current.gCost + rule.cost;
            int hCost = ManhattanDistance(newNodePos, targetPosition) * 11;

            // if already in open set, check if better path
            if (openSetReferences.TryGetValue(newNodePos, out PathNode? existingNode))
            {
                Console.WriteLine("already in open set");
                if (gCost + hCost < existingNode.FCost)
                {
                    // better path found, update existing node
                    existingNode.gCost = gCost;
                    existingNode.hCost = hCost;
                    existingNode.parent = current;
                    openSet.Enqueue(existingNode, existingNode.FCost);
                }
            }
            else
            {
                Console.WriteLine("new node");
                // create new node and add to open set
                PathNode newNode = new(newNodePos, current, gCost, hCost);
                openSet.Enqueue(newNode, newNode.FCost);
                Console.WriteLine(newNode.position + " " + newNode.FCost);
                openSetReferences.Add(newNode.position, newNode);
            }
        }

        closedSet.Add(current.position);
    }

    public static int ManhattanDistance(VecInt2 a, VecInt2 b)
    {
        return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
    }
}
