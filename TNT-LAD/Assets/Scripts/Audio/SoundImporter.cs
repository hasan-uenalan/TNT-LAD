using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundImporter : MonoBehaviour
{
  public List<SoundAudioClip> soundAudioClips;

  private void Awake()
  {
    SoundManager.soundAudioClips = this.soundAudioClips;
  }
}

[Serializable]
public class SoundAudioClip
{
  public SoundManager.Sound Sound;
  public AudioClip AudioClip;
}
