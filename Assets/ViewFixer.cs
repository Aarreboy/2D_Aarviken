using UnityEngine;

public class ViewFixer : MonoBehaviour
{
    [SerializeField] Camera m_obscure_camera;
    [SerializeField] RenderTexture m_obscure_texture;

    int m_screen_y;
    int m_screen_x;

    private void Awake()
    {
        m_obscure_camera.targetTexture = m_obscure_texture;
        UpdateRenderTexCam(m_obscure_camera, "_ViewMask");
    }

    public void UpdateRenderTexCam(Camera camera, string texture_name)
    {
        m_screen_x = (int)(float)Screen.width;
        m_screen_y = (int)(float)Screen.height;
        camera.targetTexture?.Release();
        RenderTexture t_rendertex = new RenderTexture(camera.targetTexture);
        t_rendertex.width = m_screen_x;
        t_rendertex.height = m_screen_y;
        t_rendertex.Create();
        camera.targetTexture = t_rendertex;
        Shader.SetGlobalTexture(texture_name, t_rendertex);
    }
}
