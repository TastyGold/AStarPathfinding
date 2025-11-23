using AStarPathfinding;
using System.Text.Json;

internal static class TraversalRules
{
    // IncludeFields = true so System.Text.Json will serialize public fields (not just properties)
    private static readonly JsonSerializerOptions json = new() { WriteIndented = true, IncludeFields = true };
    public static TraversalRule[] cardinal = [];

    public static void Initialise()
    {
        LoadRulesetFromJsonFile("cardinal_traversal_rules.json", out cardinal);
    }

    public static void SaveRulesetToJsonFile(TraversalRule[] ruleset, string filePath)
    {
        string js = JsonSerializer.Serialize(ruleset, json);
        File.WriteAllText(filePath, js);
    }

    public static void LoadRulesetFromJsonFile(string filePath, out TraversalRule[] ruleset)
    {
        string js = File.ReadAllText(filePath);
        ruleset = JsonSerializer.Deserialize<TraversalRule[]>(js, json) ?? [];
    }
}

internal class TraversalRule(VecInt2 dv, int cost, List<TraversalRequirement> requirements)
{
    public VecInt2 dv = dv;
    public int cost = cost;
    public List<TraversalRequirement> requirements = requirements;
}

internal class TraversalRequirement(VecInt2 relativePosition, bool mustBeEmpty)
{
    public VecInt2 relativePosition = relativePosition;
    public bool mustBeEmpty = mustBeEmpty;
}