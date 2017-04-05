using UnityEngine;

public class Knight : Piece {

    public Knight()
    {

    }

    public Knight(int all, Point p, Board b) : base(all, p, b)
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
    override public MoveTypesE canMove(Point p)
    {
        MoveTypesE mt = base.canMove(p);
        if (mt == MoveTypesE.ILLEGAL)
            return MoveTypesE.ILLEGAL;
        int dy = p.getY() - loc.getY();
        int dx = p.getX() - loc.getX();

        if ((System.Math.Abs(dy) == 2 && System.Math.Abs(dx) == 1) || (System.Math.Abs(dy) == 1 && System.Math.Abs(dx) == 2))
            return mt;
        return MoveTypesE.ILLEGAL;
    }
}
