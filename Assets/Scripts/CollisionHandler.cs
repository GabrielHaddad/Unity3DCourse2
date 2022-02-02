using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Header("Levels")]
    [SerializeField] float loadDelay = 1f;
    [SerializeField] float loadNextLevelDelay = 1.5f;

    [Header("Sounds")]
    [SerializeField] AudioClip finishSound;
    [SerializeField] [Range(0, 1)] float finishVolume;
    [SerializeField] AudioClip crashSound;
    [SerializeField] [Range(0, 1)] float crashVolume;

    [Header("Particles")]
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem finishParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisable = false;

    void Awake() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() 
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(LoadNextLevel());

        } else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable;
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning || collisionDisable){ return; }

        switch(other.gameObject.tag)
        {
            case "Start":
            Debug.Log("Alo");
            break;

            case "Finish":
            StartLoadLevelSequence();
            break;
            
            default:
            StartCrashSequence();
            break;
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.pitch = 1f;
        audioSource.PlayOneShot(crashSound, crashVolume);
        crashParticles.Play();
        StartCoroutine(ReloadLevel());
    }

    void StartLoadLevelSequence()
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.pitch = 1f;
        audioSource.PlayOneShot(finishSound, finishVolume);
        finishParticles.Play();
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(loadDelay);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(loadNextLevelDelay);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
}
