using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Piece : MonoBehaviour {
    bool notClicked = true;
    protected Board gameBoard;
    public bool allegiance { get; set; }
    protected int[] loc = { 0, 0 };

    public Piece()
    {
        allegiance = true;
    }

    public Piece(bool all, int x, int y)
    {
        allegiance = all;
        loc[0] = x;
        loc[1] = y;
    }
	// Use this for initialization
	void Start () {
        gameBoard = Board.Instance;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            notClicked = !notClicked;
            Debug.Log("User clicked piece");
        }
	}

    public void setLoc(int xn, int yn)
    {
        loc[0] = xn;
        loc[1] = yn;
    }

    void OnMouseEnter()
    {
        findValidSpaces();
        GetComponent<Renderer>().material.shader = Shader.Find("Outlined/Silhouetted Diffuse");
    }

    void OnMouseExit()
    {
        if (notClicked)
        {
            GetComponent<Renderer>().material.shader = Shader.Find("Standard");
            gameBoard.unhighlight();
        }
        else
        {
            gameBoard.currentPiece = this;
        }
    }

    virtual public bool findValidSpaces()
    {
        Debug.Log("Error: findValidSpaces not implemented");
        return false;
    }
}
