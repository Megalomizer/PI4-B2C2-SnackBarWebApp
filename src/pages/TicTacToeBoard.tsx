import React from 'react';
import logo from '../logo.svg';

import '../App.css';
import '../styles/Square.css';

import { Navigate } from "react-router-dom";

export default function TicTacToeBoard() {
  const [goToHome, setGoToHome] = React.useState(false);
  const [squares, setSquares] = React.useState(Array(9).fill("_"))
  const [xIsNext, setXIsNext] = React.useState(true);

  let winner = calculateWinner({ squares });
  let status:String;

  if (goToHome) {
    return <Navigate to="/" />
  }

  function resetBoard() {
    setSquares(Array(9).fill("_"));
    setXIsNext(true);
  }

  function handleClick ({i} : {i:number}) {
    if (squares[i] !== "_" || winner !== null) {
      return
    }

    const nextSquares = squares.slice(); //create copy of the array
    if (xIsNext) {
      nextSquares[i] = "X";
    } else {
      nextSquares[i] = "O";
    }

    setSquares(nextSquares); // place the new array in place of the old one
    setXIsNext(!xIsNext); // flip the turn
    status = `Next Player: ${xIsNext ? "X" : "O"}`;
  }

  if (winner === null) {
    status = `Next Player: ${xIsNext ? "X" : "O"}`;
  } else {
    status = `The winner is ${winner}`;
  }

  return (
    <div className="App">
      Tic Tac Toe Board
      <button onClick={() => {
        setGoToHome(true);
      }}
      >
        Go to the Home page
      </button>
      <div className="tictactoe-board">
        <div className="tictactoe-board-scoreboard">
          <Status status={status} />
        </div>
        <div className="tictactoe-board-row">
          <Square value={squares[0]} onSquareClick={() => handleClick({i:0})}/>
          <Square value={squares[1]} onSquareClick={() => handleClick({i:1})}/>
          <Square value={squares[2]} onSquareClick={() => handleClick({i:2})}/>
        </div>
        <div className="tictactoe-board-row">
          <Square value={squares[3]} onSquareClick={() => handleClick({i:3})}/>
          <Square value={squares[4]} onSquareClick={() => handleClick({i:4})}/>
          <Square value={squares[5]} onSquareClick={() => handleClick({i:5})}/>
        </div>
        <div className="tictactoe-board-row">
          <Square value={squares[6]} onSquareClick={() => handleClick({i:6})}/>
          <Square value={squares[7]} onSquareClick={() => handleClick({i:7})}/>
          <Square value={squares[8]} onSquareClick={() => handleClick({i:8})}/>
        </div>
        <button className="tictactoe-board-reset" onClick={resetBoard}>Reset Game</button>
      </div>
    </div>
  );
}

/**
 * Calculate if there is a winner on the board and who it is
 * @param squares - The list of the board
 * @returns The winner of the given board, or null if there is no winner
 */
export function calculateWinner ({squares} : {squares:Array<String>}) {
  const winConditions = [
    [0, 1, 2], [3, 4, 5], [6, 7, 8], // Horizontal
    [0, 3, 6], [1, 4, 7], [2, 5, 8], // Vertical
    [0, 4, 8], [6, 4, 2], // Crossed
  ];

  for (let i = 0; i < winConditions.length; i++) {
    const [a, b, c] = winConditions[i];
    if (squares[a] != "_") {
      if (squares[a] && squares[a] === squares[b] && squares[a] === squares[c]) {
        return squares[a];
      }
    }
  }

  return null;
}

function Status({ status } : { status:String }) {
  return <p>{ status }</p>
}

// function square(String value, MouseEventHandler onSquareClick) {}
function Square({value, onSquareClick} : {value:string, onSquareClick:React.MouseEventHandler}) {
  return <button className="tictactoe-board-square" onClick={onSquareClick}>{value}</button>
}