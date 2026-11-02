using UnityEngine;

public class BossActivator : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject arenaWalls;

    private bool bossActivated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!bossActivated && other.CompareTag("Player"))
        {
            bossActivated = true;

            if (boss != null) boss.SetActive(true);
            if (arenaWalls != null) arenaWalls.SetActive(true);

        }
    }
}
