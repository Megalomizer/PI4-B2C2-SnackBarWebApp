const urlApi = "http://127.0.0.1:8000/api/student/";

export class Student {
  id?: number;
  studentId?: number;
  firstName?: string;
  lastName?: string;

  constructor(studentid?:number, firstname?:string, lastname?:string) {
    this.id = 0;
    this.studentId = studentid;
    this.firstName = firstname;
    this.lastName = lastname;
  }

  /**
   * Creates a new student and throws it to the Backend
   * @param student The newly created student
   */
  static async createNewStudent({ student }: { student:Student }) {
    try {
      console.log(`Url: ${urlApi}`);
      console.log(`Json: ${JSON.stringify(student)}`);

      const response = await fetch(
        urlApi,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(student),
        }
      );

      console.log(response);

      if (!response.ok) {
        throw new Error(`Error: ${response}`);
      }
    }
    catch (error) {
      throw new Error(`Error: ${error}`);
    }
  }

  /**
   * Get a list of all students from the API
   * @returns A List of all Students as the Student object
   */
  static async getAllStudents():Promise<Student[]> {
    try {
      const response = await fetch(urlApi);
      if (!response.ok) throw new Error(`HTTP error! Status: ${response.status}`);

      const studentData = await response.json();
      const students: Student[] = studentData.map((student: Student) => ({
        id: student.id,
        studentId: student.studentId,
        firstName: student.firstName,
        lastName: student.lastName,
      }));

      return students;
    }
    catch (error) {
      console.error('Error fetching students: ', error);
      throw new Error('Failed to fetch student data');
    }
  }
}