using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BulletsController : MonoBehaviour
{
    public GameObject particle;
    public int bulletsAmount;
    public int baseDelay = 4;
    public int delta;

    private void Start()
    {
        StartCoroutine(DelayRun());
    }

    public IEnumerator DelayRun()
    {
        var rand = 0;

        while (!gameObject.IsDestroyed())
        {
            yield return new WaitForSeconds(baseDelay - delta * bulletsAmount);

            if (bulletsAmount <= 0) continue;

            var side = (rand % 4) switch
            {
                0 => -transform.up,
                1 => transform.up,
                2 => transform.right,
                3 => -transform.right,
                _ => new Vector3()
            };

            var obj = Instantiate(particle, gameObject.transform.position, Quaternion.identity);

            obj.GetComponent<Rigidbody2D>().AddForce(side * 300f);
            rand++;
        }
    }
}