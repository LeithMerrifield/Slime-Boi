using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public GameObject m_cheatMenu = null;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            m_cheatMenu.SetActive(!m_cheatMenu.activeSelf);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
}
