using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humanoid : MonoBehaviour
{
    [Header("Base Humanoid Variables")]
    public int m_health = 100;
    public WeaponBase m_weapon = null;
    public GameObject m_hand = null;
    public bool m_invincible = false;
    public bool m_invisible = false;

    public HUMANOIDTYPE m_type;

    protected virtual void Start()
    {

    }

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
        if(m_weapon.m_isAutomatic)
        {
            if (Input.GetKey(KeyCode.Mouse0))
                m_weapon.Attack();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                m_weapon.Attack();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider myCollider = collision.contacts[0].thisCollider;

        if (collision.transform.tag == "Bullet")
        {
            if (!(myCollider.tag == "Sword"))
                TakeDamage(collision.gameObject.GetComponent<Bullet>().m_gun.m_damage);
            else
                collision.gameObject.GetComponent<Bullet>().Explode();
            Destroy(collision.gameObject); // TODO
            return;
        }

        if(myCollider.transform.tag == "Sword" && collision.collider.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            return;
        }
    }
}

public enum HUMANOIDTYPE
{
    PLAYER = 0,
    ENEMY = 1
}
