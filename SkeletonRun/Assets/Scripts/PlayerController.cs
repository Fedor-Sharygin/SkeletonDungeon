using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rigidbody;
    private Animator bodyAnimator;
    private Vector2[] bodyBottomVerts;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        /// animator is guaranteed to come from the first child
        bodyAnimator = transform.GetComponentInChildren<Animator>();

        BoxCollider2D bodyBox = GetComponent<BoxCollider2D>();
        Vector2 tpOffset = bodyBox.size / 2;
        /// find local positions of bottom left, bottom center, and bottom right of the body
        bodyBottomVerts = new Vector2[3];
        bodyBottomVerts[0] = bodyBox.offset - tpOffset;
        tpOffset.x *= -1;
        bodyBottomVerts[2] = bodyBox.offset - tpOffset;
        tpOffset.x = 0;
        bodyBottomVerts[1] = bodyBox.offset - tpOffset;
    }

    private Vector2 nextVelocity;
    private bool isGrounded = true;
    private bool prevGrounded = true;
    private void Update()
    {
        nextVelocity = rigidbody.velocity;
        Move();
        Jump();
        Vector3 sprRotation = transform.GetChild(0).localEulerAngles;
        if (nextVelocity.x < 0)
            sprRotation.y = 0;
        if (nextVelocity.x > 0)
            sprRotation.y = 180;
        transform.GetChild(0).localEulerAngles = sprRotation;
        rigidbody.velocity = nextVelocity;

        /// flying + landing animations
        bodyAnimator.SetBool("isGrounded", isGrounded);
        /// if we landed in the previous frame => play land animation
        if (!prevGrounded && isGrounded)
            bodyAnimator.SetTrigger("land");

        prevGrounded = isGrounded;
    }

    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 9f;
    private float curSpeed = 0f;
    private void Move()
    {
        nextVelocity.x = Input.GetAxis("hor");

        curSpeed = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
            curSpeed = runSpeed;
        nextVelocity.x *= curSpeed;
        bodyAnimator.SetFloat("speed", Mathf.Abs(nextVelocity.x));
    }

    [SerializeField] private float jumpForce = 10f;
    private void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
        {
            isGrounded = false;
            nextVelocity.y = jumpForce;
            bodyAnimator.SetTrigger("jump");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 v2pos = transform.position;
        if (Physics2D.Raycast(v2pos + bodyBottomVerts[1], Vector2.down, .1f) ||
            Physics2D.Raycast(v2pos + bodyBottomVerts[0], Vector2.down, .1f) ||
            Physics2D.Raycast(v2pos + bodyBottomVerts[2], Vector2.down, .1f))
        {
            isGrounded = true;
        }
    }

}
