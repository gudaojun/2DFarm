using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator[] animators;
    public float speed;
    private float inputX;
    private float inputY;
    private bool isMoving;
    private Vector2 moveInput;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animators = rb.GetComponentsInChildren<Animator>();
    }

    void Update()
    {
        PlayerInput();
        SwichAnimation();
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
        if (Input.GetKey(KeyCode.LeftShift))
        {
            inputX = 0.5f * inputX;
            inputY = 0.5f * inputY;
        }
        moveInput = new Vector2(inputX, inputY);
        isMoving = moveInput != Vector2.zero;
    }

    /// <summary>
    /// �ƶ�
    /// </summary>
    private void Movement()
    {
        rb.MovePosition(rb.position+moveInput * speed * Time.deltaTime);
    }

    private void SwichAnimation()
    {
        foreach (var anim in animators)
        {
            anim.SetBool("IsMoving", isMoving);
            if (isMoving)
            {
                anim.SetFloat("InputX",inputX);
                anim.SetFloat("InputY",inputY);
            }
        }
    }
}
