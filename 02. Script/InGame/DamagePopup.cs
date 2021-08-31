using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeMonkey.Utils;

public class DamagePopup : MonoBehaviour
{
    public static DamagePopup Create(Vector3 position, float damageAmount, bool isCriticalHit)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.self._damagePopup, position, Quaternion.identity);

        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.setup(damageAmount, isCriticalHit);

        return damagePopup;
    }

    //public static void Create(Vector3 position, float damageAmount, bool isCriticalHit)
    //{
    //    Debug.Log("Transform : " + _damagePopup_);
    //    //Debug.Log("GameObject : " + GameAssets.self._damagePopup.gameObject);
    //    //Debug.Log("_damagePopup : " + GameAssets.self._damagePopup);
    //    //Transform damagePopupTransform = Instantiate(GameAssets.self._damagePopup, position, Quaternion.identity);

    //    ////DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
    //    ////damagePopup.setup(damageAmount, isCriticalHit);

    //    //DamagePopup damagePopup = _damagePopup_.GetComponent<DamagePopup>();
    //    //damagePopup.setup(damageAmount, isCriticalHit);
    //    //return damagePopup;
    //}


    private static int sortingOrder;
    
    private const float DISAPPEAR_TIMER_MAX = 1f;
    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private Vector3 moveVector;
    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void setup(float damageAmount, bool isCriticalHit)
    {
        textMesh.SetText(damageAmount.ToString());

        if (!isCriticalHit)
        {
            //Normal Hit
            textMesh.fontSize = 2;
            textColor = UtilsClass.GetColorFromString("FFC500");
           // Debug.Log("Normal textColor: " + textColor);
        }
        else
        {
            //CriticalHit
            textMesh.fontSize = 3;
            textColor = UtilsClass.GetColorFromString("FF2B00");
           // Debug.Log("Critical textColor: " + textColor);
        }
        textMesh.color = textColor;
        disappearTimer = DISAPPEAR_TIMER_MAX;
        
        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;

        moveVector = new Vector3(.7f, 1) * 2f;
    }
    // Start is called before the first frame update

    private void Update()
    {
        //float moveYSpeed = 20f;
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 2f * Time.deltaTime;


        if (disappearTimer > DISAPPEAR_TIMER_MAX * 1f)
        {
            //First half of the popup lifetime
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            float increaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        
        disappearTimer -= Time.deltaTime;



        if(disappearTimer < 0)
        {
            float disappearSpeed = 3f;
            //start disappearing
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
