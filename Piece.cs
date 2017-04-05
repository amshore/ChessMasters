using UnityEngine;

abstract public class Piece : MonoBehaviour {

    public enum MoveTypesE
    {
        ILLEGAL,
        NORMAL,
        CASTLE,
        DOUBLESTEP,
        ENPASSANT,
        CAPTURE,
        PROMOTE
    };

    protected bool hasMoved = false;
    bool notClicked = true;
    protected Board gameBoard;
    public int allegiance;
    protected Point loc;

    //Default constructor, should never be used
    public Piece()
    {
        allegiance = (int)Board.PlayerE.White;
    }

    //Constructor with color, location, and a reference to the board
    public Piece(int all, Point p, Board b)
    {
        allegiance = all;
        loc = p;
        gameBoard = b;
    }

	// Use this for initialization
	void Start () {
        gameBoard = Board.Instance;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            notClicked = !notClicked;
            Debug.Log("User clicked piece");
        }
	}

    //returns hasMoved, which returns if the piece has moved from its initial location this game
    public bool getHasMoved()
    {
        return hasMoved;
    }

    //The default canMove function, should always be overwritten
    //This is called for each subpiece and determines if:
    // (a) The piece is moved into the board (ILLEGAL if outside the board)
    // (b) The move would leave the king in check (ILLEGAL if so)
    // (c) There is a piece at the spot moving to (NORMAL if no piece)
    // (d) The piece at the spot moving to is of the same color (CAPTURE if opposite)
    public virtual MoveTypesE canMove(Point p)
    {
        if((p.getX() >= 0) && (p.getX() <= 7) && (p.getY() >= 0) && (p.getY() <= 7))
        {
            if (gameBoard.inCheck(loc, p))
            {
                return MoveTypesE.ILLEGAL;
            }
            if (gameBoard.pieceAt(p) == null)
            {
                return MoveTypesE.NORMAL;
            }
            else if (gameBoard.pieceAt(p).getAllegiance() != allegiance)
                return MoveTypesE.CAPTURE;
        }
        return MoveTypesE.ILLEGAL;
    }

    //The default tryToMove function, and only should be overwritten if piece has special rules
    // Pawn: ENPASSANT, DOUBLESTEP, PROMOTE
    // King: CASTLE
    // Uses canMove to deterine if move is ILLEGAL, and if not, makes the move
    public virtual void tryToMove(Point p)
    {
        MoveTypesE mt = canMove(p);
        if(mt != MoveTypesE.ILLEGAL)
        {
            gameBoard.Move(loc, p);
            hasMoved = true;
        }
    }

    //Returns the allegiance (0 for WHITE and 1 for BLACK)
    public int getAllegiance()
    {
        return allegiance;
    }

    //Sets the location of the piece to (xn, yn)
    public void setLoc(int xn, int yn)
    {
        loc.setPoint(xn, yn);
    }

    //Sets the location of the piece to p
    public void setLoc(Point p)
    {
        loc = p;
    }

    //Returns the location of the piece
    public Point getLoc()
    {
        return loc;
    }

    //Used for highlighting the piece
    void OnMouseEnter()
    {
        findValidSpaces();
        GetComponent<Renderer>().material.shader = Shader.Find("Outlined/Silhouetted Diffuse");
    }

    //Used for highlighting the piece
    void OnMouseExit()
    {
        if (notClicked)
        {
            GetComponent<Renderer>().material.shader = Shader.Find("Standard");
            gameBoard.unhighlight();
        }
        else
        {
            gameBoard.currentPiece = this;
        }
    }
}
