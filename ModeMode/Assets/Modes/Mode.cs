using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Mode : ScriptableObject
{
    public GameObject[] enemies;
    public Sprite[] decorations;
    public int numOfDecorations;
    public GameObject ground;
    public Vector2 approxGroundSize;
}
