using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // null 체크
        if(controller == null)
            return;
        
        // 이동
        float xInput = Input.GetAxis("Horizontal");
        float xSpeed = xInput * speed;
        Vector3 moveVector = new Vector3(xSpeed, 0f, 0f);
        controller.Move(moveVector * Time.deltaTime);

    }
}
