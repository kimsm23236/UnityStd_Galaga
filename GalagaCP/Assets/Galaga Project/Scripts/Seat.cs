using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour
{
    private bool isTake;
    private bool isAssign;
    private GameObject SeatedEnemy;

    public delegate void OnEnemyAssignHandle(GameObject enemy);
    public delegate void OnEnemyTakeHandle();

    public OnEnemyAssignHandle enemyAssignHandle;
    public OnEnemyTakeHandle enemyTakeHandle;

    public EnemyArray ParentEnemyArray;

    private float seatTimer;
    private float seatTime;

    public bool Taked
    {
        get { return isTake; }
    }
    public bool Assigned
    {
        get { return isAssign; }
    }

    public GameObject SeatEnemy
    {
        get { return SeatedEnemy; }
    }
    // Start is called before the first frame update
    void Start()
    {
        InitSeat();
        enemyTakeHandle = new OnEnemyTakeHandle(EnemyTake);
        enemyAssignHandle = new OnEnemyAssignHandle(AssignedEnemy);
        seatTimer = 0f;
        seatTime = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        ReleaseCheck();
    }

    void OnTriggerEnter(Collider other)
    {
        // 충돌은 적 비행기가 자리잡는 경우만 해당되므로 그 외에 경우에는 전부 리턴
        if(other.tag != "Enemy" || other.gameObject != SeatedEnemy)
        {
            return;
        }

        enemyTakeHandle();
    }
    private void EnemyTake()
    {
        isTake = true;
        SeatedEnemy.transform.rotation = Quaternion.Euler(new Vector3(0f, 180.0f, 0f));
        SeatedEnemy.transform.position = transform.position;
        
        //
        EnemyMovement emc = SeatedEnemy.GetComponent<EnemyMovement>();
        emc.SetParent(transform);
        //
        SeatedEnemy.transform.parent = gameObject.transform;
        seatTimer = 0f;
    }
    public void ReleaseCheck()
    {
        seatTimer += Time.deltaTime;
        if(seatTimer >= seatTime)
        {
            if(SeatedEnemy != null && SeatedEnemy != default)
            {
                ReleaseProcess();
                seatTimer = 0f;
            }
        }
    }
    public void AssignedEnemy(GameObject takedObj)
    {
        SeatedEnemy = takedObj;
        isAssign = true;
        seatTimer = 0f;
    }
    public void InitSeat()
    {
        SeatedEnemy = default;
        isAssign = false;
        isTake = false;
    }
    public void ReleaseProcess()
    {
        EnemyMovement emc = SeatedEnemy.GetComponent<EnemyMovement>();
        if(emc == null || emc == default)
        {
            return;
        }
        int randNum = Random.Range(1, 2 + 1);
        MoveTrail newMT = default;
        if(randNum == 1)
        {
            newMT = ParentEnemyArray.moveTrail1;
        }
        else
        {
            newMT = ParentEnemyArray.moveTrail2;
        }
        emc.SetMoveTrail(newMT);
        emc.SetMovementType(MovementType.ALONGTRAIL);
        emc.SetPrevParent();
        ParentEnemyArray.IncreaseCntEmptySeat();
        InitSeat();
    }

}
