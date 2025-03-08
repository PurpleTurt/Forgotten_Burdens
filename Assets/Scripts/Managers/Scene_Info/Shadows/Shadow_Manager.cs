using UnityEngine;

public class Shadow_Manager : MonoBehaviour
{
    [SerializeField]
    private Shadow_Scriptable_Object shadow_data;


    // Update is called once per frame
    void Update()
    {
        Vector2 Scale = transform.localScale;
        Scale.x = shadow_data.Shadow_Scale;
        transform.localScale = Scale;
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        Vector2 Scale = transform.localScale;
        Scale.x = shadow_data.Shadow_Scale;
        transform.localScale = Scale;
    }
#endif
}
