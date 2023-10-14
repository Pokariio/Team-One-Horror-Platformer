using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float moveSpd;

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
        playerIn();
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
}
