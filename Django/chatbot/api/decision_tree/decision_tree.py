import numpy as np
import pandas as pd
import os
# from gui_stuff import *

symptom=['back_pain','constipation','abdominal_pain','diarrhoea','mild_fever','yellow_urine',
'yellowing_of_eyes','acute_liver_failure','fluid_overload','swelling_of_stomach',
'swelled_lymph_nodes','malaise','blurred_and_distorted_vision','phlegm','throat_irritation',
'redness_of_eyes','sinus_pressure','runny_nose','congestion','chest_pain','weakness_in_limbs',
'fast_heart_rate','pain_during_bowel_movements','pain_in_anal_region','bloody_stool',
'irritation_in_anus','neck_pain','dizziness','cramps','bruising','obesity','swollen_legs',
'swollen_blood_vessels','puffy_face_and_eyes','enlarged_thyroid','brittle_nails',
'swollen_extremeties','excessive_hunger','extra_marital_contacts','drying_and_tingling_lips',
'slurred_speech','knee_pain','hip_joint_pain','muscle_weakness','stiff_neck','swelling_joints',
'movement_stiffness','spinning_movements','loss_of_balance','unsteadiness',
'weakness_of_one_body_side','loss_of_smell','bladder_discomfort','foul_smell_of urine',
'continuous_feel_of_urine','passage_of_gases','internal_itching','toxic_look_(typhos)',
'depression','irritability','muscle_pain','altered_sensorium','red_spots_over_body','belly_pain',
'abnormal_menstruation','dischromic _patches','watering_from_eyes','increased_appetite','polyuria','family_history','mucoid_sputum',
'rusty_sputum','lack_of_concentration','visual_disturbances','receiving_blood_transfusion',
'receiving_unsterile_injections','coma','stomach_bleeding','distention_of_abdomen',
'history_of_alcohol_consumption','fluid_overload','blood_in_sputum','prominent_veins_on_calf',
'palpitations','painful_walking','pus_filled_pimples','blackheads','scurring','skin_peeling',
'silver_like_dusting','small_dents_in_nails','inflammatory_nails','blister','red_sore_around_nose',
'yellow_crust_ooze']


print(len(symptom))
disease=[
['Fungal infection',['Infectious Diseases']],
['Allergy', ['Internal respiration']],
['GERD',['Gastroenterology']],
['Chronic cholestasis',['Gastroenterology']],
['Drug Reaction',['Internal respiration']],
['Peptic ulcer diseae',['Gastroenterology']],
['AIDS', ['Infectious Diseases']],
['Diabetes',['Gastroenterology']],
['Gastroenteritis',['Gastroenterology']],
['Bronchial Asthma', ['Internal respiration']],
['Hypertension',['Cardiomyopathy']],
[' Migraine',['Neuromuscular - musculoskeletal - blood transfusion']],
['Cervical spondylosis',['Neuromuscular - musculoskeletal - blood transfusion']],
['Paralysis (brain hemorrhage)',['Foreign Neurology']],
['Jaundice',['Gastroenterology']],
['Malaria',['Infectious Diseases']],
['Chicken pox',['Infectious Diseases']],
['Dengue',['Infectious Diseases']],
['Typhoid', ['Infectious Diseases']],
['hepatitis A',['Infectious Diseases', 'Gastroenterology']],
['Hepatitis B',['Infectious Diseases', 'Gastroenterology']],
['Hepatitis C',['Infectious Diseases', 'Gastroenterology']],
['Hepatitis D',['Infectious Diseases', 'Gastroenterology']],
['Hepatitis E',['Infectious Diseases', 'Gastroenterology']],
['Alcoholic hepatitis',['Gastroenterology']],
['Tuberculosis',['Internal respiration']],
['Common Cold',['Internal synthesis','Internal respiration']],
['Pneumonia',['Internal respiration']],
['Dimorphic hemmorhoids(piles)', ['External digestion', 'Gastroenterology']],
['Heartattack', ['Cardiovascular intervention']],
['Varicoseveins',['Cardiomyopathy']],
['Hypothyroidism', ['Gastroenterology']],
['Hyperthyroidism',['Gastroenterology']],
['Hypoglycemia',['Gastroenterology']],
['Osteoarthristis',['Neurology - musculoskeletal - blood transfusion']],
['Arthritis',['Neurology - musculoskeletal - blood transfusion']],
['(vertigo) Paroymsal  Positional Vertigo',['Ear, nose and throat']],
['Acne',['Dermatology']],
['Urinary tract infection',['Gastroenterology']],
['Psoriasis',['Dermatology']],
['Impetigo',['Dermatology']]
]



module_dir = os.path.dirname(__file__)
path = os.path.join(module_dir, 'Training.csv')
# TESTING DATA df -------------------------------------------------------------------------------------
df=pd.read_csv(path)

df.replace({'prognosis':{'Fungal infection':0,'Allergy':1,'GERD':2,'Chronic cholestasis':3,'Drug Reaction':4,
'Peptic ulcer diseae':5,'AIDS':6,'Diabetes ':7,'Gastroenteritis':8,'Bronchial Asthma':9,'Hypertension ':10,
'Migraine':11,'Cervical spondylosis':12,
'Paralysis (brain hemorrhage)':13,'Jaundice':14,'Malaria':15,'Chicken pox':16,'Dengue':17,'Typhoid':18,'hepatitis A':19,
'Hepatitis B':20,'Hepatitis C':21,'Hepatitis D':22,'Hepatitis E':23,'Alcoholic hepatitis':24,'Tuberculosis':25,
'Common Cold':26,'Pneumonia':27,'Dimorphic hemmorhoids(piles)':28,'Heart attack':29,'Varicose veins':30,'Hypothyroidism':31,
'Hyperthyroidism':32,'Hypoglycemia':33,'Osteoarthristis':34,'Arthritis':35,
'(vertigo) Paroymsal  Positional Vertigo':36,'Acne':37,'Urinary tract infection':38,'Psoriasis':39,
'Impetigo':40}},inplace=True)

# print(df.head())
arrrrrr = [data[0] for data in disease]
print(arrrrrr)
X= df[symptom]

y = df[["prognosis"]]
np.ravel(y)

from sklearn import tree

clf3 = tree.DecisionTreeClassifier()  
clf3 = clf3.fit(X,y)
# calculating accuracy-------------------------------------------------------------------
from sklearn.metrics import accuracy_score
y_pred=clf3.predict(X)
print(accuracy_score(y, y_pred))
print(accuracy_score(y, y_pred,normalize=False))

import pydotplus
import graphviz
data = tree.export_text(clf3)
from matplotlib import pyplot as plt
from sklearn import datasets
from sklearn.tree import DecisionTreeClassifier 
from sklearn import tree
fig = plt.figure(figsize=(200,200))
_ = tree.plot_tree(clf3, 
                   feature_names=symptom,
                   class_names=arrrrrr, 
                   filled=True)

fig.savefig("decistion_tree.png")







def predict(data_request):
    print("asdasd")
    data = [ 0 for i in range(95)]

    for item in data_request:
        if item in [s for s in symptom]:
            data[symptom.index(item)] = 1

    
    return disease[clf3.predict([data])[0]]

