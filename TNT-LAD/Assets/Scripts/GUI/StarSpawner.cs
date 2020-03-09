using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarSpawner : MonoBehaviour
{
  public void SpawnStars(int score, Transform parent)
  {
    for (int curScorePoint = 1; curScorePoint <= score; curScorePoint++) {
      GameObject starPrefab = (GameObject)Resources.Load("GUIComponents/Star");
      GameObject starPic = Instantiate(starPrefab,parent);
      starPic.transform.GetComponent<Image>().rectTransform.localPosition = new Vector3(-262 + curScorePoint * 80, 0, 0);
    }
  }
}
