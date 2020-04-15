using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
  public float walkSpeed = 3;
  public Animator anim;

  private Vector2 axis;
  private CharacterController characterController;

  private void Start()
  {
    characterController = gameObject.GetComponent<CharacterController>();
  }

  void Update()
  {
    if (characterController.enabled)
    {
      MovePlayer();
      
    }
    CheckMovement();
  }

  void CheckMovement()
  {
    if(characterController.velocity != Vector3.zero)
    {
      anim.SetBool("movement", true);
      Debug.Log("moving");

    }
    else
    {
      anim.SetBool("movement", false);
    }
  }

  void MovePlayer()
  {
    Vector3 movement = new Vector3(axis.x, 0, axis.y) * walkSpeed;
    if (!characterController.isGrounded)
    {
      movement.y = Physics.gravity.y * Time.fixedDeltaTime * 100f;
    }
    characterController.Move(movement * Time.deltaTime);
    Vector3 newDirection = Vector3.RotateTowards(transform.forward, new Vector3(axis.x, 0, axis.y), Time.deltaTime * 20f, 0.0f);
    transform.rotation = Quaternion.LookRotation(newDirection * Time.deltaTime);
  }

  void OnMovement(InputValue inputValue)
  {
    axis = inputValue.Get<Vector2>();
  }
}
