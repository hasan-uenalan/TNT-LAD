using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStarter : MonoBehaviour
{
  void Start()
  {
    FindObjectOfType<MusicHandler>().ChangeTrack("EndScreen");
  }
}
