using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float m_rotationSpeed = 1f;
    public float m_amplitude = 0.05f;
    public float m_frequency = 1f;

    Vector3 offset = new Vector3();
    Vector3 tempUp = new Vector3();

    // Update is called once per frame
    void Update()
    {
        Bob();
    }

    public void Bob()
    {
        offset = transform.position;
        Quaternion temp = new Quaternion(0f, m_rotationSpeed * Time.deltaTime, 0f, 0f);
        transform.Rotate((new Vector3(0f, -1f, 0)) * m_rotationSpeed * Time.deltaTime * 50f);

        tempUp = offset;
        tempUp.y += Mathf.Sin(Time.fixedTime * Mathf.PI * m_frequency) * m_amplitude;
        transform.position = tempUp;
    }
}