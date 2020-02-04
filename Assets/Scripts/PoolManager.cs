using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject m_Pool = null;
    public static List<GameObject> m_bulletPool = new List<GameObject>();
    public GameObject m_bulletPrefab = null;

    private static int m_bulletPoolSize = 1000;
    private static Vector3 m_poolLocation = new Vector3( 999.0f, 999.0f, 999.0f );
    private static int m_currentBulletCount = 0;
    public static bool m_stop = false;

    public void Start()
    {
        for(int i = 0; i < m_bulletPoolSize; ++i)
        {
            m_bulletPool.Add(Instantiate(m_bulletPrefab));
            m_bulletPool[i].SetActive(false);
            m_bulletPool[i].transform.position = m_poolLocation;
            m_bulletPool[i].transform.parent = m_Pool.transform;

        }
    }

    public static GameObject GetBullet()
    {

        var bullet = m_bulletPool[m_currentBulletCount];


        bullet.SetActive(true);
        m_currentBulletCount++;
        return bullet;
    }

    public static void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bullet.transform.position = m_poolLocation;
        bullet.GetComponent<Bullet>().m_isCopy = false;

        m_currentBulletCount--;

        for (int i = 0; i < m_currentBulletCount; ++i)
        {
            if (!m_bulletPool[i].activeSelf)
            {
                int count = i;
                while (m_bulletPool[count + 1].activeSelf)
                {
                    var temp = m_bulletPool[count + 1];
                    m_bulletPool[count + 1] = m_bulletPool[count];
                    m_bulletPool[count] = temp;
                    count++;
                }
            }
        }

    }
}
