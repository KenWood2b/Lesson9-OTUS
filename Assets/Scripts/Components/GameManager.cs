using UnityEngine;

using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Debug.LogError($"Find dubplicate of SingletonController [{this.name}]");
            //Destroy( _instance.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    [SerializeField] private  int _zombiCount;


    public void KillZombi()
    {
        _zombiCount--;

    }

    private void Update()
    {
        if(_zombiCount == 0)
        {
            Debug.Log("You Win");
        }
    }


}