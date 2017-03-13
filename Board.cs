//Most of this game logic is much more complicated than I had predicted.
//I have the basic logic of the game, but I am working on studying chessprogramming.wikispaces.com to learn more about algorithms to work with chess

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : Singleton<Board> {
    bool gameActive;
    bool turn = true;
    bool piecesUpdated = false;
    Piece[,] boardPieces;
    public Piece currentPiece;
    public int[,] attackField;

	// Use this for initialization
	void Start () {
        boardPieces = new Piece[8, 8];
        attackField = new int[8, 8];
        setupBoard();
        setupAttackField();	
	}
	
	// Update is called once per frame
	void Update () {
		if(piecesUpdated)
        {
            turn = !turn;
            piecesUpdated = false;
            setupAttackField();
            gameActive = isCheckmate();
        }
        //During Milestone 2, there will be tiles once we integrate the graphics with this code
        //I will use a similar detection of click as for the Piece class
        //When I detect a click, if the tile clicked is highlighted, I will move currentPiece to the new location
        //Next, I will remove any piece currently on that tile from the game
        //I will then update the location of the piece
        //Finally, I will make piecesUpdated true
	}

    //First all the pieces are created (Milestone 1)
    //Then all of the piece game objects are instantiated (Milestone 2)
    void setupBoard()
    {
        boardPieces[0,0] = new Rook(true, 0,0);
        boardPieces[0, 1] = new Knight(true,0,1);
        boardPieces[0, 2] = new Bishop(true,0,2);
        boardPieces[0, 3] = new Queen(true,0,3);
        boardPieces[0, 4] = new King(true,0,4);
        boardPieces[0, 5] = new Bishop(true,0,5);
        boardPieces[0, 6] = new Knight(true,0,6);
        boardPieces[0, 7] = new Rook(true,0,7);
        boardPieces[1, 0] = new Pawn(true,1,0);
        boardPieces[1, 1] = new Pawn(true,1,1);
        boardPieces[1, 2] = new Pawn(true,1,2);
        boardPieces[1, 3] = new Pawn(true,1,3);
        boardPieces[1, 4] = new Pawn(true,1,4);
        boardPieces[1, 5] = new Pawn(true,1,5);
        boardPieces[1, 6] = new Pawn(true,1,6);
        boardPieces[1, 7] = new Pawn(true,1,7);
        boardPieces[6, 0] = new Pawn(false,6,0);
        boardPieces[6, 1] = new Pawn(false,6,1);
        boardPieces[6, 2] = new Pawn(false,6,2);
        boardPieces[6, 3] = new Pawn(false,6,3);
        boardPieces[6, 4] = new Pawn(false,6,4);
        boardPieces[6, 5] = new Pawn(false,6,5);
        boardPieces[6, 6] = new Pawn(false,6,6);
        boardPieces[6, 7] = new Pawn(false,6,7);
        boardPieces[7, 0] = new Rook(false,7,0);
        boardPieces[7, 1] = new Knight(false,7,1);
        boardPieces[7, 2] = new Bishop(false,7,2);
        boardPieces[7, 3] = new Queen(false,7,3);
        boardPieces[7, 4] = new King(false,7,4);
        boardPieces[7, 5] = new Bishop(false,7,5);
        boardPieces[7, 6] = new Knight(false,7,6);
        boardPieces[7, 7] = new Rook(false,7,7);

        //In Milestone 2, part of project is making game objects of these pieces
    }

    //Each point has value 0,1,2,3:
    //0 - No piece can interact with this square
    //1 - Only a piece of the current turn's color can interact with this square
    //2 - Only a piece of the opposite turn's color can interact with this square
    //3 - At least one piece from each side can interact with this square
    private void setupAttackField()
    {
        for(int i = 0; i < 7; i ++)
            for(int j = 0; j < 7; j++)
            {
                attackField[i, j] = 0;
                Piece thisPiece = boardPieces[i, j];
                if(thisPiece && thisPiece.allegiance ^ turn)
                {
                    thisPiece.findValidSpaces();
                }
            }
        for(int i = 0; i < 7; i++)
            for(int j = 0; j < 7; j++)
            {
                //For Milestone 2
                //Sketch:
                //IF tile(i,j) is highlighted
                //      attackField(i,j) += 1;
            }
        unhighlight();
        for (int i = 0; i < 7; i++)
            for (int j = 0; j < 7; j++)
            {
                attackField[i, j] = 0;
                Piece thisPiece = boardPieces[i, j];
                if (thisPiece && !(thisPiece.allegiance ^ turn))
                {
                    thisPiece.findValidSpaces();
                }
            }
        for (int i = 0; i < 7; i++)
            for (int j = 0; j < 7; j++)
            {
                //For Milestone 2
                //Sketch:
                //IF tile(i,j) is highlighted
                //      attackField(i,j) += 2;
            }
        unhighlight();
    }

    //0 means no piece at location
    //1 means enemy piece at location
    //2 means friendly piece at location
    public int pieceAtSpace(int x, int y, bool inType)
    {
        Piece thisPiece = boardPieces[x, y];
        if (!thisPiece)
        {
            return 0;
        }
        if (thisPiece.allegiance ^ inType)
        {
            return 1;
        }
        return 2;
    }

    //Highlights square (x,y)
    public void highlightSquare(int x, int y)
    {
        //Part of Milestone 2
    }

    //Removes highlighting from every square
    public void unhighlight()
    {
        //Part of Milestone 2
    }

    //Checks if king is in position to be taken and cannot move to get out of checkmate
    //Milestone 2 will include a search table to figure out if there is another way to prevent checkmate (blocking)
    bool isCheckmate()
    {
        bool flag = true;
        for(int i = 0; i < 7 && flag; i++)
        {
            for(int j = 0; j < 7 && flag; j++)
            {
                Piece thisPiece = boardPieces[i, j];
                if(thisPiece.GetType() == typeof(King) && thisPiece.allegiance == turn && attackField[i,j] > 1)
                {
                    bool[] whichEdges = { true, true, true, true, true, true, true, true };
                    if (i == 0)
                    {
                        whichEdges[5] = false;
                        whichEdges[6] = false;
                        whichEdges[7] = false;
                    }
                    if (i == 7)
                    {
                        whichEdges[1] = false;
                        whichEdges[2] = false;
                        whichEdges[3] = false;
                    }
                    if (j == 0)
                    {
                        whichEdges[3] = false;
                        whichEdges[4] = false;
                        whichEdges[5] = false;
                    }
                    if (j == 7)
                    {
                        whichEdges[7] = false;
                        whichEdges[0] = false;
                        whichEdges[1] = false;
                    }
                    if (whichEdges[0] && attackField[i, j + 1] > 1)
                    {
                        whichEdges[0] = false;
                    }
                    if (whichEdges[1] && attackField[i+1, j + 1] > 1)
                    {
                        whichEdges[1] = false;
                    }
                    if (whichEdges[2] && attackField[i+1, j] > 1)
                    {
                        whichEdges[2] = false;
                    }
                    if (whichEdges[3] && attackField[i+1, j - 1] > 1)
                    {
                        whichEdges[3] = false;
                    }
                    if (whichEdges[4] && attackField[i, j - 1] > 1)
                    {
                        whichEdges[4] = false;
                    }
                    if (whichEdges[5] && attackField[i-1, j - 1] > 1)
                    {
                        whichEdges[5] = false;
                    }
                    if (whichEdges[6] && attackField[i-1, j] > 1)
                    {
                        whichEdges[6] = false;
                    }
                    if (whichEdges[7] && attackField[i-1, j + 1] > 1)
                    {
                        whichEdges[7] = false;
                    }
                    if (!whichEdges[0] && !whichEdges[1] && !whichEdges[2] && !whichEdges[3] && !whichEdges[4] && !whichEdges[5] && !whichEdges[6] && !whichEdges[7])
                        return true;
                    else
                        flag = false;
                }
            }
        }
        return false;
    }

}
