using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsController : MonoBehaviour
{
  private CharacterController characterController;
  private Animator anim;

  // Start is called before the first frame update
  void Start()
  {
    characterController = gameObject.GetComponent<CharacterController>();
    anim = gameObject.GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {
    CheckMovement();
  }

  void CheckMovement()
  {
    if (characterController.velocity != Vector3.zero)
    {
      anim.SetBool("movement", true);
    }
    else
    {
      anim.SetBool("movement", false);
    }
  }
}
