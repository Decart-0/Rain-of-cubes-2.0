using System.Collections;
using UnityEngine;

public class Bomb : PoolableObject<Bomb>, ISpawner<Bomb>
{
    public override void Init()
    {
        base.Init();
        StartCoroutine(DecreaseLifeTime());
    }

    protected override IEnumerator DecreaseLifeTime()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(1f);
        float initialLifeTime = LifeTime;

        while (LifeTime > 0)
        {
            Color newColor = Renderer.material.color;
            newColor.a = Mathf.Lerp(1, 0, 1 - (LifeTime / initialLifeTime));
            Renderer.material.color = newColor;
            LifeTime--;

            yield return waitForSeconds;
        }

        LifeTimeExpired();
    }
}