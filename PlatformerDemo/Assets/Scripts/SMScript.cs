using UnityEngine;

public class SMScript : MonoBehaviour
{
    [SerializeField] private AudioClip _player_jump_clip;
    [SerializeField] private AudioClip _enemy_defeated_clip;
    [SerializeField] private AudioClip _collectible_clip;
    [SerializeField] private AudioClip _powerup_clip;
    [SerializeField] private AudioClip _background_clip;
    [SerializeField] private float _background_volume;

    private void Start()
    {
        BackGroundMusic();
    }

    // loop sounds
    private void BackGroundMusic()
    {
        if (_background_clip == null)
        {
            return;
        }
        AudioSource sound = gameObject.AddComponent<AudioSource>();
        sound.clip = _background_clip;
        sound.loop = true;
        sound.volume = _background_volume >= 0f ? _background_volume : 1f;
        sound.Play();
    }

    // single instance sounds
    public void JumpSound()
    {
        if (_player_jump_clip == null)
        {
            return;
        }
        AudioSource sound = gameObject.AddComponent<AudioSource>();
        sound.clip = _player_jump_clip;
        sound.Play();
    }

    public void DefeatSound()
    {
        if (_enemy_defeated_clip == null)
        {
            return;
        }
        AudioSource sound = gameObject.AddComponent<AudioSource>();
        sound.clip = _enemy_defeated_clip;
        sound.Play();
    }

    public void CollectibleSound()
    {
        if (_collectible_clip == null)
        {
            return;
        }
        AudioSource sound = gameObject.AddComponent<AudioSource>();
        sound.clip = _collectible_clip;
        sound.Play();
    }

    public void PowerupSound()
    {
        if (_powerup_clip == null)
        {
            return;
        }
        AudioSource sound = gameObject.AddComponent<AudioSource>();
        sound.clip = _powerup_clip;
        sound.Play();
    }
}
