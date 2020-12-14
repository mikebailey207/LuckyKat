using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BallController : MonoBehaviour
{
    private Rigidbody ballRB;
    private int rushLevel;
    private bool canRush;
    private int bounceForce = 10;
    private int level = 1;
    private bool ignoreDoubleCollision; //to avoid colliding with two segments and doubling the AddForce

    AudioSource bounceSound;
    Vector3 startPos;
    Quaternion startRot;


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
      
    void Start()
    {
        startPos = transform.position;
        startRot = helix.transform.rotation;
        ballRB = GetComponent<Rigidbody>();
        bounceSound = GetComponent<AudioSource>();
        canRush = true;
        rushLevel = 1;
    }
    private void Update()
    {     
        levelText.text = "Level: " + level;
        if (rushLevel > 3 && canRush)
        {
            Rush();
            canRush = false;
            Invoke("ResetRush", 0.5f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("End"))
        {
            ShowLevelUpScreen();
            return; 
        }
        if (ignoreDoubleCollision) return;
            
        if (collision.gameObject.GetComponent<Renderer>().material.color == Color.blue) // blue = death
        {
            if (rushLevel <= 3)
            {
                SceneManager.LoadScene(0);
                GameManager.singleton.lastScore = GameManager.singleton.highScore;
            }
            else if (rushLevel > 3 && !collision.gameObject.CompareTag("End"))
            {
                Destroy(collision.transform.parent.gameObject);
                RushCollide();
            }
        }
      
        if (rushLevel > 3)
        {
            Destroy(collision.transform.parent.gameObject);
            RushCollide();
            
        }
        else if (rushLevel >= 3)
        {
            rushLevel = 1;
        }
        PlayBounceSound();
        Bounce();
       
    }

    private void OnTriggerEnter(Collider other)
    {
        GameManager.singleton.AddScore(10 * rushLevel * level);
        rushLevel++;
        bubbleSound.pitch += 0.2f;
        fishAnim.speed += 1f;
        Destroy(other.gameObject);                   
    }
    void Bounce()
    {
        ballRB.velocity = Vector3.zero;
        ballRB.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);

        ignoreDoubleCollision = true;
        Invoke("AllowBounce", 0.15f);
    }
    void RushCollide()
    {
        splashSound.Play();
        rushParticle.Play();
        bubbleSound.pitch = 0.3f;
        fishAnim.speed = 1;
        rushLevel = 1;
        StartCoroutine(SlowTimeEnum());
    }
    void PlayBounceSound()
    {
        bounceSound.pitch = UnityEngine.Random.Range(1f, 2f);
        bounceSound.Play();
    }
    void AllowBounce()
    {
        ignoreDoubleCollision = false;
    }
    void Rush()
    {
        ballRB.AddForce(Vector3.down);
    }
    void ResetRush()
    {
        canRush = true;
    }
    void ShowLevelUpScreen()
    {       
        nextLevelButton.SetActive(true); 
    }
    public void LevelUp()
    {    
        level++;
        bubbleSound.pitch = 0.3f;
        fishAnim.speed = 1;
        transform.position = startPos;
        helix.transform.rotation = startRot;
        StartCoroutine(WaitForNextLevel());
        PlatformSpawner.psSingleton.LoadPlatform();
        nextLevelButton.SetActive(false);
        rushLevel = 1;
        if (PlatformSpawner.psSingleton.difficultyModifierPlatforms >= 2)
            PlatformSpawner.psSingleton.difficultyModifierPlatforms--;
    }
    IEnumerator SlowTimeEnum()
    {
        bounceForce = 4;
        Physics.gravity /= 4;
        yield return new WaitForSeconds(1);
        bounceForce = 10;
        Physics.gravity *= 4;
    }   
    IEnumerator WaitForNextLevel()
    {
        helix.GetComponent<MobileAndMouseRotater>().enabled = false;
        yield return new WaitForSeconds(3);
        helix.GetComponent<MobileAndMouseRotater>().enabled = true;
    }
}
