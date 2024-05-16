import { expect, test } from '@jest/globals';
import { fireEvent, render, screen } from '@testing-library/react';
import { Student } from "../abstracts/export-models";
import HelloWorld from '../pages/HelloWorld';
import CreateStudentForm from '../pages/CreateStudentForm';
import { randomInt } from 'crypto';

test(
  "Test fetching studens from DB",
  async () => {
    let studentsFetched = await Student.getAllStudents();
    expect(studentsFetched).toBeDefined();
  }
); 

test(
  "Test Creation of new student with a random student id",
  async () => {
    const studenListBefore:Student[] = await Student.getAllStudents();
    let randomId = randomInt(2300000);
    let student:Student = new Student(randomId, "John", "Doe");
    Student.createNewStudent({student});
    const studentListAfter:Student[] = await Student.getAllStudents();
    expect(studentListAfter).toHaveLength(studenListBefore.length + 1);
  }
);