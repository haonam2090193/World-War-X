using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAimming : MonoBehaviour
{
    public float turnSpeed = 15f;
    public float aimDur = 0.3f;
    private Camera mainCam;
    private void Awake()
    {
        mainCam = Camera.main;

    }
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {     
        float yawCamera = mainCam.transform.rotation.eulerAngles.y; //lay goc quay cua truc Y
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.deltaTime);

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
