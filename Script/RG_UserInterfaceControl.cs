using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RG_UserInterfaceControl : MonoBehaviour
{
    [SerializeField] private GameObject _medKitUI;

    [SerializeField] private GameObject _pistolUI;

    [SerializeField] private GameObject _adrenalineUI;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnable(){
        RG_Medkit.OnFirstMedkitPickup += OnMedKitPickUp;
        RG_PickItemUp.OnGunPickUp += OnPistolPickUp;
        RG_Adrenaline.OnAdrenalinePickUp += OnAdrenalinePickUp;
    }

    public void OnDisable(){
        RG_Medkit.OnFirstMedkitPickup -= OnMedKitPickUp;
        RG_PickItemUp.OnGunPickUp -= OnPistolPickUp;
        RG_Adrenaline.OnAdrenalinePickUp -= OnAdrenalinePickUp;

    }

    private void OnMedKitPickUp(){
        _medKitUI.SetActive(true);
    }

    private void OnPistolPickUp(){
        _pistolUI.SetActive(true);
    }

    private void OnAdrenalinePickUp(){
        _adrenalineUI.SetActive(true);
    }


}
