﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    private Transform _cam; 
    private Vector3 _previousCamPosition; //main camera position on previous frame
    private float[] _parallaxScales; // camera movement scales which parallax will use

    public Transform[] Backgrounds; //List of backgrounds we want to pbe parallaxed
    public float Smoothing; //parallax smoothing. has to be more than 1

    private void Awake() 
    {
        _cam = Camera.main.transform;  //main camera transform link 

    }
    private void Start()
    {
        _previousCamPosition = _cam.position;
        _parallaxScales = new float[Backgrounds.Length];
        
        for(int i=0; i< Backgrounds.Length; i++)
        {
            _parallaxScales[i] = Backgrounds[i].position.z;
        }
    }


    private void Update()
    {
        for (int i = 0; i < Backgrounds.Length; i++)
        {
            //parallax - difference between previous camera position and current (x axis), multiplied to BG scale(z axis). The further is BG - the stronger the parallax
            float parallax = (_previousCamPosition.x - _cam.position.x) * _parallaxScales[i];
            //set the target point of BG (X axis), which is our current position + parallax 
            float backgroundTargetPosX = Backgrounds[i].position.x - parallax;
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, Backgrounds[i].position.y, Backgrounds[i].position.z);
            //smooth transition from current pos to target using Lerp
            Backgrounds[i].position = Vector3.Lerp(Backgrounds[i].position, backgroundTargetPos, Smoothing * Time.deltaTime);
            _previousCamPosition = _cam.position;
        }
    }
}
