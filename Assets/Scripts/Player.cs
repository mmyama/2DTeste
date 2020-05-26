using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float Speed;
    public float JumpForce;

    private bool isJumping;
    private bool doubleJump;

    private Rigidbody2D _rigidBody2D;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;

        
        if (Input.GetAxis("Horizontal") > 0f)
        {
            _animator.SetBool("isRunning", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        
        else if (Input.GetAxis("Horizontal") < 0f)
        {
            _animator.SetBool("isRunning", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        } 
        else
        {
            _animator.SetBool("isRunning", false);
        }
        
            
    }

    private void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                _rigidBody2D.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                isJumping = true;
                _animator.SetBool("isJumping", true);
            } else if (!doubleJump)
            {
                _rigidBody2D.AddForce(new Vector2(0f, JumpForce*0.5f), ForceMode2D.Impulse);
                doubleJump = true;
                _animator.SetBool("_doubleJump", true);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isJumping = false;
            doubleJump = false;
            _animator.SetBool("isRunning", false);
            _animator.SetBool("isJumping", false);
            _animator.SetBool("_doubleJump", false);
        }
    }

}
