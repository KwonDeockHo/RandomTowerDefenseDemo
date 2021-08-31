using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Actor : MonoBehaviour
{
    public abstract void LOCKONTARGET();
    public abstract void ATTACK();
    public abstract void UPDATETARGET();

}
