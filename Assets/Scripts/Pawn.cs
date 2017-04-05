using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece {

    public Pawn()
    {

    }

    public Pawn(bool all, int x, int y): base(all, x, y)
    {

    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //(a)
    //The pawn may move only forward[except as limited by Article 4.2].
    //(b)
    //Except when making a capture, it advances from its original square either one or two vacant squares along the file on which it is placed, 
    //and on subsequent moves it advances one vacant square along the file.When capturing, it advances one square along either of the diagonals 
    //on which it stands.
    //(c)
    //A pawn, attacking a square crossed by an opponent's pawn which has [just] been advanced two squares in one move from its original square, 
    //may capture this opponent's pawn as though the latter had been moved only one square.This capture may only be made in [immediate]
    //reply to such an advance, and is called an "en passant" capture.
    override public bool findValidSpaces()
    {
        bool flag = false;
        if(allegiance)
        {
            if(loc[1] == 1 && gameBoard.pieceAtSpace(loc[0], 3, true) == 0)
            {
                Debug.Log("Pawn can move to: (" + loc[0] + ",3)");
                flag = true;
                gameBoard.highlightSquare(loc[0], 3);
            }
            if(gameBoard.pieceAtSpace(loc[0], loc[1] + 1, true) == 0)
            {
                Debug.Log("Pawn can move to: (" + loc[0] + "," + (loc[1]+1) + ")");
                flag = true;
                gameBoard.highlightSquare(loc[0], loc[1]+1);
            }
            if (gameBoard.pieceAtSpace(loc[0] - 1, loc[1] + 1, true) == 1)
            {
                Debug.Log("Pawn can move to: (" + (loc[0] - 1) + "," + (loc[1] + 1) + ")");
                flag = true;
                gameBoard.highlightSquare(loc[0] - 1, loc[1] + 1);
            }
            if (gameBoard.pieceAtSpace(loc[0] + 1, loc[1] + 1, true) == 1)
            {
                Debug.Log("Pawn can move to: (" + (loc[0] + 1) + "," + (loc[1] + 1) + ")");
                flag = true;
                gameBoard.highlightSquare(loc[0] + 1, loc[1] + 1);
            }
        }
        else
        {
            if (loc[1] == 6 && gameBoard.pieceAtSpace(loc[0], 4, false) == 0)
            {
                Debug.Log("Pawn can move to: (" + loc[0] + ",4)");
                flag = true;
                gameBoard.highlightSquare(loc[0], 3);
            }
            if (gameBoard.pieceAtSpace(loc[0], loc[1] - 1, false) == 0)
            {
                Debug.Log("Pawn can move to: (" + loc[0] + "," + (loc[1] - 1) + ")");
                flag = true;
                gameBoard.highlightSquare(loc[0], loc[1] - 1);
            }
            if (gameBoard.pieceAtSpace(loc[0] - 1, loc[1] - 1, false) == 1)
            {
                Debug.Log("Pawn can move to: (" + (loc[0] - 1) + "," + (loc[1] - 1) + ")");
                flag = true;
                gameBoard.highlightSquare(loc[0] - 1, loc[1] - 1);
            }
            if (gameBoard.pieceAtSpace(loc[0] + 1, loc[1] - 1, false) == 1)
            {
                Debug.Log("Pawn can move to: (" + (loc[0] + 1) + "," + (loc[1] - 1) + ")");
                flag = true;
                gameBoard.highlightSquare(loc[0] + 1, loc[1] - 1);
            }
        }
        return flag;
    }
}
