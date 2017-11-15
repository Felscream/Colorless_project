using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionDetector : MonoBehaviour {

    private static InteractionDetector instance;
    [SerializeField]
    private float detectorRadius, detectorX, detectorY, detectorZ;
    private Collider[] detector;
    private Transform player;
    private Vector3 detectorLocation;
    private Text interactionPrompt;
    private TextManager textManager;

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

    private void Start()
    {
        player = GetComponent<Transform>();
        interactionPrompt = GameObject.FindGameObjectWithTag("InteractionPrompt").GetComponent<Text>();
        interactionPrompt.enabled = false;
        textManager = TextManager.GetInstance();
    }
    private void UpdateDetectorLocationAndScan()
    {
        //update detector location just before the player
        if(player != null)
        {
            detectorLocation = player.position + player.forward + new Vector3(detectorX, detectorY, detectorZ);
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
        {
            return true;
        }
            
        return false;
    }
    void Update () {
        UpdateDetectorLocationAndScan();
        if (InteractionPossible())
        {
            Interaction interaction = GetClosestInteraction();
            if(interaction != null)
            {
                if (interaction.IsInteractive())
                {
                    if (interaction is Item)
                    {
                        string action = textManager.GetInteraction("TAKE");
                        string itemName = textManager.GetObject(interaction.GetItemName());
                        interactionPrompt.text = action + " " + itemName;
                    }
                    Debug.Log("Interaction possible");
                    interactionPrompt.enabled = true;
                }
            } 
        }
        else
        {
            interactionPrompt.enabled = false;
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
        //check if player is looking at specific interactible
        Vector3 dir = Camera.main.transform.forward;
        Vector3 rayOrigin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, dir, out hit, detectorRadius, LayerMask.GetMask("Interaction")))
        {
            closestInteraction = hit.transform.gameObject.GetComponent<Interaction>();
        }
        else
        {
            foreach (Collider it in detector)
            {
                if (Vector3.Distance(player.position, it.gameObject.GetComponent<Transform>().position) <= closestInteractionDistance)
                {
                    closestInteractionDistance = Vector3.Distance(player.position, it.gameObject.GetComponent<Transform>().position);
                    closestInteraction = it.gameObject.GetComponent<Interaction>();
                }
            }
        }
        if (closestInteraction == null)
        {
            Debug.Log("Interaction not found");
        }
        return closestInteraction;
    }
}
