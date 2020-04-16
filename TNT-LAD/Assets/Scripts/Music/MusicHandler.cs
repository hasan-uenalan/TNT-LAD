using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
  private AudioSource audioSource;

  public AudioSource MainMenuAudio;
  public AudioSource InGameAudio;
  public AudioSource SuddonDeathAudio;
  public AudioSource EndScreenAudio;

  public Dictionary<string, AudioSource> Tracks;

  void Awake()
  {
    FillTracks();
    GameObject[] handlers = GameObject.FindGameObjectsWithTag("Music");
    if(handlers.Length>1)
    {
      Destroy(this.gameObject);
    }
    audioSource = gameObject.GetComponent<AudioSource>();
    DontDestroyOnLoad(this.gameObject);
  }

  public void ChangeTrack(string trackName)
  {
    foreach (KeyValuePair<string, AudioSource> curTrack in Tracks) 
    {
      if(curTrack.Key == trackName)
      {
        audioSource = curTrack.Value;
        audioSource.loop = true;
      }
    }
  }

  private void FillTracks()
  {
    Tracks = new Dictionary<string, AudioSource>();
    Tracks.Add("MainMenu", MainMenuAudio);
    Tracks.Add("InGame", InGameAudio);
    Tracks.Add("SuddonDeath", SuddonDeathAudio);
    Tracks.Add("EndScreen", EndScreenAudio);
  }
}
