using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject m_canvas = null;
    public GameObject m_bullet = null;
    public bool m_automatic = false;
    public float m_fireRate = 1;
    public int m_damage = 100;
    public int m_thrust = 100;
    public int m_spread = 1;
    public int m_spreadDistance = 5;
    public int m_recoilAmount = 5;

    [HideInInspector]public Vector3 m_originalScale;
    [HideInInspector]public bool m_active = false;
    private bool m_hasFired = false;
    private float m_timeSinceLastShot = 0.0f;

    private void Start()
    {
        m_originalScale = transform.localScale;
    }
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

        int switchSide = -1;

        for(int i = 0; i < m_spread; ++i)
        {
            var bullet = Instantiate(m_bullet);
            bullet.SetActive(true);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.parent.rotation;
            bullet.transform.rotation = Quaternion.identity;
            bullet.transform.Rotate(new Vector3(0,  switchSide * (i * m_spreadDistance),0));

            switch(switchSide)
            {
                case -1:
                    switchSide = 1;
                    break;
                default:
                    switchSide = -1;
                    break;
            }

            
            bullet.GetComponent<Rigidbody>().AddForce(transform.parent.forward * m_thrust, ForceMode.Impulse);
        }

        transform.parent.parent.GetComponent<Rigidbody>().AddForce(-1 * (transform.parent.forward) * (m_spread * m_recoilAmount), ForceMode.Impulse);
    }
}
