using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    private float timer = 0f;
    private float lvl_duration = 9f;
    public bool is_max_duration;
    private Vector3 wall_start_pos = new Vector3(200, 37, 503);
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = wall_start_pos;

        if (!is_max_duration)
        {
            StartCoroutine(CloseDistance());
        }
    }

    private IEnumerator CloseDistance()
    {
        Vector3 end_pos = new Vector3(-750, 37, 503);

        do {
            transform.position = Vector3.Lerp(wall_start_pos, end_pos, timer / lvl_duration);
            timer += Time.deltaTime;
            yield return null;
        }

        while(timer < lvl_duration);
        is_max_duration = true;
    }
}
