INCLUDE GlobalScript.ink

?? #speaker: Mr. White #portrait:Mr_White_Neutral #layout:left #audio:beep1
-> main

===main===
what ?
+ [Give]
~ playEmote("exclamation")
    -> questStart
+ [Dont Give]
~ playEmote("question")
    ??#portrait:Mr_White_Sad
    -> END
    
===questStart===
    thank you?#speaker: Mr. White #portrait:Mr_White_Neutral #layout:left #audio:beep3 #gaveXp : 100 #questId: quest1
    
-> END 

