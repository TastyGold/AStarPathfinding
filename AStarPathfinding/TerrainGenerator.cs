internal static class TerrainGenerator
{
    public static void RunCaveAutomataSteps(Board board, int steps)
    {
        for (int i = 0; i < steps; i++)
        {
            RunCaveAutomataStep(board);
        }
    }

    // https://code.tutsplus.com/generate-random-cave-levels-using-cellular-automata--gamedev-9664t
    public static void RunCaveAutomataStep(Board board)
    {
        const int deathLimit = 4;
        const int birthLimit = 4;

        Tile[,] newBoard = new Tile[board.width, board.height];
        //Loop over each row and column of the map 
        for (int x = 0; x < board.width; x++)
        {
            for (int y = 0; y < board.height; y++)
            {
                int nbs = CountAliveNeighbours(board, x, y);
                //The new value is based on our simulation rules 
                //First, if a cell is alive but has too few neighbours, kill it. 
                if (!board.GetTile(x, y).IsEmpty)
                {
                    if (nbs < deathLimit)
                    {
                        newBoard[x, y] = Tile.empty;
                    }
                    else
                    {
                        newBoard[x, y] = Tile.present;
                    }
                } //Otherwise, if the cell is dead now, check if it has the right number of neighbours to be 'born' 
                else
                {
                    if (nbs > birthLimit)
                    {
                        newBoard[x, y] = Tile.present;
                    }
                    else
                    {
                        newBoard[x, y] = Tile.empty;
                    }
                }
            }
        }

        for (int x = 0; x < board.width; x++)
        {
            for (int y = 0; y < board.height; y++)
            {
                board.SetTile(x, y, newBoard[x, y]);
            }
        }
    }

    //Returns the number of cells in a ring around (x,y) that are alive. 
    public static int CountAliveNeighbours(Board board, int x, int y)
    {
        int count = 0;
        for (int nx = x - 1; nx <= x + 1; nx++)
        {
            for (int ny = y - 1; ny <= y + 1; ny++)
            {
                //If we're looking at the middle point 
                if (nx == x && ny == y)
                {
                    //Do nothing, we don't want to add ourselves in! 
                }
                //In case the index we're looking at it off the edge of the map 
                else if (nx < 0 || ny < 0 || nx >= board.width || ny >= board.height)
                {
                    count++;
                }
                //Otherwise, a normal check of the neighbour 
                else if (!board.GetTile(nx, ny).IsEmpty)
                {
                    count++;
                }
            }
        }

        return count;
    }
}
