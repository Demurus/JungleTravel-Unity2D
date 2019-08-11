using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public Transform[] backgorunds; //массив (список) фонов, которые мы хотим параллаксить
    private float[] parallaxScales; // пропорции движения камеры, по которым будут двигаться параллаксы
    public float smoothing = 1F; //мягкость параллакса. Должен быть больше 0

    private Transform cam; //ссылка на трансформ главной камеры
    private Vector3 previousCamPos; //позиция камеры на предыдущем кадре

    private void Awake() // всегда вызывается до Start()
    {
        cam = Camera.main.transform;  //ссылка на главную камеру

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
            //parallax- разница между предыдущей позицией камеры и нынешней (по х), умноженная на масштаб фонов. Чем дальше фон - тем сильнее параллакс
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];
            //устанавливаем целевую точку фонов по Х, которая есть нынешняя позиция+наш парраллакс
            float backgroundTargetPosX = backgorunds[i].position.x + parallax;
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgorunds[i].position.y, backgorunds[i].position.z);
            //мягкий переход из нынешнего положения в целевое используя Lerp
            backgorunds[i].position = Vector3.Lerp(backgorunds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
            previousCamPos = cam.position;
        }
    }
}
