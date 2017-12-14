# Mixed Reality Motion Controllers Starter

This is a Unity project exploring some basic functionality for Windows Mixed Reality motion controllers. It's a basic scene with a few platforms floating in space, which you can walk around on and interact with some objects.

# Entering Play Mode in Editor

There is (or was last time I looked into it) a bug in the Unity implementation of Mixed Reality where playing a scene from the Unity editor doesn't give unity control of the motion controller information. To work around this, start playing in the editor like this:
- Put the headset on, but in open position. 
- Press the play button.
- Close the headset.
- Release the play button
This was advice I found in the Windows MR forums when initially working on this project. I haven't checked recently for better solution, but will update here if I find one.

# Play Instructions

As this is just a test project to get to grips with the basics of using the motion controller data in Unity there isn't any real gameplay as such, but there are some simple interactions.

To walk around, press the left and right flat thumbpads alternately. This will move you horizontally in the direction you are looking. 

You should be able to see 2 very basic red representations of the hand controller position and rotations. Clicking the trigger on each controller will emit a beam from each of these.

Pointing the beam at the suspended spheres will apply gravity to them, causing them to fall.

Pointing the beam at the cubes and holding will draw them to you. When close, you can hold them and move around, as well as throw them (move hand quickly and let go).

Pointing the beam at the top of the green platform will grow a stair case - just keep the beam pointed at the top of the highest stair. You can then walk up the stairs and continue to grow more. 

# Technical Info

Most of the info you'll need to work out how this fits together and works is in the code comments, and there isn't a huge amount of code. However feel free to contact me with any questions. 
