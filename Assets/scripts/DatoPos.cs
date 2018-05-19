using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatoPos : MonoBehaviour {
    private int posX;
    private int posY;
    public int PosX
    {
        get
        {
            return posX;
        }

        set
        {
            posX = value;
        }
    }

    public int PosY
    {
        get
        {
            return posY;
        }

        set
        {
            posY = value;
        }
    }
}
