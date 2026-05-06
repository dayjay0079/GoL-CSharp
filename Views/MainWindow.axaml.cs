using System;
using System.Runtime.ConstrainedExecution;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using Avalonia.Media;
using GameOfLife.Models;


namespace GameOfLife.Views;

public partial class MainWindow : Window
{
    private readonly int GRIDSIZE = 50;
    private GolGame game;

    public MainWindow()
    {
        this.game = new GolGame(GRIDSIZE);
        InitializeComponent();
        golGrid.Rows = GRIDSIZE;
        golGrid.Columns = GRIDSIZE;
    }

    private void golGrid_Step(object? sender, RoutedEventArgs e)
    {
        game.doStep();
        drawGrid();
    }

    private void drawGrid()
    {
        golGrid.Children.Clear();
        for(int y = 0; y < GRIDSIZE; y++)
        {
            for(int x = 0; x < GRIDSIZE; x++)
            {
                if (!this.game.getCell(x, y))
                {
                    continue;
                }
                var cell = new Rectangle
                {
                    Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                    Width = 800/GRIDSIZE,
                    Height = 800/GRIDSIZE
                };
                golGrid.Children.Add(cell);
            }
        }

    }
}