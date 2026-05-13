using System;
using System.Runtime.ConstrainedExecution;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Threading;
using Avalonia.Input;
using GameOfLife.Models;


namespace GameOfLife.Views;

public partial class MainWindow : Window
{
    private readonly Color WHITE = Color.FromRgb(255, 255, 255);
    private readonly Color BLACK = Color.FromRgb(0, 0, 0);
    private readonly int GRIDSIZE = 50;
    private readonly int UPDATES_PER_SECOND = 20;

    private DispatcherTimer timer;
    private bool isRunning = false;
    private GolGame game;

    public MainWindow()
    {
        this.game = new GolGame(GRIDSIZE);
        this.timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(1000/UPDATES_PER_SECOND)
        };
        this.timer.Tick += onTimerTick;


        InitializeComponent();
        golGrid.Rows = GRIDSIZE;
        golGrid.Columns = GRIDSIZE;
        drawGrid();
    }

    private void onTimerTick(object? sender, EventArgs e)
    {
        game.doStep();
        drawGrid();
    }

    private void golGrid_Run(object? sender, RoutedEventArgs e)
    {
        if (!this.isRunning)
        {
            this.timer.Start();
            this.isRunning = true;
        }
    }

    private void golGrid_Pause(object? sender, RoutedEventArgs e)
    {
        if (this.isRunning)
        {
            this.timer.Stop();
            this.isRunning = false;
        }
    }

    private void golGrid_Step(object? sender, RoutedEventArgs e)
    {
        // game.doStep();
        game.setGlider();
        drawGrid();
    }

    private void golGrid_Reset(object? sender, RoutedEventArgs e)
    {
        golGrid_Pause(sender, e);
        game.resetGrid();
        drawGrid();
    }

    private void PointerPressedHandler(object sender, PointerPressedEventArgs args)
    {
        var point = args.GetCurrentPoint(sender as Control);
        int x = Convert.ToInt32(Math.Truncate(point.Position.X*GRIDSIZE/800));
        int y = Convert.ToInt32(Math.Truncate(point.Position.Y*GRIDSIZE/800));
        game.toggleCell(x, y);
        drawGrid();
    }

    private void drawGrid()
    {
        golGrid.Children.Clear();
        for(int y = 0; y < GRIDSIZE; y++)
        {
            for(int x = 0; x < GRIDSIZE; x++)
            {
                var color = this.game.getCell(x, y) 
                            ? WHITE
                            : BLACK;

                var cell = new Rectangle
                {
                    Fill = new SolidColorBrush(color),
                    Width = 800/GRIDSIZE,
                    Height = 800/GRIDSIZE
                };
                golGrid.Children.Add(cell);
            }
        }
    }
}