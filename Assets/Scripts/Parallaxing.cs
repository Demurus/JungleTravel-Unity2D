using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public Transform[] backgorunds; //List of backgrounds we want to pbe parallaxed
    private float[] parallaxScales; // camera movement scales which parallax will use
    public float smoothing = 1F; //parallax smoothing. has to be more than 1

    private Transform cam; 
    private Vector3 previousCamPos; //main camera position on previous frame
    private void Awake() 
    {
        cam = Camera.main.transform;  //main camera transform link 

    }
    void Start()
    {
        previousCamPos = cam.position;
        parallaxScales = new float[backgorunds.Length];
        
        for(int i=0; i<backgorunds.Length; i++)
        {
            parallaxScales[i] = backgorunds[i].position.z;
        }
    }

    
    void Update()
    {
        for (int i = 0; i < backgorunds.Length; i++)
        {
            //parallax - difference between previous camera position and current (x axis), multiplied to BG scale(z axis). The further is BG - the stronger the parallax
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];
            //set the target point of BG (X axis), which is our current position + parallax 
            float backgroundTargetPosX = backgorunds[i].position.x - parallax;
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgorunds[i].position.y, backgorunds[i].position.z);
            //smooth transition from current pos to target using Lerp
            backgorunds[i].position = Vector3.Lerp(backgorunds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
            previousCamPos = cam.position;
        }
    }
}
