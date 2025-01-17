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
    public Transform snakeHead;
    
    public List<Transform> currentFood;

    private List<Transform> snakeBody;
    private Vector2 direction = Vector2.right;
    private bool isGameOver = false;

    void Awake()
    {
        snakeBody = new List<Transform>();
        if (snakeHead != null)
        {
            snakeHead.GetComponentInChildren<SnakeBody>().SetWord("我");
            snakeBody.Add(snakeHead);
            Debug.Log("Snake head is not null!");
        }
        else
        {
            Debug.LogError("Snake head is null!");
        }

    }
    void Start()
    {
        Vector2 spriteSize = snakeRenderer.sprite.bounds.size; //图片大小
        gridSize = spriteSize.x;

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

            for (int i = 1; i < snakeBody.Count; i++) // 蛇身移动
            {
                Vector2 temp = snakeBody[i].position;
                snakeBody[i].position = prevPosition;
                prevPosition = temp;
            }
            // Check for collision with words
            for (int i = 0; i < currentFood.Count; i++)
            {
                if (Vector2.Distance(snakeBody[0].position, currentFood[i].position) < gridSize)
                {
                    GrowSnake(currentFood[i].GetComponent<WordAction>().word);
                    //广播吃到食物的消息
                }
            }
            // Check for collision with walls or itself
            if (CheckCollision())
            {
                isGameOver = true;
                Debug.Log("Game Over!");
            }
        }
    }

    

    void GrowSnake(string w) // 增加蛇身
    {
        SnakeBody newSegment = Instantiate(snakePrefab, snakeBody[snakeBody.Count - 1].position, Quaternion.identity).GetComponent<SnakeBody>();
        newSegment.word = w;// 增加的蛇身显示的文字
        snakeBody.Add(newSegment.transform);
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
                return true;
        }
        return false;
    }
}