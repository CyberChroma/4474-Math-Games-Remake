using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Roly_Movement : MonoBehaviour
{

    public Transform startPos;
    public Sprite [] playerSprites;

    public bool canMove = false, checkCollision = false;
    float movementDistance = 1.177381f;

    public Vector2 moveDirection = new Vector2(-1,-1);

    private Vector2 target;

    public float moveSpeed;

    public Transform x, y;

    public Button runBtn;
    public DragDropManager manager;

    public LayerMask mask;
    public ContactFilter2D filter;

    public GameObject fallReminder;
    public GameObject wormReminder;

    // Start is called before the first frame update
    void Start()
    {
      //  Debug.Log(Vector2.Distance(x.position,y.position));
    } // end start

    // Update is called once per frame
    void Update()
    {
        if(canMove){ // can only update when allowed to move
       //     runBtn.interactable = false;
       //     manager.nextCommand = false;

            transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed);
            
            if(Vector2.MoveTowards(transform.position, target, moveSpeed) == new Vector2(transform.position.x,transform.position.y)){ // if player no longer needs to move
                canMove = false;
                CheckColllide();

     //         runBtn.interactable = true;
     //         manager.nextCommand = true;
            } // end if

        } // end if
    } // end update

    public void SetMove(bool _canMove){
        canMove = _canMove;
    } // end setter method

    public void Forward(){
        target = new Vector2(transform.position.x, transform.position.y) + (moveDirection.normalized * movementDistance); // move player certain amopunt in distance specified
        canMove = true;
    } // end method
    public void TurnClockwise(){   
    //    runBtn.interactable = false;     
    //    manager.nextCommand = false;

        if(moveDirection.x == moveDirection.y)
            moveDirection = new Vector2(moveDirection.x, -moveDirection.y);
        else
            moveDirection = new Vector2(-moveDirection.x, moveDirection.y);
     //   runBtn.interactable = true;
      //  manager.nextCommand = true;

        changeSprite();
        CheckColllide();
    } // end method

    public void TurnCounterClockwise(){
   //     runBtn.interactable = false;
  //      manager.nextCommand = false;
        if(moveDirection.x == moveDirection.y)
            moveDirection = new Vector2(-moveDirection.x, moveDirection.y);
        else
            moveDirection = new Vector2(moveDirection.x, -moveDirection.y);
    //    runBtn.interactable = true;
   //     manager.nextCommand = true;

        changeSprite();
        CheckColllide();
    } // end method

    public void CheckColllide(){
        List<Collider2D> results = new List<Collider2D>();
        Physics2D.OverlapCircle(transform.position, 0.05f, filter, results);
        if(results.Count == 0){
            Debug.Log("n");
            transform.position = startPos.position;
            moveDirection = new Vector2(-1, -1);
            changeSprite();
            GameObject.Find("DragDropManager").GetComponent<DragDropManager>().canContinue = false;
            fallReminder.SetActive(true);
            return;
        } // end if
        for(int i = 0; i < results.Count; i++){
                switch(results[i].gameObject.GetComponent<Roly_Tile>().tileType){
                    case 'g':
                        Debug.Log("g");
                        break;
                    case 'a':
                        Debug.Log("a");
                        GameObject.Find("--- Next Level UI ---").transform.GetChild(0).gameObject.SetActive(true);
                        GameObject.Find("--- Game ---").SetActive(false);
                        break;
                    case 'w':
                        Debug.Log("w");
                        transform.position = startPos.position;
                        moveDirection = new Vector2(-1, -1);
                        changeSprite();
                        wormReminder.SetActive(true);

                        break;
                } // end switch
        } // end

    } // end method

    public void changeSprite(){
            if(new Vector2(-1, -1) == moveDirection)
                gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = playerSprites[0];
            if(new Vector2(-1, 1) == moveDirection)
                gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = playerSprites[1];
            if(new Vector2(1, 1) == moveDirection)
                gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = playerSprites[2];
            if(new Vector2(1, -1) == moveDirection)
                gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = playerSprites[3];
    } // end method

    private void OnTriggerStay2D(Collider2D other){
        if(checkCollision){
                            Debug.Log("Checkemout");

            if(other.gameObject.layer == 6){
                switch(other.gameObject.GetComponent<Roly_Tile>().tileType){
                    case 'g':
                        Debug.Log("g");
                        break;
                    case 'a':
                        Debug.Log("a");
                        break;
                    case 'w':
                        Debug.Log("w");
                        break;
                } // end switch
                checkCollision = false;
            } // end if
            else{
                Debug.Log("null");
                checkCollision = false;
            } // end if
        } // end if
    } // end method



} // end class