using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Mode : ScriptableObject
{

    public Enemy[] foundational;
    public Enemy[] other;


    public Sprite[] decorations;
    public int numOfDecorations;
    public GameObject ground;
    public Vector2 approxGroundSize;
}
