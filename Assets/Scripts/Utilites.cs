using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilites : MonoBehaviour
{
   
    public static int distFunc(int dist_x, int dist_y, int force)
    {
        dist_x = Mathf.Abs(dist_x);
        dist_y = Mathf.Abs(dist_y);
        float f = Mathf.Sqrt(((dist_x * dist_x) + (dist_y * dist_y)));
        if ((force - f) < 0)
            return 0;
        else
            return (int)(force - f);
    }

     public static int distWaveFunc(int dist_x, int dist_y, int force)
    {
        dist_x = Mathf.Abs(dist_x);
        dist_y = Mathf.Abs(dist_y);
        float f = (float)Mathf.Sqrt(dist_x * dist_x + dist_y * dist_y);
        int val = (int)(1000 * Mathf.Sin(f / force) - f);
        if (val >= 0) return val;
        else return 0;
    }


    public static int[,] getWaveMatrix(int width, int height, Mine[] mines, out int min, out int max)
    {
       
       
        int[,] matrix = new int[width, height];

        min = int.MaxValue;
        max = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int val = 0;
                for (int n = 0; n < mines.Length; n++)
                {
                    int mineX = (int)mines[n].position.x;
                    int mineY = (int)mines[n].position.y;
                    int force = (int)mines[n].force;
                    val += distWaveFunc(width - x - mineX, height - y - mineY, force);
                    val += distWaveFunc(width + x - mineX, height - y - mineY, force);
                    val += distWaveFunc(width * 3 - x - mineX, height - y - mineY, force);
                    val += distWaveFunc(width - x - mineX, height + y - mineY, force);
                    val += distWaveFunc(width + x - mineX, height + y - mineY, force);
                    val += distWaveFunc(width * 3 - x - mineX, height + y - mineY, force);
                    val += distWaveFunc(width - x - mineX, height * 3 - y - mineY, force);
                    val += distWaveFunc(width + x - mineX, height * 3 - y - mineY, force);
                    val += distWaveFunc(width * 3 - x - mineX, height * 3 - y - mineY, force);
                }
                matrix[x, y] = val;
                if (val < min) min = val;
                if (val > max) max = val;
            }
        }
        return matrix;
    }

    public static Mine[] generateMines(int width, int height, int count, int maxforсe, int minforce)
    {
        Mine[] mines = new Mine[count];
        for(int i = 0; i< mines.Length; i++)
        {
            mines[i] = new Mine(new Vector2(Random.Range(0, width), Random.Range(0, height)), Random.Range(minforce, maxforсe));
        }
        return mines;
    }

    public static int[,] getMatrix(int width, int height, Mine[] mines, out int min, out int max)
    {
        int[,] matrix = new int[width, height];
      
        min = int.MaxValue;
        max = 0;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                matrix[x, y] = 0;
                for (int n = 0; n < mines.Length; n++)
                {

                    matrix[x, y] += distFunc(x - (int)mines[n].position.x, y - (int)mines[n].position.y, (int)mines[n].force);
                }
                int value = matrix[x, y];
                if (min > value) min = value;
                if (max < value) max = value;
            }
        }

        return matrix;
    }
    public static float map(int x, int in_min, int in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
    public static float map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
    public static float[,] normolizeMatrix(int[,] matrix, int width, int height, int max, int min)
    {
        float[,] normMatr = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                normMatr[x, y] = map(matrix[x, y],min,max,0,1);
            }
        }

        return normMatr;
    }
   

}


