import React, { useEffect, useState } from 'react';

import { Navigate } from "react-router-dom";
import { Student } from "../abstracts/export-models";

import '../App.css';
import '../styles/CreateStudentForm.css';

export default function CreateStudentForm() {
  const [newStudent, setNewStudent] = useState<Student>(new Student());

  const handleSubmit = async (e: React.ChangeEvent<HTMLFormElement>) => {
    e.preventDefault();
    try {
      let studentStudentid:HTMLInputElement = document.getElementById("studentid") as HTMLInputElement;
      let studentFirstname:HTMLInputElement = document.getElementById("firstname") as HTMLInputElement;
      let studentLastname:HTMLInputElement = document.getElementById("lastname") as HTMLInputElement;

      let student:Student = new Student(parseInt(studentStudentid.value), studentFirstname.value, studentLastname.value);

      console.log(`Student id: ${e.target.value}`);
      console.log(`Firstname: ${e.target.value}`);
      console.log(`Lastname: ${e.target.value}`);

      await Student.createNewStudent({ student:student });
      window.location.reload();
    }
    catch (error) {
      throw new Error(`Error: ${error}`);
    }
  }

  return (
    <div className='create-student-form'>
      <h1 className='create-student-form-title'>Create new Student</h1>
      <form onSubmit={handleSubmit} className='create-student-input-container' id='form-student-creation'>
        <input 
          type="text"
          id="firstname"
          placeholder='Firstname'
          value={newStudent.firstName}
          className='create-student-input'
        />
        <br/>
        <input 
          type="text"
          id="lastname"
          placeholder='Lastname'
          value={newStudent.lastName}
          className='create-student-input'
        />
        <br/>
        <input 
          type="number"
          id="studentid"
          placeholder='Student number'
          value={newStudent.studentId}
          className='create-student-input'
        />
        <br/>
        <button type="submit" className='create-student-submit-btn' id="create-student-form-submit-btn">Submit</button>
      </form>
    </div>
  );
}