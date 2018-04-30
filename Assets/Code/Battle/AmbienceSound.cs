using UnityEngine;
using System.Collections;
using System.Linq;

public class AmbienceSound : MonoBehaviour
{
    public float AmbienceDelay;
    public float AmbienceAmplitude;
    public AudioClip victory;

    private bool isAmbiencePlaying;
    private AudioSource ambience;
    private AudioSource singleClip;

    private void Start()
    {
        singleClip = this.GetComponents<AudioSource>()[0];
        ambience = this.GetComponents<AudioSource>()[1];
    }

    // Update is called once per frame
	void Update ()
	{
	    if (!isAmbiencePlaying)
	    {
	        AmbienceDelay -= Time.deltaTime;
	        if (AmbienceDelay <= 0)
	        {
                ambience.Play();
	            isAmbiencePlaying = true;
	        }
	    }

	    if (isAmbiencePlaying && ambience.volume < 0.8)
	    {
	        ambience.volume += AmbienceAmplitude;
	    }
	}

    public void EndBattleVictory()
    {
        ambience.Stop();
        singleClip.clip = victory;
        singleClip.Play();
    }
}
