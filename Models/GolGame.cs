using System.Collections.Generic;

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

        // Glider
        this.grid[0, 1] = true;
        this.grid[1, 2] = true;
        this.grid[2, 0] = true;
        this.grid[2, 1] = true;
        this.grid[2, 2] = true;
    }

    private int countNeighbors(bool[,] grid, int x, int y)
    {
        int neighbors = 0;        
        for(int yOffset = -1; yOffset <= 1; yOffset++)
        {
            for(int xOffset = -1; xOffset <= 1; xOffset++)
            {
                if (xOffset == 0 && yOffset == 0) 
                    continue;

                int xNeighbor = x + xOffset;
                int yNeighbor = y + yOffset;
                if (xNeighbor < 0 || xNeighbor > this.gridSize-1 ||
                    yNeighbor < 0 || yNeighbor > this.gridSize-1)
                    continue;

                neighbors += grid[yNeighbor, xNeighbor] ? 1 : 0;
            }
        }

        return neighbors;
    }

    public void doStep()
    {
        step++;
        bool[,] oldGrid = (bool[,]) this.grid.Clone();

        for(int y = 0; y < this.gridSize; y++)
        {
            for(int x = 0; x < this.gridSize; x++)
            {
                int neighbors = countNeighbors(oldGrid, x, y);
                
                if (this.grid[y, x])
                {
                    // Cell is alive
                    if (neighbors < 2 || neighbors > 3)
                        this.grid[y, x] = false;
                    else
                        this.grid[y, x] = true;
                }
                else if (neighbors == 3)
                    // Cell is dead
                    this.grid[y, x] = true;
            }
        }
    }

    public bool getCell(int x, int y)
    {
        return this.grid[y, x];
    }
}
