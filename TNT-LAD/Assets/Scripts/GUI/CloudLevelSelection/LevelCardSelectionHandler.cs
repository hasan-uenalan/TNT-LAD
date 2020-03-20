using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelCardSelectionHandler : MonoBehaviour
{
  public CloudLevelSelectionHandler cloudLevelSelectionHandler;

  void Start()
  {
    cloudLevelSelectionHandler = FindObjectOfType<CloudLevelSelectionHandler>();
    gameObject.GetComponent<Button>().onClick.AddListener(delegate () { cloudLevelSelectionHandler.SelectLevelCard(gameObject); });
  }
}
