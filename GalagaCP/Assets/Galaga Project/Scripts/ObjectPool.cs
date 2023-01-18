using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // 플레이어 총알 풀링
    private List<GameObject> PlayerBulletPool;
    public GameObject PlayerBulletPrefab;
    public int MaxPBullet;
    // Start is called before the first frame update
    void Start()
    {
        PlayerBulletPool = new List<GameObject>();
        for(int i = 0 ; i < MaxPBullet; i++)
        {
            GameObject newBullet = Instantiate(PlayerBulletPrefab, transform.position, transform.rotation);
            PlayerBulletPool.Add(newBullet);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject SpawnPBullet()
    {
        GameObject newBullet = default;
        foreach(GameObject bullet in PlayerBulletPool)
        {
            if(!bullet.activeSelf)
            {
                newBullet = bullet;
                break;
            }
        }
        return newBullet;
    }
}
