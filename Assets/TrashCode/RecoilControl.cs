/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamUtility.IO;

public class RecoilControl : MonoBehaviour {
    [SerializeField]
    private float recoilControlVertical, recoilControlHorizontal, maxVerticalRecoil, maxHorizontalRecoil;
    private float verticalRotation = 0.0f, horizontalRotation = 0.0f;
    private Transform parent;
    private Quaternion camOriginalRotation;
    // Use this for initialization
    void Start () {
        camOriginalRotation = Camera.main.transform.rotation;
        parent = Player.GetInstance().gameObject.transform;
    }
	
	// Update is called once per frame
	void Update () {
        ControlRecoil();
        
    }

    public void Recoil(float recoilVertical, float recoilHorizontal)
    {
        verticalRotation += recoilVertical * Time.deltaTime;
        horizontalRotation = Random.Range(-recoilHorizontal, recoilHorizontal) * Time.deltaTime;
        
    }
    private void ControlRecoil()
    {
        if (verticalRotation > 0)
        {
            verticalRotation -= recoilControlVertical * Time.deltaTime;
        }
        if(horizontalRotation > 0)
        {
            horizontalRotation -= recoilControlHorizontal * Time.deltaTime;
        }
        if (verticalRotation > maxVerticalRecoil)
        {
            verticalRotation = maxVerticalRecoil + Random.Range(-1, 2) * Time.deltaTime;
        }

        if (horizontalRotation > maxHorizontalRecoil)
        {
            horizontalRotation = maxHorizontalRecoil + Random.Range(-1, 1) * Time.deltaTime;
        }

        if(horizontalRotation < 0)
        {
            horizontalRotation += recoilControlHorizontal * Time.deltaTime;
        }
        if (!(InputManager.GetAxis("Fire0") > 0 || InputManager.GetMouseButton(0)))
        {
            verticalRotation = 0;
        }
        if (! (verticalRotation < 0.02))
        {
            transform.Rotate(transform.rotation.x - verticalRotation, 0, 0);
            //parent.Rotate(0, parent.rotation.y - horizontalRotation, 0);
        }
        Debug.Log(verticalRotation);
        //Debug.Log(horizontalRotation);
        
    }
    protected void CameraRecoil()
    {
        if (recoilControl)
        {
            recoilControl.Recoil(verticalRecoilIncrease, horizontalRecoilIncrease);
        }

    }

    protected void ResetRecoil()
    {
        recoilControl.Recoil(0, 0);
    }
}*/
