from rest_framework import serializers
from .models import Student # Import all models

class StudentSerializer(serializers.ModelSerializer):
  """
  The serializer for the Student model

  ---
  ### Attributes
  model : The model used for the serializer
  fields : The fields used from the model and for the serializer
  """
  class Meta:
    model = Student
    fields = '__all__'