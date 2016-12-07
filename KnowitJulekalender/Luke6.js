
Array.min = function (array) {
    return Math.min.apply(Math, array);
};

class Coordinate {
    constructor(x, y) {
        this.X = x;
        this.Y = y;
        this.Value = x + y;
    }

    getPossibleMoves() {
        return [
            new Coordinate(this.X + 2, this.Y + 1),
            new Coordinate(this.X + 2, this.Y - 1),
            new Coordinate(this.X - 2, this.Y + 1),
            new Coordinate(this.X - 2, this.Y - 1),
            new Coordinate(this.X + 1, this.Y + 2),
            new Coordinate(this.X + 1, this.Y - 2),
            new Coordinate(this.X - 1, this.Y + 2),
            new Coordinate(this.X - 1, this.Y - 2),
        ];
    }

    getDiff(val) {
        return Math.abs(val - this.Value);
    }

    filterByEasiestMove(moves) {
        let easiestMove = Array.min(moves.map(m => this.getDiff(m.Value)));
        return moves.filter(m => this.getDiff(m.Value) === easiestMove);
    }

    filterByLowestX(moves) {
        let lowestx = Array.min(moves.map(m => m.X));
        return moves.filter(m => m.X === lowestx);
    }

    filterByLowestY(moves) {
        let lowesty = Array.min(moves.map(m => m.Y));
        return moves.filter(m => m.Y === lowesty);
    }

    findNextMove() {
        var moves = this.filterByLowestY(this.filterByLowestX(this.filterByEasiestMove(this.getPossibleMoves())));
        if (moves.length != 1)
            throw new Error("Smth wrong");
        return moves[0];
    }
}

const history = []

var c = new Coordinate(0, 0);

for (var i = 0; i < 100; i++) {
    c = c.findNextMove();
    history.push(c);
    console.log("Moved to: (" + c.X + ", " + c.Y + ")");
}