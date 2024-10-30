using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Mode : ScriptableObject
{

    public Enemy[] foundational;
    public Enemy[] common;
    public Enemy[] uncommon;
    public Enemy[] rare;
    public Enemy[] legendary;


    public Sprite[] decorations;
    public int numOfDecorations;
    public GameObject ground;
    public Vector2 approxGroundSize;
}
