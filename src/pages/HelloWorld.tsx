import React, { useEffect, useState } from 'react';

import { Navigate } from "react-router-dom";
import { Student } from "../abstracts/export-models";
import { CreateStudentForm } from "../abstracts/export-pages";

import '../App.css';
import '../styles/HelloWorld.css';

export default function HelloWorld() {
  const [goToTicTacToeBoard, setGoToTicTacToeBoard] = useState(false);
  const [students, setStudents] = useState<Student[]>([]);
  const [foundError, setFoundError] = useState<Boolean>(false);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const fetchedStudents = await Student.getAllStudents();
        setStudents(fetchedStudents);
      } catch (error) {
        setFoundError(true);
      }
    }

    fetchData();

    return () => {
      // cleanup if needed
    }
  }, []);

  if (goToTicTacToeBoard) {
    return <Navigate to="/tictactoe" />
  }

  return (
    <div className="App">
      <h1>Hello World!</h1>
      <button onClick={() => { setGoToTicTacToeBoard(true) }}>
        Go to the TicTacToe board
      </button>

      <div className='student-container'>
        <h1 className='student-container-titel'>List of Students</h1>
        <div className='student-list-container'>
          {students.map((student) => {
            return (
            <p key={student.id} className='student-list-element'>
              {student.studentId}: {student.firstName} {student.lastName}
            </p>
            )
          })}
        </div>
      </div>

      <div>
        <CreateStudentForm />
      </div>

    </div>
  );
}