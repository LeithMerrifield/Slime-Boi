using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    public Gun m_gun = null; // to be replaced with just the gun script
    public GameObject m_hand = null;
    public int m_health = 100;
    public Camera m_camera = null;

    private void Update()
    {
        MouseMovement();
        Fire();
    }

    void MouseMovement()
    {
        RaycastHit hit;
        Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Vector3 dir = hit.point - m_hand.transform.position;
            m_hand.transform.rotation = Quaternion.LookRotation(dir);
            var angles = m_hand.transform.localEulerAngles;

            if (angles.y >= 180.0f)
            {
                m_hand.transform.localEulerAngles = new Vector3(angles.x, -90.0f);
            }
            else
            {
                m_hand.transform.localEulerAngles = new Vector3(angles.x, 90.0f);
            }
        }
    }

    void Fire()
    {
        if(m_gun.m_automatic)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                m_gun.FireGun();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                m_gun.FireGun();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Lava")
        {
            SceneManager.LoadScene(0);
        }
    }
}
