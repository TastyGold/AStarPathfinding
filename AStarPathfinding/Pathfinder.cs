using AStarPathfinding;
using System.Runtime.CompilerServices;

internal class Pathfinder
{
    private readonly PriorityQueue<PathNode, long> openSet = new();
    private readonly HashSet<VecInt2> closedSet = [];
    private readonly Dictionary<VecInt2, PathNode> openSetReferences = [];
    private VecInt2 targetPosition;

    private TraversalRule[] traversalRules = TraversalRules.cardinal;

    public bool ReachedTarget { get; private set; }

    public PathNode? finishedNode;

    public Dictionary<VecInt2, PathNode>.ValueCollection GetOpenSet()
    {
        return openSetReferences.Values;
    }

    public HashSet<VecInt2> GetClosedSet()
    {
        return closedSet;
    }

    public PathNode? FindPath(Board board, VecInt2 start,  VecInt2 end)
    {
        Start(start, end);

        if (!IsPositionReachable(board, end))
        {
            // target not reachable
            return null;
        }

        while (GetOpenSet().Count > 0 && !ReachedTarget)
        {
            Step(board);
        }
        return finishedNode;
    }

    public void Start(VecInt2 start, VecInt2 end)
    {
        openSet.Clear();
        openSetReferences.Clear();
        closedSet.Clear();
        PathNode startNode = new(start, null, 0, 0);

        targetPosition = end;
        traversalRules = TraversalRules.cardinal;
        ReachedTarget = false;
        finishedNode = null;

        openSet.Enqueue(startNode, 0);
        openSetReferences.Add(startNode.position, startNode);
        enqueueCounter = 0;
    }

    public static bool IsPositionReachable(Board board, VecInt2 pos)
    {
        return board.IsInBounds(pos.x, pos.y) && board.GetTile(pos.x, pos.y).IsEmpty;
    }

    private int enqueueCounter = 0;
    public void Step(Board board)
    {
        PathNode current;
        do
        {
            // because we add nodes multiple times when a better path is found,
            // we may need to dequeue multiple times to find a node not already in closed set
            if (openSet.Count == 0) return;
            current = openSet.Dequeue();
            openSetReferences.Remove(current.position);
        }
        while (closedSet.Contains(current.position));


        if (current.position == targetPosition)
        {
            // reached goal
            ReachedTarget = true;
            finishedNode = current;
            return;
        }

        // check possible new node positions
        foreach (var rule in traversalRules)
        {
            VecInt2 newNodePos = current.position + rule.dv;
            if (closedSet.Contains(newNodePos) || !board.IsInBounds(newNodePos.x, newNodePos.y)) continue;

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
            int hCost = ManhattanDistance(newNodePos, targetPosition) * 10;

            // if already in open set, check if better path
            if (openSetReferences.TryGetValue(newNodePos, out PathNode? existingNode))
            {
                if (gCost + hCost < existingNode.FCost)
                {
                    // better path found, update existing node
                    existingNode.gCost = gCost;
                    existingNode.hCost = hCost;
                    existingNode.parent = current;

                    // re-enqueue with updated cost
                    // worth noting this means there may be multiple entries for the same node in the priority queue
                    openSet.Enqueue(existingNode, GetPriority(existingNode));
                    enqueueCounter++;
                }
            }
            else
            {
                // create new node and add to open set
                PathNode newNode = new(newNodePos, current, gCost, hCost);
                openSet.Enqueue(newNode, GetPriority(newNode));
                openSetReferences.Add(newNode.position, newNode);
                enqueueCounter++;
            }
        }

        closedSet.Add(current.position);
    }

    public long GetPriority(PathNode node)
    {
        // combine FCost, hCost, and enqueueCounter into a single long for priority queue
        return ((long)node.FCost << 42) + (node.hCost << 21) + enqueueCounter;
    }

    public static int ManhattanDistance(VecInt2 a, VecInt2 b)
    {
        return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
    }

    public static int OctileDistance(VecInt2 a, VecInt2 b)
    {
        int dx = Math.Abs(a.x - b.x);
        int dy = Math.Abs(a.y - b.y);
        return 10 * (dx + dy) + (4 * Math.Min(dx, dy));
    }
}
