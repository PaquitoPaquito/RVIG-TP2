using TMPro;
using UnityEngine;

public class FloatingMenu : MonoBehaviour
{
    private Camera _camera;
    private TextMeshPro _menuText;
    
    private void Start()
    {
        _camera = Camera.main;
        _menuText = GetComponentInChildren<TextMeshPro>();
    }

    private void Update()
    {
        transform.LookAt(_camera.transform);
        _menuText.text = "Position_locale=" 
                         + (transform.position-transform.parent.transform.position).ToString()
                         + " Position_mondiale=" 
                         + transform.position.ToString();
    }
}
