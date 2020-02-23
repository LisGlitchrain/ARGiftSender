using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    Transform cameraTransform;
    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }
    // Update is called once per frame
    void Update()
    {
        var lookAt = cameraTransform.transform.position;
        lookAt.y = transform.position.y;
        transform.LookAt(lookAt, Vector3.up);
    }
}
