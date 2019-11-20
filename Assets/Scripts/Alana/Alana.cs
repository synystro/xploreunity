using UnityEngine;

public class Alana : MonoBehaviour {
    
    //new AudioSource audio;

    #region Singleton

    public static Alana instance;

    AudioSource audio;

    AudioClip[] welcomeBackSir;

    private void Awake() {
        if (instance != null) {
            Debug.Log("More than one instance of Alana found.");
            return;
        }
        instance = this;

        // load AudioSource.
        audio = GetComponent<AudioSource>();

        // load welcome back audio files.
        welcomeBackSir = Resources.LoadAll<AudioClip>("Audio/Alana/WelcomeBackSir");

    }
    #endregion

    private void Start() {

        

        audio.PlayOneShot(welcomeBackSir[Random.Range(0, welcomeBackSir.Length)]);

    }
}
