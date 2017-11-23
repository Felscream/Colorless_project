using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {

    [SerializeField]
    private int rotationX;
    [SerializeField]
    private int rotationY;
    [SerializeField]
    private int rotationZ;
    // Update is called once per frame
    void Update () {
        transform.Rotate(new Vector3(rotationX, rotationY, rotationZ) * Time.deltaTime);
    }
    
    
}
