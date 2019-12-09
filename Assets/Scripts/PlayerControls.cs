using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    private Animator anim;
    public float Speed = 0;
    public float MaxSpeed = 5;
    public float Acceleration = 0.1f;
    public float Deceleration = 1f;
    public float DeadCounter = 3.0f;
    private float deadElapsed = 0;
    private bool isDead = false;
    private bool isGrinding = false;
    private bool isPopping = false;

    public bool Grinding { get { return isGrinding; } }
    public bool Dead { get { return isDead; } }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            deadElapsed += Time.deltaTime;
            if (deadElapsed >= DeadCounter)
            {
                Respawn();
            }
            return;
        }
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var startingPop = Input.GetAxis("Jump") > 0 && isPopping == false;
        Debug.Log(vertical);
        var isPowersliding = vertical > 0;
        if (startingPop)
        {
            isPopping = true;
            anim.SetFloat("Direction", 0);
            anim.SetTrigger("Popping");
        }
        else
        {
            if (isPowersliding)
            {
                anim.SetBool("Sliding", true);
                Speed -= Deceleration * Time.deltaTime;
                if (Speed < 0) { Speed = 0; }
            }
            else
            {
                anim.SetBool("Sliding", false);
                anim.SetBool("Grinding", isGrinding);
                anim.SetFloat("Direction", horizontal);
                if (Speed < MaxSpeed)
                {
                    Speed += Acceleration * Time.deltaTime;
                }

            }
        }
            
        var oldpos = transform.position += new Vector3(horizontal * Time.deltaTime * Speed, 0, 0);
    }

    private void Die()
    {
        anim.SetBool("Dead", true);
        Speed = 0;
        isDead = true;

    }

    private void Respawn()
    {
        deadElapsed = 0;
        isDead = false;
        anim.SetBool("Dead", false);
        transform.position = Vector3.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Die();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Curb" && isPopping)
        {
            isGrinding = true;
            isPopping = false;
        }
        else
        {
            isGrinding = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Curb" && isGrinding)
        {
            isGrinding = false;
        }
    }

}
