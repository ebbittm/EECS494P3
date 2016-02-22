using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Compass : MonoBehaviour {

    public Texture2D CompassTexture;
    public float CameraAngle;
    private float TextureWidth;
    private float TextureHeight;

    void Start()
    {
        TextureWidth = Screen.width;// (360 / Camera.main.fieldOfView) * Camera.main.aspect * Screen.width;
        TextureHeight = CompassTexture.height * TextureWidth / CompassTexture.width;
    }

    void OnGUI()
    {
        CameraAngle = Camera.main.transform.eulerAngles.y;

        if (CameraAngle > 180)
        {
            CameraAngle -= 360;
        }

        float compX = Screen.width / 2 - CameraAngle / 360 * TextureWidth;
        GUI.DrawTexture(new Rect(compX - TextureWidth, 0, TextureWidth, TextureHeight), CompassTexture);
        GUI.DrawTexture(new Rect(compX, 0, TextureWidth, TextureHeight), CompassTexture);
    }
}
