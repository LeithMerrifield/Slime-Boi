using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    public Gun m_gun = null;
    public GameObject m_hand = null;
    public int m_health = 100;
    public Camera m_camera = null;
    public float m_pickupRadius = 5.0f;

    private GameObject m_previousGun = null;

    void Start()
    {
        m_gun.m_active = true;
    }

    private void Update()
    {
        Pickup();
        MouseMovement();
        Fire();
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
            if(item.tag == "Gun" && item.gameObject != m_gun.gameObject)
            {
                guns.Add(item.gameObject);
            }
            count++;
        }

        // If there are no pickups nearby then return
        if(guns.Count == 0)
        {
            if(m_previousGun != null)
                m_previousGun.GetComponent<Gun>().m_canvas.SetActive(false);
            return;
        }
        
        // Compares the distance between all the pickups to only display the closest
        for( int i = 0 ; i < guns.Count; ++i)
        {
            if(closestGun == null)
                closestGun = guns[i];
            else
            {
                if(Vector3.Distance(guns[i].transform.position,transform.position) < Vector3.Distance(closestGun.transform.position,transform.position))
                {
                    closestGun.GetComponent<Gun>().m_canvas.SetActive(false);
                    closestGun = guns[i];
                }
            }
        }

        //closest gun ui activate
        closestGun.GetComponent<Gun>().m_canvas.SetActive(true);
        m_previousGun = closestGun.gameObject;

        // Fak this stupid pick up shit
        if(Input.GetKeyDown(KeyCode.E))
        {
            var temp = m_gun.transform.position;
            m_gun.transform.parent = null;
            m_gun.transform.position = closestGun.transform.position;
            m_gun.transform.eulerAngles = new Vector3(0.0f,-90.0f,0.0f);

            m_gun = closestGun.GetComponent<Gun>();
            m_gun.transform.parent = m_hand.transform;
            m_gun.gameObject.transform.rotation = Quaternion.identity;
            m_gun.transform.position = temp;
            m_gun.transform.localScale = m_gun.m_originalScale;
        }
    }

    void Fire()
    {
        if(m_gun.m_automatic)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                m_gun.FireGun();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                m_gun.FireGun();
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
