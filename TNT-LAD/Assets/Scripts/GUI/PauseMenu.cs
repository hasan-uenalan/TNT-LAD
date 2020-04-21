using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
  public GameObject shownPivot;
  public GameObject hiddenPivot;
  
  private bool menuShown;
  private bool menuExpanding;

  public void TogglePauseMenu()
  {
    if (menuExpanding)
    {
      return;
    }
    menuExpanding = true;
    if (menuShown)
    {
      //hide
      PauseGame(false);
      LeanTween.move(gameObject, hiddenPivot.transform.position, 1f).setEaseOutElastic().setOnComplete(delegate() { menuExpanding = false; });
    }
    else
    {
      //show
      PauseGame(true);
      LeanTween.move(gameObject, shownPivot.transform.position, 1f).setEaseOutElastic().setOnComplete(delegate () { menuExpanding = false; });
    }
    menuShown = !menuShown;
  }

  public void PauseGame(bool pause)
  {
    foreach(var playerObj in GameObject.FindGameObjectsWithTag("Player"))
    {
      if (pause)
      {
        playerObj.GetComponent<PlayerInput>().DeactivateInput();
      }
      else
      {
        playerObj.GetComponent<PlayerInput>().ActivateInput();
      }
      var playerHandler = playerObj.GetComponent<PlayerHandler>();
      if (playerHandler.PlayerStatus != PlayerHandler.Status.dead)
      {
        playerHandler.PlayerStatus = pause ? PlayerHandler.Status.invincible : PlayerHandler.Status.alive;
      }
    }
    FindObjectOfType<Countdown>().paused = pause;
  }
}
