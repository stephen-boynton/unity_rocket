using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {
    
    [SerializeField]float rcsThrust = 150f;
    [SerializeField] float mainThrust = 10f;
    [SerializeField] float loadTime = 2f;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip levelComplete;

    [SerializeField] ParticleSystem thrustParticles;
    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] ParticleSystem successParticles;

    Rigidbody rigidbody;
    AudioSource audio;

    enum State 
    {
        Alive, Dying, Transcending
    }

    State state = State.Alive;

	// Use this for initialization
	void Start () 
    {
        rigidbody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (state == State.Alive)
        {
            RespondToThrust();
            RespondToRotation();
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        if(state != State.Alive) { return; }

        switch(collision.gameObject.tag) 
        {
            case "Friendly":
                print("Okay.");
                break;
            case "Finish":
                state = State.Transcending;
                audio.PlayOneShot(levelComplete);
                successParticles.Play();
                print("Done!");
                Invoke("LoadNextScene", loadTime);
                break;
            default:
                state = State.Dying;
                audio.Stop();
                thrustParticles.Stop();
                audio.PlayOneShot(explosion);
                explosionParticles.Play();
                print("Dead.");
                Invoke("restartGame", loadTime);
                break;
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }

    private void restartGame() 
    {
        SceneManager.LoadScene(0);
    }
    private void RespondToThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            audio.Stop();
            thrustParticles.Stop();
        }
    }

    private void ApplyThrust()
    {
        rigidbody.AddRelativeForce(Vector3.up * mainThrust);

        if (!audio.isPlaying)
        {
            audio.PlayOneShot(mainEngine);
        }
        thrustParticles.Play();
    }

    private void RespondToRotation()
    {
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        rigidbody.freezeRotation = true; //take manual control of roatation

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back * rotationThisFrame);
        }

        rigidbody.freezeRotation = false; // let physics do its work
    }


}
