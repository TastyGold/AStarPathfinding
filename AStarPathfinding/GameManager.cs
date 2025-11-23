using Raylib_cs;

internal class GameManager
{
    private readonly BoardManager board = new(154, 84);
    private double population = 0.483f;

    public void Initialise()
    {
        board.SetRandomTilePresence(population);
    }

    public void Update()
    {
        if (Raylib.IsKeyDown(KeyboardKey.Up)) population += Raylib.GetFrameTime() / 10.0;
        if (Raylib.IsKeyDown(KeyboardKey.Down)) population -= Raylib.GetFrameTime() / 10.0;
        if (Raylib.IsKeyPressed(KeyboardKey.J) || Raylib.IsKeyPressed(KeyboardKey.K))
        {
            Initialise();
        }
        board.Update();
    }

    public void Draw()
    {
        board.DrawBoard(30, 30, 10);
        Raylib.DrawText($"Population = {population}", 10, 10, 10, Color.White);
    }
}
