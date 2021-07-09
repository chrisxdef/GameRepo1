using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalMovement;
    private float verticalMovement;
    private Vector3 playerInput;
    public float playerSpeed;
    public CharacterController player;
    public Transform cam;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Vector3 velocity;
    public bool isGrounded;
    public float jumpHeight = 2f;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        Vector3 move = transform.right * horizontalMovement + transform.forward * verticalMovement;
        player.Move(move.normalized * playerSpeed * Time.deltaTime);
        Debug.Log(player.velocity.magnitude);
        //Gravity and jump
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        player.Move(velocity * Time.deltaTime);
    }

}
