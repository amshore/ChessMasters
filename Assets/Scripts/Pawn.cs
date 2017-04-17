using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece {

    //Used for determing which direction pawn moves (1 for WHITE, -1 for BLACK)
    int direction;

    //Default constructor, should never be used
    public Pawn()
    {
        direction = 1;
    }

    //Calls the base constructor and sets direction
    public Pawn(int all, Point p, Board b, PieceTypeE t) : base(all, p, b,t)
    {

    }

    override public void initialize(int all, Point p, Board b, PieceTypeE t)
    {
        base.initialize(all, p, b, t);
        direction = (all == 0) ? 1 : -1;
    }

    //Calculates all spaces in box around piece to see if legal
    public override List<Point> canMoveList()
    {
        List<Point> retMoveList = new List<Point>();
        for(int i = -1; i <= 1; i++)
            for(int j = -1; j <= 1; j++)
            {
                Point pt = new Point(i, j);
                if (canMove(pt) != MoveTypesE.ILLEGAL)
                    retMoveList.Add(pt);
            }
        if(!hasMoved)
        {
            Point pt = new Point(loc.getX(), loc.getY() + 2 * direction);
            if (canMove(pt) != MoveTypesE.ILLEGAL)
                retMoveList.Add(pt);
        }

        Debug.Log("Pawn at (" + loc.getX() + ", " + loc.getY() + ") can move to: ");
        foreach (Point p in retMoveList)
        {
            Debug.Log("(" + p.getX() + ", " + p.getY() + ")");
        }
        if (retMoveList.Count == 0)
            Debug.Log("No Possible Moves");

        return retMoveList;
    }

    //(a) The pawn may move only forward[except as limited by Article 4.2].
    //(b) Except when making a capture, it advances from its original square either one or two vacant squares along the file on which it is placed, 
    //     and on subsequent moves it advances one vacant square along the file.When capturing, it advances one square along either of the diagonals 
    //     on which it stands.
    //(c) A pawn, attacking a square crossed by an opponent's pawn which has [just] been advanced two squares in one move from its original square, 
    //     may capture this opponent's pawn as though the latter had been moved only one square.This capture may only be made in [immediate]
    //     reply to such an advance, and is called an "en passant" capture.
    override public MoveTypesE canMove(Point p)
    {
        if (base.canMove(p) == MoveTypesE.ILLEGAL)
            return MoveTypesE.ILLEGAL;
        int dy = p.getY() - loc.getY();
        int dx = p.getX() - loc.getX();
        if (dy == direction)
        {
            Piece pAt;
            if (gameBoard.pieceAt(p) == null)
                pAt = null;
            else
                pAt = (Piece)gameBoard.pieceAt(p).GetComponent("Piece");
            if ((System.Math.Abs(dx) == 1) && pAt == null)
            {
                if (gameBoard.getEnPassant() != null && gameBoard.getEnPassant() == p)
                    return MoveTypesE.ENPASSANT;
            }
            else if ((System.Math.Abs(dx) == 1) && (allegiance == pAt.getAllegiance()))
            {
                if ((direction == 1 && p.getY() == 7) || (direction == -1 && p.getY() == 0))
                    return MoveTypesE.PROMOTE;
                else
                    return MoveTypesE.CAPTURE;
            }
            if ((dx == 0) && gameBoard.pieceAt(p) == null)
            {
                if ((direction == 1 && p.getY() == 7) || (direction == -1 && p.getY() == 0))
                    return MoveTypesE.PROMOTE;
                else
                    return MoveTypesE.NORMAL;
            }
        }
        if (!hasMoved && (dy == 2 * direction) && (dx == 0) && (gameBoard.pieceAt(p) == null) && (gameBoard.pieceAt(p.getX(), p.getY() + direction) == null))
            return MoveTypesE.DOUBLESTEP;
        return MoveTypesE.ILLEGAL;
    }

    //Same basic functionality as normal move but keeps track of setting enpassant if the pawn doublesteps and promotes the pawn if it reaches the end
    public override void tryToMove(Point p)
    {
        MoveTypesE mt = canMove(p);
        if (mt != MoveTypesE.ILLEGAL)
        {
            if (mt == MoveTypesE.DOUBLESTEP)
                gameBoard.Move(loc, p, new Point((loc.getX() + p.getX()) / 2, (loc.getY() + p.getY()) / 2));
            else
            {
                if (mt == MoveTypesE.ENPASSANT)
                    gameBoard.killEnPassant();
                if (mt == MoveTypesE.PROMOTE)
                    gameBoard.promotePawn(loc);
                gameBoard.Move(loc, p);
            }
            hasMoved = true;
        }
    }
}
