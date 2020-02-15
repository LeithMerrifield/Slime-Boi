using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : Humanoid
{
    [Header("Character Variables")]
    public GameObject m_subHand = null;
    public GameObject m_landingDebris = null;
    public Camera m_camera = null;


    public float m_pickupRadius = 5.0f;
    public float m_speedLimit = 5.0f;

    public int m_debrisAmount = 5;
    public float m_speedToDebris = 5.0f;


    private Rigidbody m_rb = null;
    private GameObject m_previousGun = null;
    private bool m_landingFlag = false;

    protected override void Start()
    {
        base.Start();
        m_type = HUMANOIDTYPE.PLAYER;
        m_rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Pickup();
        MouseMovement();

        if(m_weapon != null)
            Attack();
        //RaycastDown();
    }

    IEnumerator LandingCooldown()
    {
        yield return new WaitForSeconds(1);
        m_landingFlag = false;
    }

    void RaycastDown()
    {
        if (m_landingFlag)
            return;

        var localVel = transform.InverseTransformDirection(m_rb.velocity);
        RaycastHit hit;
        if(Physics.Raycast(transform.position,-1 * transform.up,out hit,transform.localScale.magnitude / 2.0f + 0.1f))
        {
            Debug.DrawRay(transform.position, -1 * transform.up * hit.distance, Color.green);

            if(m_landingDebris != null && localVel.y < -5.0f)
            {
                var rightCorner = new Vector3(transform.localPosition.x + (transform.localScale.magnitude / 2.0f) - 0.2f, transform.localPosition.y + (-(transform.localScale.magnitude / 2.0f)), 0.0f);
                var leftCorner = new Vector3(transform.localPosition.x + (-(transform.localScale.magnitude / 2.0f)) + 0.2f, transform.localPosition.y + ( -(transform.localScale.magnitude / 2.0f)), 0.0f);

                for(int i = 0; i < m_debrisAmount; ++i)
                {
                    var left =  Instantiate(m_landingDebris,leftCorner,Quaternion.identity);
                    var right = Instantiate(m_landingDebris,rightCorner,Quaternion.identity);

                }

                m_landingFlag = true;
                StartCoroutine(LandingCooldown());
            }
        }
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

    void Pickup()
    {
        var objects = Physics.OverlapSphere(transform.position,m_pickupRadius);
        List<GameObject> guns = new List<GameObject>();
        GameObject closestGun = null;

        // loops through to remove non guns
        // layermask wasnt working
        int count = 0;
        foreach(var item in objects)
        {
            if((item.tag == "Gun" || item.tag == "Sword") && m_weapon == null)
            {
                    guns.Add(item.gameObject);
            }
            else if(m_weapon != null)
            {
                if((item.tag == "Gun" || item.tag == "Sword") && item.gameObject != m_weapon.gameObject)
                    guns.Add(item.gameObject);
            }

            count++;
        }

        // If there are no pickups nearby then return
        if(guns.Count == 0)
        {
            if(m_previousGun != null)
                m_previousGun.GetComponent<Outline>().enabled = false;
            return;
        }
        
        // Compares the distance between all the pickups to only display the closest
        for( int i = 0 ; i < guns.Count; ++i)
        {
            if (guns[i].GetComponent<WeaponBase>().m_isActive)
                continue;
            if(closestGun == null)
                closestGun = guns[i];
            else
            {
                if(Vector3.Distance(guns[i].transform.position,transform.position) < Vector3.Distance(closestGun.transform.position,transform.position))
                {
                    closestGun.GetComponent<Outline>().enabled = false;
                    closestGun = guns[i];
                }
            }
        }

        if (closestGun == null)
            return;

        //closest gun outline activate
        closestGun.GetComponent<Outline>().enabled = true;
        m_previousGun = closestGun.gameObject;

        if(Input.GetKeyDown(KeyCode.E))
        {
            var temp = m_weapon.transform.position;
            m_weapon.transform.parent = null;
            m_weapon.transform.position = closestGun.transform.position;
            m_weapon.GetComponent<Outline>().enabled = true;
            m_weapon.GetComponent<Rotate>().enabled = true;

            if(m_weapon.GetComponent<Animator>() != null)
            {
                m_weapon.GetComponent<Animator>().enabled = false;
            }

            m_weapon.m_isActive = false;


            m_weapon = closestGun.GetComponent<WeaponBase>();
            m_weapon.transform.parent = m_subHand.transform;
            m_weapon.transform.localEulerAngles = Vector3.zero;
            m_weapon.transform.localPosition = Vector3.zero;
            m_weapon.transform.localScale = m_weapon.m_originalScale;
            m_weapon.GetComponent<Outline>().enabled = false;
            m_weapon.GetComponent<Rotate>().enabled = false;
            m_weapon.m_playerRigidBody = m_rb;
            m_weapon.m_isActive = true;

            if (m_weapon.GetComponent<Animator>() != null)
            {
                m_weapon.GetComponent<Animator>().enabled = true;
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_pickupRadius);
    }
}
