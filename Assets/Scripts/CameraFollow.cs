using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject objectToFollow;
    public float speed = 0.1f;
    public float offset = 5f;

    // Update is called once per frame
    void LateUpdate()
    {
        float interpolate = 1f - Mathf.Pow(1f - speed, Time.deltaTime * 30f);
        float buffer = speed * Time.deltaTime;

        Vector3 position = this.transform.position;
        position.y = Mathf.Lerp(this.transform.position.y, objectToFollow.transform.position.y + offset, interpolate);
        position.x = Mathf.Lerp(this.transform.position.x, objectToFollow.transform.position.x, interpolate);

        //position.x = objectToFollow.transform.position.x;
        //position.y = objectToFollow.transform.position.y;

        this.transform.position = position;
    }
}