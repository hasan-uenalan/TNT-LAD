using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecayAfterTime : MonoBehaviour
{
  public float lifeTime;
  public bool destroy;
  Transform destroyedTransform;

  void Start()
  {
    destroyedTransform = gameObject.transform;
    StartCoroutine(StartLifeTime());
  }

  public IEnumerator StartLifeTime()
  {
    yield return new WaitForSeconds(lifeTime);
    
    //Fade out animation
    float elapsedTime = 0f;
    float fadeTime = 2f;
    while(elapsedTime < fadeTime)
    {
      elapsedTime += Time.deltaTime;
      foreach(Transform boxFragment in destroyedTransform)
      {
        Color objColor = boxFragment.gameObject.GetComponent<MeshRenderer>().material.color;
        objColor.a = Mathf.SmoothStep(1f, 0f, (elapsedTime / fadeTime));
        boxFragment.gameObject.GetComponent<MeshRenderer>().material.color = objColor;
      }
      yield return null;
    }
    if (destroy)
    {
      Destroy(gameObject);
    }
  }
}
