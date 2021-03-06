using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Controller : MonoBehaviour
{
    public Slider slSpeeddestination, slSpeedvra;

    public GameObject panelInfo;
    public float forceMove, forceRotate, zoomSpeed, variable;
    public Transform mainCamera;

    public Text X,Y,Z;
    public float speed = 0.01f;
    public Vector3 destination, posStay;
    public Quaternion vra;
    public bool movePosition = false;
    public Slider SliderY;

    public void pushSpeeddestination(){
        forceMove = slSpeeddestination.value;
    }
    public void pushSpeedvra(){
        forceRotate = slSpeedvra.value;
    }
    void Start()
    {
        zoomSpeed = 100f;
        destination = transform.position;
        mainCamera = this.gameObject.GetComponent<Transform>();
        
    }

    void Update()
    {
        if(movePosition){
            transform.position = Vector3.Lerp(transform.position, destination, speed);
            transform.rotation = Quaternion.Lerp(transform.transform.rotation, new Quaternion(0, vra.y, 0, vra.w), speed);
            if(Vector3.Distance(transform.position, destination) < .3f) movePosition = false;
            if(Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.Mouse2)) movePosition = false;
            posStay = transform.position;
        }else{
            if(posStay != transform.position){
                panelInfo.SetActive(false);
            }
            X.text = "X = " + Convert.ToString(Math.Round(mainCamera.position.x, 2));
            Z.text = "Z = " + Convert.ToString(Math.Round(mainCamera.position.z, 2));
            Y.text = "Y = " + Convert.ToString(Math.Round(mainCamera.position.y, 2));

            mainCamera.position += mainCamera.forward * forceMove * Input.GetAxis("Vertical");
            mainCamera.position += mainCamera.right * forceMove * Input.GetAxis("Horizontal");
            
            if(Input.GetKey(KeyCode.E)){
                mainCamera.transform.Rotate(.0f, 1.0f * forceRotate, .0f, Space.World);
            }

            if(Input.GetKey(KeyCode.Q)){
                mainCamera.transform.Rotate(.0f, -1.0f * forceRotate, .0f, Space.World);
            }

            if(Input.GetKey(KeyCode.Z)&&transform.position.y < SliderY.maxValue)
            {
                mainCamera.position += mainCamera.up * forceMove;
            }

            if(Input.GetKey(KeyCode.X)&&transform.position.y > SliderY.minValue){
                mainCamera.position -= mainCamera.up * forceMove;
                
            }


        }
        SliderY.value = transform.position.y;
    } 
    public void SetY(){
        transform.position = new Vector3(transform.position.x,SliderY.value,transform.position.z);
    }  
}
