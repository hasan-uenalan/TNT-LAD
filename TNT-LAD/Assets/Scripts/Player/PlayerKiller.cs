using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerKiller : MonoBehaviour
{
  public GameObject hat;

  public DetachableLimbs[] detachableLimbs;
  [Header("Ragdoll")]
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

    foreach (GameObject bodyPart in bodyParts)
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
    foreach (Rigidbody rb in gameObject.transform.GetComponentsInChildren<Rigidbody>())
    {
      parts.Add(rb.gameObject);
    }
    return parts;
  }
}

[Serializable]
public struct DetachableLimbs
{
  public GameObject bone;
  public GameObject prefab;
}
