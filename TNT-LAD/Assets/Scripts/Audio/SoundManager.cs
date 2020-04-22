using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SoundManager
{
  public enum Sound
  {
    PlayerDamage,
    PowerUp,
    Explosion,
    Firework,
    RPG
  }

  //Set by the SoundImporter in the MainScene
  public static List<SoundAudioClip> soundAudioClips;

  public static void PlaySound(Sound sound)
  {
    var soundObj = new GameObject($"SoundEmitter_{sound.ToString()}_{(int)(UnityEngine.Random.value*100)}");
    var audioSource = soundObj.AddComponent<AudioSource>();
    audioSource.clip = soundAudioClips.FirstOrDefault(x => x.Sound == sound).AudioClip;
    audioSource.volume = CrossSceneInformation.SoundVolume;
    audioSource.Play();
    GameObject.Destroy(soundObj, audioSource.clip.length + 1f);
  }
}
