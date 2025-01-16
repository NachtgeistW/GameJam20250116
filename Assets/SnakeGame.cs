using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SnakeGame : MonoBehaviour
{
    public GameObject snakePrefab; 
    //public float gridSize = 1.0f; //单元格大小 使用图片的尺寸
    public SpriteRenderer snakeRenderer;
    public float gridSize; //图片大小
    public float moveInterval = 0.5f;
    private List<Transform> snakeBody = new List<Transform>();
    private Vector2 direction = Vector2.right;
    private bool isGameOver = false;

    void Start()
    {
        Vector2 spriteSize = snakeRenderer.sprite.bounds.size; //图片大小
        gridSize = spriteSize.x;
        // Initialize snake
        snakeBody.Add(Instantiate(snakePrefab, Vector2.zero, Quaternion.identity).transform);
        
        StartCoroutine(MoveSnake());
    }

    void Update()
    {
        if (isGameOver) return;

        // Change direction based on input
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down)
            direction = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up)
            direction = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right)
            direction = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left)
            direction = Vector2.right;

    }

    IEnumerator MoveSnake()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(moveInterval);

            // Move snake body
            Vector2 prevPosition = snakeBody[0].position;
            snakeBody[0].position += (Vector3)direction * gridSize; // 移动距离为单元格大小

            for (int i = 1; i < snakeBody.Count; i++)
            {
                Vector2 temp = snakeBody[i].position;
                snakeBody[i].position = prevPosition;
                prevPosition = temp;
            }

            // Check for collision with food
            
            // Check for collision with walls or itself
            if (CheckCollision())
            {
                isGameOver = true;
                Debug.Log("Game Over!");
            }
        }
    }

    

    void GrowSnake() // 增加蛇身
    {
        Transform newSegment = Instantiate(snakePrefab, snakeBody[snakeBody.Count - 1].position, Quaternion.identity).transform;
        snakeBody.Add(newSegment);
    }

    bool CheckCollision()
    {
        // Check collision with walls
        //if (Mathf.Abs(snakeBody[0].position.x) > 10 || Mathf.Abs(snakeBody[0].position.y) > 10)
            //return true;

        // Check collision with itself
        for (int i = 1; i < snakeBody.Count; i++)
        {
            if (snakeBody[0].position == snakeBody[i].position)
                return false;
        }
        return false;
    }
}