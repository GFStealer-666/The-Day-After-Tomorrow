using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RG_HealthBar : MonoBehaviour
{

    public Slider _heatlhBarSlider ;
    
    [SerializeField] private Color _lowHealth ;
    
    [SerializeField] private Color _highHealth;

    [SerializeField] private GameObject _playerGraphic;
    [SerializeField] private GameObject _playerHealthBarCanvas;
    [SerializeField] private Vector3 _offset;
    public void SetHealthBar(float _currentHealth , float _maxHealth)
    {
        _heatlhBarSlider.gameObject.SetActive(_currentHealth < _maxHealth);
        _heatlhBarSlider.value = _currentHealth;
        _heatlhBarSlider.maxValue = _maxHealth;
        _heatlhBarSlider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(_lowHealth , _highHealth , _heatlhBarSlider.normalizedValue);
        
    }

    void Update()
    {
        _heatlhBarSlider.transform.position = _playerGraphic.transform.position + _offset;        
    }



    
}
