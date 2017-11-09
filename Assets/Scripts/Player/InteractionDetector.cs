using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDetector : MonoBehaviour {

    private static InteractionDetector instance;
    [SerializeField]
    private float detectorRadius, detectorX, detectorY, detectorZ;
    private Collider[] detector;
    private Transform player;
    private Vector3 detectorLocation;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static InteractionDetector GetInstance()
    {
        if (instance == null)
        {
            Debug.Log("No instance of " + typeof(InteractionDetector));
            return null;
        }
        return instance;

    }
    private void UpdateDetectorLocationAndScan()
    {
        //update detector location just before the player
        player = GetComponent<Transform>();
        if(player != null)
        {
            detectorLocation = player.position + new Vector3(detectorX, detectorY, detectorZ);
            detector = Physics.OverlapSphere(detectorLocation, detectorRadius, LayerMask.GetMask("Interaction"));
        }
        else
        {
            Debug.Log("Interaction detector - Parent transform not found");
        }
    }

    private bool InteractionPossible()
    {
        if (detector.Length > 0)
            return true;
        return false;
    }
    void Update () {
        UpdateDetectorLocationAndScan();
        if (InteractionPossible())
        {
            Debug.Log("Interaction possible");
        }
    }

    public Interaction GetClosestInteraction()
    {
        Interaction closestInteraction = null;
        float closestInteractionDistance = detectorRadius;
        //check if there is a possible interaction
        if (!InteractionPossible())
        {
            return null;
        }
        //look for closest interaction
        foreach(Collider it in detector)
        {
            if(Vector3.Distance(player.position,it.gameObject.GetComponent<Transform>().position) <= closestInteractionDistance)
            {
                closestInteractionDistance = Vector3.Distance(player.position, it.gameObject.GetComponent<Transform>().position);
                closestInteraction = it.gameObject.GetComponent<Interaction>();
                if(closestInteraction == null)
                {
                    Debug.Log("Interaction not found on " + it.gameObject.transform.name);
                }
            }
        }
        return closestInteraction;
    }
}
