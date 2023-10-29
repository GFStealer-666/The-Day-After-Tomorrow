using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsScript : MonoBehaviour , IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameID;
    [SerializeField] string _iosGameID;
    [SerializeField] bool _testMode = true;
    private string _gameID;
    private void Awake() {
        Initializeads();
    }
    public void Initializeads(){
        _gameID = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iosGameID
            : _androidGameID;
        Advertisement.Initialize(_gameID , _testMode , this);
        
    }

    public void OnInitializationComplete(){
        Debug.Log("Unity Ads Intialization complete");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error , string message){
        Debug.Log($"Unity Ads Initialization failed : {error.ToString()} - {message}");
    }
}
