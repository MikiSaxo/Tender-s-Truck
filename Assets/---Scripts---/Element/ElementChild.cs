using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ElementChild : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;

    public ElementType CurrentType => _currentType;

    private ElementType _currentType;

    public void Init(ElementType elementToSpawn, bool isEditor)
    {
        if (isEditor)
            gameObject.GetComponent<Rigidbody>().isKinematic = true;


        if (elementToSpawn is ElementType.RedVertical or ElementType.YellowVertical)
        {
            // transform.DORotate(new Vector3(0, 0, -90), 0);
            gameObject.GetComponent<Frite>().Init(elementToSpawn, isEditor);
        }

        if (elementToSpawn is ElementType.YellowHorizontal or ElementType.RedHorizontal)
        {
            gameObject.GetComponent<Frite>().Init(elementToSpawn, isEditor);
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