using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    Color currentColor;
    [SerializeField]
    float duration;
    float t = 0.2f;

    private void Start()
    {
        currentColor = GetComponent<Renderer>().material.color;
    }

    public void ChangeColor(float g)
    {
        float value = Mathf.Lerp(0f, 1f, t);
        t += Time.deltaTime / duration;
        currentColor.g = g;
        currentColor.r = (1-g);
        GetComponent<Renderer>().material.color = Color.Lerp(GetComponent<Renderer>().material.color, currentColor, 1);
    }
}
