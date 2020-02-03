using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float m_fireRate = 1;
    public bool m_automatic = false;
    public int m_damage = 100;
    public int m_thrust = 100;

    private float m_timeSinceLastShot = 0.0f;
    private bool m_hasFired = false;
    public GameObject m_bullet = null;

    public void FireGun()
    {
        if (m_hasFired && (Time.realtimeSinceStartup - m_fireRate) > m_timeSinceLastShot)
        {
            m_hasFired = false;
        }
        else if(m_hasFired)
            return;

        if(!m_hasFired)
        {
            m_timeSinceLastShot = Time.realtimeSinceStartup;
            m_hasFired = true;
        }

        var bullet = Instantiate(m_bullet);
        bullet.SetActive(true);
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;

        
        bullet.GetComponent<Rigidbody>().AddForce(transform.parent.forward * m_thrust, ForceMode.Impulse);
    }
}
