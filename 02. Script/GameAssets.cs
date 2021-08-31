
using UnityEngine;
using System.Reflection;

public class GameAssets : MonoBehaviour
{
    public Transform _damagePopup;

    public static GameAssets _self;

    public static GameAssets self
    {
        get
        {
            if (_self == null)
                _self = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return _self;
        }
    }


}
