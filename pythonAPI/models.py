from django.db import models

# Create your models here.

class Student(models.Model):
  """
  A Model containing the data about a student

  ---
  #### Attributes
  StudentId : int
    -> The id of the student

  FirstName : str | { max_length=100 }
    -> The first name of the student

  LastName : str | { max_length=100 }
    -> The last name of the student
  """
  studentId = models.IntegerField()
  firstName = models.CharField(max_length=100)
  lastName = models.CharField(max_length=100)