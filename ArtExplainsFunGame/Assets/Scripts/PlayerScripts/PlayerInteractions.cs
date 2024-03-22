using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class PlayerInteractions : MonoBehaviour
{
    [Header("Components")] private Rigidbody2D myRb2d;
    //private Animator myAnimator;
    private BoxCollider2D myBodyCollider2D;
    private PlayerManager playerManager;
    private SpriteRenderer mySpriteRenderer;

    [Header("Ground")] 
    private CheckOnGround checkOnGround;
    [SerializeField] bool isOnGround;

    [Header("Dialog + Interactions")] 
    [SerializeField] private bool canInteract = false;
    public DialogueRunner dialogueRunner;
   public SpriteRenderer exclamation;
    [SerializeField] private string nodeString;
   // [SerializeField] private RaycastHit2D myRaycast;
    
    [Header("Other")] 
    private bool isAlive = true;
    
    void Start()
    {
        myRb2d = GetComponent<Rigidbody2D>();
        //myAnimator = GetComponent<Animator>();
        myBodyCollider2D = GetComponent<BoxCollider2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        playerManager = FindObjectOfType<PlayerManager>();
        checkOnGround = GetComponent<CheckOnGround>();
    }

    // Update is called once per frame
    void Update()
    {
        //isAlive = playerManager.playerIsAlive;
        //myRaycast = Physics2D.Raycast(transform.position, Vector2.right, 1f);
        //CheckInteraction(myRaycast);
    }
    
    private void FixedUpdate()
    {
        if (!isAlive)
        {
            return;
        }
        isOnGround = checkOnGround.isOnGround;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            //sign doesn't have anything dialog related on it
                nodeString = other.gameObject.GetComponent<Interactable>().NodeTitle;
                exclamation.enabled = true;
                
                canInteract = true;
    
    
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            exclamation.enabled = false;
            canInteract = false;
            nodeString = null;
        }    
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!isAlive) { return; }
        
        if (context.started)
        {
            Interact();
        }
    }
  
    //THIS IS FOR RAYCASTING
    // void CheckInteraction()
    // {
    //     if (myRaycast)
    //     {
    //         Interact();
    //     }
    // }

    private void Interact()
    {
        if (canInteract)
        {
            //print("Interacting!");
            dialogueRunner.StartDialogue(nodeString);
        }
        else
        {
            Debug.Log("Nothing to interact with");
        }
    }
}


