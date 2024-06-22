using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class End : MonoBehaviour
{
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);
    AudioSource pickupSource;

    private void Awake() {
        pickupSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Damageable damageable = collision.GetComponent<Damageable>();

            if(pickupSource){
                  AudioSource.PlayClipAtPoint(pickupSource.clip, gameObject.transform.position, pickupSource.volume);
                
                SceneManager.LoadScene("QuitScene");
                
            Destroy(gameObject);
            }
    }
    private void Update() {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
}
