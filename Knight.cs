using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece {

    public Knight()
    {

    }

    public Knight(bool all, int x, int y): base(all, x, y)
    {

    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //The knight's move is composed of two different steps; first, it makes one step of one single square along its rank or file, and then, 
    //still moving away from the square of departure, one step of one single square on a diagonal. It does not matter if the square of the 
    //first step is occupied.
    public override bool findValidSpaces()
    {
        bool flag = false;
        bool[] whichEdges = { true, true, true, true, true, true, true, true };
        if(loc[0] < 2)
        {
            if(loc[0] == 0)
            {
                whichEdges[4] = false;
                whichEdges[7] = false;
            }
            whichEdges[5] = false;
            whichEdges[6] = false;
        }
        if (loc[0] > 5)
        {
            if (loc[0] == 7)
            {
                whichEdges[0] = false;
                whichEdges[3] = false;
            }
            whichEdges[1] = false;
            whichEdges[2] = false;
        }
        if (loc[1] < 2)
        {
            if (loc[1] == 0)
            {
                whichEdges[2] = false;
                whichEdges[5] = false;
            }
            whichEdges[3] = false;
            whichEdges[4] = false;
        }
        if (loc[1] > 5)
        {
            if (loc[1] == 7)
            {
                whichEdges[6] = false;
                whichEdges[1] = false;
            }
            whichEdges[7] = false;
            whichEdges[0] = false;
        }
        if (whichEdges[0] && gameBoard.pieceAtSpace(loc[0]+1, loc[1] + 2, allegiance) != 2)
        {
            Debug.Log("Knight can move to: (" + (loc[0]+1) + "," + (loc[1] + 2) + ")");
            flag = true;
            gameBoard.highlightSquare(loc[0]+1, loc[1] + 2);
        }
        if (whichEdges[1] && gameBoard.pieceAtSpace(loc[0] + 2, loc[1] + 1, allegiance) != 2)
        {
            Debug.Log("Knight can move to: (" + (loc[0] + 2) + "," + (loc[1] + 1) + ")");
            flag = true;
            gameBoard.highlightSquare(loc[0] + 2, loc[1] + 1);
        }
        if (whichEdges[2] && gameBoard.pieceAtSpace(loc[0] + 2, loc[1] - 1, allegiance) != 2)
        {
            Debug.Log("Knight can move to: (" + (loc[0] + 2) + "," + (loc[1] - 1) + ")");
            flag = true;
            gameBoard.highlightSquare(loc[0] + 2, loc[1] - 1);
        }
        if (whichEdges[3] && gameBoard.pieceAtSpace(loc[0] + 1, loc[1] - 2, allegiance) != 2)
        {
            Debug.Log("Knight can move to: (" + (loc[0] + 1) + "," + (loc[1] - 2) + ")");
            flag = true;
            gameBoard.highlightSquare(loc[0] + 1, loc[1] - 2);
        }
        if (whichEdges[4] && gameBoard.pieceAtSpace(loc[0] - 1, loc[1] - 2, allegiance) != 2)
        {
            Debug.Log("Knight can move to: (" + (loc[0] - 1) + "," + (loc[1] - 2) + ")");
            flag = true;
            gameBoard.highlightSquare(loc[0] - 1, loc[1] - 2);
        }
        if (whichEdges[5] && gameBoard.pieceAtSpace(loc[0] - 2, loc[1] - 1, allegiance) != 2)
        {
            Debug.Log("Knight can move to: (" + (loc[0] - 2) + "," + (loc[1] - 1) + ")");
            flag = true;
            gameBoard.highlightSquare(loc[0] - 2, loc[1] - 1);
        }
        if (whichEdges[6] && gameBoard.pieceAtSpace(loc[0] - 2, loc[1] + 1, allegiance) != 2)
        {
            Debug.Log("Knight can move to: (" + (loc[0] - 2) + "," + (loc[1] + 1) + ")");
            flag = true;
            gameBoard.highlightSquare(loc[0] - 2, loc[1] + 1);
        }
        if (whichEdges[7] && gameBoard.pieceAtSpace(loc[0] - 1, loc[1] + 2, allegiance) != 2)
        {
            Debug.Log("Knight can move to: (" + (loc[0] - 1) + "," + (loc[1] + 2) + ")");
            flag = true;
            gameBoard.highlightSquare(loc[0] - 1, loc[1] + 2);
        }
        return flag;
    }
}
