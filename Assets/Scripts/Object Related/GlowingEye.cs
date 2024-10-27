using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowingEye : MonoBehaviour
{
    private Renderer _objectRenderer;
    private float intensity;

    private void Awake()
    {
        _objectRenderer = GetComponent<Renderer>();
    }

    public void ChangeColorOverTime(float time)
    {
        intensity = Mathf.MoveTowards(intensity, 100, Time.deltaTime / 5);

        Color finalColor = Color.white * intensity;

        _objectRenderer.material.SetColor("_EmissionColor", finalColor);

    }
}
