using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Range(-1, 1)][SerializeField] int PlayerPosition;

    public GameObject Floor;

    public int PlayerRunVelocity = 15;
    public int PlayerMovementVelocity = 15;
    public float PlayerJumpForce = 60;
    public int LaneWidth = 3;

    Vector3 PlayerDirection;

    private Animator PlayerAnimation;
    private Rigidbody PlayerRigidBody;

    public float PlayerPositionX, PlayerPositionY;
    public bool canJump;
    public bool PlayerIsAlive = true;

    void Start()
    {
        PlayerDirection = transform.position;
        PlayerAnimation = GetComponent<Animator>();
        PlayerRigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        InitAnimations();
        MoveXAxis();
        MoveYAxis();
        canJump = (transform.position.y - Floor.transform.position.y) < 0.5 ;
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            PlayerAnimation.SetBool("IsJump", true);
            PlayerRigidBody.AddForce(Vector3.up * PlayerJumpForce, ForceMode.VelocityChange);
        }
        else if ( transform.position.y <= 1)
        {
            PlayerAnimation.SetBool("IsJump", false);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PlayerRigidBody.AddForce(Vector3.down * PlayerJumpForce, ForceMode.VelocityChange);
        }
        //Debug.Log(PlayerAnimation.GetBool("IsJump"));
        PlayerDirection.z = transform.position.z;
        transform.position = Vector3.MoveTowards(transform.position, PlayerDirection, PlayerMovementVelocity * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (!PlayerIsAlive) return;
        Vector3 forwardMove = PlayerRunVelocity * Time.fixedDeltaTime * transform.forward;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + forwardMove, PlayerRunVelocity * Time.fixedDeltaTime);
    }

    public void InitAnimations()
    {
        PlayerAnimation.SetFloat("VelocityX", PlayerDirection.x);
        PlayerAnimation.SetFloat("VelocityY", PlayerDirection.x);
    }

    public void MoveXAxis()
    {
        if (Math.Round(transform.position.x) == Math.Round(PlayerDirection.x))
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && PlayerPosition < 1)
            {
                PlayerPosition++;
                PlayerDirection = new Vector3(PlayerPosition * LaneWidth, transform.position.y, transform.position.z);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && PlayerPosition > -1)
            {
                PlayerPosition--;
                PlayerDirection = new Vector3(PlayerPosition * LaneWidth, transform.position.y, transform.position.z);
            }
        }
    }
    
    public void MoveYAxis()
    {

    }
    public void Die()
    {
        PlayerIsAlive = false;
        SceneManager.LoadScene("Menu");
    }
}
