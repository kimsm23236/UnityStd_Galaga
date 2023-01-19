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
    private MovementType movementType;

    private GameObject target;

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
    private Seat ownSeat;

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

    public void SetRandomBehavior()
    {
        int randNum = Random.Range(1, 3+1);

        switch(randNum)
        {
            case 1:
            movementType = MovementType.PLAYERTRACE;
            break;
            case 2:
            movementType = MovementType.ALONGTRAIL;
            break;
            case 3:
            movementType = MovementType.SEATED;
            break;
        }

    }


    public void SetCurrentTargetMovePoint(MovePoint nextMovePoint)
    {
        if(currentTargetMovePoint == nextMovePoint)
        {
            return;
        }
        currentTargetMovePoint = nextMovePoint;
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
