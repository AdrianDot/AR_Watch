# AR_Watch

This is a little application to demonstrate the potential of Apple AR glases + Apple Watch
-
**Use Cases:**
1. Augment the 2D watch-face with different timezones.
2. Receive lidar-scans from others via messaging and display them on your Apple Watch in 3D.

The project was done with ARFoundation in Unity. It uses image-detection to recognize the flower-watch face and instantiates different AR watch-faces. The added AR-flowers calculate the current time at different locations. Their position is being updated according to the movement of the Apple watch.
Feel free to clone this project and try it out for yourself! You can find the used versions below :) 

Whats included in the Project?
-
The repo doesn't contain the hole project you can see in the video on my Twitter account, duo to licencing restrictions. Unfortunately I'm not only allowed to share certain assets in this project. Because of that I swapped out the flower model from the video for a different one. The effect is still the same :)
Additionally the scene with the couch is not including, because the recognized couch-image is hardcoded and therefore not usable for everyone, in contrast to the flower-image.

Credits:
-
Huge thanks to the Carnegie Museum of Natural History Powdermill Nature Reserve and The Harrington Lab at the University of Central Florida for letting me use their wood-poppy flower asset. You can find the original [here](https://sketchfab.com/3d-models/wood-poppy-stylophorum-diphyllum-9ec0e9caea9b435d9df465a0f61b08919).
For the calculation of the clock movement I used parts of catlikecoding´s tutorial you can find [here](https://catlikecoding.com/unity/tutorials/basics/game-objects-and-scripts/).
To display the bloom of the flower correct I also used parts of a shader posted in the unity.forum-post by "Jasper", which you can see [here](https://forum.unity.com/threads/unlit-with-adjustable-alpha.115455/).


used Versions:
-
* **Unity**:        2019.4.16f1
* **ARFoundation**: 4.11
* **ARKit**:        4.11
* **Tested with iOS**: 14.4
