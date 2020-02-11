using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humanoid : MonoBehaviour
{
    [Header("Base Humanoid Variables")]
    public int m_health = 100;
    public WeaponBase m_gun = null;
    public GameObject m_hand = null;
    public bool m_invincible = false;
    public bool m_invisible = false;

    public void TakeDamage(int damage)
    {
        if (m_invincible)
            return;

        m_health -= damage;
        if(m_health <= 0)
        {
            m_health = 0;
            gameObject.SetActive(false);
        }
    }

    public void ToggleInvisibility()
    {
        m_invisible = !m_invisible;
    }

    public void ToggleInvincibility()
    {
        m_invincible = !m_invincible;
    }

    protected virtual void Attack()
    {
        //if (m_gun.m_automatic)
        //{
        //    if (Input.GetKey(KeyCode.Mouse0))
        //    {
        //        m_gun.Attack();
        //    }
        //}
        //else
        //{
        //    if (Input.GetKeyDown(KeyCode.Mouse0))
        //    {
        //        m_gun.Attack();
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            m_gun.Attack();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Bullet")
        {
            Collider myCollider = collision.contacts[0].thisCollider;
            if (!(myCollider.tag == "Sword"))
                TakeDamage(collision.gameObject.GetComponent<Bullet>().m_gun.m_damage);
            else
                collision.gameObject.GetComponent<Bullet>().Explode();
            Destroy(collision.gameObject); // TODO
        }
    }
}
