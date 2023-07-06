using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellEffect : MonoBehaviour
{
    public ParticleSystem shellEffect;
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            shellEffect.Emit(1);
        }
    }
}
