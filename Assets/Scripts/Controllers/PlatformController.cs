using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {

    public GameObject platformModel;

    private int maxRows = 50;
    private GameObject[,] platforms;

    private Platform sinkedPlatform;

	public void Init () {
        platforms = new GameObject[maxRows, 6];
	}

    public void SinkPlatform(Platform platform) {
        UnSinkPlatform();
        platform.SinkPlatform();
        sinkedPlatform = platform;
    }

    public void UnSinkPlatform()
    {
        if (sinkedPlatform != null)
        {
            sinkedPlatform.UnSinkPlatform();
            sinkedPlatform = null;
        }
    }

    // This function only is called if the position is a river
    public void CreatePlatformRow (int pos) {
        int row = pos % maxRows;
        float tamPlatform = Random.Range(2, 4);

        float speed = Random.Range(40, 60) / 10.0f;

        int direction = Random.Range(0, 2);
        if (direction == 0) direction = -1;

        GameObject platformTmp;

        for (int i = 0; i < 6; ++i)
        {
            Destroy(platforms[row, i]);
            platformTmp = Instantiate(platformModel, new Vector3(-8.0f + (2.5f * (tamPlatform * 1.5f) * i), 0.0f, pos * 1.5f), new Quaternion(0.0f, Mathf.PI / 2, 0.0f, 0.0f)) as GameObject;

            Platform platformSettings = platformTmp.GetComponent<Platform>();
            platformSettings.SetDirection(direction);
            platformSettings.SetSpeed(speed);

            platformTmp.transform.localScale = new Vector3(tamPlatform / 10.0f, 0.15f, 0.2f);
            platforms[row, i] = platformTmp;
        }
        
	}
}
