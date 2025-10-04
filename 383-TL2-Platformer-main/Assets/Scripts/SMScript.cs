using UnityEngine;

public class SMScript : MonoBehaviour
{
    /* Manages sounds for all sounds
        - most sounds are made to parallax
     * 
     * Script made by Connor Wolfe (sound)
     */


    [Tooltip("The audio clip needs to be a .wav file")]
    // player sounds
    [SerializeField] private AudioClip player_jump_clip;

    //enemy sounds
    [SerializeField] private AudioClip enemy_defeated_clip;

    // item sounds
    [SerializeField] private AudioClip collectable_clip;
    [SerializeField] private AudioClip powerup_clip;

    // background
    [SerializeField] private AudioClip background_clip;
    [SerializeField] private float background_volume;

    private void Start()
    {
        BackgroundMusic();
    }

    // looped sounds //
    public void BackgroundMusic()
    {
        if (background_clip == null)
            return;

        AudioSource sound = gameObject.AddComponent<AudioSource>();
        sound.clip = background_clip;
        sound.loop = true;
        sound.volume = background_volume >= 0f ? background_volume : 1f;
        sound.Play();
    }


    // single instance sounds //
    public void JumpSound()
    {
        if (player_jump_clip == null)
            return;

        AudioSource sound = gameObject.AddComponent<AudioSource>();
        sound.clip = player_jump_clip;
        sound.Play();   
    }
    
    public void DefeatSound()
    {
        if (enemy_defeated_clip == null)
            return;

        AudioSource sound = gameObject.AddComponent<AudioSource>();
        sound.clip = enemy_defeated_clip;
        sound.Play();
    }

    public void CollectableSound()
    {
        if (collectable_clip == null)
            return;

        AudioSource sound = gameObject.AddComponent<AudioSource>();
        sound.clip = collectable_clip;
        sound.Play();
    }

    public void PowerupSound()
    {
        if (powerup_clip == null)
            return;

        AudioSource sound = gameObject.AddComponent<AudioSource>();
        sound.clip = powerup_clip;
        sound.Play();
    }
}