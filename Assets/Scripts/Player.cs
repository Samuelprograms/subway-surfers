using System;
using TreeEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Range(-1, 1)][SerializeField] int PlayerPosition;
    public GameObject Floor;

    public int PlayerRunVelocity = 15;
    public int PlayerMovementVelocity = 20;
    public float PlayerRotationVelocity = 200.0f;
    public float PlayerJumpForce = 70;
    public float LaneWidth = 3f;

    Vector3 PlayerDirection;

    private Animator PlayerAnimation;
    private Rigidbody PlayerRigidBody;

    public float PlayerPositionX, PlayerPositionY;
    public bool canJump;
    public bool PlayerIsAlive = true;

    void Start()
    {
        PlayerDirection = transform.position;
        canJump = false;
        PlayerAnimation = GetComponent<Animator>();
        PlayerRigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {

        PlayerAnimation.SetFloat("VelocityX", PlayerDirection.x);
        PlayerAnimation.SetFloat("VelocityY", PlayerDirection.x);
        canJump = Math.Round(transform.position.y) == Math.Round(Floor.transform.position.y);

        if (Math.Round(transform.position.x) == Math.Round(PlayerDirection.x))
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && PlayerPosition < 1)
            {
                PlayerPosition++;
                PlayerDirection = new Vector3(PlayerPosition * LaneWidth, transform.position.y, transform.position.z);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && PlayerPosition > -1)
            {
                PlayerPosition--;
                PlayerDirection = new Vector3(PlayerPosition * LaneWidth, transform.position.y, transform.position.z);
            }
        }

        PlayerDirection.z = transform.position.z;
        transform.position = Vector3.MoveTowards(transform.position, PlayerDirection, PlayerMovementVelocity * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerRigidBody.AddForce(Vector3.up * PlayerJumpForce, ForceMode.VelocityChange);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PlayerRigidBody.AddForce(Vector3.down * PlayerJumpForce, ForceMode.VelocityChange);
        }
    }

    private void FixedUpdate()
    {
        if (!PlayerIsAlive) return;
        Vector3 forwardMove = PlayerRunVelocity * Time.fixedDeltaTime * transform.forward;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + forwardMove, PlayerRunVelocity * Time.fixedDeltaTime);
    }
    public void Die()
    {
        PlayerIsAlive = false;
        SceneManager.LoadScene("Menu");
    }
}
