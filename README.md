# Unity-2D-Platformer-Smart-Camera-Implementation
A smart Camera Script for basic 2D Platformer

PreRequisites:
Main Camera should be named : MainCamera  
Player Character should be named : Player  	

Will remove these requirements after further update

HOW TO USE

1) Attach CameraSmooth2D script to the main camera GameObject  
2) Make sure the MainCamera is orthographic  
3) Create a generic rectangle as a new GameObject .. Preferably the rectangle should be twice the height and width of your player character.. This will serve as your TRAP to handle camera calculations.. Center it precisely on top of the Player GameObject. Name it CameraWindow  
4) Attach a boxcollider2D as a Trigger to the TRAP and attach the script CameraWindowMove.cs to it  
5) Click the play Button  

HOW IT WORKS  

Generic camera controllers/Unity Inbuilt controller simply track the position and movement of the player controller. However this is very imprecise and can look quite odd in fast paced platformers.  
The smart camera (as I like to call it), uses a rectangular TRAP that encases the player character. The player cannot move out of this TRAP. The camera ONLY starts moving when the player hits the boundaries of the TRAP. This gives a small room for the player to move around without affecting the camera

DemoScene Controls ->  
Left arrow - move left  
Right arrow - move right  
Up arrow - Jump  
