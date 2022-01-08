using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotation : MonoBehaviour
{
    public bool autoStart = true;

    [Range(0, 5)]
    public float speed = 1;

    private void FixedUpdate()
    {
        if (autoStart)
            RenderSettings.skybox.SetFloat("_Rotation", Time.time * speed);
    }
}
