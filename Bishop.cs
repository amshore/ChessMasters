using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece {

    public Bishop()
    {

    }

    public Bishop(bool all, int x, int y): base(all, x, y)
    {

    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //The bishop moves to any square (except as limited by Article 4.2) on the diagonals on which it stands.
    public override bool findValidSpaces()
    {
        bool flag = false;
        for(int i = 1; i <= Mathf.Min(7 - loc[0], 7 - loc[1]) && gameBoard.pieceAtSpace(loc[0] + i, loc[1] + i, allegiance) != 2; i++)
        {
            Debug.Log("Bishop can move to: (" + (loc[0] + i) + "," + (loc[1] + i) + ")");
            flag = true;
            gameBoard.highlightSquare(loc[0] + i, loc[1] + i);
        }
        for (int i = 1; i <= Mathf.Min(loc[0], 7 - loc[1]) && gameBoard.pieceAtSpace(loc[0] - i, loc[1] + i, allegiance) != 2; i++)
        {
            Debug.Log("Bishop can move to: (" + (loc[0] - i) + "," + (loc[1] + i) + ")");
            flag = true;
            gameBoard.highlightSquare(loc[0] - i, loc[1] + i);
        }
        for (int i = 1; i <= Mathf.Min(loc[0], loc[1]) && gameBoard.pieceAtSpace(loc[0] - i, loc[1] - i, allegiance) != 2; i++)
        {
            Debug.Log("Bishop can move to: (" + (loc[0] - i) + "," + (loc[1] - i) + ")");
            flag = true;
            gameBoard.highlightSquare(loc[0] - i, loc[1] - i);
        }
        for (int i = 1; i <= Mathf.Min(7 - loc[0], loc[1]) && gameBoard.pieceAtSpace(loc[0] + i, loc[1] - i, allegiance) != 2; i++)
        {
            Debug.Log("Bishop can move to: (" + (loc[0] + i) + "," + (loc[1] - i) + ")");
            flag = true;
            gameBoard.highlightSquare(loc[0] + i, loc[1] - i);
        }
        return flag;
    }
}
