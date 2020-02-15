using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public int m_damage = 100;
    public bool m_isAutomatic = false;
    [HideInInspector] public bool m_isActive = false;
    [HideInInspector] public Rigidbody m_playerRigidBody = null;
    [HideInInspector] public Vector3 m_originalScale;


    protected virtual void Start()
    {
        m_originalScale = transform.localScale;
        if (transform.parent != null)
        {
            m_playerRigidBody = transform.parent.parent.parent.GetComponent<Rigidbody>();
            m_isActive = true;
        }
    }

    protected virtual void Update()
    {
        
    }
    public virtual void Attack()
    {

    }
}
