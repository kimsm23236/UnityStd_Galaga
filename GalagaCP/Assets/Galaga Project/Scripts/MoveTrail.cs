using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrail : MonoBehaviour
{
    public MovePoint root;
    private MovePoint tail;

    // Start is called before the first frame update
    void Start()
    {
        DFS(root);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DFS(MovePoint mp)
    {
        if(mp.next == null || mp.next == default)
        {
            tail = mp;
        }
        else
        {
            DFS(mp.next);
        }
    }
}
