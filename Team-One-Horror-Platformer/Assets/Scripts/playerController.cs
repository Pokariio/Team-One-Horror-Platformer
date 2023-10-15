using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float moveSpd;

    public float drag;

    public float jumpForce;
    public float jumpCoolDown;
    public float airMultiplier;
    bool readyToJump = true;

    public KeyCode jumpKey = KeyCode.Space; 

    public float playerHeight;
    public LayerMask ground;
    bool onGround;

    public Transform orientation;

    float horizontalIn;
    float verticalIn;

    Vector3 moveDir;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics.Raycast(transform.position, Vector3  .down, playerHeight * .5f + .2f, ground);

        playerIn();
        speedCtrl();

        if (onGround)
            rb.drag = drag;
        else
            rb.drag = 0;
    }

    void FixedUpdate()
    {
        movePlyr();
    }

    //handles player input
    private void playerIn()
    {
        horizontalIn = Input.GetAxisRaw("Horizontal");
        verticalIn = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && onGround)
        {
            readyToJump = false;
            jump();
            Invoke(nameof(resetJump), jumpCoolDown);
        }
    }

    //actually moves the player
    private void movePlyr()
    {
        moveDir = orientation.forward * verticalIn + orientation.right * horizontalIn;

        //ground movement
        if (onGround)
            rb.AddForce(moveDir.normalized * moveSpd * 10f, ForceMode.Force);

        //air movement
        else if (!onGround)
            rb.AddForce(moveDir.normalized * moveSpd * 10f * airMultiplier, ForceMode.Force);
    }

    //prevents the player from sliding and overaccelerating
    private void speedCtrl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpd)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpd;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void resetJump()
    {
        readyToJump = true;
    }
}
