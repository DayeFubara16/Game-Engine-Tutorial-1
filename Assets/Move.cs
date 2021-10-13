using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Rigidbody rb;
    private float moveSpeed = 5f;
    private float dirX, dirZ;

    //new stuff
    //This is where we will ask for our audio 
    public AudioSource audiosource;

    //We assign a value for the enemy health
    private float enemyHealth = 100f;

    //our SceneManager, that allows us to do stuff with persistence 
    private SceneManager health;

    //end of new stuff 

    //speed force for the jump 
    private float jumpSpeed = 2f; 

    //Vector3 created for our player so that it doesn't interfere with main functionality
    private Vector3 jump;

    //Bool created to help check if the player is grounded 
    private bool isGrounded; 

    //Start is called before the first frame update 
    private void Start()
    {
        //Getting our Rigidbody 
        rb = GetComponent<Rigidbody>();

        //Jump functionality 
        jump = new Vector3(0.0f, 3.0f, 0.0f);
    }

    //Helps reset our isGrounded bool, without the main player will only jump once 
    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }


    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxis("Horizontal") * moveSpeed;
        dirZ = Input.GetAxis("Vertical") * moveSpeed;


        //simple if statement that allows us to pass the jump and necessary information 
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(jump * jumpSpeed, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    //destroys enemy player 
    void OnCollisionEnter(Collision col)
    {
        // When target is hit
        if (col.gameObject.tag == "Enemy")
        {
            enemyHealth -= 30;
            
            if (enemyHealth <= 0)
            {
                Destroy(col.gameObject);
                //All our stuff that allows us to store the score
                PersistanceManager.Instance.Value--;
                health.ValueText.text = "Enemy Health: " + PersistanceManager.Instance.Value.ToString();
                //this function will tell the game play an audio sound when the enemy dies 
                audiosource.Play();
                
            }
        }
    }

    private void FixedUpdate()
    {
        //rb velocity settings 
        rb.velocity = new Vector3(dirX, rb.velocity.y, dirZ);   
    }
}
