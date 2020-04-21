using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
  public List<PowerUpDrop> powerUpDrops;

  [Serializable]
  public class PowerUpDrop
  {
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
      float randomMultiplier = randomForceMultiplier ? UnityEngine.Random.Range(0.6f, 1.4f) : 1;
      rb.AddForce(direction * explosionForce * randomMultiplier, ForceMode.Impulse);
      Vector3 torque = new Vector3(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
      rb.AddTorque(torque, ForceMode.Impulse);
    }
    Destroy(gameObject);
    SpawnPowerUp();
  }

  private void SpawnPowerUp()
  {
    if(UnityEngine.Random.value <= generalDropProbability)
    {
      var randomPowerUp = RandomWeightedPick(powerUpDrops);
      Instantiate(randomPowerUp.item, transform.position, Quaternion.identity);
    }
  }

  public PowerUpDrop RandomWeightedPick(List<PowerUpDrop> powerUpDrops)
  {
    int systemCenterBodyLength = powerUpDrops.Count;
    List<int> weights = new List<int>();
    int weightTotal = 0;

    //Fills weights List with keys from the dictionary.
    //Calculates total weight.
    foreach (var item in powerUpDrops)
    {
      weights.Add(item.dropRarity);
      weightTotal += item.dropRarity;
    }

    //Picks a random entry from the dictionary under consideration of the weight AND the seed.
    int result = 0, total = 0;
    int randVal = UnityEngine.Random.Range(0, weightTotal + 1);
    for (result = 0; result < weights.Count; result++)
    {
      total += weights[result];
      if (total >= randVal) break;
    }
    return powerUpDrops.ElementAt(result);
  }

}
