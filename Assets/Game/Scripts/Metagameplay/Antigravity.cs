using System.Collections.Generic;
using Infrastructure;
using UnityEngine;
using Zenject;

public class Antigravity : MonoBehaviour
{
    [Inject] private IPlayerInput _playerInput;
    [SerializeField] List<ObjectOfGravity> _antigravityList = new List<ObjectOfGravity>();
    [SerializeField] public bool IsAntigravity;// { private set; get; }
    public GameObject _SphereGravty;
    public bool isStick;
    public void Initialize()
    {
        _SphereGravty = Instantiate(_SphereGravty);
        _SphereGravty.SetActive(false);
    }

    private void Update()
    {
        if(_playerInput.RPressed()) SetGravity();
    }

    public void AddObject(ObjectOfGravity objectOfGravity)
    {
        _antigravityList.Add(objectOfGravity);
    }

    public bool IsExists(ObjectOfGravity objectOfGravity)
    {
        return _antigravityList.Contains(objectOfGravity);
    }

    public void SetGravity()
    {
        if (GameManager.instatnce.Player.GetComponent<MoveAndRotation>().IsGround | isStick) {

            IsAntigravity = !IsAntigravity;
            GameManager.instatnce.Player.GetComponent<ObjectOfGravity>().VerticalPosition = 1;//todo ����������� ������������ ������� ������ � ������. �������� �������� �������� ������� ������� � �����������

            _SphereGravty.SetActive(IsAntigravity);
            _SphereGravty.transform.position = GameManager.instatnce.Player.transform.position;
            SetColidersInSphere(IsAntigravity);

            foreach (ObjectOfGravity item in _antigravityList)
            {
                item.SetGravity(IsAntigravity);
                if (IsAntigravity == false) item.InSphere = false;
            }
        }
    }

    public void SetColidersInSphere(bool isActive)
    {
        Collider[] coliders = Physics.OverlapSphere(_SphereGravty.transform.position, _SphereGravty.transform.lossyScale.x/2);
        foreach (Collider collider in coliders)
        {
            if (collider.TryGetComponent(out ObjectOfGravity objectOfGravity))
            {
                objectOfGravity.InSphere = isActive;

                print($"{objectOfGravity.name} ---> {isActive} ");
            }
        }
    }
}
