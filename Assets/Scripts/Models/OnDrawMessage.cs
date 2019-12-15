using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDrawMessage  
{
    public int x { get; set; }
    public int y { get; set; }
    public int brushSize { get; set; }
    public Mine[] mines;
    public float[,] normilized;


    public OnDrawMessage(int _x, int _y, int _brushSize, Mine[] _mines, float[,] _normlized)
    {
        x = _x;
        y = _y;
        brushSize = _brushSize;
        mines = _mines;
        normilized = _normlized;
    }
}
