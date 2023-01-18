using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour
{
    private bool isTake;
    private GameObject SeatedEnemy;
    // Start is called before the first frame update
    void Start()
    {
        isTake = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Taked(GameObject takedObj)
    {
        SeatedEnemy = takedObj;
    }
    public void ReleaseSeat()
    {
        SeatedEnemy = default;
    }

}
