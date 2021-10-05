using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testComponent : MonoBehaviour
{
    //QUESTION: are putting variables before the methods in c# stylistic or required?
    //TODO: see how you can edit the initial default variables numbers from within unity instead of in code.

    //movement variables
    //movement speed in units per second
    private float movementSpeed = 5f;

    //tilt variables    
    private float smooth = 5.0f;
    private float tiltAngle = 60.0f;

    //growth variables
    //NOTE: scaleRate must be a static field because it's used as an initializer
    private const float scaleRate = -0.005f; 
    private Vector3 scaleChange = new Vector3(scaleRate, scaleRate, scaleRate);
    private float scaleLowerBound = 0.1f;
    private float scaleUpperBound = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        var transform = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //I moved individual logics for each transform type into separate methods
        Move();
        //Tilt();
        Grow();
    }

    void Move()
    {   //This code was copy pasted from the unity documentation on transforms and position
        //https://docs.unity3d.com/ScriptReference/Transform-position.html
        //TODO: see if directional input is reversed or if the camera is facing the opposite direction

        //get the Input from Horizontal axis
        float horizontalInput = Input.GetAxis("Horizontal");
        //get the Input from Vertical axis
        float verticalInput = Input.GetAxis("Vertical");

        //update the position
        transform.position = transform.position + new Vector3(horizontalInput * movementSpeed * Time.deltaTime, 
                                                              verticalInput * movementSpeed * Time.deltaTime, 0);

        //output to log the position change
        //Debug.Log(transform.position);
    }

    void Tilt()
    {   //This code was copy pasted from the unity documentation on transforms and rotation
        //https://docs.unity3d.com/ScriptReference/Transform-rotation.html
        //TODO: understand Quaternions, review Euler math and research what the heck a Slerp is

        // Smoothly tilts a transform towards a target rotation.
        float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
        float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;

        // Rotate the cube by converting the angles into a quaternion.
        Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

        // Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth);
    }

    void Grow()
    {   //Slightly adapted code from the unity documentation
        //https://docs.unity3d.com/ScriptReference/Transform-localScale.html

        //constantly change scale by the current scale change vector
        transform.localScale += scaleChange;

        //oscillate scale change vector when object's current scale reaches upper or lower bounds
        if (transform.localScale.y < scaleLowerBound 
        || transform.localScale.y > scaleUpperBound)
            scaleChange = -scaleChange;
    }

}
