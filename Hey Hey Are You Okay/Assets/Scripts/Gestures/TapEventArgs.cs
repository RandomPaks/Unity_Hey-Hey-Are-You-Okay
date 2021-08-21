using System;
using UnityEngine;

public class TapEventArgs : EventArgs
{
    private Vector2 tapPosition;

    public TapEventArgs(Vector2 pos)
    {
        tapPosition = pos;
    }
    public Vector2 TapPosition
    {
        get
        {
            return tapPosition;
        }
    }
}
