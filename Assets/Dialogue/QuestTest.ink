INCLUDE GlobalScript.ink

{LetterToAMan == false : ->main | ->already_gave}


-> main
=== main ===
Hello there !! #speaker: Mr. White #portrait: Mr_White_Neutral #layout: left #audio: beep1
Can you help me with something? #audio: beep1 #save
+ [Yes]
    ~ playEmote("exclamation")
    -> questStart
+ [No]
    ~ playEmote("question")
    Okay then. #portrait: Mr_White_Sad
    -> END
    
=== questStart ===
Can you give this letter to that man over there? #speaker: Mr. White #portrait: Mr_White_Neutral #layout: left #audio: beep1 #save 
+ [Yes]
 ~LetterToAMan = true
    -> already_gave
   
+ [No]
    What?? 
    -> END   

=== already_gave ===
Thank you for delivering the letter! #speaker: Mr. White #portrait: Mr_White_Happy #audio: beep1 #gaveXp: 500 #questId: quest2
-> END

=== rewardCollected ===
Thanks! #speaker: Mr. White #portrait: Mr_White_Happy #audio: beep1
-> DONE
