using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePoint : MonoBehaviour
{
    public MovePoint next = default;
    public bool isAttackPoint;
    // Start is called before the first frame update
    void Start()
    {
        isAttackPoint = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{gameObject.name} 도착");
        if(other.tag != "Enemy")
        {
            return;
        }
        if(isAttackPoint)
        {
            // 적이 공격을 할 위치일 경우 * 적 공격 함수 추가
            
        }

        Debug.Log($"{gameObject.name} 도착");
        EnemyMovement colliderMovement = other.gameObject.GetComponent<EnemyMovement>();

        if(colliderMovement.CurrentMovePoint != this)
        {
            return;
        }

        if(colliderMovement != null && colliderMovement != default)
        {
            if(next != null && next != default)
            {
                Debug.Log($"{next.gameObject.name} 으로 이동 시작");
                colliderMovement.SetCurrentTargetMovePoint(next);
            }
        }

    }
}
