using UnityEngine;

public class EnemyPointer : MonoBehaviour
{
    public string enemyTag = "Enemy";
    public Transform player;
    public Renderer arrowRenderer;   // Assign the arrow’s material/mesh renderer
    public Color farColor = Color.green;
    public Color closeColor = Color.red;
    public Color noEnemyColor = Color.gray;

    private Transform closestEnemy;

    private void Update()
    {
        FindClosestEnemy();

        if (closestEnemy != null)
        {
            Vector3 dir = closestEnemy.position - player.position;
            dir.y = 0f;

            if (dir != Vector3.zero)
            {
                Quaternion targetRot = Quaternion.LookRotation(-dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 10f);
            }

            UpdateArrowColor();
        }
        else
        {
            // No enemy found → turn gray
            if (arrowRenderer != null)
                arrowRenderer.material.color = noEnemyColor;
        }
    }

    private void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float minDist = Mathf.Infinity;
        Transform nearest = null;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(player.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = enemy.transform;
            }
        }

        closestEnemy = nearest;
    }

    private void UpdateArrowColor()
    {
        if (arrowRenderer == null) return;

        float dist = Vector3.Distance(player.position, closestEnemy.position);

        float t = Mathf.InverseLerp(0f, 50f, dist);
        Color currentColor = Color.Lerp(closeColor, farColor, t);

        arrowRenderer.material.color = currentColor;
    }
}
