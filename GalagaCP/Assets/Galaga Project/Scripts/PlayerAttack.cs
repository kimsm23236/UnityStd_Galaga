using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private ObjectPool Pool;
    public float attackRate;
    private float timeAfterAttack;

    public GameObject Muzzle;
    // Start is called before the first frame update
    void Start()
    {
        GameObject op = Tools.GetRootObj("Pool");
        Pool = op.GetComponent<ObjectPool>();
        attackRate = 0.5f;
        timeAfterAttack = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        timeAfterAttack += Time.deltaTime;

        if(Pool == null || Pool == default)
        {
            return;
        }

        if(Input.GetKey(KeyCode.Z) == false)
        {
            return;
        }
        
        if(timeAfterAttack >= attackRate)
        {
            GameObject newBullet = Pool.SpawnPBullet();
            if(newBullet == null || newBullet == default)
            {
                return;
            }
            PlayerBullet PB = newBullet.GetComponent<PlayerBullet>();
            if(PB == null || PB == default)
            {
                return;
            }
            PB.Activate(Muzzle.transform);
            
            timeAfterAttack = 0f;
        }
    }
}
