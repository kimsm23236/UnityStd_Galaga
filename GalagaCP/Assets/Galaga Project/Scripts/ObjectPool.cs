using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPool : MonoBehaviour
{
    // 플레이어 총알 풀링
    private List<GameObject> playerBulletPool;
    private List<GameObject> enemy1Pool;
    private List<GameObject> enemy2Pool;
    private List<GameObject> enemy3Pool;
    private List<GameObject> enemyBulletPool;
    public GameObject playerBulletPrefab;
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject enemy3Prefab;
    public int MaxPBullet;
    private const int MAX_ENEMY_1 = 20;
    private const int MAX_ENEMY_2 = 20;
    private const int MAX_ENEMY_3 = 20;

    // spawn event
    public delegate GameObject SpawnEvent(int poolingObject);
    public SpawnEvent spawnHandle;
    // Start is called before the first frame update
    void Awake()
    {
        spawnHandle = new SpawnEvent(ReqSpawn);
    }
    void Start()
    {
        playerBulletPool = new List<GameObject>();
        enemy1Pool = new List<GameObject>();
        enemy2Pool = new List<GameObject>();
        enemy3Pool = new List<GameObject>();

        
        for(int i = 0 ; i < MaxPBullet; i++)
        {
            GameObject newBullet = Instantiate(playerBulletPrefab, transform.position, transform.rotation);
            newBullet.SetActive(false);
            newBullet.transform.parent = gameObject.transform.Find("PBullets");
            playerBulletPool.Add(newBullet);
        }
        for(int i = 0; i < MAX_ENEMY_1; i++)
        {
            GameObject newEnemy1 = Instantiate(enemy1Prefab, transform.position, transform.rotation);
            GameObject newEnemy2 = Instantiate(enemy2Prefab, transform.position, transform.rotation);
            GameObject newEnemy3 = Instantiate(enemy3Prefab, transform.position, transform.rotation);

            newEnemy1.transform.parent = gameObject.transform.Find("Enemies");
            newEnemy2.transform.parent = gameObject.transform.Find("Enemies");
            newEnemy3.transform.parent = gameObject.transform.Find("Enemies");

            newEnemy1.SetActive(false);
            newEnemy2.SetActive(false);
            newEnemy3.SetActive(false);

            enemy1Pool.Add(newEnemy1);
            enemy2Pool.Add(newEnemy2);
            enemy3Pool.Add(newEnemy3);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject ReqSpawn(int poolingObject)
    {
        GameObject obj_ = default;
        // poolingObject = 1 : PBullet, 2 : EBullet, 3, 4, 5 : Enemy 1, 2, 3 
        switch(poolingObject)
        {
            case 1:
            obj_ = SpawnPBullet();
            break;
            case 2:

            break;
            case 3:
            obj_ =  SpawnEnemy(1);
            break;
            case 4:
            obj_ =  SpawnEnemy(2);
            break;
            case 5:
            obj_ =  SpawnEnemy(3);
            break;
            default:
            break;
        }

        return obj_;
    }

    private GameObject SpawnPBullet()
    {
        GameObject newBullet = default;
        foreach(GameObject bullet in playerBulletPool)
        {
            if(!bullet.activeSelf)
            {
                newBullet = bullet;
                break;
            }
        }
        return newBullet;
    }
    private GameObject SpawnEnemy(int type)
    {
        GameObject newEnemy = default;

        switch(type)
        {
            case 1:
            foreach(GameObject enemy in enemy1Pool)
            {
                if(!enemy.activeSelf)
                {
                newEnemy = enemy;
                break;
                }
            }
            break;
            case 2:
            foreach(GameObject enemy in enemy2Pool)
            {
                if(!enemy.activeSelf)
                {
                newEnemy = enemy;
                break;
                }
            }
            break;
            case 3:
            foreach(GameObject enemy in enemy3Pool)
            {
                if(!enemy.activeSelf)
                {
                newEnemy = enemy;
                break;
                }
            }
            break;
        }

        return newEnemy;
    }
}
