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
    /// ��ȡPlayer Input����
    /// </summary>
    private void PlayerInput()
    {
        //if(inputY==0) ֻ�ܵ������ƶ�
        inputX = Input.GetAxisRaw("Horizontal");
        //if(inputX==0)
        inputY = Input.GetAxisRaw("Vertical");

        //����б�ǵĿ����ƶ�
        if (inputX!=0&&inputY!=0)
        {
            inputX = 0.6f;
            inputY = 0.6f;
        }
        moveInput = new Vector2(inputX, inputY);
    }

    /// <summary>
    /// �ƶ�
    /// </summary>
    private void Movement()
    {
        rb.MovePosition(rb.position+moveInput * speed * Time.deltaTime);
    }

}
