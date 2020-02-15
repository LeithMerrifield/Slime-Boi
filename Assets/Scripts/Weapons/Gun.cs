using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : WeaponBase
{
    public GameObject m_barrel = null;
    public GameObject m_bullet = null;
    public float m_fireRate = 1;
    public int m_thrust = 100;
    public int m_spread = 1;
    public int m_spreadDistance = 5;
    public int m_recoilAmount = 5;

    private bool m_hasFired = false;
    private float m_timeSinceLastShot = 0.0f;

    public override void Attack()
    {
        // if the time since you last fired is less than the firerate, dont fire
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
            bullet.transform.position = m_barrel.transform.position;
            bullet.GetComponent<Bullet>().m_gun = this;
            bullet.transform.rotation = transform.parent.rotation;
            bullet.transform.rotation = Quaternion.identity;
            bullet.transform.Rotate(new Vector3(0,  switchSide * (i * m_spreadDistance),0));

            // Not sure if this even works :/
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

        if(m_playerRigidBody.GetComponent<Character>() != null)
        {
            if (m_playerRigidBody.velocity.magnitude > m_playerRigidBody.GetComponent<Character>().m_speedLimit)
            {
                m_playerRigidBody.velocity = m_playerRigidBody.velocity.normalized * m_playerRigidBody.GetComponent<Character>().m_speedLimit;
                return;
            }

            transform.parent.parent.parent.GetComponent<Rigidbody>().AddForce(-1 * (m_barrel.transform.forward) * m_recoilAmount, ForceMode.Impulse);
        }
    }
}
