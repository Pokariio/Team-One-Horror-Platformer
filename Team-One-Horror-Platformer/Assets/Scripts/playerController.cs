using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float moveSpd;

    public float drag;

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

    private void playerIn()
    {
        horizontalIn = Input.GetAxisRaw("Horizontal");
        verticalIn = Input.GetAxisRaw("Vertical");  
    }

    private void movePlyr()
    {
        moveDir = orientation.forward * verticalIn + orientation.right * horizontalIn;
        rb.AddForce(moveDir.normalized * moveSpd * 10f, ForceMode.Force);
    }

    private void speedCtrl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpd)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpd;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
