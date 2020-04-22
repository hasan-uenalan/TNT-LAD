using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
  private AudioSource audioSource;

  public AudioClip MainMenuAudio;
  public AudioClip InGameAudio;
  public AudioClip SuddonDeathAudio;
  public AudioClip EndScreenAudio;

  public Dictionary<string, AudioClip> Tracks;

  void Awake()
  {
    FillTracks();
    GameObject[] handlers = GameObject.FindGameObjectsWithTag("Music");
    if(handlers.Length>1)
    {
      Destroy(this.gameObject);
    }
    audioSource = gameObject.GetComponent<AudioSource>();
    audioSource.volume = CrossSceneInformation.MusicVolume;
    DontDestroyOnLoad(this.gameObject);
  }

  public void ChangeTrack(string trackName)
  {
    foreach (KeyValuePair<string, AudioClip> curTrack in Tracks) 
    {
      if(curTrack.Key == trackName)
      {
        audioSource.clip = curTrack.Value;
        audioSource.Play();
      }
    }
  }

  public void ChangeVolume()
  {
    audioSource.volume = CrossSceneInformation.MusicVolume;   
  }

  private void FillTracks()
  {
    Tracks = new Dictionary<string, AudioClip>();
    Tracks.Add("MainMenu", MainMenuAudio);
    Tracks.Add("InGame", InGameAudio);
    Tracks.Add("SuddonDeath", SuddonDeathAudio);
    Tracks.Add("EndScreen", EndScreenAudio);
  }
}
