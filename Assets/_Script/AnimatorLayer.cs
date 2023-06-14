using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorLayer : MonoBehaviour
{
    private Animator animator;
    private int layerIndex;
    private float layerWeightVelocity;
    private float currentLayerWeight;
    private float targetLayerWeight;
    public float respawnTerm = 1;


    private void Awake()
    {
        animator = GetComponent<Animator>();       
    }
    void Start()
    {
        layerIndex = animator.GetLayerIndex("Gun Layer");
         currentLayerWeight = animator.GetLayerWeight(layerIndex);
         targetLayerWeight = 1;
    }
    void Update()
    {      
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Da Gun idle");
            animator.SetLayerWeight(layerIndex,
                Mathf.SmoothDamp(currentLayerWeight, targetLayerWeight, ref layerWeightVelocity, 0f));
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Da dung Gun idle");
            animator.SetLayerWeight(layerIndex, 0);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Da Aim");
            animator.SetBool("IsAiming", true);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetBool("IsAiming", false);
            Debug.Log("Da dung Aim");
        }
    }
    IEnumerator changeLayer()
    {
        Debug.Log("Da Gun idle");
        animator.SetLayerWeight(layerIndex,
            Mathf.SmoothDamp(currentLayerWeight, targetLayerWeight, ref layerWeightVelocity, 0f));
        yield return new WaitForSeconds(respawnTerm);
    }

}
