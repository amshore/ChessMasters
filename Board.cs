//Most of this game logic is much more complicated than I had predicted.
//I have the basic logic of the game, but I am working on studying chessprogramming.wikispaces.com to learn more about algorithms to work with chess

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : Singleton<Board>
{
    public enum PlayerE
    {
        White = 0,
        Black = 1
    };

    bool gameActive;
    int turn = (int) PlayerE.White;
    bool piecesUpdated = false;
    Piece[,] boardPieces;
    History firstHistory, lastHistory;
    Point enPassant;
    Piece[] kings;

    // Use this for initialization
    void Start()
    {
        boardPieces = new Piece[8, 8];
        kings = new Piece[2];
        setupBoard();
    }

    // Update is called once per frame
    void Update()
    {
        if (piecesUpdated)
        {
            piecesUpdated = false;
            gameActive = isCheckmate();
        }
        //During Milestone 2, there will be tiles once we integrate the graphics with this code
        //I will use a similar detection of click as for the Piece class
        //When I detect a click, if the tile clicked is highlighted, I will move currentPiece to the new location
        //Next, I will remove any piece currently on that tile from the game
        //I will then update the location of the piece
        //Finally, I will make piecesUpdated true
    }

    private void switchTurn()
    {
        turn += 1;
        turn %= 2;
    }

    //This is effectively acting as the constructor for Board
    void setupBoard()
    {
        boardPieces[0, 0] = new Rook((int)PlayerE.White, 0, 0);
        boardPieces[0, 1] = new Knight((int)PlayerE.White, 0, 1);
        boardPieces[0, 2] = new Bishop((int)PlayerE.White, 0, 2);
        boardPieces[0, 3] = new Queen((int)PlayerE.White, 0, 3);
        boardPieces[0, 4] = new King((int)PlayerE.White, 0, 4);
        boardPieces[0, 5] = new Bishop((int)PlayerE.White, 0, 5);
        boardPieces[0, 6] = new Knight((int)PlayerE.White, 0, 6);
        boardPieces[0, 7] = new Rook((int)PlayerE.White, 0, 7);
        boardPieces[1, 0] = new Pawn((int)PlayerE.White, 1, 0);
        boardPieces[1, 1] = new Pawn((int)PlayerE.White, 1, 1);
        boardPieces[1, 2] = new Pawn((int)PlayerE.White, 1, 2);
        boardPieces[1, 3] = new Pawn((int)PlayerE.White, 1, 3);
        boardPieces[1, 4] = new Pawn((int)PlayerE.White, 1, 4);
        boardPieces[1, 5] = new Pawn((int)PlayerE.White, 1, 5);
        boardPieces[1, 6] = new Pawn((int)PlayerE.White, 1, 6);
        boardPieces[1, 7] = new Pawn((int)PlayerE.White, 1, 7);
        boardPieces[6, 0] = new Pawn((int)PlayerE.Black, 6, 0);
        boardPieces[6, 1] = new Pawn((int)PlayerE.Black, 6, 1);
        boardPieces[6, 2] = new Pawn((int)PlayerE.Black, 6, 2);
        boardPieces[6, 3] = new Pawn((int)PlayerE.Black, 6, 3);
        boardPieces[6, 4] = new Pawn((int)PlayerE.Black, 6, 4);
        boardPieces[6, 5] = new Pawn((int)PlayerE.Black, 6, 5);
        boardPieces[6, 6] = new Pawn((int)PlayerE.Black, 6, 6);
        boardPieces[6, 7] = new Pawn((int)PlayerE.Black, 6, 7);
        boardPieces[7, 0] = new Rook((int)PlayerE.Black, 7, 0);
        boardPieces[7, 1] = new Knight((int)PlayerE.Black, 7, 1);
        boardPieces[7, 2] = new Bishop((int)PlayerE.Black, 7, 2);
        boardPieces[7, 3] = new Queen((int)PlayerE.Black, 7, 3);
        boardPieces[7, 4] = new King((int)PlayerE.Black, 7, 4);
        boardPieces[7, 5] = new Bishop((int)PlayerE.Black, 7, 5);
        boardPieces[7, 6] = new Knight((int)PlayerE.Black, 7, 6);
        boardPieces[7, 7] = new Rook((int)PlayerE.Black, 7, 7);

        kings[0] = boardPieces[0, 4];
        kings[1] = boardPieces[7, 4];

        firstHistory = null;
        lastHistory = null;
        enPassant = null;
    }

    public Piece pieceAt(Point p)
    {
        return boardPieces[p.getX() , p.getY()];
    }

    public Piece pieceAt(int x, int y)
    {
        return boardPieces[x, y];
    }

    public void placePieceAt(Piece p, Point pt)
    {
        boardPieces[pt.getX(), pt.getY()] = p;
    }

    public void Move(Point p1, Point p2)
    {
        Move(p1, p2, null);
    }

    public void Move(Point p1, Point p2, Point ep)
    {
        History temp_hist = new History(p1, p2, this, lastHistory);
        lastHistory.setNext(temp_hist);
        lastHistory = temp_hist;
        enPassant = ep;
        switchTurn();
        placePieceAt(pieceAt(p1), p2);
        boardPieces[p1.getX(), p1.getY()] = null;
    }

    public void tryToMove(Point p1, Point p2)
    {
        Piece temp_piece = pieceAt(p1);
        if (temp_piece != null)
        {
            temp_piece.tryToMove(p2);
        }
    }

    public void killEnPassant()
    {
        boardPieces[enPassant.getX(), enPassant.getY()] = null;
    }

    public bool inCheck(Point start, Point finish)
    {
        Piece startPiece = boardPieces[start.getX(), start.getY()];
        Piece finishPiece = boardPieces[finish.getX(), finish.getY()];

        boardPieces[finish.getX(), finish.getY()] = startPiece;

        bool flag = inCheck(kings[turn].getLoc());
        boardPieces[start.getX(), start.getY()] = startPiece;
        boardPieces[finish.getX(), finish.getY()] = finishPiece;

        return flag;
    }

    public bool inCheck(Point p)
    {
        for(int i = 0; i < 7; i++)
            for(int j = 0; j < 7; j++)
                if(boardPieces[i,j] != null && boardPieces[i,j].getAllegiance() != turn)
                    if (boardPieces[i, j].canMove(p) != Piece.MoveTypesE.ILLEGAL)
                        return true;
        return false;
    }

    public bool inCheck(Piece notKing, Piece king, int xloc, int yloc)
    {
        boardPieces[notKing.getLoc().getX(), notKing.getLoc().getY()] = null;
        boardPieces[king.getLoc().getX(), king.getLoc().getY()] = null;
        bool flag = inCheck(new Point(xloc, yloc));
        boardPieces[notKing.getLoc().getX(), notKing.getLoc().getY()] = notKing;
        boardPieces[king.getLoc().getX(), king.getLoc().getY()] = king;
        return flag;
    }

    public void promotePawn(Point p)
    {
        boardPieces[p.getX(), p.getY()] = new Queen(turn, p.getX(), p.getY());
    }

    public Point getEnPassant()
    {
        return enPassant;
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
        return false;
    }

}