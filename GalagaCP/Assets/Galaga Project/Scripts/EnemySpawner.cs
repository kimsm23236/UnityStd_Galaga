using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private ObjectPool Pool;

    // 스폰 에너미 종류
    private int spawnEnemyType;
    public int SpawnEnemyType
    {
        get { return spawnEnemyType; }
        set { spawnEnemyType = value; }
    }
    // 
    private int spawnCount;
    private int CPW; // Count per Wave
    private float spawnRate;
    private bool isAbleSpawn;
    private float waveRate;
    private bool isAbleWave;


    public MoveTrail enemyMoveTrail;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject op = Tools.GetRootObj("Pool");
        Pool = op.GetComponent<ObjectPool>();

        spawnCount = 0;
        spawnRate = 0.2f;
        isAbleSpawn = true;
        waveRate = 1f;
        CPW = 6;
        isAbleWave = true;
    }

    // Update is called once per frame
    void Update()
    {
        Spawn();
    }
    public void Spawn()
    {
        if(!isAbleWave || !isAbleSpawn)
        {
            return;
        } 

        if(Pool == null || Pool == default)
            return;

        GameObject newEnemy = Pool.spawnHandle(3);

        if(newEnemy == null || newEnemy == default)
            return;

        EnemyMovement em = newEnemy.gameObject.GetComponent<EnemyMovement>();

        if(em == null || em == default)
            return;
        em.onSpawnHandle(transform, enemyMoveTrail);
        spawnCount++;
        isAbleSpawn = false;
        StartCoroutine(SpawnCoolDown());
        
        if(spawnCount >= CPW)
        {
            isAbleWave = false;
            StartCoroutine(WaveCoolDown());
        }
        
        
        
    }

    IEnumerator SpawnCoolDown()
    {
        yield return new WaitForSeconds(spawnRate);

        isAbleSpawn = true;
    }
    IEnumerator WaveCoolDown()
    {
        yield return new WaitForSeconds(waveRate);

        spawnCount = 0;
        isAbleWave = true;
    }
}
