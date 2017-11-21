using UnityEngine;

public class Rocket : MonoBehaviour {
    
    [SerializeField]float rcsThrust = 150f;
    [SerializeField] float mainThrust = 10f;
    Rigidbody rigidbody;
    AudioSource audio;

	// Use this for initialization
	void Start () 
    {
        rigidbody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        Thrust();
        Rotation();
	}

    void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag) 
        {
            case "Friendly":
                print("Okay.");
                break;
            default:
                print("Dead.");
                break;
        }
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up * mainThrust);

            if (!audio.isPlaying)
            {
                audio.Play();
            }
        }
        else
        {
            audio.Stop();
        }
    }

    private void Rotation()
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
