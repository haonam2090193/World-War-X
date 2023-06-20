using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAimming : MonoBehaviour
{
    public float turnSpeed = 15f;

    public Transform ignoreZone;
    public float radius;
    public bool showGizmos;

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

        Collider[] colliders = Physics.OverlapSphere(ignoreZone.position, radius);
        foreach (var collider in colliders)
        {
            if (!collider.gameObject.layer.Equals(LayerMask.NameToLayer("Ignore Raycast")))
            {
                collider.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (!showGizmos) return;
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(ignoreZone.position, radius);
    }
}
