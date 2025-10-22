using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class HellGrayboxSetup : MonoBehaviour
{
    void Start()
    {
        // Fog
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Exponential;
        RenderSettings.fogDensity = 0.06f;
        RenderSettings.fogColor = new Color(0.24f, 0.04f, 0.04f);

        // Ambient
        RenderSettings.ambientMode = AmbientMode.Flat;
        RenderSettings.ambientLight = new Color(0.1f, 0.03f, 0.03f);

        // Directional light
        GameObject lightObj = new GameObject("Hell Light");
        Light dirLight = lightObj.AddComponent<Light>();
        dirLight.type = LightType.Directional;
        dirLight.color = new Color(1f, 0.27f, 0.1f);
        dirLight.intensity = 1.5f;
        dirLight.shadows = LightShadows.Soft;
        lightObj.transform.rotation = Quaternion.Euler(40, 45, 0);

        // Global Volume setup
        GameObject volumeObj = new GameObject("Hell Volume");
        Volume vol = volumeObj.AddComponent<Volume>();
        vol.isGlobal = true;
        vol.profile = ScriptableObject.CreateInstance<VolumeProfile>();

        Bloom bloom = vol.profile.Add<Bloom>(true);
        bloom.intensity.value = 0.5f;
        bloom.threshold.value = 1f;

        ColorAdjustments colorAdj = vol.profile.Add<ColorAdjustments>(true);
        colorAdj.postExposure.value = 0.3f;
        colorAdj.contrast.value = 35;
        colorAdj.colorFilter.value = new Color(1f, 0.2f, 0f);

        Vignette vignette = vol.profile.Add<Vignette>(true);
        vignette.intensity.value = 0.35f;
        vignette.smoothness.value = 0.5f;
        vignette.color.value = new Color(0.15f, 0f, 0f);

        ChromaticAberration chrom = vol.profile.Add<ChromaticAberration>(true);
        chrom.intensity.value = 0.1f;
    }
}
