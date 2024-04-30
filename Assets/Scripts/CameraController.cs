using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    private Vector3 offest;
    void Start()
    {
        offest = transform.position - target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPostion = new Vector3(transform.position.x, transform.position.y, offest.z + target.position.z);
        transform.position = Vector3.Lerp(transform.position, newPostion, 10 * Time.deltaTime);
    }
}
