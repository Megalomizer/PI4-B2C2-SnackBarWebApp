import React from 'react';
import logo from './logo.svg';
import './App.css';

import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import { HelloWorld, Home, TicTacToe } from "./abstracts/export-pages";

export default function App() {
  return (
    <div className="App">
      <Router>
        <Routes>
          <Route path="/" element={<Home/>} />
          <Route path="/tictactoe" element={<TicTacToe/>} />
          <Route path="/helloworld" element={<HelloWorld/>} />
        </Routes>
      </Router>
    </div>
  );
}