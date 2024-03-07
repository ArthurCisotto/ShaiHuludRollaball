using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class SC_WormController : MonoBehaviour
{

    public int maxHealth = 100;
    private int currentHealth;

    public float speed = 3.0f;
    public float rotateSpeed = 1.0f;
    public float jumpSpeed = 5.0f;
    public float gravity = 9.8f;

    CharacterController controller;
    Vector3 moveDirection;

    public Slider healthBar;
    public TextMeshProUGUI healthText;

    public float gameTime = 30.0f;
    public TextMeshProUGUI timerText;

    public GameOverScreen gameOverScreen;
    float secondsSurvived = 0.0f;

    public AudioSource collectSound;
    public AudioSource damageSound;

    bool timeEnded;


    // Start is called before the first frame update
    void Start()
    {
        timeEnded = false;
        controller = GetComponent<CharacterController>();
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        healthText.text = "HP: " + currentHealth.ToString();
        UpdateTimerText();
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate around y - axis
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

        // Move forward / backward
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * Input.GetAxis("Vertical");
        float movementDirectionY = moveDirection.y;
        moveDirection = forward * curSpeed;

        // Jumping
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!controller.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);

        if (gameTime > 0)
        {
            gameTime -= Time.deltaTime;
            secondsSurvived += Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            timeEnded = true;
            EndGame();
        }
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(gameTime / 60F);
        int seconds = Mathf.FloorToInt(gameTime - minutes * 60);
        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    void EndGame()
    {   
        gameOverScreen.Setup(Mathf.FloorToInt(secondsSurvived));
        timerText.gameObject.SetActive(false);
        healthBar.gameObject.SetActive(false);
        healthText.gameObject.SetActive(false);
        this.enabled = false;


    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0 && !timeEnded)
        {
            currentHealth -= damage;
            damageSound.Play();
            healthBar.value = currentHealth;
            healthText.text = "HP: " + currentHealth.ToString();
        }
        else
        {
            EndGame();
        }

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            Destroy(other.gameObject);
            collectSound.Play();
            gameTime += 3.0f; // Add 3 seconds to the timer
            FindObjectOfType<PickUpManager>().PickUpCollected(); // Tell the manager to spawn a new pick-up
        }
    }


}