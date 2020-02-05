using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int m_decay = 2;
    public GameObject bullet = null;
    public int m_explodeCount = 10;
    public int m_explodeSize = 2;

    [HideInInspector]public bool m_isCopy = false; // used to deactivate a clone from cloning again

    private Vector3 m_position;
    private Vector3 m_velocity;

    private Vector3 m_previousPosition = Vector3.zero;
    private List<GameObject> m_smallBullets = new List<GameObject>();


    private void Start()
    {
        StartCoroutine("StartLife");
    }

    IEnumerator StartLife()
    {
        yield return new WaitForSeconds(m_decay);

        if (!m_isCopy)
            Explode();
        else
        {
            transform.localScale *= m_explodeSize;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        Destroy(gameObject);

    }

    private void LateUpdate()
    {
        m_previousPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Ground" && !m_isCopy)
        {
            Explode();
            Destroy(gameObject);
        }
    }

    private void Explode()
    {
        for (int i = 0; i < m_explodeCount; i++)
        {
            m_smallBullets.Add(Instantiate(bullet));
            m_smallBullets[i].transform.position = transform.position;
            m_smallBullets[i].transform.localScale /= m_explodeSize;
            m_smallBullets[i].GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
            m_smallBullets[i].GetComponent<Bullet>().m_isCopy = true;
            m_smallBullets[i].GetComponent<Rigidbody>().mass = 0;
            // m_smallBullets[i].GetComponent<TrailRenderer>().enabled = false;
        }
    }
}
