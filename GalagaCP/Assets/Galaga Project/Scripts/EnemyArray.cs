using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArray : MonoBehaviour
{

    public List<Seat> SeatList;
    private int cntSeat;
    private int cntEmptySeat;
    public GameObject moveTarget1;
    public GameObject moveTarget2;
    public MoveTrail moveTrail1;
    public MoveTrail moveTrail2;
    private Transform curTarget;
    private Transform prevTarget;
    private float moveSpeed;


    // Start is called before the first frame update
    void Start()
    {
        cntSeat = SeatList.Count;
        cntEmptySeat = cntSeat;
        moveSpeed = 10f;
        curTarget = moveTarget1.transform;
        prevTarget = moveTarget2.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag != "ChangeDirection")
        {   
            Debug.LogError("체인지디렉션박스 아님");
            Debug.LogError($"other tag : {other.tag}");
            return;
        }
        Debug.LogError("체인지디렉션박스 닿았음");
        if(curTarget.gameObject == other.gameObject)
            ChangeDirection();
    }
    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, curTarget.transform.position, Time.deltaTime * moveSpeed);
    }
    private void ChangeDirection()
    {
        Transform tempTransform = prevTarget;
        prevTarget = curTarget;
        curTarget = tempTransform;
    }

    public Seat ReqEmptySeat()
    {
        List<Seat> EmptyList = new List<Seat>();
        foreach(Seat seat in SeatList)
        {
            if(!seat.Assigned)
            {
                EmptyList.Add(seat);
            }
            else
            {
                continue;
            }
        }

        int randNum = Random.Range(0, EmptyList.Count);

        DecreaseCntEmptySeat();

        return EmptyList[randNum];
    }

    public bool IsThereEmptySeat()
    {
        if(cntEmptySeat > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void IncreaseCntEmptySeat()
    {
        cntEmptySeat++;
    }
    public void DecreaseCntEmptySeat()
    {
        cntEmptySeat--;
    }
}
