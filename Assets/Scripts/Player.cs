using UnityEngine;
using UnityEngine.TextCore.Text;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    public float jumpForce = 10f;
    public float gravity = 9.80667f;
    private Vector3 direction;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        direction = Vector3.zero;
    }

    private void Update()
    {
        direction += Vector3.down * gravity * Time.deltaTime;

        if(characterController.isGrounded)
        {
            direction.y = 0f;

            if(Input.GetButton("Jump"))
            {
                direction.y = jumpForce;
            }
        }
        else
        {
            direction.y -= gravity * Time.deltaTime;
        }

        characterController.Move(direction * Time.deltaTime);
    }
}
