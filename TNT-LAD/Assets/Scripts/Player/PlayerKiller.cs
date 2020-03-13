using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerKiller : MonoBehaviour
{
  public GameObject hat;
  public GameObject bloodParticleSystem;
  public GameObject visceraSprite;
  public Vector3 visceraScale = new Vector3(0.025f, 0.025f, 0.025f);
  [Tooltip("'Bone' has to have at least ONE child bone")]
  public DetachableLimb[] detachableLimbs;
  public List<GameObject> ragdollBodyParts;

  void Start()
  {
    ragdollBodyParts = GetBodyParts();
  }

  public void KillPlayer(bool dismemberAll)
  {
    SetRagdollActive(true);
    DetachLimbs(dismemberAll);
    DetachHat();
  }

  private void SetRagdollActive(bool active)
  {
    int noPlayerCollisionLayerIndex = LayerMask.NameToLayer("NoPlayerCollision");
    gameObject.GetComponent<CharacterController>().enabled = !active;
    gameObject.GetComponent<PlayerInput>().enabled = !active;

    foreach (GameObject bodyPart in ragdollBodyParts)
    {
      bodyPart.layer = noPlayerCollisionLayerIndex;
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

  /// <summary>
  /// Get all GameObjects of the player that are part of the Ragdoll.
  /// </summary>
  /// <returns></returns>
  private List<GameObject> GetBodyParts()
  {
    var parts = new List<GameObject>();
    foreach (Rigidbody rb in gameObject.transform.GetComponentsInChildren<Rigidbody>())
    {
      parts.Add(rb.gameObject);
    }
    return parts;
  }

  /// <summary>
  /// Detach limbs of player use <paramref name="complete"/> to control if all limbs should detach.
  /// </summary>
  /// <param name="complete">Complete detaching of all limbs, or alternatively random detachment of one or no limbs.</param>
  private void DetachLimbs(bool complete)
  {
    if (!complete)
    {
      //randomly choose to detach or not
      if (UnityEngine.Random.Range(0, 2) == 0)
      {
        return;
      }
    }

    int randomLimbIndex = UnityEngine.Random.Range(0, detachableLimbs.Length);
    var limbsToDetach = complete
      ? new List<DetachableLimb>(detachableLimbs)
      : new List<DetachableLimb> { detachableLimbs[randomLimbIndex] };
    foreach (DetachableLimb limb in limbsToDetach)
    {
      RemoveLimb(limb.bone);
      CreateDetachedLimb(limb.bone, limb.prefab);
      SpawnBloodParticleEffect(limb.bone);
    }
  }

  /// <summary>
  /// Shrink limb to make it seem removed and the cover stump with viscera.
  /// </summary>
  /// <param name="bone"></param>
  private void RemoveLimb(GameObject bone)
  {
    if(bone.transform.childCount <= 0)
    {
      Debug.LogWarning($"Bone '{bone.name}' for limb detachment does not have a child bone");
      return;
    }
    //shrink bones to appear removed
    var childBone = bone.transform.GetChild(0).gameObject;
    childBone.transform.localScale = Vector3.zero;
    bone.transform.localScale = new Vector3(bone.transform.localScale.x, 0f, bone.transform.localScale.z);
    
    // //instantiate viscera to hide the gliched stump  //TODO: z-fighting because y scale is zero -> sprite can't be moved along y to solve this.
    // var viscera = Instantiate(visceraSprite);
    // viscera.transform.localScale = visceraScale;
    // viscera.transform.rotation = Quaternion.LookRotation(bone.transform.up);
    // viscera.transform.parent = bone.transform;
    // viscera.transform.localPosition = new Vector3(0, 0.01f, 0);
  }

  /// <summary>
  /// Instatiate copy of the detached at the right position.
  /// </summary>
  /// <param name="bone"></param>
  /// <param name="prefab"></param>
  private void CreateDetachedLimb(GameObject bone, GameObject prefab)
  {
    var newLimbRotation = Quaternion.LookRotation(gameObject.transform.forward);
    Instantiate(prefab, bone.transform.position, newLimbRotation);
  }

  private void SpawnBloodParticleEffect(GameObject bone)
  {
    var particleDirection = Quaternion.LookRotation(bone.transform.up);
    Instantiate(bloodParticleSystem, bone.transform.position, particleDirection, bone.transform);
  }
}

[Serializable]
public struct DetachableLimb
{
  public GameObject bone;
  public GameObject prefab;
}
