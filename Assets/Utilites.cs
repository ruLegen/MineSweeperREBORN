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

    public static int[,] getMatrix(int width, int height, int count, int maxforсe, int minforce, out int min, out int max)
    {
        int[,] matrix = new int[width, height];
        int[] x_coords = new int[count];
        int[] y_coords = new int[count];
        int[] force = new int[count];
        for (int i = 0; i < count; i++)
        {
            x_coords[i] = Random.Range(0 , width);
            y_coords[i] = Random.Range(0, height);
            force[i] = Random.Range(minforce, maxforсe);
        }

        min = int.MaxValue;
        max = 0;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                matrix[x, y] = 0;
                for (int n = 0; n < count; n++)
                {

                    matrix[x, y] += distFunc(x - x_coords[n], y - y_coords[n], force[n]);
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
