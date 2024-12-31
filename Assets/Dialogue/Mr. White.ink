INCLUDE GlobalScript.ink

Hello there !! #speaker: Mr. White #portrait:Mr_White_Neutral #layout:left #audio:beep1
-> main

=== main ===
How are you feeling today ?
+ [Happy]
~ playEmote("exclamation")
    That makes me <color=\#F8FF30>happy </color>too !!#portrait:Mr_White_Happy
+ [Sad]
~ playEmote("question")
    That makes me <color=\#5B81FF>sad </color>too..#portrait:Mr_White_Sad
    
- Dont trust him, he's <b><color=\#FF1E35>not  </color> </b> a real doctor ! #speaker: Mr. Black #portrait:Mr_Black_Neutral #layout:right #audio:beep3
~ playEmote("question")
Well, do you have any questions ?? #speaker: Mr. White #portrait:Mr_White_Neutral #layout:left #audio:beep1
+[Yes]
    -> main
+ [No]
    Goodbye then !!
-> END   