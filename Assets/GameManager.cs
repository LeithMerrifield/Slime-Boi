using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject m_wall = null;
    public GameObject m_player = null;

    private void Update()
    {
        if(m_player != null && m_wall != null)
        {
            m_wall.transform.position = new Vector3(m_player.transform.position.x, m_player.transform.position.y, m_wall.transform.position.z);
        }

    }

}
