using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu] 
public class Enemy : ScriptableObject
{
    public GameObject unit;
    public float cost;
    public float weight;
    public int firstWave;
}
