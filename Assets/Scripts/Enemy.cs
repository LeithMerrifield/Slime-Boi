using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Humanoid
{
    [Header("Enemy Variables")]
    public LayerMask m_layerMask;
    public float m_detectRange = 10.0f;
    public int m_detectAngle = 10;

    private bool m_targetFound = false;
    private GameObject m_target = null;
   
    IEnumerator Firing()
    {
        while(true)
        {
            if(m_targetFound)
                Attack();
            yield return new WaitForSeconds(.1f);
        }
    }

    public void Start()
    {
        StartCoroutine(Firing());
    }

    public void Update()
    {
        FindPlayer();

        if(m_target != null)
            LookAtPlayer();
    }
    protected override void Attack()
    {
        m_gun.Attack();
    }

    void FindPlayer()
    {
        int loop = 360 / m_detectAngle;
        for(int i = 0; i < loop; i++)
        {
            var angle = i  * (m_detectAngle);
            var lDirection = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), Mathf.Cos(Mathf.Deg2Rad * angle));

            RaycastHit hit;
            if (Physics.Raycast(transform.position, lDirection, out hit, m_detectRange, m_layerMask))
            {
                Debug.DrawRay(transform.position, lDirection * hit.distance, Color.green);
                if(hit.collider.gameObject.tag == "Player" && !hit.collider.gameObject.GetComponent<Humanoid>().m_invisible)
                {
                    m_targetFound = true;
                    m_target = hit.collider.gameObject;
                    return;
                }
            }
        }

        m_targetFound = false;
        m_target = null;
    }

    void  LookAtPlayer()
    {
        Vector3 dir = m_target.transform.position - m_hand.transform.position;
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
