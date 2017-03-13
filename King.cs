using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece {

    public King()
    {

    }

    public King(bool all, int x, int y): base(all, x, y)
    {

    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    //(a)
    //Except when castling, the king moves to any adjoining square that is not attacked by an opponent's piece.
    //(b)
    //Castling is a move of the king and either rook, counting as a single move of the king and executed as follows: the king is transferred from 
    //its original square two squares toward either rook on the same rank; then that rook is transferred over the king to the square the king has just crossed.
    //(e)
    //Castling is [permanently]
    //illegal:
    //(i)
    //if the king has already been moved; or
    //(ii)
    //with a rook that has already been moved.
    //(f)
    //Castling is prevented for the time being:
    //(i)
    //if the king's original square, or the square which the king must pass over, or that which it is to occupy, is attacked by an opponent's piece; or
    //(ii)
    //if there is any piece between the king and the rook with which castling is to be effected[i.e.castling may still be legal even if the rook is attacked or, 
    //when castling queenside, passes over an attacked square] .
    public override bool findValidSpaces()
    {
        bool flag = false;
        bool[] whichEdges = { true, true, true, true, true, true, true, true };
        if(loc[0] == 0)
        {
            whichEdges[5] = false;
            whichEdges[6] = false;
            whichEdges[7] = false;
        }
        if(loc[0] == 7)
        {
            whichEdges[1] = false;
            whichEdges[2] = false;
            whichEdges[3] = false;
        }
        if(loc[1] == 0)
        {
            whichEdges[3] = false;
            whichEdges[4] = false;
            whichEdges[5] = false;
        }
        if(loc[1] == 7)
        {
            whichEdges[7] = false;
            whichEdges[0] = false;
            whichEdges[1] = false;
        }
        if (whichEdges[0] && gameBoard.pieceAtSpace(loc[0], loc[1]+1, allegiance) != 2)
        {
            Debug.Log("King can move to: (" + loc[0] + ","+(loc[1]+1) + ")");
            flag = true;
            gameBoard.highlightSquare(loc[0], loc[1]+1);
        }
        if (whichEdges[1] && gameBoard.pieceAtSpace(loc[0]+1, loc[1]+1, allegiance) != 2)
        {
            Debug.Log("King can move to: (" + (loc[0]+1) + "," + (loc[1] + 1) + ")");
            flag = true;
            gameBoard.highlightSquare(loc[0]+1, loc[1]+1);
        }
        if (whichEdges[2] && gameBoard.pieceAtSpace(loc[0]+1, loc[1], allegiance) != 2)
        {
            Debug.Log("King can move to: (" + (loc[0]+1) + "," + (loc[1]) + ")");
            flag = true;
            gameBoard.highlightSquare(loc[0]+1, loc[1]);
        }
        if (whichEdges[3] && gameBoard.pieceAtSpace(loc[0]+1, loc[1]-1, allegiance) != 2)
        {
            Debug.Log("King can move to: (" + (loc[0]+1) + "," + (loc[1] - 1) + ")");
            flag = true;
            gameBoard.highlightSquare(loc[0]+1, loc[1]-1);
        }
        if (whichEdges[4] && gameBoard.pieceAtSpace(loc[0], loc[1]-1, allegiance) != 2)
        {
            Debug.Log("King can move to: (" + loc[0] + "," + (loc[1] - 1) + ")");
            flag = true;
            gameBoard.highlightSquare(loc[0], loc[1]-1);
        }
        if (whichEdges[5] && gameBoard.pieceAtSpace(loc[0]-1, loc[1]-1, allegiance) != 2)
        {
            Debug.Log("King can move to: (" + (loc[0]-1) + "," + (loc[1] - 1) + ")");
            flag = true;
            gameBoard.highlightSquare(loc[0]-1, loc[1]-1);
        }
        if (whichEdges[6] && gameBoard.pieceAtSpace(loc[0]-1, loc[1], allegiance) != 2)
        {
            Debug.Log("King can move to: (" + (loc[0]-1) + "," + (loc[1]) + ")");
            flag = true;
            gameBoard.highlightSquare(loc[0]-1, loc[1]);
        }
        if (whichEdges[7] && gameBoard.pieceAtSpace(loc[0]-1, loc[1]+1, allegiance) != 2)
        {
            Debug.Log("King can move to: (" + (loc[0]-1) + "," + (loc[1] + 1) + ")");
            flag = true;
            gameBoard.highlightSquare(loc[0]-1, loc[1]+1);
        }
        return flag;
    }
    
}
