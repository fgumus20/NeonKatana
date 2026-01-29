using UnityEngine;
using DG.Tweening;

public class SimpleEnemy : MonoBehaviour
{
    public void Die()
    {
        GetComponent<Collider>().enabled = false;

        transform.DOScale(Vector3.zero, 0.2f).OnComplete(() =>
        {
            Destroy(gameObject);
        });

        Debug.Log("Enemy died: " + gameObject.name);
    }
}