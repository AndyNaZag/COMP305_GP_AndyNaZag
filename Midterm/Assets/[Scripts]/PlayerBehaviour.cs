using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Player Movement")]
    public float speed = 5;   
    public float jumpForce = 1;

    [Header("Player Actions")]
    public bool isInteracting;
    public bool openChest;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundRadius;
    public LayerMask groundLayerMask;
    public bool isGrounded;

    private Rigidbody2D _rigidbody;         //guion bajo para private variables
    private Animator _animator;
    private float move;    

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        openChest = false;
    }

    
    void Update()
    {
        Move();
        Interact();
        isDucking();
    }

    private void Move()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayerMask);
        
        move = Input.GetAxis("Horizontal");
        _animator.SetFloat("xSpeed", Mathf.Abs(move));
        _animator.SetBool("isGrounded", isGrounded);       
        
        //Flip
        var x = (move < 0) ? -1 : 1;  //Ternary operator
        transform.localScale = new Vector3(x, 1.0f);

        transform.position += new Vector3(move, 0, 0) * Time.deltaTime * speed;

        if(Input.GetButtonDown("Jump") && isGrounded)   
        {
            _rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);       //Force mode para aplicar Impulse
        }                   
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }

    private void isDucking()
    {
        if (Input.GetKey(KeyCode.S) && (move == 0))
        {
            _animator.SetBool("isDucking", true);   
        }
        else {
            _animator.SetBool("isDucking", false);  
        }         
    }

    private void Interact()
    {
        isInteracting = Input.GetKey(KeyCode.E);
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if(isInteracting && other.gameObject.CompareTag("Chest"))
        {
            openChest = true;
        }
    }

}
