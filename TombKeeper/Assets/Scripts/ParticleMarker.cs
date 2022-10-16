using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ParticleMarker : MonoBehaviour
{
    public GameObject redParticle;
    public GameObject neithRune;
    public GameObject winRune;
    public GameObject winPillar;
    public TextMeshProUGUI markerText;
    public TextMeshProUGUI runText;
    public GameObject guidingObject;
    public GameObject guidingObjectWin;
    public bool neithruneCollect;
    public Camera cam;
    private float radius = 10f;
    private int maxMarkers = 15;
    private Vector3 Neithrune_collected = new Vector3(-190.8f, 0.62f, 67.0f);
    public string chaseSound = "Why?";

    // Start is called before the first frame update
    void Start()
    {
        markerText.text = maxMarkers.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && maxMarkers > 0)
        {
            PlaceMarker();
        }

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, radius))
        {
            if(hit.collider.gameObject == neithRune)
            {
                guidingObject.SetActive(true);
                if(Input.GetKeyDown(KeyCode.F))
                {
                    GatherRune();
                }
            }
            else if(hit.collider.gameObject == winPillar && neithruneCollect)
            {
                guidingObjectWin.SetActive(true);
                if(Input.GetKeyDown(KeyCode.F))
                {
                    PlaceRune();
                }
            }
            else
            {
                guidingObject.SetActive(false);
                guidingObjectWin.SetActive(false);
            }
        }
    }

    public void PlaceMarker()
    {
        GameObject marker = Instantiate(redParticle);
        marker.transform.position = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
        maxMarkers -= 1;
        markerText.text = maxMarkers.ToString();
    }

    public void GatherRune()
    {
        Destroy(neithRune);
        guidingObject.SetActive(false);
        GameObject.Find("Enemy").GetComponent<EnemyMovement>().New_Enemy_Pos(Neithrune_collected);
        neithruneCollect = true;
        runText.enabled = true;
        FindObjectOfType<AudioManager>().Play(chaseSound);
    }

    public void PlaceRune()
    {
        winRune.SetActive(true);
        runText.enabled = false;
        neithruneCollect = false;
        SceneManager.LoadScene("Victory");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            FindObjectOfType<AudioManager>().Stop("TableTennis");
            SceneManager.LoadScene("GameOver");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }   
    }
}