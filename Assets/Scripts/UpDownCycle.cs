using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownCycle : MonoBehaviour
{

    public float yUp = 0.2f;
    public float speed = 0.2f;

    Vector3 startPosition;
    Vector3 speedVect;

    bool goUp = true;
    private void Start()
    {
        startPosition = transform.localPosition;
        speedVect = new Vector3(0, speed, 0); 
    }

    // Update is called once per frame
    void Update()
    {
        if (goUp && transform.localPosition.y < startPosition.y + yUp)
            transform.localPosition += speedVect * Time.deltaTime;
        else if(goUp)
            goUp = false;
        if (!goUp && transform.localPosition.y > startPosition.y)
            transform.localPosition -= speedVect * Time.deltaTime;
        else if (!goUp)
            goUp = true;
    }
}
