using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class InfoPanel : MonoBehaviour
{
    private static InfoPanel _instance;
    public static InfoPanel Instance
    {
        get 
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<InfoPanel>();
            }

            return _instance;
        }
    }
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Debug.LogError($"Find dubplicate of SingletonController [{this.name}]");
        }
        else
        {
            _instance = this;
        } 
    }

    [SerializeField] private Text _zombiStatus;


    public void SetZombiStatus(string status)
    {
        _zombiStatus.text = status;
        
    }
    
}
