using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class DecorationManager : MonoBehaviour
{
    public Mode modeRef;
    [SerializeField] SpriteRenderer decorationPrefab;
    [SerializeField] LayerMask groundLayer;
    int decorationsSpawned;
    void Start()
    {
        SpawnDecorations();
    }

    public void SpawnDecorations()
    {
        while (decorationsSpawned < modeRef.numOfDecorations)
        {
            Vector2 spawnPoint = new Vector2(Random.Range(-modeRef.approxGroundSize.x / 2, modeRef.approxGroundSize.x / 2), Random.Range(-modeRef.approxGroundSize.y / 2, modeRef.approxGroundSize.y / 2));
            RaycastHit hit;
            if (Physics.Raycast(new Vector3(spawnPoint.x-0.5f, 10, spawnPoint.y), -Vector3.up, out hit, Mathf.Infinity, groundLayer) && Physics.Raycast(new Vector3(spawnPoint.x+0.5f, 10, spawnPoint.y), -Vector3.up, out hit, Mathf.Infinity, groundLayer))
            {
                spawnDecoration(spawnPoint);
                decorationsSpawned++;

            }
        }
    }



    void spawnDecoration(Vector2 spawn)
    {
        SpriteRenderer newRenderer = Instantiate(decorationPrefab, transform.TransformPoint(new Vector3(spawn.x, 0, spawn.y)), decorationPrefab.transform.rotation, transform);
        newRenderer.sprite = modeRef.decorations[Random.Range(0, modeRef.decorations.Length)];
    }
}
