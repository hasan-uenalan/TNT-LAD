public class LevelInfo
{
  public string levelFileName { get; set; }
  public CloudLevel cloudLevel { get; set; }

  public void SetLevel(string fileName)
  {
    cloudLevel = null;
    this.levelFileName = fileName;
  }

  public void SetLevel(CloudLevel cloudLevel)
  {
    levelFileName = null;
    this.cloudLevel = cloudLevel;
  }
}

