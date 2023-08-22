using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CircleSpawner : MonoBehaviour
{
    public GameObject circlePrefab;
    public int minCircles = 5;
    public int maxCircles = 10;
    public float spawnRange = 5f;

    public LineRenderer lineRenderer;
    public float lineWidth = 0.1f;

    public Button restartButton;

    private Vector3 lineStartPos;
    private bool drawingLine = false;

    private void Start()
    {
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        restartButton.onClick.AddListener(RestartScene);

        SpawnCircles();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            drawingLine = true;
            lineStartPos = GetMouseWorldPosition();
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, lineStartPos);
            lineRenderer.SetPosition(1, lineStartPos);
        }

        if (drawingLine)
        {
            Vector3 lineEndPos = GetMouseWorldPosition();
            lineRenderer.SetPosition(1, lineEndPos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            drawingLine = false;
            lineRenderer.positionCount = 0;
            Vector3 lineEndPos = GetMouseWorldPosition();
            CheckAndRemoveCirclesInLine(lineStartPos, lineEndPos);
        }
    }

    private void SpawnCircles()
    {
        int circleCount = Random.Range(minCircles, maxCircles + 1);

        for (int i = 0; i < circleCount; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            Instantiate(circlePrefab, spawnPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(-spawnRange, spawnRange);
        float randomY = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);
        return spawnPosition;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void CheckAndRemoveCirclesInLine(Vector3 startPos, Vector3 endPos)
    {
        Collider2D[] collidersInLine = Physics2D.OverlapAreaAll(startPos, endPos);

        foreach (Collider2D collider in collidersInLine)
        {
            if (collider.CompareTag("Circle"))
            {
                Destroy(collider.gameObject);
            }
        }
    }
    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}