using UnityEngine;

public class GameState : MonoBehaviour
{
    public bool isPlaying = true;
    public float playerCurrency = 50;
    
    public bool Finished => !isPlaying;
    
    public static GameState Instance;

    public GameObject wonPanel;
    public GameObject lostPanel;
    
    void Awake()
    {
        Instance = this;
    }

    public void Win()
    {
        Debug.Log("Won");
        isPlaying = false;
        wonPanel.SetActive(true);
    }

    public void Lose()
    {
        Debug.Log("Lost");
        isPlaying = false;
        lostPanel.SetActive(true);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
