using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MovementType
    {
        NONE = 0,
        PLAYERTRACE, ALONGTRAIL, SEATED
    }

public class EnemyMovement : MonoBehaviour
{
    private CharacterController controller;
    public MovementType defaultMovementType;
    public MovementType movementType;

    private GameObject target;

    public Transform prevParent;

    // 플레이어 추적 관련 멤버 * 다른 타입에서도 재사용가능
    private bool isTrace;
    private float traceSpeed = 50.0f;
    
    // 궤적따라 이동 관련 멤버
    public MoveTrail ownTrail;

    private float rotateSpeed = 5.0f;
    private MovePoint currentTargetMovePoint;
    public MovePoint CurrentMovePoint
    {
        get { return currentTargetMovePoint; }
    }
    // 대열 합류 관련 멤버
    private bool isSeated;
    public Seat ownSeat;

    public EnemyArray enemyArray;

    // 스폰 관련 멤버
    public delegate void SpawnedHandle(Transform startTransform, MoveTrail newTrail);
    public SpawnedHandle onSpawnHandle;

    void Awake()
    {
        onSpawnHandle = new SpawnedHandle(OnSpawn);
    }
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        target = Tools.GetRootObj("PlayerSpaceShip");
        movementType = defaultMovementType;  
        GameObject eag = Tools.GetRootObj("EnemyArray1");
        enemyArray = eag.GetComponent<EnemyArray>();

        if(ownTrail != null && ownTrail != default)
        {
            currentTargetMovePoint = ownTrail.root;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        switch(movementType)
        {
            case MovementType.PLAYERTRACE:
                MovePrc_Trace();
                break;
            case MovementType.ALONGTRAIL:
                MovePrc_AlongTrail();
                break;
            case MovementType.SEATED:
                MovePrc_Seated();
                break;
        }
    }

    private void MovePrc_Trace()
    {
        if(target == null || target == default)
        {
            return;
        }
        transform.LookAt(target.transform);
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * traceSpeed);

    }
    private void MovePrc_AlongTrail()
    {
        if(currentTargetMovePoint == null || currentTargetMovePoint == default)
            return;
        Transform targetTransform = currentTargetMovePoint.transform;

        // this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * speed);
        Vector3 dir = targetTransform.position - transform.position;
        // transform.LookAt(
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir) , Time.deltaTime * rotateSpeed);
        transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, Time.deltaTime * traceSpeed);

    }

    private void MovePrc_Seated()
    {
        if(ownSeat == null || ownSeat == default)
            return;
        
        if(ownSeat.Taked)
            return;

        Transform targetTransform = ownSeat.transform;
        Vector3 dir = targetTransform.position - transform.position;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir) , Time.deltaTime * rotateSpeed);
        transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, Time.deltaTime * traceSpeed);

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "InitWall")
        {
            DeSpawn();
        }

    }

    public void Die()
    {

    }

    public void SetParent(Transform newParent)
    {
        prevParent = transform.parent;
        transform.parent = newParent;
    }
    public void SetPrevParent()
    {
        transform.parent = prevParent;
    }

    public void SetRandomBehavior()
    {
        int randNum = default;
        if(enemyArray.IsThereEmptySeat())
        {
            randNum = Random.Range(1, 100+1);
        }
        else
        {
            randNum = Random.Range(21, 100+1);
        }

        if(randNum > 40)
        movementType = MovementType.ALONGTRAIL;
        else if (randNum > 20)
        movementType = MovementType.ALONGTRAIL; // 플레이어 추적 구현시 바꿈
        else
        {
            movementType = MovementType.SEATED;
            // 자리 배치 작업
            // 임시로 대열 하나만 해봄
            // 이후 대열 늘어나면 관리자를 통해 배치 작업 하는 것으로 바꿀 예정
            ownSeat = enemyArray.ReqEmptySeat();
            ownSeat.enemyAssignHandle(gameObject);
        }

    }

    public void SetMovementType(MovementType newMovementType)
    {
        movementType = newMovementType;
    }


    public void SetCurrentTargetMovePoint(MovePoint nextMovePoint)
    {
        if(currentTargetMovePoint == nextMovePoint)
        {
            return;
        }
        currentTargetMovePoint = nextMovePoint;
    }

    public void SetMoveTrail(MoveTrail newMoveTrail)
    {
        ownTrail = newMoveTrail;
        currentTargetMovePoint = ownTrail.root;
    }

    private void OnSpawn(Transform startTransform, MoveTrail newTrail)
    {
        gameObject.transform.position = startTransform.position;
        gameObject.transform.rotation = startTransform.rotation;
        ownTrail = newTrail;
        currentTargetMovePoint = ownTrail.root;
        gameObject.SetActive(true);
    }
    private void DeSpawn()
    {
        gameObject.SetActive(false);
    }
}
