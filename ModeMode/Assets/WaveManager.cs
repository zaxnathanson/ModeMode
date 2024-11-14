using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;
    public Mode currentMode;
    Transform enemyContainer;
    public GameObject player;
    [SerializeField] float foundationalEnemyChance;
    [SerializeField] AnimationCurve enemyAmountCurve;
    [SerializeField] AnimationCurve enemyScreenMax;
    [SerializeField] AnimationCurve spawnCooldownCurve;
    [SerializeField] public AnimationCurve expMultiplier;
    public int wave;
    float spawnTimer = 0;
    int enemiesSpawnedThisWave;

    public delegate void NextWave();
    public NextWave nextWave;

    [SerializeField] LayerMask groundLayer;

    void Awake()
    {
        Instance = this;
        enemyContainer = GameObject.FindWithTag("EnemyContainer").transform;
        player = GameObject.FindWithTag("Player");
    }

    // CallUpdate is called once per frame
    void Update()
    {
        SpawnCooldown();
    }

    void SpawnCooldown()
    {
        if (enemiesSpawnedThisWave < enemyAmountCurve.Evaluate(wave))
        {
            spawnTimer += Time.deltaTime;
            if (enemyContainer.childCount < Mathf.Round(enemyScreenMax.Evaluate(wave)))
            {
                if (spawnTimer > spawnCooldownCurve.Evaluate(wave))
                {
                    spawnTimer = 0f;
                    enemiesSpawnedThisWave++;
                    if (Random.Range(0f, 100f) < foundationalEnemyChance)
                    {
                        SpawnFoundationalEnemy(findSpawnPoint());
                    }
                    else
                    {
                        SpawnEnemy(findSpawnPoint());
                    }
                }
            }
        }
        else
        {
            Wave();
        }
    }



    void Wave()
    {
        if (enemyContainer.childCount == 0)
        {
            wave++;
            enemiesSpawnedThisWave = 0;
            Debug.Log("playerShoot");
            nextWave?.Invoke();
        }
    }

    Vector3 findSpawnPoint()
    {
        float sizeX = currentMode.approxGroundSize.x / 2;
        float sizeY = currentMode.approxGroundSize.y / 2;

        RaycastHit hit;
        Vector3 spawnPoint;

        do
        {
            do
            {
                spawnPoint = new Vector3(Random.Range(-sizeX, sizeX), 0, Random.Range(-sizeY, sizeY));
            }
            while (Mathf.Abs(Vector3.Distance(spawnPoint, player.transform.position)) < 8);

            Physics.Raycast(new Vector3(spawnPoint.x, 5, spawnPoint.z), -Vector3.up, out hit, 10, groundLayer) ;
        }
        while (hit.transform == null);


        return spawnPoint;

    }
    private void SpawnFoundationalEnemy(Vector3 spawnLocation)
    {
        float totalWeight = 0;

        float[] weights = new float[currentMode.foundational.Length];

        int i = 0;
        foreach (Enemy enemy in currentMode.foundational)
        {
            weights[i] = 1 / enemy.weight;
            i++;
        }

        //get totalWeight
        for (int n = 0; n < weights.Length; n++)
        {
            totalWeight += weights[n];
        }

        //normalize weights
        for (int n = 0; n < weights.Length; n++)
        {
            weights[n] /= totalWeight;
            weights[n] *= 100;
        }

        float randomEnemy = Random.Range(0f, 100f);
        float randomTotal = 0;
        int enemyIndex = 0;
        for (int n = 0; n < weights.Length; n++)
        {
            randomTotal += weights[n];
            if (randomTotal > randomEnemy)
            {
                enemyIndex = n;
                break;
            }
        }

        Instantiate(currentMode.foundational[enemyIndex].unit, spawnLocation, Quaternion.identity, enemyContainer);

    }

    void SpawnEnemy(Vector3 spawnLocation)
    {
        float totalWeight = 0;

        float[] weights = new float[currentMode.other.Length];
        
        int i = 0;
        foreach (Enemy enemy in currentMode.other)
        {
            weights[i] = 1 / enemy.weight;
            i++;
        }

        //get totalWeight
        for (int n = 0; n < weights.Length; n++)
        {
            totalWeight += weights[n];
        }

        //normalize weights
        for (int n = 0; n < weights.Length; n++)
        {
            weights[n] /= totalWeight;
            weights[n] *= 100;
        }
        
        float randomTotal = 0;
        int enemyIndex = 0;
        do
        {
            float randomEnemy = Random.Range(0f, 100f);

            for (int n = 0; n < weights.Length; n++)
            {
                randomTotal += weights[n];
                if (randomTotal > randomEnemy)
                {
                    enemyIndex = n;
                    randomTotal = 0;
                    break;
                }
            }
        } while (currentMode.other[enemyIndex].firstWave > wave);


        Instantiate(currentMode.other[enemyIndex].unit, spawnLocation, Quaternion.identity, enemyContainer);

    }
}
