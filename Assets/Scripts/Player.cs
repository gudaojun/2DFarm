using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    private float inputX;
    private float inputY;

    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        Movement();
    }
    /// <summary>
    /// 获取Player Input输入
    /// </summary>
    private void PlayerInput()
    {
        //if(inputY==0) 只能单方向移动
        inputX = Input.GetAxisRaw("Horizontal");
        //if(inputX==0)
        inputY = Input.GetAxisRaw("Vertical");

        //避免斜角的快速移动
        if (inputX!=0&&inputY!=0)
        {
            inputX = 0.6f;
            inputY = 0.6f;
        }
        moveInput = new Vector2(inputX, inputY);
    }

    /// <summary>
    /// 移动
    /// </summary>
    private void Movement()
    {
        rb.MovePosition(rb.position+moveInput * speed * Time.deltaTime);
    }

}
