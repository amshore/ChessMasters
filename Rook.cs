using UnityEngine;

public class Rook : Piece {

    public Rook()
    {

    }

    public Rook(int all, Point p, Board b): base(all, p, b)
    {

    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //The rook moves to any square (except as limited by Article 4.2) on the file or rank on which it stands.
    override public MoveTypesE canMove(Point p)
    {
        MoveTypesE mt = base.canMove(p);
        if(mt == MoveTypesE.ILLEGAL)
            return MoveTypesE.ILLEGAL;
        int dy = p.getY() - loc.getY();
        int dx = p.getX() - loc.getX();

        if(dy == 0)
        {
            int signFactor = (dx * System.Math.Abs(dx) > 0) ? 1 : -1;
            for(int i = 1; i < System.Math.Abs(dx); i++)
            {
                if (gameBoard.pieceAt(loc.getX() + signFactor * i, loc.getY()) != null)
                    return MoveTypesE.ILLEGAL;
            }
        }
        else if (dx == 0)
        {
            int signFactor = (dy * System.Math.Abs(dy) > 0) ? 1 : -1;
            for (int i = 1; i < System.Math.Abs(dy); i++)
            {
                if (gameBoard.pieceAt(loc.getX(), loc.getY() + signFactor * i) != null)
                    return MoveTypesE.ILLEGAL;
            }
        }
        return mt;
    }
}
