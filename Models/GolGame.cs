namespace GameOfLife.Models;

public class GolGame
{
    private readonly int gridSize;
    private readonly bool[,] grid;
    private int step = -1;

    public GolGame(int gridSize)
    {
        this.gridSize = gridSize;  
        this.grid = new bool[gridSize, gridSize]; 
        initializeGrid();
    }

    private void initializeGrid()
    {
        for(int y = 0; y < this.gridSize; y++)
        {
            for(int x = 0; x < this.gridSize; x++)
            {
                this.grid[y, x] = false;
            }
        }
    }

    public void doStep()
    {
        step++;
        this.grid[step/gridSize, step%gridSize] = true;
    }

    public bool getCell(int x, int y)
    {
        return this.grid[y, x];
    }
}
