using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDistroyAudio : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}


