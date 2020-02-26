using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoxDestroyer : MonoBehaviour
{
  public GameObject preShatteredObj;
  public bool randomForceMultiplier;
  public float explosionForce;

  public void OnMouseDown()
  {
    DestroyBox();
  }

  [ContextMenu("Destroy Box")]
  void DestroyBox()
  {
    GameObject destroyedBox = Instantiate(preShatteredObj, gameObject.transform.position, Quaternion.identity);
    foreach(Transform boxPart in destroyedBox.transform)
    {
      Rigidbody rb = boxPart.GetComponent<Rigidbody>();
      Vector3 direction = (boxPart.position - gameObject.transform.position).normalized;
      float randomMultiplier = randomForceMultiplier ? Random.Range(0.6f, 1.4f) : 1;
      rb.AddForce(direction * explosionForce * randomMultiplier, ForceMode.Impulse);
      Vector3 torque = new Vector3(Random.value, Random.value, Random.value);
      rb.AddTorque(torque, ForceMode.Impulse);
    }
    Destroy(gameObject);
  }
}
