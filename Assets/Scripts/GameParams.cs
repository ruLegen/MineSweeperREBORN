using System.Collections;
using System.Collections.Generic;

public static class GameParams
{
    public static int mineCount { get; set; }
    public static int minPower { get; set; }
    public static int maxPower { get; set; }
    public static void initializeParams(int mine, int _minPower, int _maxPower)
    {
        mineCount = mine;
        minPower = _minPower;
        maxPower = _maxPower;
    }
    // Start is called before the first frame update
}
