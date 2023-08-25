using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Wave
{
    public int[] waves;
    public int[] gates;

    public Wave(int[] waves, int[] gates)
    {
        this.waves = waves;
        this.gates = gates;
    }
}
