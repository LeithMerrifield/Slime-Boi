using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humanoid : MonoBehaviour
{
    public int m_health = 100;
    public Gun m_gun = null;
    public GameObject m_hand = null;

    public void TakeDamage(int damage)
    {
        m_health -= damage;
    }
}
