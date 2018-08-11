using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    public AudioSource efxSource;  //Drag a reference to the audio source which will play the sound effects
    [SerializeField]
    private AudioClip[] musicSource;             //Drag a reference to the audio source which will play the music.
    public static SoundManager instance = null; //Allows other scripts to call functions from SoundManager.
    public float lowPitchRange = .95f;          //The lowest a sound effect will be randomly pitched.
    public float highPitchRange = 1.05f;        //The highest a sound effect will be randomly pitched
    [SerializeField] 
    private Slider efxVolume; //Reference for a slider
    public AudioMixer audioMixer;

    private int playScene;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)       //Check if there is already an instance of SoundManager
            instance = this;        //If not, set it to this
        else if (instance != this)  //If instance already exists,
            Destroy(gameObject);    //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager

        DontDestroyOnLoad(gameObject);

    }

    void Start()
    {
//        playScene = SceneManager.GetActiveScene().buildIndex;
        efxSource = GetComponent<AudioSource>();
//        //Play the clip
////        efxSource.clip = musicSource[1];
////        efxSource.Play();
//        if (playScene == 0)
//        {
//            efxSource.Stop();
//            efxSource.clip = musicSource[0];
//            efxSource.Play();
//        }
//
//        if (playScene == 1)
//        {
//            efxSource.PlayOneShot(musicSource[1]);
//        }
    }

//    //Used to play single sound clips.
//    public void PlaySingle(AudioClip clip)
//    {
//        //Set the clip of our efxSource audio source to the clip passed in as a parameter
//        efxSource.clip = clip;
//
//        
//    }


    //RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
    public void RandomizeSfx(params AudioClip[] clips)
    {
        //Generate a random number between 0 and the length of our array of clips passed in.
        int randomIndex = Random.Range(0, clips.Length);

        //Choose a random pitch to play back our clip at between our high and low pitch ranges.
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        //Set the pitch of the audio source to the randomly chosen pitch.
        efxSource.pitch = randomPitch;

        //Set the clip to the clip at our randomly chosen index.
        efxSource.clip = clips[randomIndex];

        //Play the clip.
        efxSource.Play();
    }

//    public void SetEfxVolume()
       //    {
       //        efxSource.volume = efxVolume.value;
       //    }
    
    public void SetVolumeMusic(float volume)
    {
        audioMixer.SetFloat("volumeTest", volume);
    }

    public void SetVolumeSfx(float volume)
    {
        audioMixer.SetFloat("volumeSFX", volume);
    }

    public void ChangeBGM(int trackNum)
    {
        efxSource.Stop();
        efxSource.clip = musicSource[trackNum];
        efxSource.Play();
    }

}



        