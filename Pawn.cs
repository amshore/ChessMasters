using UnityEngine;

public class Pawn : Piece {

    int direction;

    public Pawn()
    {
        direction = 1;
    }

    public Pawn(int all, Point p, Board b) : base(all, p, b)
    {
        direction = (all == 0)? 1 : -1;
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
    override public MoveTypesE canMove(Point p)
    {
        if (base.canMove(p) == MoveTypesE.ILLEGAL)
            return MoveTypesE.ILLEGAL;
        int dy = p.getY() - loc.getY();
        int dx = p.getX() - loc.getX();
        if (dy == direction)
        {
            Piece pAt = gameBoard.pieceAt(p);
            if ((System.Math.Abs(dx) == 1) && pAt == null)
            {
                if (gameBoard.getEnPassant() == p)
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
