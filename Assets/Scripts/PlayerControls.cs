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
    public float PopCounter = 1.0f;
    public float ScorpionTIme = 1.0f;
    private float popElapsed = 0;
    private float deadElapsed = 0;
    private float scorpionElapsed = 0;
    private bool isDead = false;
    private bool isGrinding = false;
    private bool isPopping = false;
    private bool inScorpion = false;
    private float currentGrindX = float.NegativeInfinity;


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
        if (inScorpion)
        {
            anim.SetBool("Scorpion", true);
            scorpionElapsed += Time.deltaTime;
            if (scorpionElapsed >= ScorpionTIme)
            {
                scorpionElapsed = 0;
                anim.SetBool("Scorpion", false);
                //transform.position += new Vector3(horizontal * Time.deltaTime * Speed, 0, 0);
                Die();
       
            }
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
                if (isPopping)
                {
                    popElapsed += Time.deltaTime;
                    if (popElapsed >= PopCounter)
                    {
                        popElapsed = 0;
                        isPopping = false;
                    }
                }
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
        anim.SetBool("Sliding", false);
        anim.SetBool("Grinding", false);
        anim.SetBool("Dead", true);

        Speed = 0;
        isDead = true;

    }

    private void Respawn()
    {
        deadElapsed = 0;
        isDead = false;
        anim.SetBool("Dead", false);
        anim.SetBool("Scorpion", false);
        inScorpion = false;
        transform.position = Vector3.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Die();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Curb")
        {
            if (isPopping)
            {
                isGrinding = true;
                currentGrindX = transform.position.x;
                isPopping = false;
            }
            else
            {
                isPopping = false;
                inScorpion = true;
            }
        }
        else
        {
            isGrinding = false;
            currentGrindX = float.NegativeInfinity;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Curb" && isGrinding && transform.position.x != currentGrindX)
        {
            isGrinding = false;
        }
    }

}
