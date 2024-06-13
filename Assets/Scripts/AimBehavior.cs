using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AimBehavior : MonoBehaviour
{
    //[SerializeField] Transform customers;
    GameObject customer;

    Vector2 aimPos; //niþan
    Vector2 mousPos; //fare 
    bool isFreeToShoot = true;
    bool shootTrigger = false;
    float loadTime = 0.8f;

    [SerializeField] AudioClip shootsound; //might not need these
    [SerializeField] AudioClip reloadsound; //might not need these
    [SerializeField] AudioSource shootSoundSource;
    [SerializeField] AudioSource reloadSoundSource;

    [SerializeField] Text infoText; // Reference to the UI Text component

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        GetComponent<Transform>().position = aimPos;
        GetComponent<Collider2D>().isTrigger = shootTrigger;
        customer = GameObject.Find("Customer");
        shootSoundSource = GetComponent<AudioSource>();
        shootsound = GetComponent<AudioClip>();
        reloadsound = GetComponent<AudioClip>();
    }    
    
    IEnumerator ShootLogic() //main shooting coroutine
    {
        if (Input.GetMouseButtonDown(0) && isFreeToShoot) 
        {
            shootTrigger = true;
            isFreeToShoot = false;
            shootSoundSource.Play();
            Debug.Log("Shot!");

            //ALSO BELOW

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // cast ray to hit the player so it even incase of imperfect Z axis allignments
            RaycastHit2D hit = Physics2D.Raycast(transform.position, (mousePosition - (Vector2)transform.position).normalized);
            if (hit.collider != null)
            {
                Debug.Log("Shot!");
                shootSoundSource.Play();

                if (hit.collider.CompareTag("Customers")) //checks if the hit object is a customer 
                {
                    Debug.Log("CUSTOMER hit! Sending death message.");
                    customer.SendMessage("OnDeath");
                }
            }
        } else if (shootTrigger)
        { 
            shootTrigger = false;
            Debug.Log("Waiting...");  
            yield return new WaitForSeconds(loadTime);
            isFreeToShoot = true;
            reloadSoundSource.Play();
            Debug.Log("Done! Next round?");
        }    
    }

    void CursorLogic() //handling cursor mouse positioning
    {
        Cursor.visible = false;
        mousPos = Input.mousePosition;
        aimPos = Camera.main.ScreenToWorldPoint(mousPos);
        transform.position = aimPos;
    }
    void RestartCheck() 
    { 
        if (Input.GetKeyDown("r")) 
        {
            // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            // Reload the current scene
            SceneManager.LoadScene(currentSceneIndex);
        }
    }
    void Update()
    {
        CursorLogic();
        StartCoroutine(ShootLogic());
        RestartCheck();
    }  
}
