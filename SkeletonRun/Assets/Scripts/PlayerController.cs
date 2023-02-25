using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rigidbody;
    private Animator bodyAnimator;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        /// animator is guaranteed to come from the first child
        bodyAnimator = transform.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        
    }
}
