using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float m_speed = 0.0f;
    public float m_jumpHeight = 0.0f;
    public int m_maxJumps = 0;

    private int m_jumps = 0;
    private Rigidbody m_rb = null;

    private bool m_space = false;
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var h = Input.GetAxisRaw("Horizontal");
        h *= m_speed;

        var velocity = new Vector3(h, m_rb.velocity.y, m_rb.velocity.z);

        m_rb.velocity = velocity;

        if (m_space)
        {
            m_jumps++;
            var jump = new Vector3(0.0f, m_jumpHeight);
            m_rb.AddForce(jump, ForceMode.Impulse);
            m_space = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && m_jumps < m_maxJumps)
        {
            m_space = true;
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