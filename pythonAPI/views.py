from rest_framework import viewsets
from .models import Student
from .serializers import StudentSerializer

# Create your views here.
class StudentViewSet(viewsets.ModelViewSet):
  """
  The view of the student model

  ---
  ### Attributes
  queryset : The dataset we want to query when calling this view
  serializer_class : The serializer this view needs to use
  """
  queryset = Student.objects.all()
  serializer_class = StudentSerializer