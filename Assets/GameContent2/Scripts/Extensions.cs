using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LD_Extensions
{
    public static IEnumerator SetInactiveDelay(this GameObject _go, float _time)
    {
        yield return new WaitForSeconds(_time);
        _go.SetActive(false);
    }
}
