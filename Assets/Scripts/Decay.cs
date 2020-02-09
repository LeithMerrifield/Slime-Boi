using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decay : MonoBehaviour
{
    public int m_timeTillDecay = 2;

    private void Start()
    {
        StartCoroutine(Poop());
    }

    IEnumerator Poop()
    {
        yield return new WaitForSeconds(m_timeTillDecay);
        Destroy(gameObject);
    }
}
