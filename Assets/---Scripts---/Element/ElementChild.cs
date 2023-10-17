using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ElementChild : MonoBehaviour
{
    [SerializeField] private Mesh[] _meshes;
    [SerializeField] private MeshFilter _elementMesh;
    [SerializeField] private MeshRenderer _meshRenderer;

    public ElementType CurrentType => _currentType;

    private ElementType _currentType;

    private void Start()
    {
        if (_meshes.Length > 0)
        {
            int randomNumber = Random.Range(0, _meshes.Length);
            _elementMesh.mesh = _meshes[randomNumber];
        }
    }

    public void Init(ElementType elementToSpawn, bool isEditor)
    {
        if (isEditor)
            gameObject.GetComponent<Rigidbody>().isKinematic = true;


        if (elementToSpawn is ElementType.RedVertical or ElementType.YellowVertical)
        {
            // transform.DORotate(new Vector3(0, 0, -90), 0);
            gameObject.GetComponent<Frite>().Init(elementToSpawn);
        }

        if (elementToSpawn is ElementType.YellowHorizontal or ElementType.RedHorizontal)
        {
            gameObject.GetComponent<Frite>().Init(elementToSpawn);
        }

        if (elementToSpawn == ElementType.Mozza_ClockWise)
        {
            gameObject.GetComponent<MozzaStick>().SpawnClock(ClockWise.Clockwise);
        }
        else if (elementToSpawn == ElementType.Mozza_Anti_ClockWise)
        {
            gameObject.GetComponent<MozzaStick>().SpawnClock(ClockWise.Anti_Clockwise);
        }

        _currentType = elementToSpawn;

        if (_meshRenderer == null)
            return;

        if (PartyManager.Instance != null)
            _meshRenderer.material = PartyManager.Instance.GetElementTypeMat((int)_currentType);
        else if (EditorManager.Instance != null)
            _meshRenderer.material = EditorManager.Instance.GetElementType((int)_currentType);
    }
}