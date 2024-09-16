using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    [SerializeField] private Slider _Slider;
    [SerializeField] private Image _FillImage;
    [SerializeField] private ParticleSystem _ExplosionParticles;
    [SerializeField] private AudioSource _ExplosionAudio;

    public event Action OnDie;

    private float _StartingHealth=100;
    private Color _FullHealthColor=Color.green;
    private Color _ZeroHealthColor = Color.red;
          
    private float _CurrentHealth;                      
    private bool _IsDead;

    private void Start()
    {
        ReSetTank();
    }
    private void ReSetTank()
    {
        _CurrentHealth=_StartingHealth;
        _IsDead = false;
        SetHealthUI();
    }

    private void SetHealthUI()
    {
        _Slider.value = _CurrentHealth;
        _FillImage.color = Color.Lerp(_ZeroHealthColor, _FullHealthColor,_CurrentHealth/_StartingHealth);
    }

    public void TakeDamage(float damageAmount)
    {
        _CurrentHealth -= damageAmount;

        SetHealthUI();

        if(_CurrentHealth <=0 && !_IsDead)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        _IsDead=true;
        _ExplosionParticles.Play();
        _ExplosionAudio.Play();
        if (TryGetComponent<PlayerInput>(out PlayerInput playerInput))
        {
            playerInput.enabled = false;
        }
        OnDie?.Invoke();
    }
}
