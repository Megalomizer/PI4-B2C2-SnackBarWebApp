from django.urls import path, include
from rest_framework.routers import DefaultRouter
from .views import StudentViewSet

router = DefaultRouter()

# URLS to acces the views
router.register(r'student', StudentViewSet)

# App paths
urlpatterns = [
  path('', include(router.urls)) # Include all registered URLS that are in router
]