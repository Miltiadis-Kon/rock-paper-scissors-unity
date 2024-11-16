#Import the necessary Packages for this software to run
import mediapipe
import cv2
from collections import Counter
import random
from time import sleep
import time


#Use MediaPipe to draw the hand framework over the top of hands it identifies in Real-Time
drawingModule = mediapipe.solutions.drawing_utils
handsModule = mediapipe.solutions.hands


#Use CV2 Functionality to create a Video stream and add some values
cap = cv2.VideoCapture(0)

my_list=['rock', 'paper', 'scissors']


h=480
w=640
tip=[8,12,16,20]
mid=[6,10,14,18] 
fingers=[]
finger=[]


               
def findnameoflandmark(frame1):
     list=[]
     results = hands.process(cv2.cvtColor(frame1, cv2.COLOR_BGR2RGB))
     if results.multi_hand_landmarks != None:
        for handLandmarks in results.multi_hand_landmarks:


            for point in handsModule.HandLandmark:
                 list.append(str(point).replace ("< ","").replace("HandLandmark.", "").replace("_"," ").replace("[]",""))
     return list

counter=0

while True:
     print('Rock...')
     sleep(1)
     print('Paper...')
     sleep(1)
     print('Scissors...')
     sleep(1)
     print('Shoot!')
     sleep(0.2)
     computer=random.choice(my_list)
     
     with handsModule.Hands(static_image_mode=False, min_detection_confidence=0.7, min_tracking_confidence=0.7, max_num_hands=1) as hands:
          

     #Create an infinite loop which will produce the live feed to our desktop and that will search for hands
          while True:           
                ret, frame = cap.read()
                frame1 = cv2.resize(frame, (640, 480))
                
                #produces the hand framework overlay ontop of the hand, you can choose the colour here too)
                list=[] #initializing position list before calling landmark
                results = hands.process(cv2.cvtColor(frame1, cv2.COLOR_BGR2RGB))
                
                #Incase the system sees multiple hands this if statment deals with that and produces another hand overlay
                if results.multi_hand_landmarks != None:
                   for handLandmarks in results.multi_hand_landmarks:
                       drawingModule.draw_landmarks(frame1, handLandmarks, handsModule.HAND_CONNECTIONS)
                       list=[]
                       for id, pt in enumerate (handLandmarks.landmark):
                           x = int(pt.x * w)
                           y = int(pt.y * h)
                           list.append([id,x,y])  #Gets landmarks position
                      
                #print('list=', list)
                a = list
                b= findnameoflandmark(frame1)

           
               
           
                if len(b and a)!=0:  #a[id,x,y]; x,y=[0,0] on top left of the screen; and x,y=[1,1] at the bottom right
                    fingers=[]
                    for id in range(0,4):
                       if tip[id]==8 and mid[id]==6:  #index_finger_tip landmark 
                           if (a[tip[id]][2:] < a[mid[id]][2:]):
                               fingers.append(1)
                           else:
                               fingers.append(0)
                       if tip[id]==12 and mid[id]==10: #middle_finger_tip landmark
                           if (a[tip[id]][2:] < a[mid[id]][2:]):
                               fingers.append(1)
                           else:
                               fingers.append(0)
                       if tip[id]==16 and mid[id]==14: #ring_finger_tip landmark
                           if (a[tip[id]][2:] < a[mid[id]][2:]):
                               fingers.append(1)
                           else:
                               fingers.append(0)
                       if tip[id]==20 and mid[id]==18: #pinky_finger_tip landmark
                           if (a[tip[id]][2:] < a[mid[id]][2:]):
                               fingers.append(1)
                           else:
                               fingers.append(0)
                                                   
                             
                    x=fingers 
                    c=Counter(x)
                    up=c[1]
                    down=c[0]
                   
                         #rock
                    if x[0] == 0 and x[1]==0 and x[2]==0 and x[3]==0 and computer == 'rock':  #rock vs rock
                              print('TIE')
                              print('Round Results:')
                              print('You played: rock')
                              print('Computer Played:' +computer)
                              counter=counter+1


                    if x[0] == 0 and x[1]==0 and x[2]==0 and x[3]==0 and computer == 'paper':  #rock vs paper
                              print('LOSS')
                              print(':(')
                              print('Round Results:')
                              print('You played: rock')
                              print('Computer Played:' +computer)
                              counter=counter+1                         
                              
                    if x[0] == 0 and x[1]==0 and x[2]==0 and x[3]==0 and computer == 'scissors':  #rock vs scissors
                              print('WIN!')
                              print('Round Results:')
                              print('You played: rock')
                              print('Computer Played:' +computer)
                              counter=counter+1
                              

                         #scissors
                    if x[0] == 1 and x[1]==1 and x[2]==0 and x[3]==0 and computer == 'rock':  #scissors vs rock
                              print('LOSS')
                              print(':(')
                              print('Round Results:')
                              print('You played: scissors')
                              print('Computer Played:' +computer)
                              counter=counter+1                         
                              

                    if x[0] == 1 and x[1]==1 and x[2]==0 and x[3]==0 and computer == 'paper':  #scissors vs paper
                              print('WIN!')
                              print('Round Results:')
                              print('You played: scissors')
                              print('Computer Played:' +computer)
                              counter=counter+1                         
                              
                              

                    if x[0] == 1 and x[1]==1 and x[2]==0 and x[3]==0 and computer == 'scissors':  #scissors vs scissors
                              print('TIE')
                              print('Round Results:')
                              print('You played: scissors')
                              print('Computer Played:' +computer)
                              counter=counter+1                                 

                         #paper
                    if x[0] == 1 and x[1]==1 and x[2]==1 and x[3]==1 and computer == 'rock':  #paper vs rock
                              print('WIN!')
                              print('Round Results:')
                              print('You played: paper')
                              print('Computer Played:' +computer)
                              counter=counter+1                         
                              

                    if x[0] == 1 and x[1]==1 and x[2]==1 and x[3]==1 and computer == 'paper':  #paper vs paper
                              print('TIE')
                              print('Round Results:')
                              print('You played: paper')
                              print('Computer Played:' +computer)
                              counter=counter+1                         
                         

                    if x[0] == 1 and x[1]==1 and x[2]==1 and x[3]==1 and computer == 'scissors':  #paper vs scissors
                              print('LOSS')
                              print(':(')
                              print('Round Results:')
                              print('You played: paper')
                              print('Computer Played:' +computer)
                              counter=counter+1                         
                              
                    if counter>=1:
                         sleep(10)
                         break        
                 
                #Below shows the current frame to the desktop 
                cv2.imshow("Frame", frame1);
                key = cv2.waitKey(1) & 0xFF
                
                #Below states that if the |q| is press on the keyboard it will stop the system
                if key == ord("q"):
                   break

GPIO.cleanup()