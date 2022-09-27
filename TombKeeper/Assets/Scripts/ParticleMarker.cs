using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ParticleMarker : MonoBehaviour
{
    public GameObject redParticle;
    public GameObject homeRune;
    public TextMeshProUGUI markerText;
    public TextMeshProUGUI runText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI loseText;
    public TextMeshProUGUI findText;
    private int maxMarkers = 10;
    private Vector3 Neithrune_collected = new Vector3(-190.8f, 0.62f, 67.0f);

    // Start is called before the first frame update
    void Start()
    {
        markerText.text = "Markers Left: " + maxMarkers;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && maxMarkers > 0)
        {
            GameObject marker = Instantiate(redParticle);
            marker.transform.position = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
            maxMarkers -= 1;
            markerText.text = "Markers Left: " + maxMarkers;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "neithrune")
        {
            Destroy(other.gameObject);
            runText.enabled = true;
            findText.enabled = false;
            homeRune.SetActive(true);
            GameObject.Find("Enemy").GetComponent<EnemyMovement>().New_Enemy_Pos(Neithrune_collected);
        }

        if(other.gameObject.tag == "homerune")
        {
            Time.timeScale = 0;
            winText.enabled = true;
            runText.enabled = false;
            markerText.enabled = false;
        }

        if(other.gameObject.tag == "Enemy")
        {
            Time.timeScale = 0;
            loseText.enabled = true;
            runText.enabled = false;
            markerText.enabled = false;
            findText.enabled = false;
        }   
    }
}