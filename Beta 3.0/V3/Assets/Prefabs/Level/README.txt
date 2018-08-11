Introduction
--------------

Hi There!

So you want to make a level huh?

This level prefab (Assets/Prefabs/Level/Level Prefab) is designed to make whiteboxing and making levels super
easy! Simply load up this level prefab, and drag & drop items that you want in the level into the scene from
the PREFABS FOLDER.





Designing the Level:
---------------------

Most main items are already in the scene and can be copy/pasted, but when you drag items in from the prefabs
folder they may need re-sizing.

Under Environment you will find the grid that the main level structure is built upon, in order to edit it go
to Window > 2D > Tile Palette and you can use the tools there to sculpt the level.





Camera:
--------

You can adjust how smooth the movement of the Camera is by increasing or deacreasing Smooth Factor.

If, for whatever reason, you wanted the camera to focus on another object in the game, set Camera Target to a
different game object.

To shake the camera, in a script locate the CameraBehaviour componenent and call Shake(intensity, time). The higher the
intensity the bigger the shake, and the higher the time the longer it lasts. Time should be between 0 and 1, as the code will
oscilate between distances from the camera at multiples of the Time. Therefore 0.2 would last for very little time as it would
quickly approach 0, but 0.999 would last for a very very long time. 1 would be infinity.


The camera has a few different behaviours. Please feel free to explore them all to see what feels right for you.
It should probably be conistant accross the game, but maybe special events or areas will require a different one.


Setting Camera Type to Screen will fix the camera into one place on the screen, and act like a single-screen game.
You will need to set the Size of the Camera to something larger than it is by default in order to have the whole
level fit in the screen. When you position the Camera how you want in the scene editor, don't forget to copy the
position inside the Transform component into Screen Position under Chappers Cam.


Setting it to Push will create an imaginary bounding box around the player, and when they push against this
the camera will move a little to the side, as if they were dragging it with them. To set the size of this bounding
box, look at the Boundary property.

The W component is how far left the player can go before they start 'pushing'. Should be negative.
The X component is how far right the player can go before they start 'pushing'. Should be positive.
The Y component is how far up the player can go before they start 'pushing'. Should be positive.
The Z component is how far down the player can go before they start 'pushing'. Should be negative.

Unity sometimes displays these out of order as XYZW, so make sure you are editing the right ones! Check the name next
to it.


Setting it to Snap will also create an imaginary bounding box, and when the player leaves it, the camera will move
towards the player until it rests on them. This is good for creating a behaviour that will allow the camera to remain
stationary while the player is doing something in one area, but will move off with them when they explore. Smooth Factor
and Boundary should be carefully balanced.


Setting it to Track will make it stay focused on the player no matter what.





To use triggerables:
---------------------

Drag in a Trigger from Assets/Prefabs/Trigger/Trigger and place it in the scene however you want it. When the
player enters this area, they will trigger whatever you set up next.

Now, drag in a Triggerable. This could be a Vanishing Block, Rolling Rock, Falling Spike, or anything else that
should have behaviour activated as the player enters an area.

Once this is positioned how you want it, drag the Triggerable game object (spike, block, etc) into the Triggerable
property box of the Trigger.

Make sure that the Active box is ticked, so that when the player enters it, they will trigger your Triggerable!

It's a good idea to have Deactivate On Trigger ticked too, so that it's only triggered the once.

If you want the Triggerable to be triggered some time AFTER the player has entered the Trigger zone? Well just set
Wait Until Triggering to the number of seconds you want there to be between the player entering the Trigger zone
and the Triggerable being triggered.

What if you don't want this Trigger to be Active? What if you wanted it to only activate after something else had
been triggered? How would you activate it? Well: if you have another Trigger you want to activate this Trigger, you
simply need to drag this Trigger into the Trigger property box of the other Trigger that is to activate it. 





Layers:
--------

Most hazards will have their sprites rendered on a layer below the environment, wheras some environment pieces may be rendered ontop.
To select the order in which things are layered, go to the Sprite Renderer for the object and change the Sorting Layer




