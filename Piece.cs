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

    public Piece()
    {
        allegiance = (int)Board.PlayerE.White;
    }

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

    public bool getHasMoved()
    {
        return hasMoved;
    }

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

    public virtual void tryToMove(Point p)
    {
        MoveTypesE mt = canMove(p);
        if(mt != MoveTypesE.ILLEGAL)
        {
            gameBoard.Move(loc, p);
            hasMoved = true;
        }
    }

    public int getAllegiance()
    {
        return allegiance;
    }

    public void setLoc(int xn, int yn)
    {
        loc.setPoint(xn, yn);
    }

    public void setLoc(Point p)
    {
        loc = p;
    }

    public Point getLoc()
    {
        return loc;
    }

    void OnMouseEnter()
    {
        findValidSpaces();
        GetComponent<Renderer>().material.shader = Shader.Find("Outlined/Silhouetted Diffuse");
    }

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

    virtual public bool findValidSpaces()
    {
        Debug.Log("Error: findValidSpaces not implemented");
        return false;
    }
}
