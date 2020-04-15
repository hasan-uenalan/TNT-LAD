using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoxHandler : MonoBehaviour
{
  [Header("Destruction")]
  public GameObject preShatteredObj;
  public bool randomForceMultiplier;
  public float explosionForce;

  [Header("Power Up")]
  [Range(0f, 1f)]
  public float generalDropProbability = .8f;
  public List<GameObject> powerUp;

  public class PowerUpDrop
  {
    public string name;
    public GameObject item;
    public int dropRarity;
  }


  [ContextMenu("Destroy Box")]
  public void DestroyBoxAndSpawnPowerUp()
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
    SpawnPowerUp();
  }

  private void SpawnPowerUp()
  {
    if(Random.value <= generalDropProbability)
    {
      int randomPowerUp = Random.Range(0, powerUp.Count);
      Instantiate(powerUp[randomPowerUp], transform.position, Quaternion.identity);
    }
  }

}
