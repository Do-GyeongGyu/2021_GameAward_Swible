using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoTonePostEffect : MonoBehaviour
{
    public Material monoTone;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, monoTone);
    }
}
