using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyButtons;

[ExecuteInEditMode]
public class WebcamCapture : MonoBehaviour {
    
    public enum Resolution
    {
        FHD_1920x1080,
        HD_1280x720,
        nHD_640x360,
        XGA_1024x768,
        SVGA_800x600,
        VGA_640x480,
        DVCPRO_HD_960x720
    }
    public Resolution resolution;
    private Vector2Int resulutionSelect()
    {
        switch (resolution)
        {
            default:
                return new Vector2Int(1280, 720);

            case Resolution.FHD_1920x1080:
                return new Vector2Int(1920, 1080);
            case Resolution.HD_1280x720:
                return new Vector2Int(1280, 720);
            case Resolution.nHD_640x360:
                return new Vector2Int(640, 360);
            case Resolution.XGA_1024x768:
                return new Vector2Int(1024, 768);
            case Resolution.SVGA_800x600:
                return new Vector2Int(800, 600);
            case Resolution.VGA_640x480:
                return new Vector2Int(640, 480);
            case Resolution.DVCPRO_HD_960x720:
                return new Vector2Int(960, 720);
        }
    }

    [ReadOnly]
    public string[] cameraList;
    public int CameraIndex = 0;
    [Range(0, 60)]
    public int camerafps = 30;

    public RawImage outputRawImage;

    private WebCamDevice[] wcd;
    protected WebCamTexture c;

    private void Start()
    {
        if (wcd == null)
            return;
            
        wcd = WebCamTexture.devices;
        cameraList = new String[wcd.Length];

        if (wcd.Length == 0)  
            Debug.LogWarning("找不到實體攝影機");
        
        else
        {
            for (int i = 0; i < wcd.Length; i++)
            {
                Debug.Log("目前可用的攝影機有：" + wcd[i].name);
                cameraList[i] = wcd[i].name;
            }

            Debug.Log("----------------------------------------------------------------");
            Debug.Log("目前使用的攝影機是：" + wcd[CameraIndex].name);

            Vector2Int resol = resulutionSelect();
            c = new WebCamTexture(wcd[CameraIndex].name, resol.x, resol.y, camerafps);
            outputRawImage.texture = c;
        }

        if (Application.isPlaying)
            c.Play();        
    }

    private void OnDisable()
    {
        if (c != null)        
            c.Stop();        
    }

    private void OnApplicationQuit()
    {
        if (c != null)
            c.Stop();
    }
}
