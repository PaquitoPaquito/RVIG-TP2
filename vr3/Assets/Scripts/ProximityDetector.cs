using TMPro;
using UnityEngine;

public class ProximityDetector : MonoBehaviour
{
    [SerializeField] private Transform controllerTransform;
    [SerializeField] private GameObject[] targets;
    [SerializeField] private Color proximityColor = Color.green;
    [SerializeField] private float proximityScale = 1.2f;
    [SerializeField] private float proximityThreshold = 20f;
    [SerializeField] private float angleThreshold = 20;

    private int _closestTargetIndex;
    private float _closestTargetDistance;
    private Color _closestTargetOriginalColor;
    private bool _isTargetShown;
    
    private LineRenderer _lineRenderer;
    private GameObject _controllerTextGameObject;
    private TextMeshPro _controllerText;

    private void Start()
    {
        _closestTargetIndex = 0;
        _closestTargetDistance = Vector3.Distance(controllerTransform.position, targets[0].transform.position);
        _closestTargetOriginalColor = targets[0].GetComponent<Renderer>().material.color;
        
        _lineRenderer = gameObject.AddComponent<LineRenderer>();
        _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        _lineRenderer.startColor = Color.white;
        _lineRenderer.endColor = Color.black;
        _lineRenderer.startWidth = 0.2f;
        _lineRenderer.endWidth = 0.2f;
        _lineRenderer.positionCount = 2;
        
        _controllerTextGameObject = new GameObject("Controller Stats");
        _controllerTextGameObject.transform.SetParent(controllerTransform);
        _controllerText = _controllerTextGameObject.AddComponent<TextMeshPro>();
        _controllerText.transform.position = new Vector3(0f, .3f, 0f);
        _controllerText.transform.localScale = 0.02f * Vector3.one;
    }
    
    private void Update()
    {
        _closestTargetDistance = Vector3.Distance(controllerTransform.position, targets[_closestTargetIndex].transform.position);
        for  (var i = 0; i < targets.Length; i++)
        {
            float distance = Vector3.Distance(controllerTransform.position, targets[i].transform.position);
            if (i != _closestTargetIndex && distance < _closestTargetDistance)
            {
                HideClosestTarget();
                SetClosestTarget(i, distance);
            }
        }

        float angle = Vector3.Angle(controllerTransform.forward,
                                    targets[_closestTargetIndex].transform.position - controllerTransform.position);
        if (!_isTargetShown && angle < angleThreshold && _closestTargetDistance < proximityThreshold)
        {
            ShowClosestTarget();
        }
        if (_isTargetShown && (angle > angleThreshold || _closestTargetDistance > proximityThreshold))
        {
            HideClosestTarget();
        }

        if (_isTargetShown)
        {
            _lineRenderer.SetPosition(0, controllerTransform.position);
            _controllerText.text = "Distance=" + _closestTargetDistance.ToString() + " Angle=" + angle.ToString();
        }
    }

    private void SetClosestTarget(int i, float distance)
    {
        _closestTargetIndex = i;
        _closestTargetDistance = distance;
        _closestTargetOriginalColor =  targets[i].GetComponent<Renderer>().material.color;
        _lineRenderer.SetPosition(1, targets[i].transform.position);
    }

    private void ShowClosestTarget()
    {
        targets[_closestTargetIndex].GetComponent<Renderer>().material.color = proximityColor;
        targets[_closestTargetIndex].transform.localScale = proximityScale * Vector3.one;
        _lineRenderer.enabled = true;
        _isTargetShown = true;
        _controllerTextGameObject.SetActive(true);
    }

    private void HideClosestTarget()
    {
        targets[_closestTargetIndex].GetComponent<Renderer>().material.color = _closestTargetOriginalColor;
        targets[_closestTargetIndex].transform.localScale = Vector3.one;
        _lineRenderer.enabled = false;
        _isTargetShown = false;
        _controllerTextGameObject.SetActive(false);
    }
}
