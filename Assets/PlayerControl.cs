using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    bool grounded = false;
    bool jump = false;
    bool shoot = false;
    bool facingRight = true;
    bool isAlive = true;

    float maxVel = 1f;
    float moveVel = 40f;
    float jumpVel = 30f;
    float shootCD = 0;
    float shootCDMax = .3f;
    float lastFacing = 1f;

    int healthMax = 10;
    public int health = 10;
    public int baseDmg = 2;

    public GameObject shotPrefab;

    private Transform groundFind;

	void Start ()
    {
        groundFind = transform.Find("groundFind");
	}
	
	void Update ()
    {
        grounded = Physics2D.Linecast(transform.position, groundFind.position, 1 << LayerMask.NameToLayer("Ground"));
        if (Input.GetKeyDown("space") && grounded)
        {
            jump = true;
        }

        if (!grounded)
        {
            moveVel = 10f;
        }
        else
        {
            moveVel = 40f;
        }

        if (shootCD > 0)
        {
            shootCD -= Time.deltaTime;
        }
        else if (shootCD <= 0 && Input.GetKeyDown("left shift"))
        {
            shoot = true;
        }

    }

    void FixedUpdate() {

        float axis = Input.GetAxis("Horizontal");

        if (GetComponent<Rigidbody2D>().velocity.x * axis < maxVel)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * axis * moveVel);
        }

        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxVel)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxVel, GetComponent<Rigidbody2D>().velocity.y);
        }

        if (grounded && Mathf.Abs(axis) <= 0)
        {
            GetComponent<Rigidbody2D>().velocity /= 1000;
        }


        if (axis > 0 && !facingRight)
            Flip();
        else if (axis < 0 && facingRight)
            Flip();

        if (jump)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpVel));
            jump = false;
        }
        if (shoot)
        {
            var shot = (GameObject)Instantiate(shotPrefab, (transform.position + (Vector3.right * lastFacing).normalized / 8), Quaternion.identity);
            shot.GetComponent<Rigidbody2D>().velocity = (Vector3.right * lastFacing).normalized * 6;
            shot.GetComponent<Shot>().damage = baseDmg;
            Destroy(shot, 2.0f);
            shoot = false;
            shootCD = shootCDMax;
        }
        
    }

    void Flip()
    {
        facingRight = !facingRight;

        //Vector3 sprScale = transform.localScale;
        //sprScale.x *= -1;
        //transform.localScale = sprScale;
        lastFacing *= -1;
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
    }
    
}
