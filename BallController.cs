using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BallController : MonoBehaviour /* handles most of the in-game logic, 
    I debated moving some of this into its own class, 
    an animation class and a sound controller class.
    But seeing as a lot of the logic is collision or trigger-based
    it made sense to have a lot of that functionality attached to the Game Object itself. 
    It is called a BallController as I added the fish later*/
{
    private Rigidbody ballRB;
    private int rushLevel;
    private bool canRush, canChooseQuote;
    private int bounceForce = 10; // set to 10 as default
    private int level = 1;
    private int multiplier; // this is the rushLevel (increases every time a platform is passed) * the level, it multiplies all scores
    private bool ignoreDoubleCollision; //to avoid colliding with two segments and doubling the AddForce

    AudioSource bounceSound;
    Vector3 startPos;
    Quaternion startRot;

    [SerializeField]
    string[] fishQuotes;
    [SerializeField]
    Animator fishAnim;
    [SerializeField]
    AudioSource bubbleSound; 
    [SerializeField]
    GameObject nextLevelButton;
    [SerializeField]
    AudioSource splashSound;
    [SerializeField]
    ParticleSystem rushParticle;
    [SerializeField]
    Transform helix;
    [SerializeField]
    TextMeshProUGUI levelText;
    [SerializeField]
    TextMeshProUGUI multiplierText;
    [SerializeField]
    TextMeshProUGUI fishQuotesText;
    [SerializeField]
    GameObject fishQuotesTextGO;
      
    void Start()
    {
        
        startPos = transform.position;// set ball start position and helix start rotation
        startRot = helix.transform.rotation;
        ballRB = GetComponent<Rigidbody>();
        bounceSound = GetComponent<AudioSource>(); // I added this to the gameobject, in a full version I would probably have all sounds within a SoundManager class
        canRush = true;
        canChooseQuote = true;
        rushLevel = 1;       
    }
    private void Update()
    {
        multiplier = rushLevel * level; // set the multiplier
        levelText.text = "Level " + level; // set level text
        multiplierText.text = "X " + multiplier; //set multiplier text
      

        if (rushLevel > 3 && canRush) // the '3' here would be it's own int rushMax in a full version, this I could tie to difficulty
        {
            Rush();
            canRush = false;
            Invoke("ResetRush", 0.5f); 
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("End")) // First check if it's the end platform
        { 
            ShowLevelUpScreen();
            return; 
        }

        if (ignoreDoubleCollision) return; // check if it's accidentally collided with two objects

        if (collision.gameObject.GetComponent<Renderer>().material.color == Color.blue) /* 
            blue = death, would be a variable in a full version, with the colour changing. 
            I originally had a class for this, but the randomized colours looked ugly so 
            I opted for one colour scheme in this prototype*/
        {
            if (rushLevel <= 3)
            {
                SceneManager.LoadScene(0); // die
                GameManager.singleton.lastScore = GameManager.singleton.highScore; // store highscore;
            }
            else if (rushLevel > 3 && !collision.gameObject.CompareTag("End")) // double check it's not the end platform and you're going fast enough
            {
                Destroy(collision.transform.parent.gameObject); // blow up platform
                RushCollide(); // run the function for colliding at speed
            }
        }
      
        if (rushLevel > 3) // Do the same for non-death collisions
        {
           
            Destroy(collision.transform.parent.gameObject); 
            RushCollide();
            
        }
        
        PlayBounceSound(); 
        Bounce();
       
    }
   
    private void OnTriggerEnter(Collider other) // Every time we go through a trigger (box collider underneath the platform) do some things
    {
        GameManager.singleton.AddScore(10 * multiplier); // add a standard score of ten, multiplied by the multiplier
        rushLevel++; // increase the rush level, above 3 you are invincible
        bubbleSound.pitch += 0.2f;// start getting squeaky
        fishAnim.speed += 1f; // start getting flappy
        Destroy(other.gameObject); // destroy the platform. I would like to add particles to this                  
    }
    void Bounce()
    {
        ballRB.velocity = Vector3.zero; // set the RigidBody's velocity to 0
        ballRB.AddForce(Vector3.up * bounceForce, ForceMode.Impulse); // add force on collision

        ignoreDoubleCollision = true; // double check not going to ad 2X the force
        Invoke("AllowBounce", 0.15f); // allow collisions again
    }
    void RushCollide()
    {
       
        splashSound.Play();  //play sounds and reset everything else, then call the SlowTimeEnum() to slow things down for a bit 
        rushParticle.Play();
        bubbleSound.pitch = 0.3f;
        fishAnim.speed = 1;
        rushLevel = 1;
        StartCoroutine(SlowTimeEnum());
    }
    void PlayBounceSound()
    {
        
        bounceSound.pitch = UnityEngine.Random.Range(1f, 2f); // vary the pitch of the bounce sound with every call
        bounceSound.Play();
    }
    void AllowBounce()
    {
        ignoreDoubleCollision = false; 
    }
    void Rush()
    {
        ballRB.AddForce(Vector3.down); // give it a LITTLE push
    }
    void ResetRush()
    {
        canRush = true;
    }
  
    void GenerateFishQuote() // this was an afterthought, but I kind of think it livens up the transition :)
    {
        if (canChooseQuote)
        {
            int fishChooser = UnityEngine.Random.Range(0, fishQuotes.Length);

            fishQuotesText.text = fishQuotes[fishChooser];
            canChooseQuote = false; // avoid generating again if it bounces
            fishQuotesTextGO.SetActive(true);
        }
    }
    void ShowLevelUpScreen()
    {
        canChooseQuote = true; // choose new quote
        GenerateFishQuote();
        nextLevelButton.SetActive(true); // show next level button
        
    }
    public void LevelUp()
    {    
        level++; // level up
        bounceForce = 10; // reset bounce force
        PlatformSpawner.psSingleton.difficultyModifierDeathSlices += 0.2f; // slowly increase the chance of getting higher amounts of death slices
        bubbleSound.pitch = 0.3f; // reset bubble sound pitch
        fishAnim.speed = 1; // reset fish anim speed
        transform.position = startPos; // reset position
        helix.transform.rotation = startRot; // reset helix rotation
        
        PlatformSpawner.psSingleton.LoadPlatform(); // Call the LoadPlatform script from the PlatformSpawner singleton
        nextLevelButton.SetActive(false); 
        fishQuotesTextGO.SetActive(false);
        rushLevel = 1; // reset rush level
        if (PlatformSpawner.psSingleton.difficultyModifierPlatforms >= 2) // incrementally increase the chance of more platforms in higher levels, but always allow for a gap
            PlatformSpawner.psSingleton.difficultyModifierPlatforms--; 
       
            
    }
    IEnumerator SlowTimeEnum() // effect gravity when you get a 'Splashdown' for 1 second to givethe player a chance to breathe
    {
        bounceForce = 4;
        Physics.gravity /= 4;
        yield return new WaitForSeconds(1);
        bounceForce = 10;
        Physics.gravity *= 4;
    }   

}
