using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RagdollController : MonoBehaviour
{
  public GameObject hat;
  public List<GameObject> bodyParts;

  void Start()
  {
    bodyParts = GetBodyParts();
  }

  [ContextMenu("Die")]
  public void Die()
  {
    SetRagdollActive(true);
    DetachHat();
  }

  private void SetRagdollActive(bool active)
  {
    gameObject.GetComponent<CharacterController>().enabled = !active;
    gameObject.GetComponent<PlayerInput>().enabled = !active;

    foreach(GameObject bodyPart in bodyParts)
    {
      bodyPart.GetComponent<Rigidbody>().isKinematic = !active;
      bodyPart.GetComponent<Collider>().enabled = active;
    }
  }

  private void DetachHat()
  {
    hat.transform.parent = null;
    hat.GetComponent<Rigidbody>().isKinematic = false;
    hat.GetComponent<Collider>().enabled = true;
  }

  private List<GameObject> GetBodyParts()
  {
    var parts = new List<GameObject>();
    foreach(Rigidbody rb in gameObject.transform.GetComponentsInChildren<Rigidbody>())
    {
      parts.Add(rb.gameObject);
    }
    return parts;
  }
}
