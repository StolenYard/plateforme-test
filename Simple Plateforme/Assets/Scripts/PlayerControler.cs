using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour {

    public float maxSpeed;
    Rigidbody2D RB;
    Animator anim;
    bool facingRight = true;
    bool grounded = false;
    public float jumpForce = 500f;

    [SerializeField]
    private float radiusCircle;

    [SerializeField]
    private GameObject groundCheck;

    [SerializeField]
    private LayerMask whatIsGround;


    // for shooting
    public Transform guntip;
    public GameObject bullet;
    float fireRate = 0.5f;
    float nextFire = 0f; 

    void Start ()
    {
        RB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}

    void Update()
    {

        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Ground", false);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
        }

        // player shooting
        if (Input.GetAxisRaw("Fire1") > 0) fireRocket();
    }


        void FixedUpdate ()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.transform.position, radiusCircle, whatIsGround);
        anim.SetBool("Ground", grounded);

        Vector3 easevelocity = GetComponent<Rigidbody2D>().velocity;
        easevelocity.y = GetComponent<Rigidbody2D>().velocity.y;
        easevelocity.z = 0.0f;
        easevelocity.x *= 0.75f;

        if (grounded)
        {
            GetComponent<Rigidbody2D>().velocity = easevelocity;
        }

        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
        float move = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(move));

        RB.velocity = new Vector2(move * maxSpeed, RB.velocity.y);

        if (move > 0 && !facingRight)     
            flip();
        else if (move < 0 && facingRight)
        {
            flip();
        }
	}

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.transform.position, radiusCircle);
    }

    void flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void fireRocket ()
    {
        if(Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            if (facingRight)
            {
                Instantiate(bullet, guntip.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            }
            else if (!facingRight)
            {
                Instantiate(bullet, guntip.position, Quaternion.Euler(new Vector3(0, 0, 180f)));
            }
        }
    }
}
