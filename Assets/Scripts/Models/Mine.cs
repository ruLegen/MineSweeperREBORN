using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine
{
    public Vector2 position;
    public float force;

    public Mine(Vector2 _position, float _force)
    {
        position = _position;
        force = _force;
    }
    
}
