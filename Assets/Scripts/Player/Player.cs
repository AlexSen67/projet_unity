
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    public float moveSpeed;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 30.0f;

    public Transform groundCheck;

    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    bool isMoving;

    private GameManager gameManager;

    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
        Application.targetFrameRate = 80;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        moveSpeed = gameManager.GetPlayerSpeed();
        // Check if the player is on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            // Reset the velocity
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Move the player
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
           
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Execute the jump
        controller.Move(velocity * Time.deltaTime);

        // Check if the player has moved
        if (lastPosition != gameObject.transform.position && isGrounded == true)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        lastPosition = gameObject.transform.position;

        Debug.Log("Speed : " + moveSpeed);  

    }
    void OnTriggerEnter(Collider collision)
    {
        // Récupérer l'objet entrant en collision
        GameObject collidedObject = collision.gameObject;

        // Vérifier le tag de l'objet
        switch (collidedObject.tag)
        {
            case "SpeedBuff":
                moveSpeed += 8.0f;
                Debug.Log("Vitesse Augmenté : "+ moveSpeed);
                Destroy(collidedObject);
                break;

            case "Target":
                Debug.Log("Game Over");
                // Charge la scène de Game Over
                SceneManager.LoadScene("GameOver");
                break;

            case "DamageBuff":
                Bullet.damage += 10;
                Debug.Log("Dégats augmentés : " + Bullet.damage);
                Destroy(collidedObject);
                break;

            default:
                Debug.Log("Collision avec un objet inconnu : " + collidedObject.name);
                // Actions pour tout autre objet
                break;
        }
        


    }


}
