﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float m_speed = 0.0f;
    public float m_jumpHeight = 0.0f;
    public int m_maxJumps = 0;

    private Humanoid m_humanoid = null;
    private int m_jumps = 0;
    private Rigidbody m_rb = null;

    private bool m_space = false;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_humanoid = GetComponent<Humanoid>();
    }

    void FixedUpdate()
    {
        switch (m_humanoid.m_movementState)
        {
            case MOVEMENTSTATE.GROUNDED:
                GroundedMovement();
                break;
            case MOVEMENTSTATE.JUMPING:
                GroundedMovement();
                //JumpingMovement();
                break;
        }

        SpeedLimit();
        Jump();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && m_jumps < m_maxJumps)
        {
            m_space = true;
        }
    }

    private void SpeedLimit()
    {
        // Grabs velocity in a way that shows what direction you are heading
        var localVel = transform.InverseTransformDirection(m_rb.velocity);


        // Only clamps speed if its not downward otherwise it would look like you're floating
        if (!(localVel.y < 0))
        {
            m_rb.velocity = Vector3.ClampMagnitude(m_rb.velocity, 10f);
        }
    }

    private void Jump()
    {
        if (m_space)
        {
            m_jumps++;
            var jump = new Vector3(0.0f, m_jumpHeight);
            m_rb.AddForce(jump, ForceMode.Impulse);
            m_space = false;
        }
    }

    private void JumpingMovement()
    {

        if (Input.GetKey("a"))
        {
            var amount = -m_speed;
            m_rb.AddForce(amount, m_rb.velocity.y, 0.0f, ForceMode.Force);

        }

        if (Input.GetKey("d"))
        {
            var amount = m_speed;
            //var temp = body;
            //temp.velocity = new Vector3(amount, body.velocity.y, 0f);

            //body.velocity = new Vector3(amount, body.velocity.y, 0f);
            m_rb.AddForce(amount, m_rb.velocity.y, 0.0f, ForceMode.Force);
        }
    }

    private void GroundedMovement()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) )
        {
            var h = Input.GetAxisRaw("Horizontal");
            h *= m_speed;

            var velocity = new Vector3(h, m_rb.velocity.y, m_rb.velocity.z);
            m_rb.velocity = velocity;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            m_jumps = 0;
        }
    }
}