using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public Vector3 move;
    public float jumpForce = 5;
    public float fallMulti = 5;
    public bool isGrounded = true;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform cam;

    public float mouseX;
    public float mouseY;

    CameraHandler cameraHandler;

    void Start()
    {
        rb = GetComponent <Rigidbody>();
        rb.freezeRotation = true;
    }
    
    void Update()
    {
        move.x = Input.GetAxisRaw("Horizontal");   
        move.z = Input.GetAxisRaw("Vertical");
        
        if (move.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.MovePosition(rb.position + moveDir.normalized * speed * Time.deltaTime);
            //controller.Move(move * speed * Time.deltaTime);
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
    }
    private void FixedUpdate()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * fallMulti * Time.deltaTime;
        }

        float delta = Time.fixedDeltaTime;
        if (cameraHandler != null)
        {
            cameraHandler.FollowTarget(delta);
            cameraHandler.HandleCameraRotation(delta, mouseX, mouseY);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }


    private void Awake()
    {
        cameraHandler = CameraHandler.singleton;
    }
}
