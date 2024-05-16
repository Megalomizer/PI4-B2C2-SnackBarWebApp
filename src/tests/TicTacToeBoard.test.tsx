/// Imports
import { describe, expect, test } from '@jest/globals';
import { calculateWinner } from '../pages/TicTacToeBoard';

/// Test CalculateWinner()
let board_1:Array<String> = ["O", "_", "_", "O", "_", "_", "O", "_", "_"];
let winner_1 = "O";
let board_2:Array<String> = ["X", "_", "O", "O", "X", "_", "O", "_", "X"];
let winner_2 = "X";
let board_3:Array<String> = ["O", "_", "X", "X", "_", "O", "O", "X", "_"];
let winner_3:null = null;

test(
  "Tests the winner of board 1: " + board_1,
  () => {
    expect(calculateWinner({squares:board_1})).toBe(winner_1);
  }
);

test(
  "Tests the winner of board 2: " + board_2,
  () => {
    expect(calculateWinner({squares:board_2})).toBe(winner_2);
  }
);

test(
  "Tests the winner of board 3: " + board_3,
  () => {
    expect(calculateWinner({squares:board_3})).toBe(winner_3);
  }
);