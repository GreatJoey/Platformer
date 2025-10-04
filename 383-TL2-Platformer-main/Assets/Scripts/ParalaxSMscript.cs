using Unity.VisualScripting;
using UnityEngine;

public class ParalaxSMscript : MonoBehaviour
{

    /* This script manages playing paralaxed sound for a tracked object relative to the player
     * 
     * 
     * 
     * Script made by Connor Wolfe (sound)
     */

    [Tooltip("Ensure you drag what you want to track here")]
    [SerializeField] private GameObject tracking;
 
    [Tooltip("Ensure your player object has the \"Player\" tag or drag player here")]
    [SerializeField] private GameObject player;
 
    [Tooltip("Farthest the player can be from the tracked object")]
    [SerializeField] private float sound_bound;

    [Tooltip("Set the sound you want parallaxed")]
    [SerializeField] private AudioClip clip;
    private AudioSource sound;

    private void Awake()
    {
        if (player == null) // find player if not assigned in Unity
            player = GameObject.FindGameObjectWithTag("Player");
       
        if (clip != null) // if we have a clip, we can set up the sound
        {
            sound = gameObject.AddComponent<AudioSource>();
            sound.clip = clip;
            sound.loop = true;
            sound.Play(); 
        }
    }

    private void Update()
    {
        UpdateVol();
    }

    // update the volume to be paralaxxed
    private void UpdateVol()
    {
        if (clip == null || player == null || tracking == null || sound == null) // do we have all our pieces?
            return;

        if (sound_bound <= 0) // can't have non-exstent or negative bound, so don't change volume
            return;

        float distance = Vector3.Distance(tracking.transform.position, player.transform.position);

        if (distance >= sound_bound)
            sound.volume = 0.01f;
        else
            sound.volume = 1.0f - (distance / sound_bound);

        Debug.Log("\ndistance == " + distance 
            + "\nsound_bound == " + sound_bound
            + "\n(distance/sound_bound) == " + (distance / sound_bound)
            + "\nsound.volume == " + sound.volume); ///////////////////////////////////////// remove this

    }    
}
