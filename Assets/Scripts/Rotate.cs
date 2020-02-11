using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public GameObject m_object = null; // Doesn't need to be specified
    public float m_rotationSpeed = 1f;
    public float m_amplitude = 0.05f;
    public float m_frequency = 1f;

    public bool m_rotateAngled = false;
    Vector3 offset = new Vector3();
    Vector3 tempUp = new Vector3();

    // Update is called once per frame
    void Update()
    {
        Bob();
    }
    private void Start()
    {
        if(m_object == null)
        {
            m_object = transform.gameObject;
        }
    }

    public void Bob()
    {
        offset = m_object.transform.position;
        Quaternion temp = new Quaternion(0f, m_rotationSpeed * Time.deltaTime, 0f, 0f);
        m_object.transform.Rotate((new Vector3(0f, -1f, 0)) * m_rotationSpeed * Time.deltaTime * 50f);

        if(m_rotateAngled)
            m_object.transform.localRotation = Quaternion.Euler(new Vector3(-45.0f, m_object.transform.localRotation.eulerAngles.y, 0.0f));

        tempUp = offset;
        tempUp.y += Mathf.Sin(Time.fixedTime * Mathf.PI * m_frequency) * m_amplitude;
        m_object.transform.position = tempUp;
    }
}