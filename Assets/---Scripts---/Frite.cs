using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Frite : MonoBehaviour
{
   [SerializeField] private Collider[] _colliders;
   [SerializeField] private float _rotateSpeed;
   [Header("Frites")]
   [SerializeField] private GameObject[] _yellowHorizontal;
   [SerializeField] private GameObject[] _yellowVertical;
   [SerializeField] private GameObject[] _redHorizontal;
   [SerializeField] private GameObject[] _redVertical;

   private ElementType _type;
   private GameObject _frite;

   public void Init(ElementType type, bool isEditor)
   {
      int rdn = Random.Range(0,3);
      _type = type;
      
      if (type == ElementType.YellowHorizontal)
      {
         GameObject go = Instantiate(_yellowHorizontal[rdn], transform);
         go.transform.DOScale(15, 0);
         go.transform.DORotate(new Vector3(0, 0, 90), 0);
         _frite = go;
      }
      else if (type == ElementType.YellowVertical)
      {
         GameObject go = Instantiate(_yellowVertical[rdn], transform);
         go.transform.DOScale(15, 0);
         go.transform.DORotate(new Vector3(0, 0, 0), 0);
         _frite = go;
      }
      else if (type == ElementType.RedHorizontal)
      {
         GameObject go = Instantiate(_redHorizontal[rdn], transform);
         go.transform.DOScale(15, 0);
         go.transform.DORotate(new Vector3(0, 0, 90), 0);
         _frite = go;
      }
      else if (type == ElementType.RedVertical)
      {
         GameObject go = Instantiate(_redVertical[rdn], transform);
         go.transform.DOScale(15, 0);
         go.transform.DORotate(new Vector3(0, 0, 0), 0);
         _frite = go;
      }

      if (_type is ElementType.RedHorizontal or ElementType.YellowHorizontal)
      {
         if(!isEditor)
            _frite.transform.DORotate(new Vector3(-360, 0, 90), _rotateSpeed, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
         else
            _frite.transform.DORotate(new Vector3(0, 0, 90), 0);
         
         _colliders[0].enabled = true;
         _colliders[1].enabled = false;
      }

      if (_type is ElementType.YellowVertical or ElementType.RedVertical)
      {
         if(!isEditor)
            _frite.transform.DORotate(new Vector3(0, 360, 0), _rotateSpeed, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
         else
            _frite.transform.DORotate(new Vector3(0, 0, 0), 0);
         
         _colliders[0].enabled = false;
         _colliders[1].enabled = true;
      }
   }
}
