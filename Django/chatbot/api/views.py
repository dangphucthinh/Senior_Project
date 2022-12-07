from django.shortcuts import render

from django.http import JsonResponse

from rest_framework.parsers import JSONParser 
from rest_framework import status
from rest_framework.decorators import api_view
import io
import os
import requests

from .chatbot import chatbot
from .decision_tree.decision_tree import predict

# Create your views here.
@api_view(['GET'])
def getALl(request):
    if request.method == 'GET':
        # tutorial_data = JSONParser().parse(request)
        data = {
        "response": "adsadad"
        }
        
        return JsonResponse(data)

@api_view(['POST'])
def predict_disease(request):
    if request.method == 'POST':
        tutorial_data = JSONParser().parse(request)
        data = list(tutorial_data['data'])

        data = {
        "response": predict(data)
        }
        
        return JsonResponse(data)

@api_view(['POST'])
def chatbot_response(request):
    if request.method == 'POST':
        tutorial_data = JSONParser().parse(request)
        data = {
        "response": chatbot.vbot(tutorial_data['data'])
        }
        
        return JsonResponse(data)
