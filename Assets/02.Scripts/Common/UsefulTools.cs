using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UsefulTools : MonoBehaviour
{
    public static UsefulTools instance = null;

    [HideInInspector]
    public delegate void Callback_Type1();
    private Callback_Type1 callbackFunc1;

    [HideInInspector]
    public delegate void Callback_Type2(int num);
    private Callback_Type2 callbackFunc2;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }


    public void ActionAfterCountdown(Callback_Type1 func, float time)
    {
        StartCoroutine(Action(func, time));
    }
    private IEnumerator Action(Callback_Type1 func, float time)
    {
        yield return new WaitForSeconds(time);
        callbackFunc1 += func;
        callbackFunc1();
        callbackFunc1 -= func;
        StopCoroutine(Action(func, time));
    }

    public void ActionAfterCountdown(Callback_Type2 func, int param, float time)
    {
        StartCoroutine(Action(func, param, time));
    }
    private IEnumerator Action(Callback_Type2 func, int param, float time)
    {
        yield return new WaitForSeconds(time);
        callbackFunc2 += func;
        callbackFunc2(param);
        callbackFunc2 -= func;
        StopCoroutine(Action(func, param, time));
    }

    public void CancleAction(Callback_Type1 func)
    {
        if(callbackFunc1 != null)
            callbackFunc1 -= func;
    }
    public void CancleAction(Callback_Type2 func)
    {
        if(callbackFunc2 != null)
            callbackFunc2 -= func;
    }

    public int GetCurrentSceneID()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
    public void ChangeScene(int SceneID)
    {
        SceneManager.LoadScene(SceneID);
    }
}