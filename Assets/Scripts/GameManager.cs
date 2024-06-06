using UnityEngine;

public class GameManager : MonoBehaviour
{
  //public PlayerView Player;

  private void Awake()
  {
    
  }
  
  private void Start()
  {
    DontDestroyOnLoad(gameObject);
  }
}
